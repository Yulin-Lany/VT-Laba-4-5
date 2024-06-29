using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Stseniayeva.Domain.Entities;
using Stseniayeva.Domain.Models;
using Stseniayeva.UI.Controllers;
using Stseniayeva.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stseniayeva.TESTS.Test
{
    public class ProductControllerTests

    {
        IProductService _productService;
        ICategoryService _categoryService;
        public ProductControllerTests()
        {
            SetupData();
        }
        // Список категорий сохраняется во ViewData
        [Fact]
        public async void IndexPutsCategoriesToViewData()
        {
            //arrange
            var controller = new ProductController(_categoryService, _productService);
            //act
            var response = await controller.Index(null);
            //assert
            var view = Assert.IsType<ViewResult>(response);
            var categories = Assert.IsType<List<Moto>>(view.ViewData["categories"]);
            Assert.Equal(6, categories.Count);
            Assert.Equal("Все", view.ViewData["currentCategory"]);
        }
        // Имя текущей категории сохраняется во ViewData
        [Fact]
        public async void IndexSetsCorrectCurrentCategory()
        {
            //arrange
            var categories = await _categoryService.GetCategoryListAsync();
            var currentCategory = categories.Data[0];
            var controller = new ProductController(_categoryService, _productService);
            //act
            var response = await controller.Index(currentCategory.NormalizedName);
            //assert
            var view = Assert.IsType<ViewResult>(response);
            Assert.Equal(currentCategory.GroupName, view.ViewData["currentCategory"]);
        }
        // В случае ошибки возвращается NotFoundObjectResult
        [Fact]
        public async void IndexReturnsNotFound()
        {
            //arrange
            string errorMessage = "Test error";
            var categoriesResponse = new ResponseData<List<MotoGroup>>();
            categoriesResponse.Success = false;
            categoriesResponse.ErrorMessage = errorMessage;
            _categoryService.GetCategoryListAsync().Returns(Task.FromResult(categoriesResponse))
            ;
            var controller = new ProductController(_categoryService, _productService);
            //act
            var response = await controller.Index(null);
            //assert
            var result = Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(errorMessage, result.Value.ToString());
        }
    }
    // Настройка имитации ICategoryService и IProductService
    void SetupData()
    {
        _categoryService = Substitute.For<ICategoryService>();
        var categoriesResponse = new ResponseData<List<MotoGroup>>();
        categoriesResponse.Data = new List<MotoGroup>
{
new MotoGroup {Id=1, GroupName="Klass moto Touring", NormalizedName="Touring"},
new MotoGroup {Id=2, GroupName="Klass moto Cruiser", NormalizedName="Cruiser"},
new MotoGroup {Id=3, GroupName="Klass moto Sport", NormalizedName="Sport"},
new MotoGroup {Id=4, GroupName="Klass moto Klassic", NormalizedName="Klassic"},
new MotoGroup {Id=5, GroupName="Klass moto Enduro", NormalizedName="Enduro"}
};
        _categoryService.GetCategoryListAsync().Returns(Task.FromResult(categoriesResponse))
    _productService = Substitute.For<IProductService>();
        var motos = new List<Moto>
{
new Moto {Id = 1 },
new Moto { Id = 2 },
new Moto { Id = 3 },
new Moto { Id = 4 },
new Moto { Id = 5 }
};
        var productResponse = new ResponseData<ListModel<Moto>>();
        productResponse.Data = new ListModel<Moto> { Items = motos };
        _productService.GetProductListAsync(Arg.Any<string?>(), Arg.Any<int>())
        .Returns(productResponse);
    }
}
