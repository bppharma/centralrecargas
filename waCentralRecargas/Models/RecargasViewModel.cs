using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace waCentralRecargas.Models
{
    public class RecargasViewModel
    {
        public class Recargas
        {
            public string Monto { get; set; }
            [Required]
            [StringLength(10, ErrorMessage = "El número debe ser de 10 dígitos", MinimumLength = 10)]
            public string Numero { get; set; }
            [Required]
            [StringLength(10, ErrorMessage = "El número debe ser de 10 dígitos", MinimumLength = 10)]
            [Compare("Numero", ErrorMessage = "El número no coincide con la confirmación.")]
            public string Confirmacion { get; set; }
            public string Carrier { get; set; }
            public string ErrorMess { get; set; }
        }
        public class Montos
        {
            public string idservdet { get; set; }
            public string monto { get; set; }
            public string bondades { get; set; }
        }
    }
}
