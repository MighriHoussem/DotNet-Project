namespace TekMarket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Commande")]
    public partial class Commande
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Commande()
        {
            Qtecommandes = new HashSet<Qtecommande>();
        }

        [Key]
        [StringLength(50)]
        public string refcomm { get; set; }

        [Column(TypeName = "date")]
        public DateTime? datecom { get; set; }

        public float? totalprix { get; set; }

        public long? idutilisateur { get; set; }

        public virtual Client Client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Qtecommande> Qtecommandes { get; set; }
    }
}
