using Businesslayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using System.Data;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Classes;

namespace Businesslayer.Classes
{
    public class BLogin : IBLogin
    {
        IDLogin objIDLogin = new DLogin();

        public DataSet ChangePassword(ClsChangePassword objClsChangePassword)
        {
            return objIDLogin.ChangePassword(objClsChangePassword);
        }

        public DataSet Login(ClsLogin objClsLogin)
        {
            return objIDLogin.Login(objClsLogin);
        }

        public DataSet Register(ClsRegister objClsRegister)
        {
            return objIDLogin.Register(objClsRegister);
        }
    }
}
