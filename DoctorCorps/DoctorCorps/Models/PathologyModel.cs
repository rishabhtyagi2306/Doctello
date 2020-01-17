using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorCorps.Models
{
    public class PathologyModel
    {
        public int PathologyID { get; set; }
        public string PathologyName { get; set; }
        public int PathologyFees { get; set; }
        public string PatholgyDescription { get; set; }
        public Nullable<int> DoctorID { get; set; }
        public Nullable<int> HospitalID { get; set; }
        public string PathologyLocation { get; set; }

        public virtual DoctorTable DoctorTable { get; set; }
        public virtual HospitalTable HospitalTable { get; set; }
    }
}