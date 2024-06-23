using Stseniayeva.Domain.Entities;
using Stseniayeva.Domain.Models;
using System.Text.RegularExpressions;

namespace Stseniayeva.UI.Services
{
    public interface ICategoryService
    {
        public Task<ResponseData<List<MotoGroup>>> GetCategoryListAsync();
    }
}
