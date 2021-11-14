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
    public class ChangePasswordController : ApiController
    {
        IBLogin objIBLogin;
        HttpResponseMessage response;
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(RegisterController));

        [Authorize]
        [HttpPost]
        [ActionName("Update")]
        public HttpResponseMessage Update(ClsChangePassword objClsChangePassword)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objIBLogin = new BLogin();
                    DataSet objDataSet = new DataSet();
                    objDataSet = objIBLogin.ChangePassword(objClsChangePassword);
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
                    logger.ErrorFormat(string.Format("Error:{0},MethodName={1},UserCode={2},EmailId={3},OldPassword={4},NewPassword={5}", objErr, "ChangePassword", objClsChangePassword.user_code, objClsChangePassword.email_id, objClsChangePassword.old_password, objClsChangePassword.new_password));
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
                logger.ErrorFormat(string.Format("Error:{0},MethodName={1},UserCode={2},EmailId={3},OldPassword={4},NewPassword={5}", json1, "ChangePassword", objClsChangePassword.user_code, objClsChangePassword.email_id, objClsChangePassword.old_password, objClsChangePassword.new_password));
                return response = Request.CreateResponse(HttpStatusCode.BadRequest, json1);
            }
        }
    }
}
