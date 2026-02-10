using Moq;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.ReplaceExtraBetOptionCorrectValues;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;
using MediatR;

namespace TipsaNu.Test.Features.AdminExtraBet
{
    public class ReplaceExtraBetOptionCorrectValuesHandlerTests
    {
        private readonly Mock<IExtraBetRepository> _repoMock = new();
        private readonly Mock<IMediator> _mediatorMock = new();
        private readonly Mock<IGenericRepository<ExtraBetOption>> _genericRepoMock = new();

        [Fact]
        public async Task Handle_ShouldReplaceAllValues()
        {
            // Arrange
            _genericRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new ExtraBetOption());

            _repoMock.Setup(r => r.GetCorrectValuesByOptionIdAsync(1, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new List<ExtraBetOptionCorrectValue> { new(), new() });

            var handler = new ReplaceExtraBetOptionCorrectValuesCommandHandler(
                _repoMock.Object,
                _genericRepoMock.Object,
                _mediatorMock.Object
            );

            var command = new ReplaceExtraBetOptionCorrectValuesCommand(
                1,
                new SetExtraBetOptionCorrectValuesDto { CorrectValues = new List<string> { "z" } }
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _repoMock.Verify(r => r.RemoveCorrectValuesAsync(1, It.IsAny<CancellationToken>()), Times.Once);
            _repoMock.Verify(r => r.AddCorrectValueAsync(1, "z", It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(m => m.Publish(It.IsAny<ExtraBetOptionCorrectValuesUpdatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldAddNewValues_WhenNoneExist()
        {
            // Arrange
            _genericRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new ExtraBetOption());

            _repoMock.Setup(r => r.GetCorrectValuesByOptionIdAsync(1, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new List<ExtraBetOptionCorrectValue>());

            var handler = new ReplaceExtraBetOptionCorrectValuesCommandHandler(
                _repoMock.Object,
                _genericRepoMock.Object,
                _mediatorMock.Object
            );

            var command = new ReplaceExtraBetOptionCorrectValuesCommand(
                1,
                new SetExtraBetOptionCorrectValuesDto { CorrectValues = new List<string> { "x", "y" } }
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _repoMock.Verify(r => r.AddCorrectValueAsync(1, "x", It.IsAny<CancellationToken>()), Times.Once);
            _repoMock.Verify(r => r.AddCorrectValueAsync(1, "y", It.IsAny<CancellationToken>()), Times.Once);
        }

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
                new SetExtraBetOptionCorrectValuesDto { CorrectValues = new List<string> { "x" } }
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("ExtraBetOption not found", result.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ShouldPublishEvent_AfterReplacement()
        {
            // Arrange
            _genericRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new ExtraBetOption());

            _repoMock.Setup(r => r.GetCorrectValuesByOptionIdAsync(1, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new List<ExtraBetOptionCorrectValue> { new() });

            var handler = new ReplaceExtraBetOptionCorrectValuesCommandHandler(
                _repoMock.Object,
                _genericRepoMock.Object,
                _mediatorMock.Object
            );

            var command = new ReplaceExtraBetOptionCorrectValuesCommand(
                1,
                new SetExtraBetOptionCorrectValuesDto { CorrectValues = new List<string> { "z" } }
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _mediatorMock.Verify(m => m.Publish(It.Is<ExtraBetOptionCorrectValuesUpdatedEvent>(e => e.OptionId == 1), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
