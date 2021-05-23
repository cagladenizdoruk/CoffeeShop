using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoffeeShop.Models.Model
{
    [Table("About")]
    public class About
    {
        [Key]
        public int AboutId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}