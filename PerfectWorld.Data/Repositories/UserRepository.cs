using Dapper;
using Microsoft.Extensions.Configuration;
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
    public class UserRepository : PwContext
    {
        private readonly ISendMail Mail;
        private readonly IConfiguration Configuration;


        public UserRepository(ISendMail mail, IConfiguration configuration)
        {
            Mail = mail;
            Configuration = configuration;
        }

        public async Task<ResultClass> Registro(Users Collection)
        {
            try
            {
                string msg = "";
                string name = await FilterData<string>($"SELECT name FROM users where name = '{Collection.name}';");
                if (name != null)
                {
                    return new ResultClass() { data = 0, message = "The account name already exists", state = false };
                }
                if (SelectData<string>($"SELECT name FROM users where email = '{Collection.email}';").Result.Count() >= 2)
                {
                    return new ResultClass() { data = 0, message = "The mail already has the 2 regulatory accounts", state = false };
                }
                //if (FilterData<Users>($"SELECT  name FROM users Where email ='{Collection.email.Trim()}' and qq = '{Collection.qq.Trim()}';").Result != null)
                //{
                //    return new ResultClass() { data = 0, message = "El correo de personaje ya posee la palabra secreta", state = false };
                //}

                DynamicParameters param = new DynamicParameters();

                param.Add("name1", Collection.name);
                param.Add("passwd1", MD5EncryptClass.GetEncrypt_64(Collection.name, Collection.passwd) + "@@");
                param.Add("passwd21", MD5EncryptClass.GetEncrypt_64(Collection.name, Collection.passwd2) + "@@");
                param.Add("email1", Collection.email);
                //param.Add("qq1", Collection.qq);
                string key = MD5EncryptClass.RandomKey(Collection.name);
                param.Add("answer1", key);


                param.Add("truename1", "0");
                param.Add("prompt1", "0");
                param.Add("idnumber1", "0");

                param.Add("mobilenumber1", "0");
                param.Add("province1", "0");
                param.Add("city1", "0");
                param.Add("phonenumber1", "0");
                param.Add("address1", "0");
                param.Add("postalcode1", "0");
                param.Add("gender1", "0");
                param.Add("birthday1", DateTime.Now);

                param.Add("qq1", "");

                string host = Configuration["WebUrl"].ToString();
                string enlace = string.Format("{0}/Users/Validar/{1}", host, key);
                String a = string.Format(@"<a href='{0}' target='{1}' rel='{2}' data-auth='{3}' data-linkindex='{4}' data-ogsc='' style='{5}'>Enlace</a>",
                    enlace,
                    "_blank",
                    "noopener noreferrer",
                    "NotApplicable",
                    "0",
                    "color: rgb(228, 159, 255) !important;");

                int Result = await PostSP("adduser", param);
                if (Result != 0)
                {
                    int mail = await Mail.Send(Collection.email, "Click on the following link to validate your account: " + enlace);
                    if (mail == 0)
                    {
                        msg = "The email could not be sent to inform the GM by Discord.";
                    }
                    else
                    {
                        msg = "Proeceso exitoso";
                    }

                }

                return new ResultClass() { data = Result + MD5EncryptClass.RandomKey(), message = msg, };
            }
            catch (Exception ex)
            {

                return new ResultClass() { message = ex.Message, state = false };

            }

        }


    }
}
