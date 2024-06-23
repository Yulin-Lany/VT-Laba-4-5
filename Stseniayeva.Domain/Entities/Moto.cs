using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stseniayeva.Domain.Entities
{
    public class Moto
    {
        [Key]
        public int Id { get; set; } // id 
        public string MotoName { get; set; } // название 
        public string Description { get; set; } // описание 
        public int SpeedMax { get; set; } // MAX скорость

        public string? Image { get; set; } // имя файла изображения 

        // Навигационные свойства
        public int MotoGroupId { get; set; }
        public MotoGroup? Group { get; set; }
    }
}
