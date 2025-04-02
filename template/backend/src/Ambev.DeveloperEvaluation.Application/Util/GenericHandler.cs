using Ambev.DeveloperEvaluation.Domain.Extensions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Util
{
    public abstract class GenericHandler<TCommand, TValidator, TInstantiate> : IRequestHandler<TCommand, TValidator> where TInstantiate : AbstractValidator<TCommand> where TCommand : IRequest<TValidator>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        protected GenericHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TValidator> Handle(TCommand request, CancellationToken cancellationToken)
        {
            TInstantiate a = typeof(TInstantiate).CreateInstance<TInstantiate>();

            await a.ValidateAndThrowAsync(request);

            return await ExecuteHandlerCode(request, cancellationToken);
        }

        public abstract Task<TValidator> ExecuteHandlerCode(TCommand request, CancellationToken cancelation);
    }
}
