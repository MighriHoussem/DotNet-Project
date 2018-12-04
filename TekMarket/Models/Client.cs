namespace TekMarket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Avis = new HashSet<Avi>();
            Commandes = new HashSet<Commande>();
            Paniers = new HashSet<Panier>();
        }

        public long id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string pwd { get; set; }

        [Required]
        public string nom { get; set; }

        [Required]
        public string prenom { get; set; }

        [Required]
        [StringLength(1)]
        public string sexe { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [MinLength(8)]
        public string tel { get; set; }


        [Required]
        [Column(TypeName = "date")]
        public DateTime? datenais { get; set; }


        [Required]
        public string adresse { get; set; }

        [Required]
        public string ville { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Avi> Avis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Commande> Commandes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Panier> Paniers { get; set; }
    }
}
