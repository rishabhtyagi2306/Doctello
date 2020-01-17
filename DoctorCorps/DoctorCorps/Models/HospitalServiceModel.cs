using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorCorps.Models
{
    public class HospitalServiceModel
    {
        public int HSID { get; set; }
        public int HospitalID { get; set; }
        public int ServiceID { get; set; }

        public virtual HospitalTable HospitalTable { get; set; }
        public virtual ServiceTable ServiceTable { get; set; }
    }
}