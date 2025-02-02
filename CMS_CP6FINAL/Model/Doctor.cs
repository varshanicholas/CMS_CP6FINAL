﻿using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model
{
    public partial class Doctor
    {
        public int DoctorId { get; set; }
        public int StaffId { get; set; }
        public decimal ConsultationFee { get; set; }
        public int SpecializationId { get; set; }
        public bool IsActive { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<DoctorAvailability> DoctorAvailabilities { get; set; } = new List<DoctorAvailability>();

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<DoctorReferral> DoctorReferrals { get; set; } = new List<DoctorReferral>();

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<NewAppointment> NewAppointments { get; set; } = new List<NewAppointment>();
        
        public virtual Specialization? Specialization { get; set; } = null!;
           
        public virtual Staff? Staff { get; set; } = null!;
    }
}
























