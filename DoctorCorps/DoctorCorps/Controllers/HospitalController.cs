using DoctorCorps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DoctorCorps.Controllers
{
    public class HospitalController : ApiController
    {
        [HttpGet]
        [Route("api/User/HospitalList/Location")]
        public HttpResponseMessage HospitalListLocation([FromBody]HospitalModel model, string token)
        {
            using(DoctorCorpsEntities context = new DoctorCorpsEntities())
            {
                UserTable user = new UserTable();
                string username = TokenManager.ValidateToken(token);
                user = context.UserTable.FirstOrDefault(m => m.UserEmail == username);
                if (user != null)
                {
                    var x = new HospitalModel().HospitalListLocation(model.HospitalLocation);
                    return Request.CreateResponse(HttpStatusCode.OK, x);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "You are not authorized to see this content");
            }
        }

        [HttpGet]
        [Route("api/User/HomeServices")]
        public HttpResponseMessage HomeServices(string token)
        {
            using(DoctorCorpsEntities context = new DoctorCorpsEntities())
            {
                UserTable user = new UserTable();
                string username = TokenManager.ValidateToken(token);
                user = context.UserTable.FirstOrDefault(m => m.UserEmail == username);
                if (user != null)
                {
                    var x = new HospitalModel().HomeServices();
                    return Request.CreateResponse(HttpStatusCode.OK, x);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "You are not authorized to see this content");
            }
        }

        [HttpGet]
        [Route("api/User/Home/Doctor/{serviceid}")]
        public HttpResponseMessage HomeDoctorList([FromBody]HospitalModel model, int serviceid, string token)
        {
            using(DoctorCorpsEntities context = new DoctorCorpsEntities())
            {
                UserTable user = new UserTable();
                string username = TokenManager.ValidateToken(token);
                user = context.UserTable.FirstOrDefault(m => m.UserEmail == username);
                if (user != null)
                {
                    var x = new HospitalModel().HomeDoctorList(model.HospitalLocation, serviceid);
                    return Request.CreateResponse(HttpStatusCode.OK, x);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "You are not authorized to see this content");
            }
        }


        [HttpGet]
        [Route("api/User/Hospital/Detail/{hospitalid}")]
        public HttpResponseMessage HospitalDetail(int hospitalid, string token)
        {
            using (DoctorCorpsEntities context = new DoctorCorpsEntities())
            {
                UserTable user = new UserTable();
                string username = TokenManager.ValidateToken(token);
                user = context.UserTable.FirstOrDefault(m => m.UserEmail == username);
                if (user != null)
                {
                    var x = new HospitalModel().HospitalDetails(hospitalid);
                    return Request.CreateResponse(HttpStatusCode.OK, x);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "You are not authorized to see this content");
            }
        }

        [HttpGet]
        [Route("api/User/HospitalDetails/Services/{hospitalid}")]
        public HttpResponseMessage HospitalServices(int hospitalid, string token)
        {
            using(DoctorCorpsEntities context = new DoctorCorpsEntities())
            {
                UserTable user = new UserTable();
                string username = TokenManager.ValidateToken(token);
                user = context.UserTable.FirstOrDefault(m => m.UserEmail == username);
                if (user != null)
                {
                    var x = new HospitalModel().HospitalService(hospitalid);
                    return Request.CreateResponse(HttpStatusCode.OK, x);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "You are not authorized to see this content");
            }
        }

        [HttpGet]
        [Route("api/User/HospitalDetails/Rating/{hospitalid}")]
        public HttpResponseMessage HospitalFeedback(int hospitalid, string token)
        {
            using (DoctorCorpsEntities context = new DoctorCorpsEntities())
            {
                UserTable user = new UserTable();
                string username = TokenManager.ValidateToken(token);
                user = context.UserTable.FirstOrDefault(m => m.UserEmail == username);
                if (user != null)
                {
                    var x = new HospitalModel().HospitalFeedback(hospitalid);
                    return Request.CreateResponse(HttpStatusCode.OK, x);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "You are not authorized to see this content");
            }
        }

        [HttpGet]
        [Route("api/User/Hospital/Pathology/{hospitalid}")]
        public HttpResponseMessage PathologyService(int hospitalid, string token)
        {
            using (DoctorCorpsEntities context = new DoctorCorpsEntities())
            {
                UserTable user = new UserTable();
                string username = TokenManager.ValidateToken(token);
                user = context.UserTable.FirstOrDefault(m => m.UserEmail == username);
                if (user != null)
                {
                    var x = new HospitalModel().PathologyServices(hospitalid);
                    return Request.CreateResponse(HttpStatusCode.OK, x);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "You are not authorized to see this content");
            }
        }
    }
}
