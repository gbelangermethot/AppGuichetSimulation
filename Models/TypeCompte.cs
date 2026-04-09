using System;
using System.Collections.Generic;

namespace Projet_Guichet.Models;

public partial class TypeCompte
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Compte> Comptes { get; set; } = new List<Compte>();
}
