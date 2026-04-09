using System;
using System.Collections.Generic;

namespace Projet_Guichet.Models;

public partial class OperationAdmin
{
    public int Id { get; set; }

    public int AdminId { get; set; }

    public int TypeId { get; set; }

    public int GuichetId { get; set; }

    public DateOnly Date { get; set; }

    public string Description { get; set; } = null!;

    public virtual Utilisateur Admin { get; set; } = null!;

    public virtual Guichet Guichet { get; set; } = null!;

    public virtual TypeOperationAdmin Type { get; set; } = null!;
}
