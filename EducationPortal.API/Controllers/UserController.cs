﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EducationPortal.API.Models;
using EducationPortal.API.DTO;

namespace EducationPortal.API.Controllers
{
    [Route("api/User/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        ResultDTO result = new ResultDTO();
        public UserController(UserManager<AppUser> userManager, IMapper mapper, RoleManager<AppRole> roleManager, IConfiguration configuration, SignInManager<AppUser> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public List<UserDTO> List()
        {
            var users = _userManager.Users.ToList();
            var userDtos = _mapper.Map<List<UserDTO>>(users);
            return userDtos;
        }
        [HttpGet("{id}")]
        public UserDTO GetById(string id)
        {
            var user = _userManager.Users.Where(s => s.Id.ToString() == id).SingleOrDefault();
            var userDto = _mapper.Map<UserDTO>(user);
            return userDto;
        }

        [HttpPost]
        public async Task<ResultDTO> Add(RegisterDTO dto)
        {
            var identityResult = await _userManager.CreateAsync(new() { UserName = dto.UserName, Email = dto.Email, FullName = dto.FullName, PhoneNumber = dto.PhoneNumber, PicUrl = "profil.jpg" }, dto.Password);

            if (!identityResult.Succeeded)
            {
                result.Status = false;
                foreach (var item in identityResult.Errors)
                {
                    result.Message += "<p>" + item.Description + "<p>";
                }

                return result;
            }
            var user = await _userManager.FindByNameAsync(dto.UserName);
            var roleExist = await _roleManager.RoleExistsAsync("Ogretmen");
            if (!roleExist)
            {
                var role = new AppRole { Name = "Ogretmen" };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, "Ogretmen");
            result.Status = true;
            result.Message = "Üye Eklendi";
            return result;
        }

        [HttpPut]
        public async Task<ResultDTO> Update(RegisterDTO dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                result.Status = false;
                result.Message = "Kullanıcı Blunamadı!";
            }

            user.PhoneNumber = dto.PhoneNumber;
            user.FullName = dto.FullName;
            user.Email = dto.Email;

            await _userManager.UpdateAsync(user);
            result.Status = true;
            result.Message = "Kullanıcı Güncellendi";

            return result;
        }

        [HttpPost]
        public async Task<ResultDTO> SignIn(LoginDTO dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user is null)
            {
                result.Status = false;
                result.Message = "Üye Bulunamadı!";
                return result;
            }
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!isPasswordCorrect)
            {
                result.Status = false;
                result.Message = "Kullanıcı Adı veya Parola Geçersiz!";
                return result;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("JWTID", Guid.NewGuid().ToString()),
                new Claim("userPicUrl", user.PicUrl),

            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GenerateJWT(authClaims);

            result.Status = true;
            result.Message = token;
            return result;

        }
        private string GenerateJWT(List<Claim> claims)
        {

            var accessTokenExpiration = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["AccessTokenExpiration"]));


            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var tokenObject = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: accessTokenExpiration,
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }

        [HttpPost]
        public async Task<ResultDTO> Upload(UploadDTO dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
            {
                result.Status = false;
                result.Message = "Kayıt Bulunmadı!";
                return result;
            }

            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "/wwwroot/Profile");
            string userPic = user.PicUrl;

            if (userPic != "profil.jpg")
            {

                var userPicUrl = Path.Combine(path, userPic);

                if (System.IO.File.Exists(userPicUrl))
                {
                    System.IO.File.Delete(userPicUrl);
                }
            }
            string data = dto.PicData;
            string base64 = data.Substring(data.IndexOf(',') + 1);
            base64 = base64.Trim('\0');
            byte[] imageBytes = Convert.FromBase64String(base64);
            string filePath = Guid.NewGuid().ToString() + dto.PicExt;


            var picPath = Path.Combine(path, filePath);

            System.IO.File.WriteAllBytes(picPath, imageBytes);

            user.PicUrl = filePath;
            await _userManager.UpdateAsync(user);

            result.Status = true;
            result.Message = "Profil Fotoğrafı Güncellendi";

            return result;
        }
    }
}
