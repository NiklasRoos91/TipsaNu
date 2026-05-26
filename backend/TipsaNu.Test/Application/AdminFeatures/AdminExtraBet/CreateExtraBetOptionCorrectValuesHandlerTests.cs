using MediatR;
using Moq;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.CreateExtraBetOptionCorrectValues;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Test.Application.AdminFeatures.AdminExtraBet
{
    public class CreateExtraBetOptionCorrectValuesHandlerTests
    {
        private readonly Mock<IExtraBetRepository> _repoMock = new();
        private readonly Mock<IGenericRepository<ExtraBetOption>> _genericRepoMock = new();
        private readonly Mock<IMediator> _mediatorMock = new();

        [Fact]
        public async Task Handle_ShouldReturnFailure_IfOptionNotFound()
        {
            // Arrange
            _genericRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync((ExtraBetOption?)null);

            var handler = new CreateExtraBetOptionCorrectValuesCommandHandler(
                _repoMock.Object, _genericRepoMock.Object, _mediatorMock.Object);
            
            var command = new CreateExtraBetOptionCorrectValuesCommand(1, new SetExtraBetOptionCorrectValuesDto { CorrectValues = new List<string> { "x" } });

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("ExtraBetOption not found", result.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_IfCorrectValuesExist()
        {
            // Arrange
            _genericRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new ExtraBetOption { OptionId = 1 });

            _repoMock.Setup(r => r.GetCorrectValuesByOptionIdAsync(1, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new List<ExtraBetOptionCorrectValue> { new() });

            var handler = new CreateExtraBetOptionCorrectValuesCommandHandler(
                _repoMock.Object, _genericRepoMock.Object, _mediatorMock.Object);

            var command = new CreateExtraBetOptionCorrectValuesCommand(1, new SetExtraBetOptionCorrectValuesDto { CorrectValues = new List<string> { "x" } });
            
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Correct values already exist, use PATCH to update.", result.ErrorMessage);
        }


        [Fact]
        public async Task Handle_ShouldAddCorrectValuesAndPublishEvent()
        {
            // Arrange
            var option = new ExtraBetOption { OptionId = 1, Status = ExtraBetOptionStatus.Open };
            
            _genericRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(option);

            _repoMock.Setup(r => r.GetCorrectValuesByOptionIdAsync(1, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new List<ExtraBetOptionCorrectValue>());

            var executionOrder = new List<string>();
            _repoMock.Setup(r => r.AddCorrectValuesRangeAsync(It.IsAny<IEnumerable<ExtraBetOptionCorrectValue>>(), It.IsAny<CancellationToken>()))
                     .Returns(Task.CompletedTask)
                     .Callback(() => executionOrder.Add("DatabaseSaved"));

            _mediatorMock.Setup(m => m.Publish(It.IsAny<ExtraBetOptionCorrectValuesUpdatedEvent>(), It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask)
                         .Callback(() => executionOrder.Add("EventPublished"));

            var handler = new CreateExtraBetOptionCorrectValuesCommandHandler(
                _repoMock.Object, _genericRepoMock.Object, _mediatorMock.Object);

            var command = new CreateExtraBetOptionCorrectValuesCommand(1, new SetExtraBetOptionCorrectValuesDto 
            { 
                CorrectValues = new List<string> { " Kane  ", "Griezmann" } 
            });

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            
            Assert.Equal(ExtraBetOptionStatus.Closed, option.Status);

            _genericRepoMock.Verify(r => r.UpdateAsync(It.Is<ExtraBetOption>(o => o.Status == ExtraBetOptionStatus.Closed), It.IsAny<CancellationToken>()), Times.Once);

            _repoMock.Verify(r => r.AddCorrectValuesRangeAsync(
                It.Is<IEnumerable<ExtraBetOptionCorrectValue>>(list => 
                    list.Count() == 2 && 
                    list.First().Value == "Kane"),
                It.IsAny<CancellationToken>()), 
                Times.Once);

            Assert.Equal(2, executionOrder.Count);
            Assert.Equal("DatabaseSaved", executionOrder[0]);
            Assert.Equal("EventPublished", executionOrder[1]);
        }
    }
}