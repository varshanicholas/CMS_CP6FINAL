using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class MedicineCategory
{
    public int MedicineCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;
     [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
