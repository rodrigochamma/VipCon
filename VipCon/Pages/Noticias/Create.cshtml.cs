using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VipCon.Data;
using VipCon.Models;
using SixLabors.ImageSharp;
using SixLabors.Primitives;

namespace VipCon.Pages.Noticias
{
    public class CreateModel : PageModel
    {
        private readonly VipCon.Data.ApplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        public CreateModel(VipCon.Data.ApplicationDbContext context, IHostingEnvironment environment)
        {
            this.hostingEnvironment = environment;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Noticia Noticia { get; set; }

        [BindProperty]
        public IFormFile Imagem { set; get; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Noticia.Add(Noticia);
            await _context.SaveChangesAsync();
            
            if (this.Imagem != null)
            {
                ImagemNoticia i = new ImagemNoticia();
                i.imagemNome = GetUniqueName(this.Imagem.FileName);
                i.idNoticia = this.Noticia.ID;

                if (!Directory.Exists(hostingEnvironment.WebRootPath + i.ImagemPasta))
                {
                    Directory.CreateDirectory(hostingEnvironment.WebRootPath + i.ImagemPasta);
                }

                if (!Directory.Exists(hostingEnvironment.WebRootPath + i.ThumbPasta))
                {
                    Directory.CreateDirectory(hostingEnvironment.WebRootPath + i.ThumbPasta);
                }

                using (var stream = new FileStream(hostingEnvironment.WebRootPath + i.ImagemCaminho, FileMode.Create))
                {
                    this.Imagem.CopyTo(stream);
                    stream.Dispose();
                }
                  
                using (Image<Rgba32> img = Image.Load(hostingEnvironment.WebRootPath + i.ImagemCaminho))
                {
                    if (img.Width > 1200)
                    {
                        img.Mutate(ctx => ctx.Resize(1200, 0));
                    }
                    img.Save(hostingEnvironment.WebRootPath + i.ImagemCaminho);

                    img.Mutate(ctx => ctx.Resize(150, 0));
                    img.Save(hostingEnvironment.WebRootPath + i.ThumbCaminho);
                }

                this.Noticia.Imagem = i.imagemNome;
                await _context.SaveChangesAsync();
            }
                      

            return RedirectToPage("./Index");
        }

        private string GetUniqueName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_" + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }
    }
}