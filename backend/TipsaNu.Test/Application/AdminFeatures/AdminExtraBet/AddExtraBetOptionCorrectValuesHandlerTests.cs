using MediatR;
using Moq;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.AddExtraBetOptionCorrectValues;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Test.Application.AdminFeatures.AdminExtraBet
{
    public class AddExtraBetOptionCorrectValuesHandlerTests
    {
        private readonly Mock<IExtraBetRepository> _repoMock = new();
        private readonly Mock<IGenericRepository<ExtraBetOption>> _genericRepoMock = new();
        private readonly Mock<IMediator> _mediatorMock = new();

        [Fact]
        public async Task Handle_ShouldReturnFailure_IfOptionNotFound()
        {
            _genericRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync((ExtraBetOption?)null);

            var handler = new AddExtraBetOptionCorrectValuesCommandHandler(_repoMock.Object, _genericRepoMock.Object, _mediatorMock.Object);

            var result = await handler.Handle(new AddExtraBetOptionCorrectValuesCommand(1, new SetExtraBetOptionCorrectValuesDto { CorrectValues = new List<string> { "x" } }), CancellationToken.None);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ShouldAddValues_WhenSomeExist()
        {
            // Arrange
            _genericRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExtraBetOption());

            _repoMock.Setup(r => r.GetCorrectValuesByOptionIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ExtraBetOptionCorrectValue> 
                { 
                    new ExtraBetOptionCorrectValue { OptionId = 1, Value = "x" } 
                });

            var handler = new AddExtraBetOptionCorrectValuesCommandHandler(_repoMock.Object, _genericRepoMock.Object, _mediatorMock.Object);

            var command = new AddExtraBetOptionCorrectValuesCommand(1, new SetExtraBetOptionCorrectValuesDto { CorrectValues = new List<string> { "y", "x" } });

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);


            _repoMock.Verify(r => r.AddCorrectValuesRangeAsync(
                    It.Is<IEnumerable<ExtraBetOptionCorrectValue>>(list => 
                        ((List<ExtraBetOptionCorrectValue>)list).Count == 1 && 
                        ((List<ExtraBetOptionCorrectValue>)list)[0].Value == "y"), 
                    It.IsAny<CancellationToken>()), 
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldPublishEvent()
        {
            _genericRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new ExtraBetOption());

            _repoMock.Setup(r => r.GetCorrectValuesByOptionIdAsync(1, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new List<ExtraBetOptionCorrectValue>());

            var handler = new AddExtraBetOptionCorrectValuesCommandHandler(_repoMock.Object, _genericRepoMock.Object, _mediatorMock.Object);

            await handler.Handle(new AddExtraBetOptionCorrectValuesCommand(1, new SetExtraBetOptionCorrectValuesDto { CorrectValues = new List<string> { "y" } }), CancellationToken.None);

            _mediatorMock.Verify(m => m.Publish(It.IsAny<ExtraBetOptionCorrectValuesUpdatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
