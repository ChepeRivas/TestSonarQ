using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PerfectWorld.Data.Dtos;
using PerfectWorld.Data.Helpers;
using PerfectWorld.Data.Models;
using PerfectWorld.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PerfectWorld.Graphics.Controllers
{
    public class LogController : Controller
    {
        private readonly IMapper _mapper;
        private readonly LoginRepository _repo;
        public LogController(LoginRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Login()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }


        [HttpPost]
        [Route("api/GPW/Log/Login")]
        public async Task<IActionResult> Loged([FromBody] VMLogin collection)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ResultClass() { message = "The model does not match", state = false });
            }
            var login = await _repo.Login(collection);
            if (login.state)
            {

                var Usuario =_mapper.Map<VMUsers>((Users)login.data);
                var claims = new List<Claim>
                {
                   new Claim("FullName", Usuario.Usuario),
                   new Claim("Id", Usuario.Id.ToString()),
                   new Claim("Correo", Usuario.Correo),
                   new Claim("Login", Usuario.Usuario)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);


                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                   new ClaimsPrincipal(claimsIdentity),
                   authProperties);
            }
            return Ok(login);

        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Log");
        }


    }
}
