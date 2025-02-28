using Ambev.DeveloperEvaluation.Domain.Extensions;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Util
{
    public abstract class GeneralHandler<T, V, U> : IRequestHandler<T, V> where U : AbstractValidator<T> where T : IRequest<V>
    {
        public async Task<V> Handle(T request, CancellationToken cancellationToken)
        {
            U a = typeof(U).CreateInstance<U>();

            await a.ValidateAndThrowAsync(request);

            return await ExecuteHandlerCode(request, cancellationToken);
        }

        public abstract Task<V> ExecuteHandlerCode(T Request, CancellationToken);
    }
}
