using System;
using System.Collections.Generic;

namespace Projet_Guichet.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int CompteId { get; set; }

    public int TypeId { get; set; }

    public decimal Montant { get; set; }

    public DateOnly Date { get; set; }

    public string Description { get; set; } = null!;

    public virtual Compte Compte { get; set; } = null!;

    public virtual TypeTransaction Type { get; set; } = null!;
    public override string ToString()
    {
        return $"  ID: {Id} date: {Date:yyyy-MM-dd} - {Type?.Description ?? "Inconnu"} ({Montant:C2})";
    }
}
