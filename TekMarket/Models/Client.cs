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

        public string email { get; set; }

        public string pwd { get; set; }

        public string nom { get; set; }

        public string prenom { get; set; }

        [StringLength(1)]
        public string sexe { get; set; }

        public string tel { get; set; }

        [Column(TypeName = "date")]
        public DateTime? datenais { get; set; }

        public string adresse { get; set; }

        public string ville { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Avi> Avis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Commande> Commandes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Panier> Paniers { get; set; }
    }
}
