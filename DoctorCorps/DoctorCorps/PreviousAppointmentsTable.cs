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
    
    public partial class PreviousAppointmentsTable
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
