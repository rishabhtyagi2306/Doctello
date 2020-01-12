using DoctorCorps.Models;
using ShoppingELF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;

namespace DoctorCorps.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("api/User/Signup")]
        public HttpResponseMessage UserSignup([FromBody]UserTable user)
        {
            bool x = new UserModel().IsEmailExist(user.UserEmail);
            if (x)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Account already exists");
            else
            {
                new UserModel().AddUser(user);
                return Request.CreateResponse(HttpStatusCode.Created, "An OTP has been sent to your phone number, please verify it to continue access");
            }

        }

        [HttpPost]
        [Route("api/User/EnterOTP/{Userid}")]
        public HttpResponseMessage EnterOTP([FromBody]UserModel model, int Userid)
        {
            using(DoctorCorpsEntities context = new DoctorCorpsEntities())
            {
                UserTable user = new UserTable();
                user = context.UserTable.FirstOrDefault(m => m.UserID == Userid);
                bool x = new UserModel().EnterOTP(model, Userid);
                if (x)
                    return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GenerateToken(user.UserEmail));
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Please Enter correct OTP");
            }
        }

        [HttpPut]
        [Route("api/User/ResendOTP/{Userid}")]
        public HttpResponseMessage ResendOTP(int Userid)
        {
            new UserModel().ResendOTP(Userid);
            return Request.CreateResponse(HttpStatusCode.OK, "An OTP has been sent to your phone number, Please verify it to continue access");
        }


        [HttpPost]
        [Route("api/User/Login")]
        public HttpResponseMessage UserLogin([FromBody]UserModel model)
        {
            
            UserTable u = new UserModel().GetUser(model.UserEmail);

            if (u == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,"The Account was not found.");
            var y = new UserModel().verification(model.UserEmail);
            var password = new UserModel().password(model.UserEmail);
            string pass = Crypto.Hash(model.Password);
            bool credentials = pass.Equals(password);
            if (credentials && y)
                return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GenerateToken(model.UserEmail));
            else
                return Request.CreateResponse(HttpStatusCode.Forbidden, "The email/password combination was wrong.");
        }
    }
}
