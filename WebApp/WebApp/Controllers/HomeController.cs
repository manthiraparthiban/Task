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
    public class HomeController : Controller
    {
        #region Declaration

        HttpClient objHttpClient;
        HttpResponseMessage responseMessage;

        #endregion

        #region constructor

        public HomeController()
        {
            objHttpClient = new HttpClient();
            objHttpClient.BaseAddress = new Uri(CommonFunction.objApiUrl);
            objHttpClient.DefaultRequestHeaders.Accept.Clear();
            objHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        public ActionResult Index()
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                Response.Redirect("/Dashboard");
            }
            return View();
        }
        public ActionResult Register()
        {
            ViewBag.Message = "Register";
            return View();
        }

        public JsonResult LoginVerification([FromBody]ClsLogin objClsLogin)
        {
            try
            {
                responseMessage = objHttpClient.PostAsJsonAsync(CommonFunction.objApiUrl + "LoginVerification/Check", objClsLogin).Result;
                var LoginData = responseMessage.Content.ReadAsStringAsync().Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return Json(LoginData, JsonRequestBehavior.AllowGet);
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


        public JsonResult RegisterUser([FromBody]ClsRegister objClsRegister)
        {
            try
            {
                responseMessage = objHttpClient.PostAsJsonAsync(CommonFunction.objApiUrl + "Register/Insert", objClsRegister).Result;
                var CreateUserData = responseMessage.Content.ReadAsStringAsync().Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return Json(CreateUserData, JsonRequestBehavior.AllowGet);
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


        public string LoginSession([FromBody]ClsSessionValues objClsSessionValues)
        {
            Session["LoginID"] = objClsSessionValues.usercode;
            Session["UserName"] = objClsSessionValues.username;
            Session["EmailID"] = objClsSessionValues.emailid;
            Session["Token"] = objClsSessionValues.token;
            Session["Password"] = objClsSessionValues.password;
            if (Session["LoginID"] == null)
            {
                HttpCookie userInfo = new HttpCookie("userInfo");
                userInfo["LoginID"] = objClsSessionValues.usercode.ToString();
                userInfo["UserName"] = objClsSessionValues.username.ToString();
                userInfo["EmailID"] = objClsSessionValues.emailid.ToString();
                userInfo["Token"] = objClsSessionValues.token.ToString();
                userInfo["Password"] = objClsSessionValues.password.ToString();
                userInfo.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(userInfo);
            }
            else
            {
                HttpCookie userInfo = new HttpCookie("userInfo");
                userInfo["LoginID"] = objClsSessionValues.usercode.ToString();
                userInfo["UserName"] = objClsSessionValues.username.ToString();
                userInfo["EmailID"] = objClsSessionValues.emailid.ToString();
                userInfo["Token"] = objClsSessionValues.token.ToString();
                userInfo["Password"] = objClsSessionValues.password.ToString();
                userInfo.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(userInfo);
            }
            return "Done";
        }


        public string LogoutSession()
        {
            Response.Cookies["userInfo"].Expires = DateTime.Now.AddMonths(-1);
            Session.Abandon();
            return "Done";
        }

    }
}