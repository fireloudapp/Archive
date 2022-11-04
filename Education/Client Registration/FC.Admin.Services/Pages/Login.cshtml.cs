using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml;
using FC.Admin.Services.Entities;
using FC.Admin.Services.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace FC.Admin.Services.Pages;

public class LoginModel : PageModel
    {
        private readonly AppSettings _appSettings;

        public LoginModel(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            user.PasswordHash = string.Empty; //Precaution measure to remove password.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("info", JsonConvert.SerializeObject(user, (Formatting)Formatting.Indented)),
                }),
                Expires = DateTime.UtcNow.AddDays(30),// generate token that is valid for 7 days
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return tokenHandler.WriteToken(token);
        }

        public void OnGet()
        {

        }
        [BindProperty]
        public LoginView Login { get; set; }
        public IActionResult OnPost(LoginView login)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (login.Email == "sr.ganeshram@gmail.com" && login.Password == "WEBAdmin@1984")
            {
                string jwtToken = GenerateJwtToken(new User()
                {
                    Email = login.Email,
                    FirstName = "Ganesh Ram",
                    Role = "FC Owner",
                    ClientDBName = "FC-Common"//This is my static common database name.
                });
                HttpContext.Session.SetString("UserToken", jwtToken);
                ViewData["welcome"] = $"Welcome {login.Email}";
                HttpContext.Session.SetString("UserName", "Ganesh Ram");
                HttpContext.Session.SetString("LoginUser_Session", login.Email);
                CreateSecureCookie(jwtToken);
                return Redirect("/Organization/QuickAdd");
            }
            //TODO: Later we can extend this.

            return BadRequest();
        }

        private void CreateSecureCookie(string userToken)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(180),
                Secure = true,
                HttpOnly = false, //so our cient js file can access the cookie.

            };
            Response.Cookies.Append("UserToken", userToken, cookieOptions);
        }

        public class LoginView
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public bool IsRemind { get; set; }
        }

        public class UserCredential
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Token { get; set; }
            public string Message { get; set; }
            public string Role { get; set; }
            public int StaffId
            {
                get;
                set;
            }

        }

    }