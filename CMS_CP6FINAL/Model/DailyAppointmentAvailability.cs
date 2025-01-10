using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class DailyAppointmentAvailability
{
    public int DailyAppointmentId { get; set; }

    public int? DocAvlId { get; set; }

    public int Token { get; set; }

    #region-4
    public int AppointmentId { get; set; }
    #endregion
    public virtual NewAppointment Appointment { get; set; } = null!;

    public virtual DoctorAvailability? DocAvl { get; set; }
}
