using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VipCon.Pages
{
    public class IndexModel : PageModel
    {
        private readonly VipCon.Data.ApplicationDbContext _context;

        public IndexModel(VipCon.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public VipCon.Models.Prospect Prospect { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {

                return Page();                
            }

            Prospect.DataSimulacao = DateTime.Now;
            _context.Prospect.Add(Prospect);
            await _context.SaveChangesAsync();
            var client = new SmtpClient()
            {
                Host = "smtp.zoho.com",
                EnableSsl = true,
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("username@domain.com", "password")
            };
            return RedirectToPage("./Index");            
        }
    }
}
