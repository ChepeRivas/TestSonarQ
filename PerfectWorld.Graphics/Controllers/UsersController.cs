using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PerfectWorld.Data.Dtos;
using PerfectWorld.Data.Helpers;
using PerfectWorld.Data.Models;
using PerfectWorld.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectWorld.Graphics.Controllers
{
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserRepository _repo;

        public UsersController(IMapper mapper, UserRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("api/PW/Account/AddOrUpdarte")]
        public async Task<ResultClass> AddOrUpdate([FromBody] VMUsers user)
        {
            if (!ModelState.IsValid)
            {
                return new ResultClass() { state = false, data = null, message = "" };
            }
            if (user.Valid())
            {
                Users ViewRequest = _mapper.Map<Users>(user);
                return await _repo.Registro(ViewRequest);
            }
            else
            {
                return new ResultClass() { state = false, data = null, message = "" };
            }


        }
        public IActionResult PerfectWorld(string key = "")
        {
            if (key != "")
            {
                return View();

            }
            else
            {
                return RedirectToAction("Err403", "Error");
            }
        }

        public IActionResult Welcome()
        {
            return View();
        }


    }
}
