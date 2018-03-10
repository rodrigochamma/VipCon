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
        public IFormFile Image { set; get; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (this.Image != null)
            {
                var fileName = GetUniqueName(this.Image.FileName);
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, fileName);
                this.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                this.Noticia.Imagem = fileName; // Set the file name
            }

            _context.Noticia.Add(Noticia);
            await _context.SaveChangesAsync();

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