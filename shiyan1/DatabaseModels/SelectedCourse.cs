using System;
using System.Collections.Generic;

namespace shiyan1.DatabaseModels;

public partial class SelectedCourse
{
    public string Cid { get; set; } = null!;
    public string Sid { get; set; } = null!;
    public DateOnly? ScDate { get; set; }

    public virtual Course Course { get; set; } = null!;
    public virtual Student Student { get; set; } = null!;
}
