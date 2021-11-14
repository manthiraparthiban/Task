using Businesslayer.Classes;
using Businesslayer.Interfaces;
using EntityLayer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class RegisterController : ApiController
    {
        IBLogin objIBLogin;
        HttpResponseMessage response;
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(RegisterController));

        [HttpPost]
        [ActionName("Insert")]
        public HttpResponseMessage Insert(ClsRegister objClsRegister)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objIBLogin = new BLogin();
                    DataSet objDataSet = new DataSet();
                    objDataSet = objIBLogin.Register(objClsRegister);
                    if (objDataSet != null && objDataSet.Tables[0].Rows.Count > 0)
                    {
                        var json = JsonConvert.SerializeObject(new
                        {
                            status = true,
                            msg = "Success",
                            data = objDataSet.Tables[0],
                        });
                        JObject json1 = JObject.Parse(json);
                        return response = Request.CreateResponse(HttpStatusCode.OK, json1);
                    }
                    else
                    {
                        var json = JsonConvert.SerializeObject(new
                        {
                            status = false,
                            msg = "no record found",
                        });
                        JObject json1 = JObject.Parse(json);
                        return response = Request.CreateResponse(HttpStatusCode.OK, json1);
                    }
                }
                catch (Exception objException)
                {
                    Exception objErr = objException.GetBaseException();
                    logger.ErrorFormat(string.Format("Error:{0},MethodName={1},FirstName={2},LastName={3},EmailID={4},Password={5}", objErr, "Register", objClsRegister.firstname, objClsRegister.lastname, objClsRegister.email_id, objClsRegister.password));
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, objException.Message);
                }
            }
            else
            {
                var json = JsonConvert.SerializeObject(new
                {
                    status = false,
                    msg = "Invalid Request",
                    data = ModelState.Values.Select(e => e.Errors).ToList()
                });
                JObject json1 = JObject.Parse(json);
                logger.ErrorFormat(string.Format("Error:{0},MethodName={1},FirstName={2},LastName={3},EmailID={4},Password={5}", json1, "Register", objClsRegister.firstname, objClsRegister.lastname, objClsRegister.email_id, objClsRegister.password));
                return response = Request.CreateResponse(HttpStatusCode.BadRequest, json1);
            }
        }
    }
}
