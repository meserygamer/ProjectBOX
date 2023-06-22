using System;
using System.Collections.Generic;

namespace ProjectBOX.EntityFrameworkModelFiles;

public partial class CompleteTask
{
    public int ObjectId { get; set; }

    public string ObjectName { get; set; } = null!;

    public string? ObjectDataDescription { get; set; }

    public string ContainerName { get; set; } = null!;

    public string? HistoryOfChangesObjectLocationDescription { get; set; }

    public string UserName { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime ChangesDate { get; set; }
}
