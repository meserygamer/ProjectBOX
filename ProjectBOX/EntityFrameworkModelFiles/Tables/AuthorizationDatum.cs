using System;
using System.Collections.Generic;

namespace ProjectBOX.EntityFrameworkModelFiles;

public partial class AuthorizationDatum
{
    public string Login { get; set; } = null!;

    public int UserId { get; set; }

    public string SecurePasssword { get; set; } = null!;

    public virtual UserDatum User { get; set; } = null!;
}
