﻿using System;
using System.Collections.Generic;

namespace CMS_CP6FINAL.Model;

public partial class LabTestCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<LabTest> LabTests { get; set; } = new List<LabTest>();
}