using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public int StaffId { get; set; }

    public decimal ConsultationFee { get; set; }

    public virtual ICollection<DoctorAvailability> DoctorAvailabilities { get; set; } = new List<DoctorAvailability>();

    public virtual ICollection<DoctorReferral> DoctorReferrals { get; set; } = new List<DoctorReferral>();

    public virtual ICollection<NewAppointment> NewAppointments { get; set; } = new List<NewAppointment>();

    public virtual Staff Staff { get; set; } = null!;


    // New properties for navigation
    public int SpecializationId
    {
        get
        {
            return Staff?.Department?.SpecializationId ?? 0;
        }
    }
    public virtual Specialization Specialization
    {
        get
        {
            return Staff?.Department?.Specialization;
        }
    }
}


