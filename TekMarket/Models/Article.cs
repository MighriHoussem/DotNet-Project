namespace TekMarket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Article")]
    public partial class Article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Article()
        {
            Avis = new HashSet<Avi>();
            Qtecommandes = new HashSet<Qtecommande>();
            Qtepaniers = new HashSet<Qtepanier>();
        }

        [Key]
        [StringLength(50)]
        [Required]
        public string refarticle { get; set; }

        [Required]
        public string libelle { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string description { get; set; }


        [Required]
        public float? prix { get; set; }

        [Column(TypeName = "text")]
        public string image { get; set; }

        [Required]
        public long? qtedisponible { get; set; }


        [Column(TypeName = "text")]
        public string couleurdispo { get; set; }

        [StringLength(50)]
        public string idcategorie { get; set; }

        public virtual Categorie Categorie { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Avi> Avis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Qtecommande> Qtecommandes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Qtepanier> Qtepaniers { get; set; }
    }
}
