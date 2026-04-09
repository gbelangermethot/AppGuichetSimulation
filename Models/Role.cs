using System;
using System.Collections.Generic;

namespace Projet_Guichet.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
}
