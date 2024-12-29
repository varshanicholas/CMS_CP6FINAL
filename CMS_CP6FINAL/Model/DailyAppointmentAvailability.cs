using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class DailyAppointmentAvailability
{
    public int DailyAppointmentId { get; set; }

    public int? DocAvlId { get; set; }

    public int Token { get; set; }

    public int PatientId { get; set; }

    public virtual DoctorAvailability? DocAvl { get; set; }

    public virtual ICollection<NewAppointment> NewAppointments { get; set; } = new List<NewAppointment>();

    public virtual Patient Patient { get; set; } = null!;
}
