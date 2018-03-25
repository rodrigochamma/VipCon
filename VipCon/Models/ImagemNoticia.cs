using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VipCon.Models
{
    public class ImagemNoticia
    {
        public string imagemNome { get; set; }
        public IFormFile imagem { get; set; }
        public int idNoticia { get; set; }
        public int cropPointX { get; set; }
        public int cropPointY { get; set; }
        public int imageCropWidth { get; set; }
        public int imageCropHeight { get; set; }
        public string ImagemCaminho
        {
            get
            {
                return @"/uploads/noticias/" + idNoticia.ToString() + "/" + imagemNome;
            }
        }
        public string ImagemPasta
        {
            get
            {
                return @"/uploads/noticias/" + idNoticia.ToString() + "/";
            }
        }
        public string ThumbCaminho {
            get
            {
                return @"/uploads/noticias_thumbs/" + idNoticia.ToString() + "/" + imagemNome;
            }
        }
        public string ThumbPasta
        {
            get
            {
                return @"/uploads/noticias_thumbs/" + idNoticia.ToString() + "/";
            }
        }

    }
}
