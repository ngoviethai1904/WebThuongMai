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
                    CategoryName = "Xe điều khiển từ xa chạy bằng pin"
                },
                new Category
                {
                    CategoryID = 2,
                    CategoryName = "Xe điều khiển từ xa chạy xăng nitro"
                },
                new Category
                {
                    CategoryID = 3,
                    CategoryName = " Xe điều khiển từ xa vượt địa hình"
                },
                new Category
                {
                    CategoryID = 4,
                    CategoryName = "Xe điều khiển từ xa tốc độ cao"
                },
                new Category
                {
                     CategoryID = 5,
                     CategoryName = "Xe điều khiển từ xa RC"
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
                    RCName = "RC Car",
                    Description = "Xe con chạy pin",
                    ImagePath="Car.jpg",
                    UnitPrice = 179.79f,
                    CategoryID = 1
                },
                new RC
                {
                    RCID = 2,
                    RCName = "RC Car Nitro",
                    Description = "Xe chạy nitro",
                    ImagePath="Car1.jpg",
                    UnitPrice = 169.69f,
                    CategoryID = 2
                },
                new RC
                {
                    RCID = 3,
                    RCName = "RC Car Nitro 2",
                    Description = "Xe chạy nitro",
                    ImagePath="Car2.jpg",
                    UnitPrice = 168.68f,
                    CategoryID = 2
                },
                new RC
                {
                    RCID = 4,
                    RCName = "RC Car over terrain",
                    Description = "Xe vượt địa hình",
                    ImagePath="Car3.jpg",
                    UnitPrice = 196.96f,
                    CategoryID = 3
                },
                new RC
                {
                    RCID = 5,
                    RCName = "RC Car high speed remote control",
                    Description = "Xe chạy tốc độ cao",
                    ImagePath="Car4.jpg",
                    UnitPrice = 119.19f,
                    CategoryID = 4
                },
                new RC
                {
                    RCID = 6,
                    RCName = "RC Car remote control",
                    Description = "Xe điều khiển",
                    ImagePath= "Car5.jpg",
                    UnitPrice = 140.40f,
                    CategoryID = 5
                },
            };
            return RCs;
        }
    }
}