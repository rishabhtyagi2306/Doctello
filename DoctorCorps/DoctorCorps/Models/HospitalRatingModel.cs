﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorCorps.Models
{
    public class HospitalRatingModel
    {
        public int RatingID { get; set; }
        public int HospitalID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int UserID { get; set; }

        public virtual HospitalModel HospitalModel { get; set; }
        public virtual UserModel UserModel { get; set; }
    }
}