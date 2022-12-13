using Microsoft.EntityFrameworkCore;
using PIXY.Models;
using System;
using System.Linq;

namespace PIXY.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PIXYContext context)
        {

            context.Database.EnsureCreated();


            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User{FirstName="Carson",LastName="Alexander",Email="abc@abc.com",PhoneNumber="3232323",Address="adaddsafadfsdf",UserName="AlexC",Password="dfsdfsd"}
            };

            foreach (User s in users)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();


            if (context.Images.Any())
            {
                return;   // DB has been seeded
            }
            
            var images = new Image[]
            {
            new Image{ UserId =1,CategoryDesc="Natural",ImageType="jpg",ImageTags="green",FilePathWatermark="\\images\\photo\\001_w.jpg",FilePath="\\images\\photo\\001.jpg",Price=10.1,HaveHardcopy=true},
            new Image{ UserId =1,CategoryDesc="Landscape",ImageType="jpg",ImageTags="nature,red",FilePathWatermark="\\images\\photo\\002_w.jpg",FilePath="\\images\\photo\\002.jpg",Price=10.1,HaveHardcopy=true},
            new Image{ UserId =2,CategoryDesc="Food",ImageType="jpg",ImageTags="red",FilePathWatermark="\\images\\photo\\003_w.jpg",FilePath="\\images\\photo\\003.jpg",Price=10.1,HaveHardcopy=true},
            new Image{ UserId =2,CategoryDesc="Landscape",ImageType="jpg",ImageTags="yello",FilePathWatermark="\\images\\photo\\004_w.jpg",FilePath="\\images\\photo\\004.jpg",Price=10.1,HaveHardcopy=true}
            };

            foreach (Image s in images)
            {
                context.Images.Add(s);
            }

            context.SaveChanges();


            if (context.Carts.Any())
            {
                return;   // DB has been seeded
            }

            var carts = new Cart[]
            {
            new Cart{ UserId =1,ImageId=1,IsHardcopy=false,NoOfHardcopy=0}
            };

            foreach (Cart s in carts)
            {
                context.Carts.Add(s);
            }

            context.SaveChanges();

            if (context.Transactions.Any())
            {
                return;   // DB has been seeded
            }

            var transactions = new Transaction[]
            {
                new Transaction{UserId =1,ImageId=2,IsHardcopy=false,NoOfHardcopy=0,PurchaseDataTime=DateTime.Parse("2005-09-01")}
            };

            foreach (Transaction s in transactions)
            {
                context.Transactions.Add(s);
            }

            context.SaveChanges();

            if (context.PurchasedItems.Any())
            {
                return;   // DB has been seeded
            }

            var purchasedItems = new PurchasedItem[]
            {
            new PurchasedItem{ UserId =1,ImageId=4,IsHardcopy=false,NoOfHardcopy=0}
            };

            foreach (PurchasedItem s in purchasedItems)
            {
                context.PurchasedItems.Add(s);
            }

            context.SaveChanges();

        }
    }
}