    using FluentValidation;
    using global::TipsaNu.Application.Commons.Results;
    using MediatR;

namespace TipsaNu.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TData>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, OperationResult<TData>>
        where TRequest : IRequest<OperationResult<TData>>
    {
        public async Task<OperationResult<TData>> Handle(
            TRequest request,
            RequestHandlerDelegate<OperationResult<TData>> next,
            CancellationToken cancellationToken)
        {
            if (!validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                validators.Select(v => v.ValidateAsync(context, cancellationToken))
            );

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .Select(f => f.ErrorMessage!)
                .ToList();

            if (failures.Any())
            {
                return OperationResult<TData>.Failure(failures);
            }

            return await next();
        }
    }
}