using System.Text;
using System.Web.UI.WebControls;

namespace TekMarket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {

        public long id { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        
        public string email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string pwd { get; set; }


        public string nom { get; set; }

        public string prenom { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(8)]
        public string cin { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string fonction { get; set; }

        
        
    }


    




    
}
