using System;
using System.Collections.Generic;

namespace Projet_Guichet.Models;

public partial class TypeOperationAdmin
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<OperationAdmin> OperationAdmins { get; set; } = new List<OperationAdmin>();
}
