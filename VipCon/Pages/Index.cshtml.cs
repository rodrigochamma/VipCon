using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using VipCon.Helpers;

namespace VipCon.Pages
{
    public class IndexModel : PageModel
    {
        private readonly VipCon.Data.ApplicationDbContext _context;
        private readonly IOptions<ConfiguracoesSMTP> _configuracoesSMTP;

        public IndexModel(VipCon.Data.ApplicationDbContext context, IOptions<ConfiguracoesSMTP> configuracoesSMTP)
        {
            _context = context;
            _configuracoesSMTP = configuracoesSMTP;
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

            var email = "<h1>Uma nova simulação foi solicitada no site:</h1>" +
                "<br>" +
                "<br><b>Nome:</b> " + Prospect.Nome +
                "<br><b>Telefone:</b> " + Prospect.Telefone +
                "<br><b>Email:</b> " + Prospect.Email +
                "<br><b>Modalidade:</b> " + Prospect.ModalidadeConsorcio;

            //EnviarEmail(email);

            @TempData["MostrarModal"] = "mostrar";
            return RedirectToPage("./Index");
        }

        public void EnviarEmail(string email)
        {
            var client = new SmtpClient()
            {
                Host = _configuracoesSMTP.Value.Host,
                EnableSsl = true,
                Port = _configuracoesSMTP.Value.Porta,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_configuracoesSMTP.Value.Login, _configuracoesSMTP.Value.Senha)
            };

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuracoesSMTP.Value.Login);
            mailMessage.To.Add("rodrigochamma@gmail.com");
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = email;
            mailMessage.Subject = "Nova simulação de consórcio no site";
            client.Send(mailMessage);
        }
    }
}
