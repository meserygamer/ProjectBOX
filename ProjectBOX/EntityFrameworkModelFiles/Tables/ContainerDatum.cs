using System;
using System.Collections.Generic;

namespace ProjectBOX.EntityFrameworkModelFiles;

public partial class ContainerDatum
{
    public int ContainerId { get; set; }

    public string ContainerName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<HistoryOfChangesObjectLocation> HistoryOfChangesObjectLocations { get; set; } = new List<HistoryOfChangesObjectLocation>();
}
