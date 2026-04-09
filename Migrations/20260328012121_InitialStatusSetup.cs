using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projet_Guichet.Migrations
{
    /// <inheritdoc />
    public partial class InitialStatusSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guichet",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guichet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeCompte",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeCompte", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeOperationAdmin",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOperationAdmin", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeTransaction",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTransaction", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIP = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Nom = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Prenom = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Telephone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Courriel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DateCreation = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Roles",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Compte",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    Solde = table.Column<decimal>(type: "money", nullable: false),
                    DateCreation = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compte", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Compte_TypeCompte",
                        column: x => x.TypeID,
                        principalTable: "TypeCompte",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Compte_Utilisateurs2",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OperationAdmin",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminID = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    GuichetId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationAdmin", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OperationAdmin_Guichet",
                        column: x => x.GuichetId,
                        principalTable: "Guichet",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_OperationAdmin_TypeOperationAdmin",
                        column: x => x.TypeId,
                        principalTable: "TypeOperationAdmin",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_OperationAdmin_Utilisateurs",
                        column: x => x.AdminID,
                        principalTable: "Utilisateurs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompteId = table.Column<int>(type: "int", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    Montant = table.Column<decimal>(type: "money", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transactions_Compte",
                        column: x => x.CompteId,
                        principalTable: "Compte",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Transactions_TypeTransaction",
                        column: x => x.TypeID,
                        principalTable: "TypeTransaction",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "Guichet",
                columns: new[] { "ID", "Balance" },
                values: new object[] { 1, 19239.1000m });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ID", "Description", "Nom" },
                values: new object[,]
                {
                    { 1, "Utilisateur client du guichet", "Utilisateur" },
                    { 2, "Administrateur du guichet", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "ID", "Description", "Nom" },
                values: new object[,]
                {
                    { 1, "Utilisateur Actif", "Actif" },
                    { 2, "Utilisateur Bloque", "Bloque" },
                    { 3, "Utilisateur Inactif", "Inactif" },
                    { 4, "Utilisateur desactive", "Desactive" }
                });

            migrationBuilder.InsertData(
                table: "TypeCompte",
                columns: new[] { "ID", "Description", "Nom" },
                values: new object[,]
                {
                    { 1, "Compte cheque du client", "Cheque" },
                    { 2, "Compte epargne du client", "Epargne" },
                    { 3, "Compte Hypothequaire du client", "Hypothequaire" },
                    { 4, "Marge de credit du client", "MargeCredit" }
                });

            migrationBuilder.InsertData(
                table: "TypeOperationAdmin",
                columns: new[] { "ID", "Description", "Nom" },
                values: new object[,]
                {
                    { 1, "charger les interets a la marge de credit", "InteretsCredit" },
                    { 2, "Payer les interets au comptes epargne", "InteretsEpargne" },
                    { 3, "Ajouter de la monaie au guichet", "RemplirGuichet" }
                });

            migrationBuilder.InsertData(
                table: "TypeTransaction",
                columns: new[] { "ID", "Description", "Nom" },
                values: new object[,]
                {
                    { 1, "Depot d'argent dans le compte", "Depot" },
                    { 2, "Retrait d'argent dans le compte", "Retrait" },
                    { 3, "Paiement de facture dans le compte", "PaimentFacture" },
                    { 4, "Transfer de fonds a partir de ce compte", "TransferEnvoye" },
                    { 5, "Transfer de fonds a partir de ce compte", "TransferRecu" }
                });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "ID", "Courriel", "DateCreation", "NIP", "Nom", "Prenom", "RoleId", "StatusId", "Telephone" },
                values: new object[] { 100003, "leia.organa@gmail.com", new DateOnly(2024, 1, 2), 1234, "Organa", "Leia", 1, 1, "1234567890" });

            migrationBuilder.CreateIndex(
                name: "IX_Compte_TypeID",
                table: "Compte",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Compte_UtilisateurId",
                table: "Compte",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationAdmin_AdminID",
                table: "OperationAdmin",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_OperationAdmin_GuichetId",
                table: "OperationAdmin",
                column: "GuichetId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationAdmin_TypeId",
                table: "OperationAdmin",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CompteId",
                table: "Transactions",
                column: "CompteId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TypeID",
                table: "Transactions",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_RoleId",
                table: "Utilisateurs",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_StatusId",
                table: "Utilisateurs",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationAdmin");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Guichet");

            migrationBuilder.DropTable(
                name: "TypeOperationAdmin");

            migrationBuilder.DropTable(
                name: "Compte");

            migrationBuilder.DropTable(
                name: "TypeTransaction");

            migrationBuilder.DropTable(
                name: "TypeCompte");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
