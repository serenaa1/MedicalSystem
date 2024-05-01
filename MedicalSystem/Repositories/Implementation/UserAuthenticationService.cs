using MedicalSystem.Interfaces.Abstract;
using MedicalSystem.Models.Domain;
using MedicalSystem.Models.DTO;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MedicalSystem.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserAuthenticationService(RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            this._userManager = userManager;
        }
        public async Task<Status> LoginAsync(LoginModel model)
        {
            var status = new Status();
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return status;
            }
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid password";
                return status;
            }
            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                foreach(var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logged in successfully";
                return status;
            }
            else if(signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User locked out";
                return status;
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on loggin in";
                return status;
            }
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Status> RegistrationAsync(RegistrationModel model)
        {
            var status = new Status();
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "User already exists";
                return status;
            }
            ApplicationUser user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = model.Name,
                Email = model.Email,
                UserName = model.Username,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }
            //role management
            if (!await _roleManager.RoleExistsAsync(model.Role))

                await _roleManager.CreateAsync(new IdentityRole(model.Role));
            if (await _roleManager.RoleExistsAsync(model.Role))
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }
            status.StatusCode = 1;
            status.Message = "User has rgistered successfully";
            return status;
        }
    }
}
