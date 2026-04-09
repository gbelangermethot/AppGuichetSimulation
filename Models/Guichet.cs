using System;
using System.Collections.Generic;

namespace Projet_Guichet.Models;

public partial class Guichet
{
    public int Id { get; set; }

    public decimal Balance { get; set; }

    public virtual ICollection<OperationAdmin> OperationAdmins { get; set; } = new List<OperationAdmin>();
}
