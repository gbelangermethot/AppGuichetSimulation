using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projet_Guichet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "OperationAdmin",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10);

            migrationBuilder.InsertData(
                table: "Compte",
                columns: new[] { "ID", "DateCreation", "Solde", "TypeID", "UtilisateurId" },
                values: new object[,]
                {
                    { 101, new DateOnly(2024, 1, 5), 1250.00m, 1, 100003 },
                    { 102, new DateOnly(2024, 1, 10), 5400.75m, 2, 100003 },
                    { 103, new DateOnly(2024, 1, 12), -450.00m, 4, 100003 },
                    { 109, new DateOnly(2024, 2, 1), 300.00m, 1, 100003 }
                });

            migrationBuilder.UpdateData(
                table: "Guichet",
                keyColumn: "ID",
                keyValue: 1,
                column: "Balance",
                value: 20000.00m);

            migrationBuilder.InsertData(
                table: "OperationAdmin",
                columns: new[] { "ID", "AdminID", "Date", "Description", "GuichetId", "TypeId" },
                values: new object[,]
                {
                    { 801, 100003, new DateOnly(2024, 2, 1), "Ouverture du guichet et vérification initiale", 1, 1 },
                    { 802, 100003, new DateOnly(2024, 2, 5), "Remplissage du guichet : +10 000.00$", 1, 2 },
                    { 803, 100003, new DateOnly(2024, 2, 10), "Blocage de l'utilisateur ID 100005 (Tentatives échouées)", 1, 3 },
                    { 805, 100003, new DateOnly(2024, 3, 1), "Maintenance préventive du lecteur de cartes", 1, 2 },
                    { 807, 100003, new DateOnly(2024, 3, 10), "Création du nouveau compte Chèque pour Utilisateur 100004", 1, 1 },
                    { 808, 100003, new DateOnly(2024, 3, 12), "Déblocage de l'utilisateur ID 100005", 1, 3 },
                    { 809, 100003, new DateOnly(2024, 3, 15), "Remplissage du guichet : +5 000.00$", 1, 2 },
                    { 810, 100003, new DateOnly(2024, 3, 20), "Audit de fin de période effectué sans erreurs", 1, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Description", "Nom" },
                values: new object[] { "Administrateur", "Admin" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Description", "Nom" },
                values: new object[] { "Client", "Utilisateur" });

            migrationBuilder.UpdateData(
                table: "TypeCompte",
                keyColumn: "ID",
                keyValue: 1,
                column: "Description",
                value: "Compte chèque");

            migrationBuilder.UpdateData(
                table: "TypeCompte",
                keyColumn: "ID",
                keyValue: 2,
                column: "Description",
                value: "Compte épargne");

            migrationBuilder.UpdateData(
                table: "TypeCompte",
                keyColumn: "ID",
                keyValue: 3,
                column: "Description",
                value: "Compte Hypothéquaire");

            migrationBuilder.UpdateData(
                table: "TypeCompte",
                keyColumn: "ID",
                keyValue: 4,
                column: "Description",
                value: "Marge de credit");

            migrationBuilder.UpdateData(
                table: "TypeOperationAdmin",
                keyColumn: "ID",
                keyValue: 1,
                column: "Description",
                value: "charger les intérêts a la marge de credit");

            migrationBuilder.UpdateData(
                table: "TypeOperationAdmin",
                keyColumn: "ID",
                keyValue: 2,
                column: "Description",
                value: "Payer les intérêts au comptes epargne");

            migrationBuilder.InsertData(
                table: "TypeOperationAdmin",
                columns: new[] { "ID", "Description", "Nom" },
                values: new object[] { 4, "Prélevement hypothéquaire executé par l'admin", "PrelevementHypothequaire" });

            migrationBuilder.UpdateData(
                table: "TypeTransaction",
                keyColumn: "ID",
                keyValue: 1,
                column: "Description",
                value: "Dépot");

            migrationBuilder.UpdateData(
                table: "TypeTransaction",
                keyColumn: "ID",
                keyValue: 2,
                column: "Description",
                value: "Retrait");

            migrationBuilder.UpdateData(
                table: "TypeTransaction",
                keyColumn: "ID",
                keyValue: 3,
                column: "Description",
                value: "Paiement de facture");

            migrationBuilder.UpdateData(
                table: "TypeTransaction",
                keyColumn: "ID",
                keyValue: 4,
                column: "Description",
                value: "Transfer de fonds a partir du compte");

            migrationBuilder.UpdateData(
                table: "TypeTransaction",
                keyColumn: "ID",
                keyValue: 5,
                column: "Description",
                value: "Transfer de fonds reçu par le compte");

            migrationBuilder.InsertData(
                table: "TypeTransaction",
                columns: new[] { "ID", "Description", "Nom" },
                values: new object[,]
                {
                    { 6, "Intérêts ajoutés au solde du compte", "Interets" },
                    { 7, "Prélevement hypothéquaire fait au compte", "PrelevementHypothequaire" }
                });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "ID", "Courriel", "DateCreation", "NIP", "Nom", "Prenom", "RoleId", "StatusId", "Telephone" },
                values: new object[,]
                {
                    { 100004, "luke.skywalker@gmail.com", new DateOnly(2023, 10, 2), 1234, "Skywalker", "Luke", 2, 1, "1234567890" },
                    { 100005, "han.solo@gmail.com", new DateOnly(1999, 11, 2), 1234, "Solo", "Han", 2, 1, "1234567890" },
                    { 100006, "lando.calrissian@gmail.com", new DateOnly(2009, 11, 16), 1234, "Calrissian", "Lando", 2, 1, "1234567890" },
                    { 100007, "darth.vader@gmail.com", new DateOnly(1993, 4, 3), 1234, "Vader", "Darth", 2, 1, "1234567890" }
                });

            migrationBuilder.InsertData(
                table: "Compte",
                columns: new[] { "ID", "DateCreation", "Solde", "TypeID", "UtilisateurId" },
                values: new object[,]
                {
                    { 104, new DateOnly(2024, 1, 15), 85.20m, 1, 100004 },
                    { 105, new DateOnly(2024, 1, 2), 150000.00m, 3, 100004 },
                    { 106, new DateOnly(2024, 1, 20), 2100.00m, 1, 100005 },
                    { 107, new DateOnly(2024, 1, 22), 10.50m, 2, 100005 },
                    { 108, new DateOnly(2024, 1, 25), -1200.30m, 4, 100005 },
                    { 110, new DateOnly(2024, 2, 5), 0.00m, 2, 100004 }
                });

            migrationBuilder.InsertData(
                table: "OperationAdmin",
                columns: new[] { "ID", "AdminID", "Date", "Description", "GuichetId", "TypeId" },
                values: new object[,]
                {
                    { 804, 100003, new DateOnly(2024, 2, 28), "Calcul et versement des intérêts mensuels (1%)", 1, 4 },
                    { 806, 100003, new DateOnly(2024, 3, 5), "Collecte des intérêts sur marges de crédit (5%)", 1, 4 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "ID", "CompteId", "Date", "Description", "Montant", "TypeID" },
                values: new object[,]
                {
                    { 501, 101, new DateOnly(2024, 2, 1), "Dépôt initial au guichet", 500.00m, 1 },
                    { 502, 101, new DateOnly(2024, 2, 5), "Retrait guichet automatique", -40.00m, 2 },
                    { 503, 102, new DateOnly(2024, 2, 2), "Transfert de fonds externe", 2000.00m, 1 },
                    { 504, 103, new DateOnly(2024, 2, 28), "Frais d'intérêt sur marge (5%)", -22.50m, 6 },
                    { 510, 102, new DateOnly(2024, 2, 28), "Intérêts épargne ajoutés (1%)", 54.01m, 6 },
                    { 511, 101, new DateOnly(2024, 3, 1), "Paiement Vidéotron", -65.00m, 3 },
                    { 514, 109, new DateOnly(2024, 3, 5), "Dépôt initial", 300.00m, 1 },
                    { 515, 103, new DateOnly(2024, 3, 6), "Retrait sur marge", -50.00m, 2 },
                    { 518, 102, new DateOnly(2024, 3, 10), "Retrait vacances", -500.00m, 2 },
                    { 520, 101, new DateOnly(2024, 3, 15), "Frais de service mensuel", -10.00m, 2 },
                    { 505, 104, new DateOnly(2024, 2, 10), "Paiement facture Hydro-Québec", -112.45m, 3 },
                    { 506, 105, new DateOnly(2024, 2, 1), "Versement hypothécaire mensuel", 1000.00m, 1 },
                    { 507, 106, new DateOnly(2024, 2, 12), "Retrait guichet", -100.00m, 2 },
                    { 508, 107, new DateOnly(2024, 2, 15), "Dépôt chèque cadeau", 50.00m, 1 },
                    { 509, 108, new DateOnly(2024, 2, 28), "Intérêts sur découvert", -60.01m, 6 },
                    { 512, 104, new DateOnly(2024, 3, 2), "Retrait guichet rapide", -20.00m, 2 },
                    { 513, 106, new DateOnly(2024, 3, 5), "Dépôt direct Employeur", 1200.00m, 1 },
                    { 516, 110, new DateOnly(2024, 3, 7), "Ouverture de compte", 10.00m, 1 },
                    { 517, 105, new DateOnly(2024, 3, 8), "Paiement taxes municipales", -450.00m, 3 },
                    { 519, 108, new DateOnly(2024, 3, 12), "Remboursement partiel dette", 200.00m, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationAdmin",
                keyColumn: "ID",
                keyValue: 801);

            migrationBuilder.DeleteData(
                table: "OperationAdmin",
                keyColumn: "ID",
                keyValue: 802);

            migrationBuilder.DeleteData(
                table: "OperationAdmin",
                keyColumn: "ID",
                keyValue: 803);

            migrationBuilder.DeleteData(
                table: "OperationAdmin",
                keyColumn: "ID",
                keyValue: 804);

            migrationBuilder.DeleteData(
                table: "OperationAdmin",
                keyColumn: "ID",
                keyValue: 805);

            migrationBuilder.DeleteData(
                table: "OperationAdmin",
                keyColumn: "ID",
                keyValue: 806);

            migrationBuilder.DeleteData(
                table: "OperationAdmin",
                keyColumn: "ID",
                keyValue: 807);

            migrationBuilder.DeleteData(
                table: "OperationAdmin",
                keyColumn: "ID",
                keyValue: 808);

            migrationBuilder.DeleteData(
                table: "OperationAdmin",
                keyColumn: "ID",
                keyValue: 809);

            migrationBuilder.DeleteData(
                table: "OperationAdmin",
                keyColumn: "ID",
                keyValue: 810);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 503);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 505);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 506);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 507);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 508);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 509);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 510);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 511);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 512);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 513);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 514);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 515);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 516);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 517);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 518);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 519);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "ID",
                keyValue: 520);

            migrationBuilder.DeleteData(
                table: "TypeTransaction",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Utilisateurs",
                keyColumn: "ID",
                keyValue: 100006);

            migrationBuilder.DeleteData(
                table: "Utilisateurs",
                keyColumn: "ID",
                keyValue: 100007);

            migrationBuilder.DeleteData(
                table: "Compte",
                keyColumn: "ID",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Compte",
                keyColumn: "ID",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Compte",
                keyColumn: "ID",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Compte",
                keyColumn: "ID",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Compte",
                keyColumn: "ID",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Compte",
                keyColumn: "ID",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Compte",
                keyColumn: "ID",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Compte",
                keyColumn: "ID",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Compte",
                keyColumn: "ID",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Compte",
                keyColumn: "ID",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "TypeOperationAdmin",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TypeTransaction",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Utilisateurs",
                keyColumn: "ID",
                keyValue: 100004);

            migrationBuilder.DeleteData(
                table: "Utilisateurs",
                keyColumn: "ID",
                keyValue: 100005);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "OperationAdmin",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Guichet",
                keyColumn: "ID",
                keyValue: 1,
                column: "Balance",
                value: 19239.1000m);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Description", "Nom" },
                values: new object[] { "Utilisateur client du guichet", "Utilisateur" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Description", "Nom" },
                values: new object[] { "Administrateur du guichet", "Admin" });

            migrationBuilder.UpdateData(
                table: "TypeCompte",
                keyColumn: "ID",
                keyValue: 1,
                column: "Description",
                value: "Compte cheque du client");

            migrationBuilder.UpdateData(
                table: "TypeCompte",
                keyColumn: "ID",
                keyValue: 2,
                column: "Description",
                value: "Compte epargne du client");

            migrationBuilder.UpdateData(
                table: "TypeCompte",
                keyColumn: "ID",
                keyValue: 3,
                column: "Description",
                value: "Compte Hypothequaire du client");

            migrationBuilder.UpdateData(
                table: "TypeCompte",
                keyColumn: "ID",
                keyValue: 4,
                column: "Description",
                value: "Marge de credit du client");

            migrationBuilder.UpdateData(
                table: "TypeOperationAdmin",
                keyColumn: "ID",
                keyValue: 1,
                column: "Description",
                value: "charger les interets a la marge de credit");

            migrationBuilder.UpdateData(
                table: "TypeOperationAdmin",
                keyColumn: "ID",
                keyValue: 2,
                column: "Description",
                value: "Payer les interets au comptes epargne");

            migrationBuilder.UpdateData(
                table: "TypeTransaction",
                keyColumn: "ID",
                keyValue: 1,
                column: "Description",
                value: "Depot d'argent dans le compte");

            migrationBuilder.UpdateData(
                table: "TypeTransaction",
                keyColumn: "ID",
                keyValue: 2,
                column: "Description",
                value: "Retrait d'argent dans le compte");

            migrationBuilder.UpdateData(
                table: "TypeTransaction",
                keyColumn: "ID",
                keyValue: 3,
                column: "Description",
                value: "Paiement de facture dans le compte");

            migrationBuilder.UpdateData(
                table: "TypeTransaction",
                keyColumn: "ID",
                keyValue: 4,
                column: "Description",
                value: "Transfer de fonds a partir de ce compte");

            migrationBuilder.UpdateData(
                table: "TypeTransaction",
                keyColumn: "ID",
                keyValue: 5,
                column: "Description",
                value: "Transfer de fonds a partir de ce compte");
        }
    }
}
