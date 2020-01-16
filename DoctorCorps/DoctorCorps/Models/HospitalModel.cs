using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorCorps.Models
{
    public class HospitalModel
    {
        public int HospitalID { get; set; }
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }
        public string HospitalLocation { get; set; }
        public string HospitalEmail { get; set; }
        public string HospitalPhone { get; set; }
        public string AboutHospital { get; set; }
        public Nullable<double> AverageRating { get; set; }
        public string HospitalImage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentsTable> AppointmentsTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DoctorTable> DoctorTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HospitalRatingTable> HospitalRatingTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HospitalServiceTable> HospitalServiceTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PathologyTable> PathologyTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PreviousAppointmentsTable> PreviousAppointmentsTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReportTable> ReportTable { get; set; }


        public List<HospitalModel> HospitalList()
        {
            using(var context = new DoctorCorpsEntities())
            {
                var result = context.HospitalTable
                    .Select(x => new HospitalModel()
                    {
                        HospitalID = x.HospitalID,
                        HospitalName = x.HospitalName,
                        HospitalEmail = x.HospitalEmail,
                        HospitalPhone = x.HospitalPhone,
                        HospitalImage = x.HospitalImage,
                        AverageRating = x.AverageRating
                    }).ToList();
                return result;
            }
        }

        public List<HospitalModel> HospitalListLocation(string Location)
        {
            using(var context = new DoctorCorpsEntities())
            {
                var result = context.HospitalTable
                    .Where(x => x.HospitalLocation == Location)
                    .Select(x => new HospitalModel()
                    {
                        HospitalID = x.HospitalID,
                        HospitalName = x.HospitalName,
                        HospitalEmail = x.HospitalEmail,
                        HospitalPhone = x.HospitalPhone,
                        HospitalImage = x.HospitalImage,
                        AverageRating = x.AverageRating
                    }).ToList();
                return result;
            }
        }
    }
}