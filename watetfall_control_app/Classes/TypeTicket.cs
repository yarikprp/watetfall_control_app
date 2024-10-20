﻿using System;
using System.Collections.Generic;

namespace watetfall_control_app.Classes;

public partial class TypeTicket
{
    public int IdType { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<Visiter> Visiters { get; set; } = new List<Visiter>();
}
