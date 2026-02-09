using MediatR;
using Moq;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.DeleteExtraBetOptionCorrectValues;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Test.Features.AdminExtraBet
{
    public class DeleteExtraBetOptionCorrectValuesHandlerTests
    {
        private readonly Mock<IExtraBetRepository> _repoMock = new();
        private readonly Mock<IMediator> _mediatorMock = new();

        [Fact]
        public async Task Handle_ShouldReturnFailure_IfNoValuesExist()
        {
            _repoMock.Setup(r => r.GetCorrectValuesByOptionIdAsync(1, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new List<ExtraBetOptionCorrectValue>());

            var handler = new DeleteExtraBetOptionCorrectValuesCommandHandler(
                _repoMock.Object,
                _mediatorMock.Object
            );

            var result = await handler.Handle(
                new DeleteExtraBetOptionCorrectValuesCommand(1),
                CancellationToken.None
            );

            Assert.False(result.IsSuccess);
            Assert.Equal("No correct values found to delete.", result.ErrorMessage);

            _repoMock.Verify(r => r.RemoveCorrectValuesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            _mediatorMock.Verify(m => m.Publish(It.IsAny<ExtraBetOptionCorrectValuesUpdatedEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldDeleteExistingValues()
        {
            _repoMock.Setup(r => r.GetCorrectValuesByOptionIdAsync(1, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new List<ExtraBetOptionCorrectValue> { new() });

            var handler = new DeleteExtraBetOptionCorrectValuesCommandHandler(
                _repoMock.Object,
                _mediatorMock.Object
            );

            var result = await handler.Handle(
                new DeleteExtraBetOptionCorrectValuesCommand(1),
                CancellationToken.None
            );

            Assert.True(result.IsSuccess);

            _repoMock.Verify(r => r.RemoveCorrectValuesAsync(1, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldPublishEvent_AfterDelete()
        {
            _repoMock.Setup(r => r.GetCorrectValuesByOptionIdAsync(1, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new List<ExtraBetOptionCorrectValue> { new() });

            var handler = new DeleteExtraBetOptionCorrectValuesCommandHandler(
                _repoMock.Object,
                _mediatorMock.Object
            );

            await handler.Handle(
                new DeleteExtraBetOptionCorrectValuesCommand(1),
                CancellationToken.None
            );

            _mediatorMock.Verify(m =>
                m.Publish(
                    It.Is<ExtraBetOptionCorrectValuesUpdatedEvent>(e => e.OptionId == 1),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
        }
    }
}
