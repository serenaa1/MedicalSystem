using MedicalSystem.Interfaces.Abstract;
using MedicalSystem.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserAuthenticationService _service;
        public LoginController(IUserAuthenticationService service)
        {
            this._service = service;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (model.Name != null && model.Email != null && model.Username != null && model.Password != null && model.ConfirmPassword != null)
            {
                model.Role = "user";
                var result = await _service.RegistrationAsync(model);
                TempData["msg"] = result.Message;
            }
            if (!ModelState.IsValid) { return View(model); }
            return RedirectToAction(nameof(Login));

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _service.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        [Authorize]
        public async Task Logout()
        {
            await _service.LogoutAsync();
        }

        //public async Task<IActionResult> Reg()
        //{
        //    var model = new RegistrationModel
        //    {
        //        Username = "admin",
        //        Name = "Max Wells",
        //        Email = "max@gmail.com",
        //        Password = "Admin@123345"
        //    };
        //    model.Role = "user";
        //    var result = await _service.RegistrationAsync(model);
        //    return Ok(result);

        //}
    }
}
