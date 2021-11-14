using Businesslayer.Classes;
using Businesslayer.Interfaces;
using EntityLayer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class LoginVerificationController : ApiController
    {
        IBLogin objIBLogin;

        HttpResponseMessage response;
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(LoginVerificationController));
        [HttpPost]
        [ActionName("Check")]
        public HttpResponseMessage Check(ClsLogin objClsLogin)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objIBLogin = new BLogin();
                    DataSet objDataSet = new DataSet();
                    objDataSet = objIBLogin.Login(objClsLogin);
                    string JWTToken = GetToken(objClsLogin.user_name, objClsLogin.password);
                    if (objDataSet != null && objDataSet.Tables[0].Rows.Count > 0)
                    {                       
                        var json = JsonConvert.SerializeObject(new
                        {
                            status = true,
                            msg = "Success",
                            data = objDataSet.Tables[0],
                            token = JWTToken
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
                    logger.ErrorFormat(string.Format("Error:{0},MethodName={1},UserName={2},Password={3}", objErr, "LoginVerificationCheck", objClsLogin.user_name, objClsLogin.password));
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
                logger.ErrorFormat(string.Format("Error:{0},MethodName={1},UserName={2},Password={3}", json1, "LoginVerificationCheck", objClsLogin.user_name, objClsLogin.password));
                return response = Request.CreateResponse(HttpStatusCode.BadRequest, json1);
            }
        }


        public string GetToken(string username, string password)
        {
            string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
            var issuer = "http://mysite.com";  //normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("name", username));
            permClaims.Add(new Claim("password", password));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }

    }
}
