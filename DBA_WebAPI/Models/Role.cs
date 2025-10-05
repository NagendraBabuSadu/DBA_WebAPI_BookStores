using System;
using System.Collections.Generic;

namespace DBA_WebAPI.Models;

public partial class Role
{
    public short RoleId { get; set; }

    public string RoleDesc { get; set; } = null!;
}
