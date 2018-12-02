namespace TekMarket.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel1")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Avi> Avis { get; set; }
        public virtual DbSet<Categorie> Categories { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Commande> Commandes { get; set; }
        public virtual DbSet<Panier> Paniers { get; set; }
        public virtual DbSet<Qtecommande> Qtecommandes { get; set; }
        public virtual DbSet<Qtepanier> Qtepaniers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.pwd)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.nom)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.cin)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.fonction)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.refarticle)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.libelle)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.couleurdispo)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.idcategorie)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.Qtecommandes)
                .WithRequired(e => e.Article)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.Qtepaniers)
                .WithRequired(e => e.Article)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Avi>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<Avi>()
                .Property(e => e.commentaire)
                .IsUnicode(false);

            modelBuilder.Entity<Avi>()
                .Property(e => e.refarticle)
                .IsUnicode(false);

            modelBuilder.Entity<Categorie>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<Categorie>()
                .Property(e => e.libelle)
                .IsUnicode(false);

            modelBuilder.Entity<Categorie>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Categorie>()
                .HasMany(e => e.Articles)
                .WithOptional(e => e.Categorie)
                .HasForeignKey(e => e.idcategorie);

            modelBuilder.Entity<Client>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.pwd)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.nom)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.sexe)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.tel)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.adresse)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.ville)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Avis)
                .WithOptional(e => e.Client)
                .HasForeignKey(e => e.idutilisateur);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Commandes)
                .WithOptional(e => e.Client)
                .HasForeignKey(e => e.idutilisateur);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Paniers)
                .WithOptional(e => e.Client)
                .HasForeignKey(e => e.id_utilisateur);

            modelBuilder.Entity<Commande>()
                .Property(e => e.refcomm)
                .IsUnicode(false);

            modelBuilder.Entity<Commande>()
                .HasMany(e => e.Qtecommandes)
                .WithRequired(e => e.Commande)
                .HasForeignKey(e => e.refcom)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Panier>()
                .HasMany(e => e.Qtepaniers)
                .WithRequired(e => e.Panier)
                .HasForeignKey(e => e.idpanier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Qtecommande>()
                .Property(e => e.refcom)
                .IsUnicode(false);

            modelBuilder.Entity<Qtecommande>()
                .Property(e => e.refarticle)
                .IsUnicode(false);

            modelBuilder.Entity<Qtepanier>()
                .Property(e => e.refarticle)
                .IsUnicode(false);
        }
    }
}
