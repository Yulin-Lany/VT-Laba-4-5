using Microsoft.EntityFrameworkCore;
using Stseniayeva.Domain.Entities;

namespace Stseniayeva.API.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {

            // Uri проекта
            var uri = "https://localhost:7002/";
            // Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            //Выполнение миграций
            await context.Database.MigrateAsync();

            if (!context.Categories.Any() && !context.Motos.Any())
            {
                var _categories = new MotoGroup[]
            {
            new MotoGroup { GroupName="Klass moto Touring", NormalizedName="Touring"},
            new MotoGroup { GroupName="Klass moto Cruiser", NormalizedName="Cruiser"},
            new MotoGroup { GroupName="Klass moto Sport", NormalizedName="Sport"},
            new MotoGroup { GroupName="Klass moto Klassic", NormalizedName="Klassic"},
            new MotoGroup { GroupName="Klass moto Enduro", NormalizedName="Enduro"}
            };

                await context.Categories.AddRangeAsync(_categories);
                await context.SaveChangesAsync();


                var _tovar = new List<Moto>
        {
            new Moto {MotoName = "Adventure Touring",
                    Description = "Очень удобный",
                    SpeedMax = 200,
                    Images = uri + "AdventureTouring.jpg",
                    Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Touring"))},

                new Moto { MotoName = "Luxury Touring",
                    Description = "Комфортный",
                    SpeedMax = 230,
                    Images = uri + "LuxTouring.jpg",
                    Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Touring")) },


                new Moto {MotoName = "Classic Cruiser",
                    Description = "Стильный",
                    SpeedMax = 235,
                    Images = uri + "ClassicCruiser.jpg",
                    Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Cruiser")) },


                new Moto {MotoName = "Power Cruiser",
                    Description = "Мощный",
                    SpeedMax = 250,
                    Images = uri + "Cruiser.jpg",
                    Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Cruiser")) },


                new Moto {MotoName = "Supermoto",
                    Description = "Дорогой",
                    SpeedMax = 110,
                    Images = uri + "Enduro.jpg",
                    Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Enduro")) },

                new Moto {MotoName = "Dual Purpose", 
                    Description = "Двойного назначения", 
                    SpeedMax = 90, 
                    Images = uri +  "Kuznechik.jpg",
                    Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Enduro"))},

                new Moto {MotoName = "Super Sports", 
                    Description = "Самый быстрый", 
                    SpeedMax = 300, 
                    Images = uri + "SuperSport.jpg",
                    Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Sport"))},
             
                new Moto {MotoName = "Sports Street Naked", 
                    Description = "Идеальный", 
                    SpeedMax = 280, 
                    Images = uri + "SportStrit.jpg",
                    Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Sport"))},

             new Moto {Id=9, MotoName = "Sports Touring", 
                 Description = "Практичный", 
                 SpeedMax = 180, 
                 Images = uri + "Sport-Touring.jpg",
                 Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Touring"))},

             new Moto {Id=10, MotoName = "Retro", 
                 Description = "Брутальный", 
                 SpeedMax = 120, 
                 Images = uri + "Retro.jpg",
                 Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Klassic"))},

             new Moto {Id=11, MotoName = "Standart Street Naked", 
                 Description = "Фееричный", 
                 SpeedMax = 170, 
                 Images = uri + "Naced.jpg",
                 Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Klassic"))}
            };

                await context.Motos.AddRangeAsync(_tovar);
                await context.SaveChangesAsync();

            }
        }
    }
}
