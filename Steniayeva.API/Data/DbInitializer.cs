using Microsoft.EntityFrameworkCore;
using Stseniayeva.Domain.Entities;

namespace Steniayeva.API.Data
{
    public class DbInitializer
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
                var _motoGroups = new MotoGroup[]
            {
            new MotoGroup {GroupName="Klass moto Touring", NormalizedName="Touring"},
            new MotoGroup {GroupName="Klass moto Cruiser", NormalizedName="Cruiser"},
            new MotoGroup {GroupName="Klass moto Sport", NormalizedName="Sport"},
            new MotoGroup {GroupName="Klass moto Klassic", NormalizedName="Klassic"},
            new MotoGroup {GroupName="Klass moto Enduro", NormalizedName="Enduro"}
            };

                await context.Categories.AddRangeAsync(_motoGroups);
                await context.SaveChangesAsync();


                var _motos = new List<Moto>
        {
             new Moto {MotoName = "Adventure Touring",
                 Description = "Стильный",
                 SpeedMax = 200,
                 Images = uri +"Images/AdventureTouring.jpeg",
             Group = _motoGroups.FirstOrDefault(c => c.NormalizedName.Equals("Touring"))},

             new Moto {MotoName = "Luxury Touring",
                 Description = "Комфортный",
                 SpeedMax = 230,
                 Images = uri +"Images/LuxTouring.jpeg",
             Group = _motoGroups.FirstOrDefault(c => c.NormalizedName.Equals("Touring"))},

             new Moto {MotoName = "Classic Cruiser",
                 Description = "Быстрый",
                 SpeedMax = 235,
                 Images = uri +"Images/ClassicCruiser.jpeg",
             Group = _motoGroups.FirstOrDefault(c => c.NormalizedName.Equals("Cruiser"))},

             new Moto {MotoName = "Power Cruiser",
                 Description = "Мощный",
                 SpeedMax = 250,
                 Images = uri +"Images/Cruiser.jpeg",
             Group = _motoGroups.FirstOrDefault(c => c.NormalizedName.Equals("Cruiser"))},

             new Moto {MotoName = "Supermoto",
                 Description = "Дорогой",
                 SpeedMax = 110,
                 Images = uri +"Images/Enduro.jpeg",
            Group = _motoGroups.FirstOrDefault(c => c.NormalizedName.Equals("Enduro"))},

             new Moto {MotoName = "Dual Purpose",
                 Description = "Двойного назначения",
                 SpeedMax = 90,
                 Images = uri +"Images/Kuznechik.jpeg",
             Group = _motoGroups.FirstOrDefault(c => c.NormalizedName.Equals("Enduro"))},

             new Moto {MotoName = "Super Sports",
                 Description = "Самый быстрый",
                 SpeedMax = 300,
                 Images = uri +"Images/SuperSport.jpeg",
             Group = _motoGroups.FirstOrDefault(c => c.NormalizedName.Equals("Sport"))},

             new Moto { MotoName = "Sports Street Naked",
                 Description = "Идеальный",
                 SpeedMax = 280,
                 Images = uri +"Images/SportStrit.jpeg",
             Group = _motoGroups.FirstOrDefault(c => c.NormalizedName.Equals("Sport"))},

                new Moto {MotoName = "Sports Touring",
                 Description = "Практичный",
                 SpeedMax = 180,
                 Images =  uri +"Images/Sport-Touring.jpeg",
             Group = _motoGroups.FirstOrDefault(c => c.NormalizedName.Equals("Touring"))},

                new Moto {MotoName = "Retro",
                 Description = "Брутальный",
                 SpeedMax = 120,
                 Images = uri +"Images/Retro.jpeg",
             Group = _motoGroups.FirstOrDefault(c => c.NormalizedName.Equals("Klassic"))},

                new Moto { MotoName = "Standart Street Naked",
                 Description = "Фееричный",
                 SpeedMax = 170,
                    Images =uri + "Images/Naced.jpeg",
             Group = _motoGroups.FirstOrDefault(c => c.NormalizedName.Equals("Klassic"))}

            };

                await context.Motos.AddRangeAsync(_motos);
                await context.SaveChangesAsync();

            }
        }
    }
}
