//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoctorCorps
{
    using System;
    using System.Collections.Generic;
    
    public partial class DoctorTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DoctorTable()
        {
            this.AppointmentsTable = new HashSet<AppointmentsTable>();
            this.PathologyTable = new HashSet<PathologyTable>();
            this.PreviousAppointmentsTable = new HashSet<PreviousAppointmentsTable>();
            this.ReportTable = new HashSet<ReportTable>();
        }
    
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentsTable> AppointmentsTable { get; set; }
        public virtual HospitalTable HospitalTable { get; set; }
        public virtual ServiceTable ServiceTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PathologyTable> PathologyTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PreviousAppointmentsTable> PreviousAppointmentsTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportTable> ReportTable { get; set; }
    }
}
