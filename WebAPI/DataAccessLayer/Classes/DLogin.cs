using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using System.Data;
using DBLibrary;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Classes
{
    public class DLogin : IDLogin
    {
        #region Declaration

        DBCon objDBCon = new DBCon();
        DBCon objDBLibrary;
        MySqlParameter[] objMySqlParameter;
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(DLogin));
        #endregion
        #region Methods / functions
        public DataSet Login(ClsLogin objClsLogin)
        {
            DataSet objDataset = new DataSet();
            try
            {
                objDBLibrary = new DBCon();
                objDBLibrary._stringCommandText = "SP_LoginVerification";
                objDBLibrary._CommandType = CommandType.StoredProcedure;

                objMySqlParameter = new MySqlParameter[2];

                objMySqlParameter[0] = new MySqlParameter("P_user_name", MySqlDbType.VarChar, 45);
                objMySqlParameter[0].Value = objClsLogin.user_name;

                objMySqlParameter[1] = new MySqlParameter("P_password", MySqlDbType.VarChar, 45);
                objMySqlParameter[1].Value = objClsLogin.password;

                objDataset = objDBLibrary.GetDataAdapter(objMySqlParameter);
                return objDataset;
            }
            catch (Exception objException)
            {
                Exception objErr = objException.GetBaseException();
                logger.ErrorFormat(string.Format("Error:{0},MethodName={1},P_user_name={2},P_password={3},Stored_Procedure={4}", objErr, "Login", objClsLogin.user_name, objClsLogin.password , "SP_LoginVerification"));
            }
            return objDataset;
        }

        public DataSet ChangePassword(ClsChangePassword objClsChangePassword)
        {
            DataSet objDataset = new DataSet();
            try
            {
                objDBLibrary = new DBCon();
                objDBLibrary._stringCommandText = "SP_ChangePassword";
                objDBLibrary._CommandType = CommandType.StoredProcedure;

                objMySqlParameter = new MySqlParameter[4];

                objMySqlParameter[0] = new MySqlParameter("P_user_code", MySqlDbType.VarChar, 45);
                objMySqlParameter[0].Value = objClsChangePassword.user_code;

                objMySqlParameter[1] = new MySqlParameter("P_old_password", MySqlDbType.VarChar, 45);
                objMySqlParameter[1].Value = objClsChangePassword.old_password;

                objMySqlParameter[2] = new MySqlParameter("P_new_password", MySqlDbType.VarChar, 45);
                objMySqlParameter[2].Value = objClsChangePassword.new_password;

                objMySqlParameter[3] = new MySqlParameter("P_email_id", MySqlDbType.VarChar, 45);
                objMySqlParameter[3].Value = objClsChangePassword.email_id;

                objDataset = objDBLibrary.GetDataAdapter(objMySqlParameter);
                return objDataset;
            }
            catch (Exception objException)
            {
                Exception objErr = objException.GetBaseException();
                logger.ErrorFormat(string.Format("Error:{0},MethodName={1},P_user_code={2},P_old_password={3},P_new_password={4},P_email_id={5},Stored_Procedure={6}", objErr, "ChangePassword", objClsChangePassword.user_code, objClsChangePassword.old_password, objClsChangePassword.new_password, objClsChangePassword.email_id, "SP_ChangePassword"));
            }
            return objDataset;
        }

        public DataSet Register(ClsRegister objClsRegister)
        {
            DataSet objDataset = new DataSet();
            try
            {
                objDBLibrary = new DBCon();
                objDBLibrary._stringCommandText = "SP_Register";
                objDBLibrary._CommandType = CommandType.StoredProcedure;

                objMySqlParameter = new MySqlParameter[4];

                objMySqlParameter[0] = new MySqlParameter("P_firstname", MySqlDbType.VarChar, 45);
                objMySqlParameter[0].Value = objClsRegister.firstname;

                objMySqlParameter[1] = new MySqlParameter("P_lastname", MySqlDbType.VarChar, 45);
                objMySqlParameter[1].Value = objClsRegister.lastname;

                objMySqlParameter[2] = new MySqlParameter("P_email_id", MySqlDbType.VarChar, 45);
                objMySqlParameter[2].Value = objClsRegister.email_id;

                objMySqlParameter[3] = new MySqlParameter("P_password", MySqlDbType.VarChar, 45);
                objMySqlParameter[3].Value = objClsRegister.password;

                objDataset = objDBLibrary.GetDataAdapter(objMySqlParameter);
                return objDataset;
            }
            catch (Exception objException)
            {
                Exception objErr = objException.GetBaseException();
                logger.ErrorFormat(string.Format("Error:{0},MethodName={1},P_firstname={2},P_lastname={3},P_email_id={4},P_password={5},Stored_Procedure={6}", objErr, "Register", objClsRegister.firstname, objClsRegister.lastname, objClsRegister.email_id, objClsRegister.password, "SP_Register"));
            }
            return objDataset;
        }
        #endregion
    }
}
