using MediatR;
using Moq;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.ReplaceExtraBetOptionCorrectValues;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Test.Application.AdminFeatures.AdminExtraBet
{
    public class ReplaceExtraBetOptionCorrectValuesHandlerTests
    {
        private readonly Mock<IExtraBetRepository> _repoMock = new();
        private readonly Mock<IMediator> _mediatorMock = new();
        private readonly Mock<IGenericRepository<ExtraBetOption>> _genericRepoMock = new();

        [Fact]
        public async Task Handle_ShouldReturnFailure_IfOptionNotFound()
        {
            // Arrange
            _genericRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                            .ReturnsAsync((ExtraBetOption?)null);

            var handler = new ReplaceExtraBetOptionCorrectValuesCommandHandler(
                _repoMock.Object,
                _genericRepoMock.Object,
                _mediatorMock.Object
            );

            var command = new ReplaceExtraBetOptionCorrectValuesCommand(
                1,
                new SetExtraBetOptionCorrectValuesDto { CorrectValues = ["x"] }
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("ExtraBetOption not found", result.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ShouldRemoveOldValues_SaveNewValuesInBatch_AndThenPublishEvent()
        {
            // Arrange
            _genericRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new ExtraBetOption { OptionId = 1 });

            var executionOrder = new List<string>();

            _repoMock.Setup(r => r.RemoveCorrectValuesAsync(1, It.IsAny<CancellationToken>()))
                     .Returns(Task.CompletedTask)
                     .Callback(() => executionOrder.Add("OldValuesRemoved"));

            _repoMock.Setup(r => r.AddCorrectValuesRangeAsync(It.IsAny<IEnumerable<ExtraBetOptionCorrectValue>>(), It.IsAny<CancellationToken>()))
                     .Returns(Task.CompletedTask)
                     .Callback(() => executionOrder.Add("NewValuesBatchSaved"));

            _mediatorMock.Setup(m => m.Publish(It.IsAny<ExtraBetOptionCorrectValuesUpdatedEvent>(), It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask)
                         .Callback(() => executionOrder.Add("EventPublished"));

            var handler = new ReplaceExtraBetOptionCorrectValuesCommandHandler(
                _repoMock.Object,
                _genericRepoMock.Object,
                _mediatorMock.Object
            );

            var command = new ReplaceExtraBetOptionCorrectValuesCommand(
                1,
                new SetExtraBetOptionCorrectValuesDto { CorrectValues = ["  Mbappé ", "Kane"] }
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);

            _repoMock.Verify(r => r.RemoveCorrectValuesAsync(1, It.IsAny<CancellationToken>()), Times.Once);

            _repoMock.Verify(r => r.AddCorrectValuesRangeAsync(
                    It.Is<IEnumerable<ExtraBetOptionCorrectValue>>(list => 
                        ((List<ExtraBetOptionCorrectValue>)list).Count == 2 && 
                        ((List<ExtraBetOptionCorrectValue>)list)[0].Value == "Mbappé"), 
                    It.IsAny<CancellationToken>()), 
                Times.Once);

            _mediatorMock.Verify(m => m.Publish(
                It.Is<ExtraBetOptionCorrectValuesUpdatedEvent>(e => e.OptionId == 1), 
                It.IsAny<CancellationToken>()), 
                Times.Once);

            Assert.Equal(3, executionOrder.Count);
            Assert.Equal("OldValuesRemoved", executionOrder[0]);
            Assert.Equal("NewValuesBatchSaved", executionOrder[1]);
            Assert.Equal("EventPublished", executionOrder[2]);
        }
    }
}