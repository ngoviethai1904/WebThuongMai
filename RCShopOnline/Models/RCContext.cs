using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace RCShopOnline.Models
{
    public class RCContext: DbContext
    {
        public RCContext(): base("RCShopOnline") { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RC> RCs { get; set; }
    }
}