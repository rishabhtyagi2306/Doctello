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

        public virtual ICollection<AppointmentsModel> AppointmentsModel { get; set; }
        
        public virtual ICollection<DoctorModel> DoctorModel { get; set; }
        
        public virtual ICollection<HospitalRatingModel> HospitalRatingModel { get; set; }
        
        public virtual ICollection<HospitalServiceModel> HospitalServiceModel { get; set; }
        
        public virtual ICollection<PathologyModel> PathologyModel { get; set; }
        
        public virtual ICollection<PreviousAppointmentsModel> PreviousAppointmentsModel { get; set; }
        
        public virtual ICollection<ReportModel> ReportModel { get; set; }




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
                        ServiceName = m.ServiceName,
                        Image = m.Image
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

        public List<DoctorModel> HospitalDetails(int hospitalid)
        {
            using(var context = new DoctorCorpsEntities())
            {
                var result = context.DoctorTable
                    .Where(x => x.HospitalTable.HospitalID == hospitalid)
                    .Select(x => new DoctorModel()
                    {
                        DoctorID = x.DoctorID,
                        DoctorName = x.DoctorName,
                        DoctorRegNo = x.DoctorRegNo,
                        DoctorFees = x.DoctorFees,
                        DoctorEmail = x.DoctorEmail,
                        AvailableDay = x.AvailableDay,
                        AvailableTimeEvening = x.AvailableTimeEvening,
                        AvailableTimeMorning = x.AvailableTimeMorning,
                        HospitalModel = new HospitalModel()
                        {
                            HospitalID = x.HospitalTable.HospitalID,
                            HospitalName = x.HospitalTable.HospitalName,
                            HospitalEmail = x.HospitalTable.HospitalEmail,
                            HospitalPhone = x.HospitalTable.HospitalPhone,
                            HospitalImage = x.HospitalTable.HospitalImage,
                            AverageRating = x.HospitalTable.AverageRating,
                            HospitalAddress = x.HospitalTable.HospitalAddress,
                            HospitalLocation = x.HospitalTable.HospitalLocation,
                            AboutHospital = x.HospitalTable.AboutHospital
                        }
                    }).ToList();
                return result;
            }
        }

        public List<HospitalServiceModel> HospitalService(int hospitalid)
        {
            using(var context = new DoctorCorpsEntities())
            {
                var result = context.HospitalServiceTable
                    .Where(x => x.HospitalID == hospitalid)
                    .Select(x => new HospitalServiceModel()
                    {
                        ServiceModel = new ServiceModel()
                        {
                            ServiceID = x.ServiceTable.ServiceID,
                            ServiceName = x.ServiceTable.ServiceName,
                            Image = x.ServiceTable.Image
                        }
                    }).ToList();
                return result;
            }
        }

        public List<HospitalRatingModel> HospitalFeedback(int hospitalid)
        {
            using(var context = new DoctorCorpsEntities())
            {
                var result = context.HospitalRatingTable
                    .Where(x => x.HospitalID == hospitalid)
                    .Select(x => new HospitalRatingModel()
                    {
                        RatingID = x.RatingID,
                        Rating = x.Rating,
                        Comment = x.Comment,
                        UserModel = new UserModel()
                        {
                            UserName = x.UserTable.UserName,
                            UserEmail = x.UserTable.UserEmail
                        }
                    }).ToList();
                return result;
            }
        }

        public List<PathologyModel> PathologyServices(int hospitalid)
        {
            using(var context = new DoctorCorpsEntities())
            {
                var result = context.PathologyTable
                    .Where(x => x.HospitalID == hospitalid)
                    .Select(x => new PathologyModel()
                    {
                        PathologyID = x.PathologyID,
                        PathologyName = x.PathologyName,
                        PathologyFees = x.PathologyFees,
                        PatholgyDescription = x.PatholgyDescription,
                        PathologyLocation = x.PathologyLocation,
                        DoctorModel = new DoctorModel()
                        {
                            DoctorID = x.DoctorTable.DoctorID,
                            DoctorName = x.DoctorTable.DoctorName,
                            DoctorRegNo = x.DoctorTable.DoctorRegNo,
                            DoctorEmail = x.DoctorTable.DoctorEmail,
                            AvailableDay = x.DoctorTable.AvailableDay,
                            AvailableTimeEvening = x.DoctorTable.AvailableTimeEvening,
                            AvailableTimeMorning = x.DoctorTable.AvailableTimeMorning
                        }
                    }).ToList();
                return result;
            }
        }
    }
}