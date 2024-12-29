using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class Weekday
{
    public int WkId { get; set; }

    public string Day { get; set; } = null!;

    public virtual ICollection<DoctorAvailability> DoctorAvailabilities { get; set; } = new List<DoctorAvailability>();

    public virtual ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
}
