using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VipCon.Models
{
    public class Parceiro
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public string Link { get; set; }
    }
}
