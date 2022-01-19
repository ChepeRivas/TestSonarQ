using Microsoft.Extensions.Configuration;
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
    public class PasswordRepository : PwContext
    {
        private readonly ISendMail Mail;
        private readonly IConfiguration Configuration;


        public PasswordRepository(ISendMail mail, IConfiguration configuration)
        {
            Mail = mail;
            Configuration = configuration;
        }

        public async Task<int> asnwer() {

            var collection =await SelectData<Users>("SELECT * FROM users");
            int cont = 0;
            foreach (var log in collection)
            {
                if (log.answer == "0")
                {
                    string key = MD5EncryptClass.RandomKey(log.name);
                    int result = await PutData($"UPDATE users SET answer = '{key}' WHERE ID={log.ID} ; ");
                    cont++;
                }

            }
            return cont;

        }
        public async Task<string> GMValidarPJ(long id)
        {

            var pj = await FilterData<Users>($"SELECT ID, passwd , passwd2 FROM users  WHERE ID = '{id}';");
            if (pj == null)
            {
                return "The character does not exist or the ID is incorrect.";
            }
            else
            {
                int result = await PutData($"UPDATE users SET passwd ='{pj.passwd.ToString().Replace("@@", "")}' , passwd2='{pj.passwd2.ToString().Replace("@@", "")}'  WHERE ID={pj.ID} ; ");
                if (result != 0)
                {
                    return $"Account: {pj.name} - Password changed successfully";
                }
                return "Error trying to change the password, please contact Tamao!";
            }
        }
        public async Task<int> ValidarPersonaje(string key = "")
        {
            try
            {

                if (key == "")
                {
                    return 403;
                }

                var pj = await FilterData<Users>($"SELECT ID, passwd , passwd2 FROM users  WHERE answer = '{key}';");
                if (pj == null)
                {
                    return 404;
                }
                else
                {
                    int result = await PutData($"UPDATE users SET passwd ='{pj.passwd.ToString().Replace("@@", "")}' , passwd2='{pj.passwd2.ToString().Replace("@@", "")}'  WHERE ID={pj.ID} ; ");
                    if (result != 0)
                    {
                        return 200;
                    }
                    return 500;
                }
            }
            catch (Exception ex)
            {
                return 500;
            }



        }
        public async Task<int> ForgotPasswd(string email, string secret)
        {
            try
            {
                var pj = await Find(email, secret);
                if (pj != null)
                {
                    string host = Configuration["WebUrl"].ToString();
                    string enlace = string.Format("{0}/Password/ChangePassword/{1}", host, pj.answer);
                    String a = string.Format(@"<a href='{0}' target='{1}' rel='{2}' data-auth='{3}' data-linkindex='{4}' data-ogsc='' style='{5}'>Enlace</a>",
                        enlace,
                        "_blank",
                        "noopener noreferrer",
                        "NotApplicable",
                        "0",
                        "color: rgb(228, 159, 255) !important;");

                   
                    int mail = await Mail.Send(email, "Click on the following link to change the password of your account: " + enlace + " ", "Change or reset my password");
                    if (mail == 0)
                    {
                        return 500;
                    }
                    else
                    {
                        return 200;
                    }
                }
                return 404;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }
        public async Task<Users> Find(string email, string name)
        {
            try
            {
                return await FilterData<Users>($"SELECT  name, answer, email  FROM users Where email ='{email.Trim()}' and name = '{name.Trim()}';");

            }
            catch (Exception ex)
            {

                return null;
            }

        }
        public async Task<Users> UserByAnswer(string key = "")
        {
            try
            {
                if (key == "")
                {
                    return null;
                }

                var pj = await FilterData<Users>($"SELECT name, answer, email  FROM users  WHERE answer = '{key}';");
                return pj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<int> ChangePassword(VMPassword newPass)
        {
            try
            {

                if (newPass.key == "")
                {
                    return 403;
                }

                var pj = await FilterData<Users>($"SELECT ID,name FROM users  WHERE answer = '{newPass.key}';");
                if (pj == null)
                {
                    return 404;
                }
                else
                {
                    string key = MD5EncryptClass.RandomKey(pj.name);
                    int result = await PutData($"UPDATE users SET answer = '{key}', passwd ='{ MD5EncryptClass.GetEncrypt_64(pj.name, newPass.NewPassword) + "@@"}' , passwd2='{ MD5EncryptClass.GetEncrypt_64(pj.name, newPass.NewPassword) + "@@"}'  WHERE ID={pj.ID} ; ");
                   
                    string host = Configuration["WebUrl"].ToString();
                    string enlace = string.Format("{0}/Users/Validar/{1}", host, key);
                   
                    if (result != 0)
                    {
                        var Collection = await UserByAnswer(key);
                        int mail = await Mail.Send(Collection.email, "Click on the following link to validate your account: " + enlace);
                        if (mail == 0)
                        {
                            return 500;
                        }
                        else
                        {
                            return 200;
                        }
                    }
                    else
                    {
                        return 500;
                    }
                }
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

    }
}
