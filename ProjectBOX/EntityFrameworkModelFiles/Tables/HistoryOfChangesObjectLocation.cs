using System;
using System.Collections.Generic;

namespace ProjectBOX.EntityFrameworkModelFiles;

public partial class HistoryOfChangesObjectLocation
{
    public int ChangesObjectLocationId { get; set; }

    public int UserId { get; set; }

    public int ObjectId { get; set; }

    public int ContainerId { get; set; }

    public string? Description { get; set; }

    public DateTime ChangesDate { get; set; }

    public virtual ContainerDatum Container { get; set; } = null!;

    public virtual ObjectDatum Object { get; set; } = null!;

    public virtual UserDatum User { get; set; } = null!;
}
