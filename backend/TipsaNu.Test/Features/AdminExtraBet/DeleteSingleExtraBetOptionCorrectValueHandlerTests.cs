using MediatR;
using Moq;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.DeleteSingleExtraBetOptionCorrectValue;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Test.Features.AdminExtraBet
{
    public class DeleteSingleExtraBetOptionCorrectValueHandlerTests
    {
        private readonly Mock<IGenericRepository<ExtraBetOptionCorrectValue>> _repoMock = new();
        private readonly Mock<IMediator> _mediatorMock = new();

        [Fact]
        public async Task Handle_ShouldReturnFailure_IfValueNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                     .ReturnsAsync((ExtraBetOptionCorrectValue?)null);

            var handler = new DeleteSingleExtraBetOptionCorrectValueCommandHandler(_repoMock.Object, _mediatorMock.Object);

            var result = await handler.Handle(new DeleteSingleExtraBetOptionCorrectValueCommand(1), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal("CorrectValue not found.", result.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ShouldDeleteCorrectValue()
        {
            var value = new ExtraBetOptionCorrectValue { CorrectValueId = 1, OptionId = 2 };
            _repoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(value);

            var handler = new DeleteSingleExtraBetOptionCorrectValueCommandHandler(_repoMock.Object, _mediatorMock.Object);

            var result = await handler.Handle(new DeleteSingleExtraBetOptionCorrectValueCommand(1), CancellationToken.None);

            Assert.True(result.IsSuccess);
            _repoMock.Verify(r => r.DeleteAsync(value.CorrectValueId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldPublishEvent()
        {
            var value = new ExtraBetOptionCorrectValue { CorrectValueId = 1, OptionId = 2 };
            _repoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(value);

            var handler = new DeleteSingleExtraBetOptionCorrectValueCommandHandler(_repoMock.Object, _mediatorMock.Object);

            await handler.Handle(new DeleteSingleExtraBetOptionCorrectValueCommand(1), CancellationToken.None);

            _mediatorMock.Verify(m => m.Publish(
                It.Is<ExtraBetOptionCorrectValuesUpdatedEvent>(e => e.OptionId == value.OptionId),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}