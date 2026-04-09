using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Projet_Guichet.Models;

public partial class GuichetContext : DbContext
{
    public GuichetContext()
    {
    }

    public GuichetContext(DbContextOptions<GuichetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Compte> Comptes { get; set; }

    public virtual DbSet<Guichet> Guichets { get; set; }

    public virtual DbSet<OperationAdmin> OperationAdmins { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TypeCompte> TypeComptes { get; set; }

    public virtual DbSet<TypeOperationAdmin> TypeOperationAdmins { get; set; }

    public virtual DbSet<TypeTransaction> TypeTransactions { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Guichet;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Compte>(entity =>
        {
            entity.ToTable("Compte");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Solde).HasColumnType("money");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Type).WithMany(p => p.Comptes)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compte_TypeCompte");

            entity.HasOne(d => d.Utilisateur).WithMany(p => p.Comptes)
                .HasForeignKey(d => d.UtilisateurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compte_Utilisateurs2");

            //seed data pour compte
            entity.HasData(
                new Compte { Id = 101, UtilisateurId = 100003, TypeId = 1, Solde = 1250.00m, DateCreation = new DateOnly(2024, 1, 5) },
                new Compte { Id = 102, UtilisateurId = 100003, TypeId = 2, Solde = 5400.75m, DateCreation = new DateOnly(2024, 1, 10) },
                new Compte { Id = 103, UtilisateurId = 100003, TypeId = 4, Solde = -450.00m, DateCreation = new DateOnly(2024, 1, 12) }, 
                new Compte { Id = 104, UtilisateurId = 100004, TypeId = 1, Solde = 85.20m, DateCreation = new DateOnly(2024, 1, 15) },
                new Compte { Id = 105, UtilisateurId = 100004, TypeId = 3, Solde = 150000.00m, DateCreation = new DateOnly(2024, 1, 2) }, 
                new Compte { Id = 106, UtilisateurId = 100005, TypeId = 1, Solde = 2100.00m, DateCreation = new DateOnly(2024, 1, 20) },
                new Compte { Id = 107, UtilisateurId = 100005, TypeId = 2, Solde = 10.50m, DateCreation = new DateOnly(2024, 1, 22) },
                new Compte { Id = 108, UtilisateurId = 100005, TypeId = 4, Solde = -1200.30m, DateCreation = new DateOnly(2024, 1, 25) }, 
                new Compte { Id = 109, UtilisateurId = 100003, TypeId = 1, Solde = 300.00m, DateCreation = new DateOnly(2024, 2, 1) },
                new Compte { Id = 110, UtilisateurId = 100006, TypeId = 1, Solde = 300.00m, DateCreation = new DateOnly(2024, 2, 1) },
                new Compte { Id = 111, UtilisateurId = 100007, TypeId = 1, Solde = 300.00m, DateCreation = new DateOnly(2024, 2, 1) },
                new Compte { Id = 112, UtilisateurId = 100004, TypeId = 2, Solde = 0.00m, DateCreation = new DateOnly(2024, 2, 5) }
            );
        });

        modelBuilder.Entity<Guichet>(entity =>
        {
            entity.ToTable("Guichet");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Balance).HasColumnType("money");

            // Seed Data pour Guichet
            entity.HasData(new Guichet { Id = 1, Balance = 20000.00m });
        });

        modelBuilder.Entity<OperationAdmin>(entity =>
        {
            entity.ToTable("OperationAdmin");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.Description).HasColumnType("text");

            entity.HasOne(d => d.Admin).WithMany(p => p.OperationAdmins)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OperationAdmin_Utilisateurs");

            entity.HasOne(d => d.Guichet).WithMany(p => p.OperationAdmins)
                .HasForeignKey(d => d.GuichetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OperationAdmin_Guichet");

            entity.HasOne(d => d.Type).WithMany(p => p.OperationAdmins)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OperationAdmin_TypeOperationAdmin");

            //seed data pour operationAdmin
            entity.HasData(
                new OperationAdmin { Id = 801, AdminId = 100003, TypeId = 1, GuichetId = 1, Date = new DateOnly(2024, 2, 1), Description = "Ouverture du guichet et vérification initiale" },
                new OperationAdmin { Id = 802, AdminId = 100003, TypeId = 2, GuichetId = 1, Date = new DateOnly(2024, 2, 5), Description = "Remplissage du guichet : +10 000.00$" },
                new OperationAdmin { Id = 803, AdminId = 100003, TypeId = 3, GuichetId = 1, Date = new DateOnly(2024, 2, 10), Description = "Blocage de l'utilisateur ID 100005 (Tentatives échouées)" },
                new OperationAdmin { Id = 804, AdminId = 100003, TypeId = 4, GuichetId = 1, Date = new DateOnly(2024, 2, 28), Description = "Calcul et versement des intérêts mensuels (1%)" },
                new OperationAdmin { Id = 805, AdminId = 100003, TypeId = 2, GuichetId = 1, Date = new DateOnly(2024, 3, 1), Description = "Maintenance préventive du lecteur de cartes" },
                new OperationAdmin { Id = 806, AdminId = 100003, TypeId = 4, GuichetId = 1, Date = new DateOnly(2024, 3, 5), Description = "Collecte des intérêts sur marges de crédit (5%)" },
                new OperationAdmin { Id = 807, AdminId = 100003, TypeId = 1, GuichetId = 1, Date = new DateOnly(2024, 3, 10), Description = "Création du nouveau compte Chèque pour Utilisateur 100004" },
                new OperationAdmin { Id = 808, AdminId = 100003, TypeId = 3, GuichetId = 1, Date = new DateOnly(2024, 3, 12), Description = "Déblocage de l'utilisateur ID 100005" },
                new OperationAdmin { Id = 809, AdminId = 100003, TypeId = 2, GuichetId = 1, Date = new DateOnly(2024, 3, 15), Description = "Remplissage du guichet : +5 000.00$" },
                new OperationAdmin { Id = 810, AdminId = 100003, TypeId = 1, GuichetId = 1, Date = new DateOnly(2024, 3, 20), Description = "Audit de fin de période effectué sans erreurs" }
            );
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Nom).HasMaxLength(50).IsUnicode(false);

            // Seed Data for Roles
            entity.HasData(
                new Role { Id = 1, Nom = "Admin", Description = "Administrateur" },
                new Role { Id = 2, Nom = "Utilisateur", Description = "Client" }
            );
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Nom).HasMaxLength(50).IsUnicode(false);

            // Seed Data for Roles
            entity.HasData(
                new Status { Id = 1, Nom = "Actif", Description = "Utilisateur Actif" },
                new Status { Id = 2, Nom = "Bloque", Description = "Utilisateur Bloque" },
                new Status { Id = 3, Nom = "Inactif", Description = "Utilisateur Inactif" },
                new Status { Id = 4, Nom = "Desactive", Description = "Utilisateur desactive" }
            );
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Montant).HasColumnType("money");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Compte).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CompteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Compte");

            entity.HasOne(d => d.Type).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_TypeTransaction");

            //seed date pour les transactions
            entity.HasData(
                new Transaction { Id = 501, CompteId = 101, TypeId = 1, Montant = 500.00m, Date = new DateOnly(2024, 2, 1), Description = "Dépôt initial au guichet" },
                new Transaction { Id = 502, CompteId = 101, TypeId = 2, Montant = -40.00m, Date = new DateOnly(2024, 2, 5), Description = "Retrait guichet automatique" },
                new Transaction { Id = 503, CompteId = 102, TypeId = 1, Montant = 2000.00m, Date = new DateOnly(2024, 2, 2), Description = "Transfert de fonds externe" },
                new Transaction { Id = 504, CompteId = 103, TypeId = 6, Montant = -22.50m, Date = new DateOnly(2024, 2, 28), Description = "Frais d'intérêt sur marge (5%)" },
                new Transaction { Id = 505, CompteId = 104, TypeId = 3, Montant = -112.45m, Date = new DateOnly(2024, 2, 10), Description = "Paiement facture Hydro-Québec" },
                new Transaction { Id = 506, CompteId = 105, TypeId = 1, Montant = 1000.00m, Date = new DateOnly(2024, 2, 1), Description = "Versement hypothécaire mensuel" },
                new Transaction { Id = 507, CompteId = 106, TypeId = 2, Montant = -100.00m, Date = new DateOnly(2024, 2, 12), Description = "Retrait guichet" },
                new Transaction { Id = 508, CompteId = 107, TypeId = 1, Montant = 50.00m, Date = new DateOnly(2024, 2, 15), Description = "Dépôt chèque cadeau" },
                new Transaction { Id = 509, CompteId = 108, TypeId = 6, Montant = -60.01m, Date = new DateOnly(2024, 2, 28), Description = "Intérêts sur découvert" },
                new Transaction { Id = 510, CompteId = 102, TypeId = 6, Montant = 54.01m, Date = new DateOnly(2024, 2, 28), Description = "Intérêts épargne ajoutés (1%)" },
                new Transaction { Id = 511, CompteId = 101, TypeId = 3, Montant = -65.00m, Date = new DateOnly(2024, 3, 1), Description = "Paiement Vidéotron" },
                new Transaction { Id = 512, CompteId = 104, TypeId = 2, Montant = -20.00m, Date = new DateOnly(2024, 3, 2), Description = "Retrait guichet rapide" },
                new Transaction { Id = 513, CompteId = 106, TypeId = 1, Montant = 1200.00m, Date = new DateOnly(2024, 3, 5), Description = "Dépôt direct Employeur" },
                new Transaction { Id = 514, CompteId = 109, TypeId = 1, Montant = 300.00m, Date = new DateOnly(2024, 3, 5), Description = "Dépôt initial" },
                new Transaction { Id = 515, CompteId = 103, TypeId = 2, Montant = -50.00m, Date = new DateOnly(2024, 3, 6), Description = "Retrait sur marge" },
                new Transaction { Id = 516, CompteId = 110, TypeId = 1, Montant = 10.00m, Date = new DateOnly(2024, 3, 7), Description = "Ouverture de compte" },
                new Transaction { Id = 517, CompteId = 105, TypeId = 3, Montant = -450.00m, Date = new DateOnly(2024, 3, 8), Description = "Paiement taxes municipales" },
                new Transaction { Id = 518, CompteId = 102, TypeId = 2, Montant = -500.00m, Date = new DateOnly(2024, 3, 10), Description = "Retrait vacances" },
                new Transaction { Id = 519, CompteId = 108, TypeId = 1, Montant = 200.00m, Date = new DateOnly(2024, 3, 12), Description = "Remboursement partiel dette" },
                new Transaction { Id = 520, CompteId = 101, TypeId = 2, Montant = -10.00m, Date = new DateOnly(2024, 3, 15), Description = "Frais de service mensuel" }
            );

        });

        modelBuilder.Entity<TypeCompte>(entity =>
        {
            entity.ToTable("TypeCompte");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.Nom).HasMaxLength(50).IsUnicode(false);

            // Seed Data for TypeCompte
            entity.HasData(
                new TypeCompte { Id = 1, Nom = "Cheque", Description = "Compte chèque" },
                new TypeCompte { Id = 2, Nom = "Epargne", Description = "Compte épargne" },
                new TypeCompte { Id = 3, Nom = "Hypothequaire", Description = "Compte Hypothéquaire" },
                new TypeCompte { Id = 4, Nom = "MargeCredit", Description = "Marge de credit" }
            );
        });

        modelBuilder.Entity<TypeOperationAdmin>(entity =>
        {
            entity.ToTable("TypeOperationAdmin");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.Nom).HasMaxLength(50).IsUnicode(false);

            // Seed Data for TypeOperationAdmin
            entity.HasData(
                new TypeOperationAdmin { Id = 1, Nom = "InteretsCredit", Description = "charger les intérêts a la marge de credit" },
                new TypeOperationAdmin { Id = 2, Nom = "InteretsEpargne", Description = "Payer les intérêts au comptes epargne" },
                new TypeOperationAdmin { Id = 3, Nom = "RemplirGuichet", Description = "Ajouter de la monaie au guichet" },
                new TypeOperationAdmin { Id = 4, Nom = "PrelevementHypothequaire", Description = "Prélevement hypothéquaire executé par l'admin" }
            );
        });

        modelBuilder.Entity<TypeTransaction>(entity =>
        {
            entity.ToTable("TypeTransaction");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Nom).HasMaxLength(50).IsUnicode(false);

            // Seed Data for TypeTransaction
            entity.HasData(
                new TypeTransaction { Id = 1, Nom = "Depot", Description = "Dépot" },
                new TypeTransaction { Id = 2, Nom = "Retrait", Description = "Retrait" },
                new TypeTransaction { Id = 3, Nom = "PaimentFacture", Description = "Paiement de facture" },
                new TypeTransaction { Id = 4, Nom = "TransferEnvoye", Description = "Transfer de fonds a partir du compte" },
                new TypeTransaction { Id = 5, Nom = "TransferRecu", Description = "Transfer de fonds reçu par le compte" },
                new TypeTransaction { Id = 6, Nom = "Interets", Description = "Intérêts ajoutés au solde du compte" },
                new TypeTransaction { Id = 7, Nom = "PrelevementHypothequaire", Description = "Prélevement hypothéquaire fait au compte" }
            );
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Courriel).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.Nip).HasColumnName("NIP");
            entity.Property(e => e.Nom).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.Prenom).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.Telephone).HasMaxLength(50).IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Utilisateurs)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Utilisateurs_Roles");
            entity.HasOne(d => d.Status).WithMany(p => p.Utilisateurs)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Utilisateurs_Status");

            
            entity.HasData(new Utilisateur
            {
                Id = 100003,
                Nip = 1234,
                RoleId = 1,
                StatusId = 1,
                Nom = "Organa",
                Prenom = "Leia",
                Telephone = "1234567890",
                Courriel = "leia.organa@gmail.com",
                DateCreation = new DateOnly(2024, 1, 2)
            });

            entity.HasData(new Utilisateur
            {
                Id = 100004,
                Nip = 1234,
                RoleId = 2,
                StatusId = 1,
                Nom = "Skywalker",
                Prenom = "Luke",
                Telephone = "1234567890",
                Courriel = "luke.skywalker@gmail.com",
                DateCreation = new DateOnly(2023, 10, 2)
            });

            entity.HasData(new Utilisateur
            {
                Id = 100005,
                Nip = 1234,
                RoleId = 2,
                StatusId = 1,
                Nom = "Solo",
                Prenom = "Han",
                Telephone = "1234567890",
                Courriel = "han.solo@gmail.com",
                DateCreation = new DateOnly(1999, 11, 2)
            });

            entity.HasData(new Utilisateur
            {
                Id = 100006,
                Nip = 1234,
                RoleId = 2,
                StatusId = 1,
                Nom = "Calrissian",
                Prenom = "Lando",
                Telephone = "1234567890",
                Courriel = "lando.calrissian@gmail.com",
                DateCreation = new DateOnly(2009, 11, 16)
            });

            entity.HasData(new Utilisateur
            {
                Id = 100007,
                Nip = 1234,
                RoleId = 2,
                StatusId = 1,
                Nom = "Vader",
                Prenom = "Darth",
                Telephone = "1234567890",
                Courriel = "darth.vader@gmail.com",
                DateCreation = new DateOnly(1993, 4, 3)
            });
        });

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
