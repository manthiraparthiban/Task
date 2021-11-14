using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DBLibrary
{
    public class DBCon
    {
        public string Stringconnection = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;

        public string StringDB()
        {
            return Stringconnection;
        }

        #region Declarations

        private MySqlDataReader objDataReader;
        private MySqlDataAdapter objDataAdapter;

        private MySqlConnection objConnection;
        private CommandType objCommandType;

        private DataSet objDataset;

        private string stringConnectionString = "";
        private string stringCommandText = "";
        private string stringResult = "";
        private string stringErrorCode = "";
        private string stringErrorDetails = "";

        #endregion

        #region Properties

        public string _stringCommandText
        {
            get
            {
                return stringCommandText;
            }
            set
            {
                stringCommandText = value;
            }
        }

        public string _stringResult
        {
            get
            {
                return stringResult;
            }
            set
            {
                stringResult = value;
            }
        }

        public string _stringErrorCode
        {
            get
            {
                return stringErrorCode;
            }
            set
            {
                stringErrorCode = value;
            }
        }

        public string _stringErrorDetails
        {
            get
            {
                return stringErrorDetails;
            }
            set
            {
                stringErrorDetails = value;
            }
        }

        public CommandType _CommandType
        {
            get
            {
                return objCommandType;
            }
            set
            {
                objCommandType = value;
            }
        }

        #endregion

        #region Constructor

        public DBCon()
        {
            stringConnectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }

        #endregion

        #region Methods\Functions

        public void OpenConnection()
        {
            using (MySqlConnection objConnection = new MySqlConnection(stringConnectionString))
            {
                objConnection.Open();
            }
        }

        public void OpenConnection(string stringConnectionString)
        {
            using (MySqlConnection objConnection = new MySqlConnection(stringConnectionString))
            {
                objConnection.Open();
            }
        }

        public void CloseConnection()
        {
            //if (objConnection.State == ConnectionState.Open)
            //{
            //    objConnection.Close();
            //}
        }

        public MySqlDataReader GetDataReader(string stringQuery)
        {
            using (MySqlCommand objCommand = new MySqlCommand())
            {
                objCommand.CommandText = stringQuery;
                using (MySqlConnection objConnection = new MySqlConnection(stringConnectionString))
                {
                    objConnection.Open();
                    objCommand.Connection = objConnection;
                    objDataReader = objCommand.ExecuteReader();
                    return objDataReader;
                }
            }
        }

        public MySqlDataReader GetDataReader(MySqlParameter[] objSqlParameters)
        {
            using (MySqlCommand objCommand = new MySqlCommand())
            {
                objCommand.CommandText = stringCommandText;
                using (MySqlConnection objConnection = new MySqlConnection(stringConnectionString))
                {
                    objConnection.Open();
                    objCommand.Connection = objConnection;
                    objCommand.CommandType = objCommandType;
                    objCommand.Parameters.AddRange(objSqlParameters);
                    objDataReader = objCommand.ExecuteReader();
                    return objDataReader;
                }

            }
        }

        public DataSet GetDataAdapter(string stringQuery)
        {
            DataSet objDataset = new DataSet();

            using (MySqlCommand objCommand = new MySqlCommand())
            {
                objCommand.CommandText = stringQuery;
                using (MySqlConnection objConnection = new MySqlConnection(stringConnectionString))
                {
                    objConnection.Open();
                    objCommand.Connection = objConnection;
                    using (MySqlDataAdapter objDataAdapter = new MySqlDataAdapter(objCommand))
                    {

                        objDataAdapter.Fill(objDataset);
                        return objDataset;

                    }
                }
            }
        }

        public DataSet GetDataAdapter(MySqlParameter[] objSqlParameters)
        {
            DataSet objDataset = new DataSet();

            using (MySqlCommand objCommand = new MySqlCommand())
            {
                objCommand.CommandText = stringCommandText;
                using (MySqlConnection objConnection = new MySqlConnection(stringConnectionString))
                {
                    objConnection.Open();
                    objCommand.Connection = objConnection;
                    objCommand.CommandType = objCommandType;
                    objCommand.Parameters.AddRange(objSqlParameters);
                    using (MySqlDataAdapter objDataAdapter = new MySqlDataAdapter(objCommand))
                    {

                        objDataAdapter.Fill(objDataset);
                        return objDataset;

                    }
                }

            }
        }




        public void GetExecutenonquery(MySqlParameter[] objSqlParameters)
        {

            using (MySqlCommand objCommand = new MySqlCommand())
            {
                objCommand.CommandText = stringCommandText;
                using (MySqlConnection objConnection = new MySqlConnection(stringConnectionString))
                {
                    objConnection.Open();
                    objCommand.Connection = objConnection;
                    objCommand.CommandType = objCommandType;
                    objCommand.Parameters.AddRange(objSqlParameters);
                    objCommand.ExecuteNonQuery();

                }

            }
        }





        #endregion


    }

}
