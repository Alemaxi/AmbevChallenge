using Ambev.DeveloperEvaluation.Domain.Extensions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Util
{
    public abstract class GenericHandler<T, V, U> : IRequestHandler<T, V> where U : AbstractValidator<T> where T : IRequest<V>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        protected GenericHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<V> Handle(T request, CancellationToken cancellationToken)
        {
            U a = typeof(U).CreateInstance<U>();

            await a.ValidateAndThrowAsync(request);

            return await ExecuteHandlerCode(request, cancellationToken);
        }

        public abstract Task<V> ExecuteHandlerCode(T request, CancellationToken cancelation);
    }
}
