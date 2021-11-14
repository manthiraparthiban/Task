using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IDLogin
    {
        DataSet Login(ClsLogin objClsLogin);
        DataSet ChangePassword(ClsChangePassword objClsChangePassword);
        DataSet Register(ClsRegister objClsRegister);
    }
}
