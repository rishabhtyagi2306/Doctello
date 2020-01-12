using DoctorCorps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DoctorCorps.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("api/User/Signup")]
        public HttpResponseMessage UserSignup([FromBody]UserTable user)
        {
            new UserModel().AddUser(user);
            return Request.CreateResponse(HttpStatusCode.Created, "An OTP has been sent to your phone no please verify it to continue access");

        }

        [HttpPost]
        [Route("api/User/EnterOTP/{Userid}")]
        public HttpResponseMessage EnterOTP([FromBody]UserModel model, int Userid)
        {
            bool x = new UserModel().EnterOTP(model, Userid);
            if (x)
                return Request.CreateResponse(HttpStatusCode.OK, "Otp Verified");
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, "Please Enter correct OTP");
        }
    }
}
