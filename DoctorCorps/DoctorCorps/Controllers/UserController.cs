using DoctorCorps.Models;
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
        [Route("api/User/AddPhone")]
        public HttpResponseMessage AddPhone([FromBody]UserTable user)
        {
            bool y = new UserModel().IsPhoneExist(user.UserPhone);
            if (y)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Account already exist with this phone number");
            else
            {
                int x = new UserModel().AddPhone(user);
                return Request.CreateResponse(HttpStatusCode.Created, x);
            }
        }


        [HttpPost]
        [Route("api/User/EnterDetails/{Userid}")]
        public HttpResponseMessage UserSignup([FromBody]UserModel model, int Userid)
        {
            bool x = new UserModel().IsEmailExist(model.UserEmail);
            if (x)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Account already exists with this E-mail");
            else
            {
                new UserModel().AddUser(model, Userid);
                return Request.CreateResponse(HttpStatusCode.Created, TokenManager.GenerateToken(model.UserEmail));
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
                    return Request.CreateResponse(HttpStatusCode.OK, "OTP entered is correct");
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

        [HttpPost]
        [Route("api/User/VerifyAadhar")]
        public HttpResponseMessage VerifyAadhar([FromBody]UserModel model, string token)
        {
            if (model.AadharNo.Length == 12)
            {
                bool x = AadharCard.validateVerhoeff(model.AadharNo);
                if (x)
                {
                    using (DoctorCorpsEntities context = new DoctorCorpsEntities())
                    {
                        UserTable user = new UserTable();
                        string username = TokenManager.ValidateToken(token);
                        user = context.UserTable.FirstOrDefault(m => m.UserEmail == username);
                        new UserModel().AadharCard(model, user.UserID);
                        return Request.CreateResponse(HttpStatusCode.OK, "Your Aadhar Number is verified");
                    }
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Please Enter a valid Aadhar number");
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Your Aadhar should be 12 digits long");
        }

        [HttpGet]
        [Route("api/User/Profile")]
        public HttpResponseMessage UserProfile(string token)
        {
            using (DoctorCorpsEntities context = new DoctorCorpsEntities())
            {
                UserTable user = new UserTable();
                string username = TokenManager.ValidateToken(token);
                user = context.UserTable.FirstOrDefault(m => m.UserEmail == username);
                if (user != null)
                {
                    var x = new UserModel().UserProfile(user.UserID);
                    return Request.CreateResponse(HttpStatusCode.OK, x);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "You are not authorized to see this content");
            }
        }
    }
}
