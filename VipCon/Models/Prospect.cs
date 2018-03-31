using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VipCon.Models
{
    public class Prospect
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigatório informar um telefone")]                
        [RegularExpression(@"^[0-9]{10,11}$", ErrorMessage = "O telefone deve conter no mínimo 10 e no máximo 11 números. ex: 27988887777")]        
        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "Digite um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a modalidade do consórcio")]
        [Display(Name = "Modalidade")]
        public string ModalidadeConsorcio { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy HH:mm}")]
        public DateTime DataSimulacao { get; set; }
    }
}
