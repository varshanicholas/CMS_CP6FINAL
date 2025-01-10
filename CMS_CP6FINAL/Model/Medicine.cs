using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class Medicine
{
    public int MedicineId { get; set; }

    public string MedicineName { get; set; } = null!;

    public DateTime ManufacturingDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public int MedicineCategoryId { get; set; }

    public string Cost { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual MedicineCategory? MedicineCategory { get; set; } = null!;
     [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<MedicineInventory> MedicineInventories { get; set; } = new List<MedicineInventory>();
     [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<MedicinePrescription> MedicinePrescriptions { get; set; } = new List<MedicinePrescription>();
}
