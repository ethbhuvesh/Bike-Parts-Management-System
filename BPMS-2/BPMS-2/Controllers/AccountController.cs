using BPMS_2.Utils;
using BPMS_2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BPMS_2.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AccountController> _logger;
       

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _logger = logger;
           
        }

       

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Copy data from RegisterViewModel to IdentityUser
                var user = new IdentityUser(model.Username)
                {
                    UserName = model.Username,
                    Email = model.Email

                };

                // Store user data in AspNetUsers database table
                var result = await userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
                    EmailSender emailSender = new EmailSender();
                    bool emailResponse = emailSender.SendEmail(model.Email, confirmationLink);

                    if (emailResponse)
                    {
                        //_customerRepository.CreateCustomer(model.CompanyName, model.Username, model.Address, model.City, model.Region, model.PostalCode, model.Country);

                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("ConfirmRequired");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Email Failed");
                    }

                    bool emailStatus = await userManager.IsEmailConfirmedAsync(user);

                    if (emailStatus)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        return View("ConfirmRequired");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }



        public IActionResult ConfirmRequired()
        {
            return View();
        }


        public async Task<string> GetCurrentUserId()
        {
            IdentityUser usr = await GetCurrentUserAsync();
            return usr?.UserName;
        }
        private Task<IdentityUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);


        [HttpPost]
        public async Task<IActionResult> Logout()
        {

            await signInManager.SignOutAsync();
            string message = $"Sign out by user {await GetCurrentUserId()}";
            _logger.LogInformation(message);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string message = $"Sign in attempt by user {model.Username}";
                _logger.LogInformation(message);

                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, true);

                if (result.Succeeded)

                {
                     message = $"Sign in successful by user {model.Username}.";
                    _logger.LogInformation(message);
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsLockedOut)
                {
                    message = $"The user {model.Username} account is locked.";
                    _logger.LogWarning(message);
                    return View("Lockout");
                }
                else
                {
                    
                    message = $"Sign in unsuccessful by user {model.Username}.";
                    _logger.LogInformation(message);
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

                }
            

            }

            return View(model);
        }




        [HttpGet]
		[Authorize]
        public IActionResult ChangePassword() => View(new ChangePasswordViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userManager.ChangePasswordAsync(await userManager.GetUserAsync(User), model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    _logger.LogWarning("Password changed by user {user} on {date}", await userManager.GetUserAsync(User), DateTime.UtcNow);
                    return View("ChangePasswordSuccess");
                }

                foreach (var error in result.Errors)
                {
                    _logger.LogWarning("Password change failed by user {user} on {date}", await userManager.GetUserAsync(User), DateTime.UtcNow);
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }



    }
}
            


        /*
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email,
                    model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);


        }*/

