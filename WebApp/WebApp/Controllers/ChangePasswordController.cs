using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApp.CommonFunctions;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ChangePasswordController : Controller
    {

        #region Declaration
        HttpClient objHttpClient;
        HttpResponseMessage responseMessage;
        #endregion

        #region constructor
        public ChangePasswordController()
        {
            objHttpClient = new HttpClient();
            objHttpClient.BaseAddress = new Uri(CommonFunction.objApiUrl);
            objHttpClient.DefaultRequestHeaders.Accept.Clear();
            objHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        // GET: ChangePassword
        public ActionResult Index()
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies == null)
            {
                Response.Redirect("/Home");
            }
            return View();
        }

        public JsonResult PasswordUpdation([FromBody]ClsChangePassword objClsChangePassword)
        {
            try
            {
                HttpCookie reqCookies = Request.Cookies["userInfo"];
                objClsChangePassword.email_id = reqCookies["EmailID"].ToString();
                objClsChangePassword.user_code = reqCookies["LoginID"].ToString();
                var access_token = reqCookies["token"].ToString();
                objHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                responseMessage = objHttpClient.PostAsJsonAsync(CommonFunction.objApiUrl + "ChangePassword/Update", objClsChangePassword).Result;
                var PasswordData = responseMessage.Content.ReadAsStringAsync().Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return Json(PasswordData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception objException)
            {
            }
            return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);
        }


    }
}