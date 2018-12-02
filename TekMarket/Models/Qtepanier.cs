namespace TekMarket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Qtepanier")]
    public partial class Qtepanier
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long idpanier { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string refarticle { get; set; }

        public long? qte { get; set; }

        public float? montant { get; set; }

        public virtual Article Article { get; set; }

        public virtual Panier Panier { get; set; }
    }
}
