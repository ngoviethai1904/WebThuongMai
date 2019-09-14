using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace RCShopOnline.Models
{
    public class RCDatabaseInitializer : DropCreateDatabaseAlways<RCContext>
    {
        protected override void Seed(RCContext context)
        {
            GetCategories().ForEach(c => context.Categories.Add(c));
            GetRCs().ForEach(p => context.RCs.Add(p));
        }
        private static List<Category> GetCategories()
        {
            var categories = new List<Category>
            {
                new Category
                {
                    CategoryID = 1,
                    CategoryName = "OnRoad"
                },
                new Category
                {
                    CategoryID = 2,
                    CategoryName = "OffRoad"
                },
                new Category
                {
                    CategoryID = 3,
                    CategoryName = "Adventure"
                },
                new Category
                {
                    CategoryID = 4,
                    CategoryName = "Racing"
                }
            };
            return categories;
        }
        private static List<RC> GetRCs()
        {
            var RCs = new List<RC>
            {
                new RC
                {
                    RCID = 1,
                    RCName = "Truck",
                    Description = "Xe tải nhỏ",
                    ImagePath="truck1.jpg",
                    UnitPrice = 179.79f,
                    CategoryID = 1
                },
                new RC
                {
                    RCID = 2,
                    RCName = "Monster Truck",
                    Description = "Xe tải đua",
                    ImagePath="truck2.jpg",
                    UnitPrice = 169.69f,
                    CategoryID = 2
                },
                new RC
                {
                    RCID = 3,
                    RCName = "Car",
                    Description = "Xe con",
                    ImagePath="truck3.jpg",
                    UnitPrice = 168.68f,
                    CategoryID = 2
                },
                new RC
                {
                    RCID = 4,
                    RCName = "Pick up",
                    Description = "Xe bán tải",
                    ImagePath="truck4.jpg",
                    UnitPrice = 196.96f,
                    CategoryID = 3
                },
                new RC
                {
                    RCID = 5,
                    RCName = "Cranes",
                    Description = "Xe cần cẩu",
                    ImagePath="truck5.jpg",
                    UnitPrice = 119.19f,
                    CategoryID = 4
                },
            };
            return RCs;
        }
    }
}