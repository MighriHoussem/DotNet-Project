namespace TekMarket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Avi
    {
        [StringLength(50)]
        public string id { get; set; }

        [Column(TypeName = "text")]
        public string commentaire { get; set; }

        public int? note { get; set; }

        public long? idutilisateur { get; set; }

        [StringLength(50)]
        public string refarticle { get; set; }

        public virtual Article Article { get; set; }

        public virtual Client Client { get; set; }
    }
}
