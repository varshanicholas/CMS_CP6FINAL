using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

<<<<<<< HEAD
    public int SpecializationId { get; set; }

<<<<<<< HEAD
    public virtual Specialization Specialization { get; set; }
=======

=======
>>>>>>> 4e7aadf4c07fde4730c818842b21554d1f7551ed
    public virtual ICollection<NewAppointment> NewAppointments { get; set; } = new List<NewAppointment>();

    public virtual ICollection<Specialization> Specializations { get; set; } = new List<Specialization>();

<<<<<<< HEAD
>>>>>>> 81d8d89bb65215eaa82ed75f09d12a5529332f40

    [System.Text.Json.Serialization.JsonIgnore] 
=======
>>>>>>> 4e7aadf4c07fde4730c818842b21554d1f7551ed
    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
