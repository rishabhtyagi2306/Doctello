using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorCorps.Models
{
    public class DoctorModel
    {
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string DoctorRegNo { get; set; }
        public string AboutDoctor { get; set; }
        public Nullable<int> DoctorFees { get; set; }
        public string DoctorEmail { get; set; }
        public Nullable<int> ServiceID { get; set; }
        public Nullable<int> HospitalID { get; set; }
        public string AvailableDay { get; set; }
        public string AvailableTimeMorning { get; set; }
        public string AvailableTimeEvening { get; set; }
        public string DoctorLocation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentsTable> AppointmentsTable { get; set; }
        public virtual HospitalModel HospitalModel { get; set; }
        public virtual ServiceModel ServiceModel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PathologyTable> PathologyTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PreviousAppointmentsTable> PreviousAppointmentsTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportTable> ReportTable { get; set; }
    }
}