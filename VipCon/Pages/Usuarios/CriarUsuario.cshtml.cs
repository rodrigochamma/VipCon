using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using VipCon.Data;
using VipCon.Pages.Account;
using VipCon.Services;

namespace VipCon.Pages.Usuarios
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress(ErrorMessage = "O Email Digitado não é válido")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }           

            [DataType(DataType.Password)]
            [Display(Name = "Confirmação Senha")]
            [Compare("Password", ErrorMessage = "O valor digitado no campo Senha e no campo Confirmação Senha são diferentes.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "O campo Nome é obrigatório")]
            public string Nome { get; set; }

            public string SobreNome { get; set; }
                        
            public bool Admin { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, Nome = Input.Nome, Sobrenome = Input.SobreNome, Admin = Input.Admin};
                var result = await _userManager.CreateAsync(user, Input.Password);


                if (result.Succeeded)
                {
                    //Adiciona as Roles.
                    if (user.Admin)
                        _userManager.AddToRoleAsync(user, "Administrator").Wait();
                    else
                        _userManager.AddToRoleAsync(user, "Parceiro").Wait();

                    _logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(Input.Email, callbackUrl);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToPage("./ListaUsuario");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
