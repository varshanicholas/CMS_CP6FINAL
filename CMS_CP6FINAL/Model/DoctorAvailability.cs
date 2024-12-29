using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class DoctorAvailability
{
    public int DocAvlId { get; set; }

    public int DocId { get; set; }

    public int WeekId { get; set; }

    public int TimeSlotId { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public virtual ICollection<DailyAppointmentAvailability> DailyAppointmentAvailabilities { get; set; } = new List<DailyAppointmentAvailability>();

    public virtual Doctor Doc { get; set; } = null!;

    public virtual ICollection<NewAppointment> NewAppointments { get; set; } = new List<NewAppointment>();

    public virtual TimeSlot TimeSlot { get; set; } = null!;

    public virtual Weekday Week { get; set; } = null!;
}
