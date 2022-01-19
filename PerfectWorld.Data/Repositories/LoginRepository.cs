using PerfectWorld.Data.Dtos;
using PerfectWorld.Data.Helpers;
using PerfectWorld.Data.Interfaces;
using PerfectWorld.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWorld.Data.Repositories
{
    public class LoginRepository : PwContext
    {
        public async Task<ResultClass> Login(VMLogin user)
        {

            try
            {
                user.Contrasenia = MD5EncryptClass.GetEncrypt_64(user.Usuario, user.Contrasenia);
                var log = await FilterData<Users>($"SELECT * FROM users WHERE passwd = '{user.Contrasenia}' && name = '{user.Usuario}';");
                if (log != null)
                {
                    if (log.answer == "0")
                    {
                        string key = MD5EncryptClass.RandomKey(log.name);
                        int result = await PutData($"UPDATE users SET answer = '{key}' WHERE ID={log.ID} ; ");

                    }
                }
                else
                {

                    return new ResultClass() { data = log, state = log == null ? false : true, message = "The user or password is not correct" };
                }

                return new ResultClass() { data = log, state = log == null ? false : true, message = "The user or password is not correct" };
            }
            catch (Exception ex)
            {
                return new ResultClass() { data = null, message = "Error", state = false };
            }
        }

    }
}
