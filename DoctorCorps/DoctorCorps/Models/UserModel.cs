using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace DoctorCorps.Models
{
    public class UserModel
    {
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Please enter a valid Phone Number")]
        public string UserPhone { get; set; }
        public string UserGender { get; set; }
        public Nullable<System.DateTime> UserDOB { get; set; }
        public string MedicalHistory { get; set; }
        public string UserLocation { get; set; }
        public string AadharNo { get; set; }

        //[Required]
        //[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{7,}$", ErrorMessage = "Password must be atleast 7 characters long with Atleast one capital letter,Number and Special symbol (e.g. !@#$%^&*)")]
        public string Password { get; set; }


        [Required(ErrorMessage = "please confirm your message")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public Nullable<bool> IsAccountVerified { get; set; }
        public Nullable<bool> IsAadharVerified { get; set; }
        public string OTP { get; set; }
        public Nullable<System.DateTime> OTPDateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentsTable> AppointmentsTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HospitalRatingTable> HospitalRatingTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PreviousAppointmentsTable> PreviousAppointmentsTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportTable> ReportTable { get; set; }


        public string GenerateOTP()
        {
            Random generator = new Random();
            String OTP = generator.Next(100000, 1000000).ToString();
            return OTP;
        }

        public int AddPhone(UserTable user)
        {
            using(DoctorCorpsEntities context = new DoctorCorpsEntities())
            {
                user.OTPDateTime = DateTime.Now;
                user.OTP = Convert.ToString(GenerateOTP());
                context.UserTable.Add(user);
                context.SaveChanges();
                new SmsService().Messages(new SmsService().Mssg(user, user.OTP));
                return user.UserID;
            }
        }

        public bool IsPhoneExist(string Phone)
        {
            using (DoctorCorpsEntities context = new DoctorCorpsEntities())
            {
                var v = context.UserTable.Where(a => a.UserPhone == Phone).FirstOrDefault();
                return v != null;
            }
        }

        public void AddUser(UserModel model, int Userid)
        {
            UserTable user = new UserTable();
            using (DoctorCorpsEntities db = new DoctorCorpsEntities())
            {
                user = db.UserTable.FirstOrDefault(m => m.UserID == Userid);
                user.UserEmail = model.UserEmail;
                user.UserName = model.UserName;
                user.UserGender = model.UserGender;
                user.UserDOB = model.UserDOB;
                user.Password = Crypto.Hash(model.Password);
                db.UserTable.Add(user);
                db.SaveChanges();
            }
        }

        public bool IsOTPVerified(int uid)
        {
            using(DoctorCorpsEntities context = new DoctorCorpsEntities())
            {
                UserTable user = new UserTable();
                user = context.UserTable.FirstOrDefault(m => m.UserID == uid);
                TimeSpan s = (TimeSpan)(DateTime.Now - user.OTPDateTime);
                if (s.TotalMinutes <= 2)
                    return true;
                else
                    return false;
            }
        }

        public bool EnterOTP(UserModel model, int uid)
        {
            using(DoctorCorpsEntities context = new DoctorCorpsEntities())
            {
                UserTable user = new UserTable();
                user = context.UserTable.FirstOrDefault(m => m.UserID == uid);
                bool x = IsOTPVerified(uid);
                if (user.OTP == model.OTP && x)
                {
                    user.IsAccountVerified = true;
                    context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public void ResendOTP(int Userid)
        {
            using (DoctorCorpsEntities context = new DoctorCorpsEntities())
            {
                UserTable user = new UserTable();
                user = context.UserTable.FirstOrDefault(m => m.UserID == Userid);
                user.OTP = Convert.ToString(GenerateOTP());
                user.OTPDateTime = DateTime.Now;
                context.SaveChanges();
                new SmsService().Messages(new SmsService().Mssg(user, user.OTP));
            }
        }

        public bool IsEmailExist(string Email)
        {
            using (DoctorCorpsEntities context = new DoctorCorpsEntities())
            {
                var v = context.UserTable.Where(a => a.UserEmail == Email).FirstOrDefault();
                return v != null;
            }
        }

        public UserTable GetUser(string username)
        {
            try
            {
                using(DoctorCorpsEntities context = new DoctorCorpsEntities())
                {
                    return context.UserTable.FirstOrDefault(user => user.UserEmail.Equals(username));
                }
            }
            catch
            {
                return null;
            }
        }

        public bool verification(string Email)
        {
            DoctorCorpsEntities context = new DoctorCorpsEntities();
            UserTable Fac = new UserTable();
            Fac = context.UserTable.SingleOrDefault(m => m.UserEmail == Email);
            var y = Convert.ToBoolean(Fac.IsAccountVerified);
            return y;
        }

        public string password(string Email)
        {
            DoctorCorpsEntities context = new DoctorCorpsEntities();
            UserTable us = new UserTable();
            us = context.UserTable.SingleOrDefault(x => x.UserEmail == Email);
            string pass = Convert.ToString(us.Password);
            return pass;
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public IdentityMessage Mssg(UserTable user, string otp)
        {
            IdentityMessage message = new IdentityMessage();

            message.Body = "Your OTP is " + otp + ". Please never share your OTP with anyone, Doctello never asks for OTP on Phone calls";
            message.Destination = user.UserPhone;
            return message;

        }

        public void Messages(IdentityMessage message)
        {
            var accountSid = ConfigurationManager.AppSettings["SMSAccountIdentification"];
            var authToken = ConfigurationManager.AppSettings["SMSAccountPassword"];
            var fromNumber = ConfigurationManager.AppSettings["SMSAccountFrom"];
            TwilioClient.Init(accountSid, authToken);

            MessageResource result = MessageResource.Create(
            new PhoneNumber(message.Destination),
            from: new PhoneNumber(fromNumber),
            body: message.Body
            );

            Trace.TraceInformation(result.Status.ToString());
            return;
        }

        Task IIdentityMessageService.SendAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }
    }
}