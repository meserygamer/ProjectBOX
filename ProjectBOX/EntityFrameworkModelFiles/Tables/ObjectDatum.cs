using System;
using System.Collections.Generic;

namespace ProjectBOX.EntityFrameworkModelFiles;

public partial class ObjectDatum
{
    public int ObjectId { get; set; }

    public string ObjectName { get; set; } = null!;

    public string? Description { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<HistoryOfChangesObjectLocation> HistoryOfChangesObjectLocations { get; set; } = new List<HistoryOfChangesObjectLocation>();

    public override string ToString()
    {
        return ObjectName + " (" + ObjectId + ")";
    }
}
