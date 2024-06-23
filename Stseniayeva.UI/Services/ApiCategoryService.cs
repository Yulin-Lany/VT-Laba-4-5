using Stseniayeva.Domain.Entities;
using Stseniayeva.Domain.Models;
using Stseniayeva.UI.Models;


namespace Stseniayeva.UI.Services
{
    public class ApiCategoryService(HttpClient httpClient) : ICategoryService
    {
        public async Task<ResponseData<List<MotoGroup>>> GetCategoryListAsync()
        {
            var result = await httpClient.GetAsync(httpClient.BaseAddress);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content
                .ReadFromJsonAsync<ResponseData<List<MotoGroup>>>();
            };
            var response = new ResponseData<List<MotoGroup>>
            { Success = false, ErrorMessage = "Ошибка чтения API" };
            return response;
        }
    }
}
