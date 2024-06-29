using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Steniayeva.API.Controllers;
using Steniayeva.API.Data;
using Stseniayeva.Domain.Entities;
using Stseniayeva.Domain.Models;
using Stseniayeva.UI.Controllers;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Stseniayeva.TESTS.Test
{
    public class MotoApiCOntrollerTests : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<AppDbContext> _contextOptions;
        private readonly IWebHostEnvironment _environment;
        public MotoApiCOntrollerTests()
        {
            _environment = Substitute.For<IWebHostEnvironment>();
            // Create and open a connection. This creates the SQLite in-memory database,
            //which will persist until the connection is closed
            // at the end of the test (see Dispose below).
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // These options will be used by the context instances in this test suite,
            //including the connection opened above.
            _contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

            // Create the schema and seed some data
            using var context = new AppDbContext(_contextOptions);
            context.Database.EnsureCreated();
            var categories = new MotoGroup[]
            {
                new MotoGroup {GroupName="Klass moto Touring", NormalizedName="Touring"},
                new MotoGroup {GroupName="Klass moto Cruiser", NormalizedName="Cruiser"},
                new MotoGroup {GroupName="Klass moto Sport", NormalizedName="Sport"}
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();
            var dishes = new List<Moto>
            {
                new Moto {MotoName ="Adventure Touring", Description="Стильный", SpeedMax=200,
                Group=categories.FirstOrDefault(c=>c.NormalizedName.Equals("Touring"))},
                new Moto {MotoName = "Touring", Description="Комфортный", SpeedMax=230,
                Group=categories.FirstOrDefault(c=>c.NormalizedName.Equals("Touring"))},
                new Moto {MotoName = "Classic Cruiser", Description="Быстрый", SpeedMax=235,
               Group=categories.FirstOrDefault(c=>c.NormalizedName.Equals("Cruiser"))},
                new Moto {MotoName = "Power Cruiser", Description="Мощный", SpeedMax=250,
                Group=categories.FirstOrDefault(c=>c.NormalizedName.Equals("Cruiser"))},
                new Moto {MotoName = "Super Sports", Description="Самый быстрый", SpeedMax=300,
                Group=categories.FirstOrDefault(c=>c.NormalizedName.Equals("Sports"))}
            };
            context.AddRange(dishes);
            context.SaveChanges();
        }
        public void Dispose() => _connection?.Dispose();
        AppDbContext CreateContext() => new AppDbContext(_contextOptions);
        // Проверка фильтра по категории
        [Fact]
        public async void ControllerFiltersCategory()
        {
            // arrange
            using var context = CreateContext();
            var category = context.Categories.First();
            var controller = new MotoController(context, _environment);
            // act
            var response = await controller.GetProductListAsync(category.NormalizedName);
            ResponseData<ListModel<Moto>> responseData = response.Value;
            var dishesList = responseData.Data.Items; // полученный список объектов
                                                      //assert
            Assert.True(dishesList.All(d => d.Id == category.Id));
        }
        // Проверка подсчета количества страниц
        // Первый параметр - размер страницы
        // Второй параметр - ожидаемое количество страниц (при условии, что всего объектов 5)
        [Theory]
        [InlineData(2, 3)]
        [InlineData(3, 2)]
        public async void ControllerReturnsCorrectPagesCount(int size, int qty)
        {
            using var context = CreateContext();
            var controller = new MotoController(context, _environment);
            // act
            var response = await controller.GetProductListAsync(null, 1, size);
            ResponseData<ListModel<Moto>> responseData = response.Value;
            var totalPages = responseData.Data.TotalPages; // полученное количество страниц
                                                           //assert
            Assert.Equal(qty, totalPages); // количество страниц совпадает
        }

        [Fact]
        public async void ControllerReturnsCorrectPage()
        {
            using var context = CreateContext();
            var controller = new MotoController(context, _environment);
            // При размере страницы 3 и общем количестве объектов 5
            // на 2-й странице должно быть 2 объекта
            int itemsInPage = 2;
            // Первый объект на второй странице
            Moto firstItem = context.Motos.ToArray()[3];
            // act
            // Получить данные 2-й страницы
            var response = await controller.GetProductListAsync(null, 2);
            ResponseData<ListModel<Moto>> responseData = response.Value;
            var dishesList = responseData.Data.Items; // полученный список объектов
            var currentPage = responseData.Data.CurrentPage; // полученный номер текущей страницы
                                                             //assert
            Assert.Equal(2, currentPage);// номер страницы совпадает
            Assert.Equal(2, dishesList.Count); // количество объектов на странице равно 2
            Assert.Equal(firstItem.Id, dishesList[0].Id); // 1-й объект в списке правильный
        }
    }
}