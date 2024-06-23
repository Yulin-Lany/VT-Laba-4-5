using Stseniayeva.Domain.Entities;
using Stseniayeva.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Stseniayeva.UI.Services
{
    public class MemoryCategoryService : ICategoryService
    {
        public Task<ResponseData<List<MotoGroup>>> GetCategoryListAsync()
        {

            var categories = new List<MotoGroup>
            {
            new MotoGroup {Id=1, GroupName="Klass moto Touring", NormalizedName="Touring"},
            new MotoGroup {Id=2, GroupName="Klass moto Cruiser", NormalizedName="Cruiser"},
            new MotoGroup {Id=3, GroupName="Klass moto Sport", NormalizedName="Sport"},
            new MotoGroup {Id=4, GroupName="Klass moto Klassic", NormalizedName="Klassic"},
            new MotoGroup {Id=5, GroupName="Klass moto Enduro", NormalizedName="Enduro"}
            };
            var result = new ResponseData<List<MotoGroup>>();
            result.Data = categories;

            return Task.FromResult(result);
        }

    }
}
