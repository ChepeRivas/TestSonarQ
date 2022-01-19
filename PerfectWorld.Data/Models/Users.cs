using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWorld.Data.Models
{
    public class Users
    {
        public Users()
        {
            this.Prompt = "0";
            this.answer = "0";
            this.truename = "0";
            this.idnumber = "0";

            this.mobilenumber = "0";
            this.province = "0";
            this.city = "0";
            this.phonenumber = "0";
            this.address = "0";
            this.postalcode = "0";
            this.gender = 0;
            this.birthday = "0";

        }
        public int ID { get; set; } = 0;
        public string name { get; set; }
        public string passwd { get; set; }
        public string Prompt { get; set; }
        public string answer { get; set; }
        public string truename { get; set; }
        public string idnumber { get; set; }
        public string email { get; set; }
        public string mobilenumber { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string phonenumber { get; set; }
        public string address { get; set; }
        public string postalcode { get; set; }
        public int gender { get; set; }
        public string birthday { get; set; }
        public DateTime creatime { get; set; }
        public string qq { get; set; }
        public string passwd2 { get; set; }
        public int money { get; set; }
        public int bonuses { get; set; }
        public int role { get; set; }
        public int last_update { get; set; }
        public int VotePoint { get; set; }
        public string VoteDates { get; set; }
        public string txt { get; set; }
    }
}
