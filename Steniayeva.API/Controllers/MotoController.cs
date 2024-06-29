using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Steniayeva.API.Data;
using Stseniayeva.Domain.Entities;
using Stseniayeva.Domain.Models;

namespace Steniayeva.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MotoController(AppDbContext context, IWebHostEnvironment _environment)
        {
            _context = context;

        }


        // GET: api/Tovar
        [HttpGet]
        public async Task<ActionResult<ResponseData<ListModel<Moto>>>> GetProductListAsync(
              string? category,
              int pageNo = 1,
              int pageSize = 3)
        {
            // Создать объект результата
            var result = new ResponseData<ListModel<Moto>>();

            // Фильтрация по категории загрузка данных категории
            var data = _context.Motos
            .Include(d => d.Group)
            .Where(d => String.IsNullOrEmpty(category)
            || d.Group.NormalizedName.Equals(category));

            // Подсчет общего количества страниц
            int totalPages = (int)Math.Ceiling(data.Count() / (double)pageSize);
            if (pageNo > totalPages)
                pageNo = totalPages;

            // Создание объекта ProductListModel с нужной страницей данных
            var listData = new ListModel<Moto>()
            {
                Items = await data
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };
            // поместить данные в объект результата
            result.Data = listData;
            // Если список пустой
            if (data.Count() == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбранной категории";
            }
            return result;
        }
        // GET: api/Tovary/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Moto>> GetTovar(int id)
        {
            var tovar = await _context.Motos.FindAsync(id);

            if (tovar == null)
            {
                return NotFound();
            }

            return tovar;
        }

        // PUT: api/Tovar/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTovar(int id, Moto tovar)
        {
            if (id != tovar.Id)
            {
                return BadRequest();
            }

            _context.Entry(tovar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TovarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tovar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Moto>> PostTovar(Moto tovar)
        {
            _context.Motos.Add(tovar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTovar", new { id = tovar.Id }, tovar);
        }

        // DELETE: api/Tovar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTovar(int id)
        {
            var tovar = await _context.Motos.FindAsync(id);
            if (tovar == null)
            {
                return NotFound();
            }

            _context.Motos.Remove(tovar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TovarExists(int id)
        {
            return _context.Motos.Any(e => e.Id == id);
        }

        [HttpPost("{id}")]

        public async Task<IActionResult> SaveImage(int id, IFormFile image, [FromServices] IWebHostEnvironment env)
        {
            // Найти объект по Id
            var tovar = await _context.Motos.FindAsync(id);
            if (tovar == null)
            {
                return NotFound();
            }

            // Путь к папке wwwroot/Images
            var imagesPath = Path.Combine(env.WebRootPath, "Images");

            // получить случайное имя файла
            var randomName = Path.GetRandomFileName();

            // получить расширение в исходном файле
            var extension = Path.GetExtension(image.FileName);

            // задать в новом имени расширение как в исходном файле
            var fileName = Path.ChangeExtension(randomName, extension);

            // полный путь к файлу
            var filePath = Path.Combine(imagesPath, fileName);

            // создать файл и открыть поток для записи
            using var stream = System.IO.File.OpenWrite(filePath);

            // скопировать файл в поток
            await image.CopyToAsync(stream);

            // получить Url хоста
            var host = "https://" + Request.Host;

            // Url файла изображения
            var url = $"{host}/Images/{fileName}";

            // Сохранить url файла в объекте
            tovar.Images = url;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
