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
        public virtual ICollection<DoctorModel> DoctorModel { get; set; }
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


        //public List<HospitalModel> HospitalList()
        //{
        //    using(var context = new DoctorCorpsEntities())
        //    {
        //        var result = context.HospitalTable
        //            .Select(x => new HospitalModel()
        //            {
        //                HospitalID = x.HospitalID,
        //                HospitalName = x.HospitalName,
        //                HospitalEmail = x.HospitalEmail,
        //                HospitalPhone = x.HospitalPhone,
        //                HospitalImage = x.HospitalImage,
        //                AverageRating = x.AverageRating
        //            }).ToList();
        //        return result;
        //    }
        //}

        public List<HospitalModel> HospitalListLocation(string Location)
        {
            using(var context = new DoctorCorpsEntities())
            {
                if(Location != "null")
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
                else
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
        }

        public List<ServiceModel> HomeServices()
        {
            using(var context = new DoctorCorpsEntities())
            {
                var result = context.ServiceTable
                    .Select(m => new ServiceModel()
                    {
                        ServiceID = m.ServiceID,
                        ServiceName = m.ServiceName
                    }).ToList();
                return result;
            }
        }

        public List<DoctorModel> HomeDoctorList(string location, int serviceid)
        {
            using(var context = new DoctorCorpsEntities())
            {
                DoctorTable doctor = new DoctorTable();
                if(location != "null")
                {
                    var result = context.DoctorTable
                        .Where(x => x.DoctorLocation == location && x.ServiceID == serviceid)
                        .Select(x => new DoctorModel()
                        {
                            DoctorID = x.DoctorID,
                            DoctorName = x.DoctorName,
                            DoctorRegNo = x.DoctorRegNo,
                            DoctorEmail = x.DoctorEmail,
                            DoctorFees = x.DoctorFees
                        }).ToList();
                    return result;
                }
                else
                {
                    var result = context.DoctorTable
                    .Where(x => x.ServiceID == serviceid)
                    .Select(x => new DoctorModel()
                    {
                        DoctorID = x.DoctorID,
                        DoctorName = x.DoctorName,
                        DoctorRegNo = x.DoctorRegNo,
                        DoctorEmail = x.DoctorEmail,
                        DoctorFees = x.DoctorFees
                    }).ToList();
                    return result;
                }
            }
        }
    }
}