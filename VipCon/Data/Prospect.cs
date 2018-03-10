using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VipCon.Data
{
    public class Prospect
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string ModalidadeConsorcio { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy HH:mm}")]
        public DateTime DataSimulacao { get; set; }
    }
}
