using Microsoft.AspNetCore.Http;
using PerfectWorld.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWorld.Data.Helpers
{
    public class WebUserHelper : IWebUserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
    
        public WebUserHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserCorreo()
        {
            var user = _httpContextAccessor.HttpContext.User;

            return user.Claims.Where(x => x.Type == "Correo").FirstOrDefault().Value;
        }

        public int GetUserId()
        {
            var user = _httpContextAccessor.HttpContext.User;

            return int.Parse(user.Claims.Where(x => x.Type == "Id").FirstOrDefault().Value);
        }

        public string GetUserLogin()
        {
            var user = _httpContextAccessor.HttpContext.User;

            return user.Claims.Where(x => x.Type == "Login").FirstOrDefault().Value;
        }


    }
}
