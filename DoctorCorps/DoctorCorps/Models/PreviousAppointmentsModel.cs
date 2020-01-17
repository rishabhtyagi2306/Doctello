using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorCorps.Models
{
    public class PreviousAppointmentsModel
    {
        public int PAID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> HospitalID { get; set; }
        public Nullable<int> DoctorID { get; set; }

        public virtual DoctorTable DoctorTable { get; set; }
        public virtual HospitalTable HospitalTable { get; set; }
        public virtual UserTable UserTable { get; set; }
    }
}