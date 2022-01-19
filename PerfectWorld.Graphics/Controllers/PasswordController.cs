using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PerfectWorld.Data.Dtos;
using PerfectWorld.Data.Helpers;
using PerfectWorld.Data.Helpers.Atributtes;
using PerfectWorld.Data.Interfaces;
using PerfectWorld.Data.Models;
using PerfectWorld.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PerfectWorld.Graphics.Controllers
{
    public class PasswordController : Controller
    {
        private readonly IMapper _mapper;
        public readonly IWebUserHelper _userHelper;
        private readonly PasswordRepository _repo;
        public PasswordController(IMapper mapper, PasswordRepository repo, IWebUserHelper userHelper)
        {
            _mapper = mapper;
            _repo = repo;
            _userHelper = userHelper;
        }

        [Route("Users/Validar/{id}")]
        public async Task<IActionResult> Validar(string id)
        {
            if (id != "")
            {
                Regex val = new Regex(@"^[a-zA-Z0-9]*$");
                if (!val.IsMatch(id))
                {
                    return RedirectToAction("Err403", "Error");
                }
                int request = await _repo.ValidarPersonaje(id);
                switch (request)
                {
                    case 403:
                        return RedirectToAction("Err403", "Error");
                    case 404:
                        return RedirectToAction("Err404", "Error");
                    case 500:
                        return RedirectToAction("Err500", "Error");
                    default:
                        return RedirectToAction("Welcome", "Users");
                }
            }
            else
            {
                return RedirectToAction("Err403", "Error");

            }

        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(VMUsers mUsers)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Err404", "Error");
            }
            int result = await _repo.ForgotPasswd(mUsers.Correo, mUsers.Usuario);

            switch (result)
            {
                case 404:
                    return RedirectToAction("Err404", "Error");
                case 500:
                    return RedirectToAction("Err500", "Error");
                default:
                    return RedirectToAction("PerfectWorld", "Password", mUsers);
            }
        }
        [HttpGet]
        [LoginAttribute()]
        public async Task<IActionResult> ModifyPassword()
        {
            var keyLogueada = await _repo.Find(_userHelper.GetUserCorreo().ToString(),_userHelper.GetUserLogin().ToString());
            //return await ChangePassword(keyLogueada.answer);
            return RedirectToAction("ChangePassword","Password", new { id = keyLogueada.answer } );
        }
        [Route("Password/ChangePassword/{id}")]
        public async Task<IActionResult> ChangePassword(string id = "")
        {
            if (id != "")
            {
                Regex val = new Regex(@"^[a-zA-Z0-9]*$");
                if (!val.IsMatch(id))
                {
                    return RedirectToAction("Err404", "Error");
                }
                Users UserOlvido = await _repo.UserByAnswer(id);
                if (UserOlvido != null)
                {
                    VMUsers UserAplicate = _mapper.Map<VMUsers>(UserOlvido);
                    return View(UserAplicate);
                }
                return RedirectToAction("Err404", "Error");
            }
            else
            {
                return RedirectToAction("Err403", "Error");
            }
        }
        public IActionResult PerfectWorld(VMUsers pUser)
        {
            if (pUser.Secret != "")
            {
                return View();

            }
            else
            {
                return RedirectToAction("Err403", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetMyPj(VMUsers pass)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            int request = await _repo.ChangePassword(new VMPassword() { key = pass.Secret, NewPassword = pass.Contrasena });
            switch (request)
            {
                case 403:
                    return RedirectToAction("Err403", "Error");
                case 404:
                    return RedirectToAction("Err404", "Error");
                case 500:
                    return RedirectToAction("Err500", "Error");
                default:
                    return RedirectToAction("PerfectWorld", "Users", new { key = pass.Secret } );
            }
        }


        [HttpGet]
        [Route("api/GM/Create")]
        public async Task<IActionResult> CrearContrasena(string name, string pass)
        {

            return Ok(MD5EncryptClass.GetEncrypt_64(name, pass));

        } 
        [HttpGet]
        [Route("api/GM/Answers")]
        public async Task<IActionResult> CrearAnswers(string name)
        {
            if (name== "trB$G58cO6bdpkoo")
            {

            return Ok(await _repo.asnwer());
            }
            else
            {
                return Ok(name);
            }

        }
        [HttpGet]
        [Route("api/GM/Modify")]
        public async Task<IActionResult> ModificarContrasena(long id = 0)
        {
            if (id != 0)
            {
                return Ok(await _repo.GMValidarPJ(id));
            }
            else
            {
                return Ok("id must not be empty or 0");

            }

        }
    }
}
