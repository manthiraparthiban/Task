using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ClsApplication
    {
    }
    public class ClsLogin
    {
        public string user_name { get; set; }
        public string password { get; set; }
    }

    public class ClsRegister
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email_id { get; set; }
        public string password { get; set; }
    }

    public class ClsChangePassword
    {
        public string old_password { get; set; }
        public string new_password { get; set; }
        public string email_id { get; set; }
        public string user_code { get; set; }
    }

    public class ClsSessionValues
    {
        public string usercode { get; set; }
        public string emailid { get; set; }
        public string username { get; set; }
        public string token { get; set; }
        public string password { get; set; }
    }
}