using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserGender { get; set; }
        public Nullable<System.DateTime> UserDOB { get; set; }
        public string MedicalHistory { get; set; }
        public string UserLocation { get; set; }
        public string AadharNo { get; set; }
        public string Password { get; set; }
        public Nullable<bool> IsAccountVerified { get; set; }
        public Nullable<bool> IsAadharVerified { get; set; }
        public string OTP { get; set; }

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


        public void AddUser(UserTable user)
        {
            UserTable us = new UserTable();
            using (DoctorCorpsEntities db = new DoctorCorpsEntities())
            {
                user.OTPDateTime = DateTime.Now;
                user.OTP = Convert.ToString(GenerateOTP());
                user.Password = Crypto.Hash(user.Password);
                db.UserTable.Add(user);
                db.SaveChanges();
                new SmsService().Messages(new SmsService().Mssg(user, user.OTP));
            }
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