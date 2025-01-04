using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class DailyAppointmentAvailability
{
    public int DailyAppointmentId { get; set; }

    public int? DocAvlId { get; set; }

    public int Token { get; set; }

    public int AppointmentId { get; set; }

    public virtual NewAppointment Appointment { get; set; } = null!;

    public virtual DoctorAvailability? DocAvl { get; set; }
}
