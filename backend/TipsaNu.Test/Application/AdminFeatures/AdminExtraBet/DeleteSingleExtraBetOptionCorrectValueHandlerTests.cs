using MediatR;
using Moq;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.DeleteSingleExtraBetOptionCorrectValue;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Test.Application.AdminFeatures.AdminExtraBet
{
    public class DeleteSingleExtraBetOptionCorrectValueHandlerTests
    {

        private readonly Mock<IGenericRepository<ExtraBetOptionCorrectValue>> _repoMock = new();
        private readonly Mock<IGenericRepository<ExtraBetOption>> _extraBetOptionGenericRepositoryMock = new();
        private readonly Mock<IExtraBetRepository> _extraBetRepositoryMock = new();
        private readonly Mock<IMediator> _mediatorMock = new();

        [Fact]
        public async Task Handle_ShouldReturnFailure_IfValueNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync((ExtraBetOptionCorrectValue)null!);
            
            var handler = new DeleteSingleExtraBetOptionCorrectValueCommandHandler(_repoMock.Object,
                _extraBetOptionGenericRepositoryMock.Object, _extraBetRepositoryMock.Object, _mediatorMock.Object);

            // Act
            var result = await handler.Handle(new DeleteSingleExtraBetOptionCorrectValueCommand(1),
                CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("CorrectValue not found.", result.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ShouldDeleteCorrectValue()
        {
            var value = new ExtraBetOptionCorrectValue { CorrectValueId = 1, OptionId = 2 };
            _repoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(value);

            _extraBetRepositoryMock
                .Setup(r => r.GetCorrectValuesByOptionIdAsync(value.OptionId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ExtraBetOptionCorrectValue>());
            
            var handler = new DeleteSingleExtraBetOptionCorrectValueCommandHandler(_repoMock.Object,
                _extraBetOptionGenericRepositoryMock.Object, _extraBetRepositoryMock.Object, _mediatorMock.Object);

            var result = await handler.Handle(new DeleteSingleExtraBetOptionCorrectValueCommand(1),
                CancellationToken.None);

            Assert.True(result.IsSuccess);
            _repoMock.Verify(r => r.DeleteAsync(value.CorrectValueId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldPublishEvent()
        {
            // Arrange
            var value = new ExtraBetOptionCorrectValue { CorrectValueId = 1, OptionId = 2 };
            _repoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(value);

            _extraBetRepositoryMock
                .Setup(r => r.GetCorrectValuesByOptionIdAsync(value.OptionId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ExtraBetOptionCorrectValue>());
            
            var handler = new DeleteSingleExtraBetOptionCorrectValueCommandHandler(_repoMock.Object,
                _extraBetOptionGenericRepositoryMock.Object, _extraBetRepositoryMock.Object, _mediatorMock.Object);

            // Act
            await handler.Handle(new DeleteSingleExtraBetOptionCorrectValueCommand(1), CancellationToken.None);

            // Assert
            _mediatorMock.Verify(m => m.Publish(
                It.Is<ExtraBetOptionCorrectValuesUpdatedEvent>(e => e.OptionId == value.OptionId),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}