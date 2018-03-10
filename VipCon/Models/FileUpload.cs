using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace VipCon.Models
{
    public class FileUpload
    {
        [Required]
        [Display(Name = "Titulo")]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Imagem")]
        public IFormFile UploadImagem { get; set; }

        [Required]
        [Display(Name = "Thumb")]
        public IFormFile UploadThumb { get; set; }
    }
}
