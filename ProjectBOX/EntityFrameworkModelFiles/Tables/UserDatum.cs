using System;
using System.Collections.Generic;

namespace ProjectBOX.EntityFrameworkModelFiles;

public partial class UserDatum
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telephone { get; set; }

    public virtual AuthorizationDatum? AuthorizationDatum { get; set; }

    public virtual ICollection<HistoryOfChangesObjectLocation> HistoryOfChangesObjectLocations { get; set; } = new List<HistoryOfChangesObjectLocation>();
}
