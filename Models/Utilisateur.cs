using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Projet_Guichet.Models;

public partial class Utilisateur
{
    public int Nip { get; set; }

    public int RoleId { get; set; }

    public int StatusId { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string Courriel { get; set; } = null!;

    public DateOnly DateCreation { get; set; }

    public int Id { get; set; }

    public string FullDisplayUser => $"{Nom}, {Prenom} (ID: {Id})";

    public virtual ICollection<Compte> Comptes { get; set; } = new List<Compte>();

    public virtual ICollection<OperationAdmin> OperationAdmins { get; set; } = new List<OperationAdmin>();

    public virtual Role Role { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public override string ToString()
    {
        return $"  {Id},  {Nom}, {Prenom} ";
    }
}
