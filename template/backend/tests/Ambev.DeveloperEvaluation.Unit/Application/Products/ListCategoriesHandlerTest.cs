using Ambev.DeveloperEvaluation.Application.Products.ListCategories;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class ListCategoriesHandlerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ListCategoriesHandler _listCategoriesHandler;

        public ListCategoriesHandlerTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();
            _listCategoriesHandler = new ListCategoriesHandler(_unitOfWork,_mapper);
        }

        [Fact(DisplayName = "Tests list categories handler")]
        public async Task TestsListCategoriesHandler()
        {
            var response = new List<string> { "cat 1", "cat 2" };

            _unitOfWork.Products.ListCategories().Returns(response);

            var result = await _listCategoriesHandler.Handle(new ListCategoriesCommand(), CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(response);
            _unitOfWork.Received(2).Products.ListCategories();
        }
    }
}
