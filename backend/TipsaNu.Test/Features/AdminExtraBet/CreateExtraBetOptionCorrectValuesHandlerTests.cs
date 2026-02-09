using MediatR;
using Moq;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.CreateExtraBetOptionCorrectValues;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Test.Features.AdminExtraBet
{
    public class CreateExtraBetOptionCorrectValuesHandlerTests
    {
        private readonly Mock<IExtraBetRepository> _repoMock = new();
        private readonly Mock<IGenericRepository<ExtraBetOption>> _genericRepoMock = new();
        private readonly Mock<IMediator> _mediatorMock = new();

        [Fact]
        public async Task Handle_ShouldReturnFailure_IfOptionNotFound()
        {
            _genericRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync((ExtraBetOption?)null);

            var handler = new CreateExtraBetOptionCorrectValuesCommandHandler(
                _repoMock.Object, _genericRepoMock.Object, _mediatorMock.Object);

            var result = await handler.Handle(new CreateExtraBetOptionCorrectValuesCommand(1, new SetExtraBetOptionCorrectValuesDto { CorrectValues = new List<string> { "x" } }), CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal("ExtraBetOption not found", result.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_IfCorrectValuesExist()
        {
            _genericRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new ExtraBetOption { OptionId = 1 });

            _repoMock.Setup(r => r.GetCorrectValuesByOptionIdAsync(1, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new List<ExtraBetOptionCorrectValue> { new() });

            var handler = new CreateExtraBetOptionCorrectValuesCommandHandler(
                _repoMock.Object, _genericRepoMock.Object, _mediatorMock.Object);

            var result = await handler.Handle(
                new CreateExtraBetOptionCorrectValuesCommand(
                    1,
                    new SetExtraBetOptionCorrectValuesDto { CorrectValues = new List<string> { "x" } }
                ),
                CancellationToken.None
            );

            Assert.False(result.IsSuccess);
            Assert.Equal("Correct values already exist, use PATCH to update.", result.ErrorMessage);
        }


        [Fact]
        public async Task Handle_ShouldAddCorrectValuesAndPublishEvent()
        {
            _genericRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(new ExtraBetOption());

            _repoMock.Setup(r => r.GetCorrectValuesByOptionIdAsync(1, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new List<ExtraBetOptionCorrectValue>());

            _repoMock.Setup(r => r.AddCorrectValueAsync(1, "x", It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new ExtraBetOptionCorrectValue());

            var handler = new CreateExtraBetOptionCorrectValuesCommandHandler(
                _repoMock.Object, _genericRepoMock.Object, _mediatorMock.Object);

            var result = await handler.Handle(new CreateExtraBetOptionCorrectValuesCommand(1, new SetExtraBetOptionCorrectValuesDto { CorrectValues = new List<string> { "x" } }), CancellationToken.None);

            Assert.True(result.IsSuccess);
            _mediatorMock.Verify(m => m.Publish(It.IsAny<ExtraBetOptionCorrectValuesUpdatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}