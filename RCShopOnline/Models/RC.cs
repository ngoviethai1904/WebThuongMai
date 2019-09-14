using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RCShopOnline.Models
{
    public class RC
    {
        [ScaffoldColumn(false)]
        public int RCID { get; set; }
        [Required, StringLength(100), Display(Name = "Name")]
        public string RCName { get; set; }
        [Required, StringLength(1000), Display(Name = "RC Description"),
        DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string ImagePath { get; set; }
        [Display(Name = "Price")]
        public float? UnitPrice { get; set; }
        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}