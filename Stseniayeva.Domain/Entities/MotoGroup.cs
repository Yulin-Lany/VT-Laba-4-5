using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stseniayeva.Domain.Entities
{
    public class MotoGroup
    {
        [Key]
        public int Id { get; set; }
        public string GroupName { get; set; }



        public string NormalizedName { get; set; }

    }
}
