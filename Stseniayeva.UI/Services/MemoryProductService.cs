using Stseniayeva.Domain.Entities;
using Stseniayeva.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;
using Humanizer;
using Stseniayeva.UI.Models;

namespace Stseniayeva.UI.Services
{
    public class MemoryProductService : IProductService
    {

         List<Moto> _motos;
         List<MotoGroup> _motoGroups;
        IConfiguration _config;

        public MemoryProductService(ICategoryService categoryService, [FromServices] IConfiguration config)
        {
            _config = config;
            _motoGroups = categoryService.GetCategoryListAsync()
                .Result
                .Data;

            SetupData();
        }

        
        private void SetupData()
         {
           
             _motos = new List<Moto>()

             {
             new Moto {Id=1, MotoName = "Adventure Touring", Description = "Очень удобный", SpeedMax = 200, Image = "AdventureTouring.jpg"},
             new Moto {Id=2, MotoName = "Luxury Touring", Description = "Комфортный", SpeedMax = 230, Image = "LuxTouring.jpg" },
             new Moto {Id=3, MotoName = "Classic Cruiser", Description = "Стильный", SpeedMax = 235, Image = "ClassicCruiser.jpg" },
             new Moto {Id=4, MotoName = "Power Cruiser", Description = "Мощный", SpeedMax = 250, Image = "Cruiser.jpg"},
             new Moto {Id=5, MotoName = "Supermoto", Description = "Дорогой", SpeedMax = 110, Image = "Enduro.jpg" },
             new Moto {Id=6, MotoName = "Dual Purpose", Description = "Двойного назначения", SpeedMax = 90, Image = "Kuznechik.jpg" },
             new Moto {Id=7, MotoName = "Super Sports", Description = "Самый быстрый", SpeedMax = 300, Image = "SuperSport.jpg" },
             new Moto {Id=8, MotoName = "Sports Street Naked", Description = "Идеальный", SpeedMax = 280, Image = "SportStrit.jpg"},
             new Moto {Id=9, MotoName = "Sports Touring", Description = "Практичный", SpeedMax = 180, Image = "Sport-Touring.jpg" },
             new Moto {Id=10, MotoName = "Retro", Description = "Брутальный", SpeedMax = 120, Image = "Retro.jpg"},
             new Moto {Id=11, MotoName = "Standart Street Naked", Description = "Фееричный", SpeedMax = 170, Image = "Naced.jpg"}
             };
         }

         Task<ResponseData<ListModel<Moto>>> IProductService.GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)

        {
            // Создать объект результата
            var result = new ResponseData<ListModel<Moto>>();

			// Id категории для фильрации
			int? categoryId = null;

			// если требуется фильтрация, то найти Id категории
			// с заданным categoryNormalizedName
			if (categoryNormalizedName != null)
				categoryId = _motoGroups
                .Find(c =>
				c.NormalizedName.Equals(categoryNormalizedName))
				?.Id;

			// Выбрать объекты, отфильтрованные по Id категории,
			// если этот Id имеется
			var data = _motos
            .Where(d => categoryId == null || d.Id.Equals(categoryId))?
            .ToList();

            // получить размер страницы из конфигурации
            int pageSize = _config.GetSection("ItemsPerPage").Get<int>();


            // получить общее количество страниц
            int totalPages = (int)Math.Ceiling(data.Count / (double)pageSize);

            // получить данные страницы
            var listData = new ListModel<Moto>()
            {
                Items = data.Skip((pageNo - 1) *
                pageSize).Take(pageSize).ToList(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };

            // поместить ранные в объект результата
            result.Data = listData;



			// Если список пустой
			if (data.Count == 0)
			{
				result.Success = false;
				result.ErrorMessage = "Нет объектов в выбраннной категории";
			}
			// Вернуть результат
			return Task.FromResult(result);

		}

        public Task<ResponseData<Moto>> CreateProductAsync(Moto product, IFormFile? formFile)
            {
                throw new NotImplementedException();
            }

            public Task DeleteProductAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ResponseData<Moto>> GetProductByIdAsync(int id)
            {
                     throw new NotImplementedException();
            }



            public Task UpdateProductAsync(int id, Moto product, IFormFile? formFile)
            {
                 throw new NotImplementedException();
            }
    }
}


