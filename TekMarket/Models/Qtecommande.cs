namespace TekMarket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Qtecommande")]
    public partial class Qtecommande
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string refcom { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string refarticle { get; set; }

        public long? qte { get; set; }

        public float? totalprix { get; set; }

        public virtual Article Article { get; set; }

        public virtual Commande Commande { get; set; }
    }
}
