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
                    Image = uri + "AdventureTouring.jpg",
                    Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Очень удобный")) },

                new Moto { MotoName = "Luxury Touring",
                    Description = "Комфортный",
                    SpeedMax = 230,
                    Image = uri + "LuxTouring.jpg",
                    Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Комфортный")) },


                new Moto {MotoName = "Classic Cruiser",
                    Description = "Стильный",
                    SpeedMax = 235,
                    Image = uri + "ClassicCruiser.jpg",
                    Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Стильный")) },


                new Moto {MotoName = "Power Cruiser",
                    Description = "Мощный",
                    SpeedMax = 250,
                    Image = uri + "Cruiser.jpg",
                    Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Мощный")) },


                new Moto {MotoName = "Supermoto",
                    Description = "Дорогой",
                    SpeedMax = 110,
                    Image = uri + "Enduro.jpg",
                    Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Дорогой")) },

                new Moto {MotoName = "Dual Purpose", 
                    Description = "Двойного назначения", 
                    SpeedMax = 90, 
                    Image = uri +  "Kuznechik.jpg",
                    Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Двойного назначения"))},

                new Moto {MotoName = "Super Sports", 
                    Description = "Самый быстрый", 
                    SpeedMax = 300, 
                    Image = uri + "SuperSport.jpg",
                    Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Самый быстрый"))},
             
                new Moto {MotoName = "Sports Street Naked", 
                    Description = "Идеальный", 
                    SpeedMax = 280, 
                    Image = uri + "SportStrit.jpg",
                    Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Идеальный"))},

             new Moto {Id=9, MotoName = "Sports Touring", 
                 Description = "Практичный", 
                 SpeedMax = 180, 
                 Image = uri + "Sport-Touring.jpg",
                 Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Практичный"))},

             new Moto {Id=10, MotoName = "Retro", 
                 Description = "Брутальный", 
                 SpeedMax = 120, 
                 Image = uri + "Retro.jpg",
                 Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Брутальный"))},

             new Moto {Id=11, MotoName = "Standart Street Naked", 
                 Description = "Фееричный", 
                 SpeedMax = 170, 
                 Image = uri + "Naced.jpg",
                 Group = _categories.FirstOrDefault(c => c.NormalizedName.Equals("Фееричный"))}
            };

                await context.Motos.AddRangeAsync(_tovar);
                await context.SaveChangesAsync();

            }
        }
    }
}
