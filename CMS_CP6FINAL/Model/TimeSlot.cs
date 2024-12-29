using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class TimeSlot
{
    public int TimeSlotId { get; set; }

    public string TimeSlot1 { get; set; } = null!;

    public int WkId { get; set; }

    public virtual ICollection<DoctorAvailability> DoctorAvailabilities { get; set; } = new List<DoctorAvailability>();

    public virtual Weekday Wk { get; set; } = null!;
}
