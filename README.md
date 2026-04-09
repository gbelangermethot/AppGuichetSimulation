# AppGuichetSimulation
Simulation d'une application banquaire/guichet automatique.

## Fonctionalités
- Login client ou Admin
- Opérations banquaires variées (dépot, retrait, paiement de facture, transfer de fonds)
- Visualisation des transactions
- marge de crédit qui peux couvrir les découverts
- creation de clients et de compte
- gestion de l'argent disponible dans le guichet

## Technologies utilisées
- C#, Microsoft SQL server
- Entity framework core

## Installation
- Avoir la version developpeur de microsoft SQl server et acces au serveur local par defaut .
- ouvrir le projet dans visual studio.
- Ouvrir la console NuGet de l'onglet tools ou outils -> NuGet Package Manager -> Package manager console.
- Executer la commande: EntityFrameworkCore\Add-Migration UpdateModels.
- Puis executer la commande: EntityFrameworkCore\Update-Database.

## Usage
- Se connecter a l'utilisateur 100003 avec le NIP 1234 ouvre l'interface admin.
- de l'interface admin l'utilisateur peut creer des utilisateur clients et des comptes pour les utilisateur clients ainsi que executer d'autre opérations.
- de l'interface client, l'utilisateur peu faire des opération banquaires sur ses différents comptes. 

## Ce que j'ai appris
- Implémenter Entity framework Core pour que l'app communique avec la base de données.
- Faire des seeds de base dans le modele de données and le fichier context.
- Migrer l'app er generer la base de données a partir du modèle dans le fichier context.
- faire des controles personalisé réutilisables comme des boutton aux coins arrondis et un clafier numerique cliquable.
- J'ai améliorer ma conaissance de Entity framework core.
- J'ai améliorer ma connaissance des application WPF et leur mise en page.

## Amélioration futures.
- Je pourrais ajouter d'autres type de comptes, d'autres type de transaction ou d'utilisateur.
- je pourrais avoir un code normalisé pour l'utilisation du clavier numérique.
- Je pourrait améliorer la mise en page pour une taille de page spécifique. Pour l'instant elle est reactive et cet app en a pas vraiment besoin.
- Je pourrait encripter les données comme le NIP dans la base de données.
