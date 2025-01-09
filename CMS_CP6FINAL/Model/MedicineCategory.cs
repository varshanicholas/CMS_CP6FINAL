using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CMS_CP6FINAL.Model;

public partial class MedicineCategory
{
    public int MedicineCategoryId { get; set; }
    public string? CategoryName { get; set; } // Nullable string


    [JsonIgnore]

    public virtual ICollection<Medicine>? Medicines { get; set; } = new List<Medicine>();
}

