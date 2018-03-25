using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VipCon.Data;
using VipCon.Models;
using SixLabors.ImageSharp;
using SixLabors.Primitives;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace VipCon.Pages.Noticias
{
    public class EditModel : PageModel
    {
        private readonly VipCon.Data.ApplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public EditModel(VipCon.Data.ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            this.hostingEnvironment = environment;
        }

        [BindProperty]
        public Noticia Noticia { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Noticia = await _context.Noticia.SingleOrDefaultAsync(m => m.ID == id);

            if (Noticia == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Noticia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticiaExists(Noticia.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        public ActionResult OnPostCriarMiniatura()
        {
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;

            //Exemplo de retorno
            List<string> lstString = new List<string>();

            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<ImagemNoticia>(requestBody);
                    if (obj != null)
                    {
                        ImagemNoticia i = obj;
                        i.idNoticia = this.Noticia.ID;

                        using (Image<Rgba32> thumb = Image.Load(hostingEnvironment.WebRootPath + i.ImagemCaminho))
                        {
                            Rectangle cropRectangle = new Rectangle(i.cropPointX, i.cropPointY, i.imageCropWidth, i.imageCropHeight);
                            thumb.Mutate(ctx => ctx.Crop(cropRectangle));
                            thumb.Mutate(ctx => ctx.Resize(150, 0));
                            thumb.Save(hostingEnvironment.WebRootPath + i.ThumbCaminho);
                            lstString.Add("~" + i.ThumbCaminho);
                        }

                    }
                }
            }
            return new JsonResult(lstString);
        }

        public ActionResult OnPostEnviarFoto()
        {
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;

            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var obj = JsonConvert.DeserializeObject<ImagemNoticia>(requestBody);
                    if (obj != null)
                    {
                        ImagemNoticia i = new ImagemNoticia();
                        i.imagem = obj.imagem;
                        i.idNoticia = this.Noticia.ID;
                        i.imagemNome = obj.imagem.FileName;                        

                        if (!Directory.Exists(hostingEnvironment.WebRootPath + i.ImagemPasta))
                        {
                            Directory.CreateDirectory(hostingEnvironment.WebRootPath + i.ImagemPasta);
                        }

                        using (var stream2 = new FileStream(hostingEnvironment.WebRootPath + i.ImagemCaminho, FileMode.Create))
                        {
                            i.imagem.CopyTo(stream2);
                            stream2.Dispose();
                        }

                        using (Image<Rgba32> img = Image.Load(hostingEnvironment.WebRootPath + i.ImagemCaminho))
                        {
                            if (img.Width > 1200)
                            {
                                img.Mutate(ctx => ctx.Resize(1200, 0));
                            }
                            img.Save(hostingEnvironment.WebRootPath + i.ImagemCaminho);
                            
                        }
                        return new JsonResult("~" + i.ImagemCaminho);
                    }
                }
            }

            return new JsonResult("");

        }

        public ActionResult OnPostUpload(List<IFormFile> files)
        {
            if (files != null && files.Count > 0)
            {
                foreach (IFormFile item in files)
                {
                    if (item.Length > 0)
                    {
                        ImagemNoticia i = new ImagemNoticia();
                        i.imagem = item;
                        i.idNoticia = this.Noticia.ID;
                        i.imagemNome = item.FileName;

                        if (!Directory.Exists(hostingEnvironment.WebRootPath + i.ImagemPasta))
                        {
                            Directory.CreateDirectory(hostingEnvironment.WebRootPath + i.ImagemPasta);
                        }

                        using (var stream2 = new FileStream(hostingEnvironment.WebRootPath + i.ImagemCaminho, FileMode.Create))
                        {
                            i.imagem.CopyTo(stream2);
                            stream2.Dispose();
                        }

                        using (Image<Rgba32> img = Image.Load(hostingEnvironment.WebRootPath + i.ImagemCaminho))
                        {
                            if (img.Width > 1200)
                            {
                                img.Mutate(ctx => ctx.Resize(1200, 0));
                            }
                            img.Save(hostingEnvironment.WebRootPath + i.ImagemCaminho);

                        }
                        return new JsonResult("~" + i.ImagemCaminho);
                    }
                }
            }
            return this.Content("Fail");
        }

        private bool NoticiaExists(int id)
        {
            return _context.Noticia.Any(e => e.ID == id);
        }
    }
}
