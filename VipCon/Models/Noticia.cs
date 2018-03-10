using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VipCon.Models
{
    public class Noticia
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Chamada { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHoraInclusao { get; set; }
        public string Imagem { set; get; }
    }
}
