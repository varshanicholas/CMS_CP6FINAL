using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class DoctorAvailability
{
    public int DocAvlId { get; set; }

    public int DoctorId { get; set; }

    public string? Weekdays { get; set; }

    public string? MorningSession { get; set; }

    public string? EveningSession { get; set; }

    public virtual ICollection<DailyAppointmentAvailability> DailyAppointmentAvailabilities { get; set; } = new List<DailyAppointmentAvailability>();

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ICollection<NewAppointment> NewAppointments { get; set; } = new List<NewAppointment>();
}
