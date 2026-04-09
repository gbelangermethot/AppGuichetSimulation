using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_Guichet.Models
{
    public partial class Status
    {
        public int Id { get; set; }

        public string Nom { get; set; } = null!;

        public string Description { get; set; } = null!;

        public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
    }
}
