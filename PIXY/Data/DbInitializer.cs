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
                 new User{FirstName="Carson",LastName="Alexander",Email="calex@hotmail.com",PhoneNumber="9024923240",Address="1075 Barrington St",PostalCode="B3H 2P8",City="Halifax",Province="Nova Scotia",Country="Canada",UserName="alexc",Password="123456"},
                 new User{FirstName="Tim",LastName="Hortans",Email="ttc@gmail.com",PhoneNumber="9024229884",Address="1120 Queen St",PostalCode="B3H 2R9",City="Halifax",Province="Nova Scotia",Country="Canada",UserName="timho",Password="123456"}
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
            new Image{ UserId =1,CategoryDesc="Natural",ImageType="jpg",ImageTags="Green",FilePathWatermark="\\images_w\\001.jpg",FilePath="\\images\\protected\\001.jpg",Price=10.1,HaveHardcopy=true},
            new Image{ UserId =1,CategoryDesc="Landscape",ImageType="jpg",ImageTags="Red",FilePathWatermark="\\images_w\\002.jpg",FilePath="\\images\\protected\\002.jpg",Price=12.1,HaveHardcopy=true},
            new Image{ UserId =2,CategoryDesc="Food",ImageType="jpg",ImageTags="Curry,Yellow",FilePathWatermark="\\images_w\\003.jpg",FilePath="\\images\\protected\\003.jpg",Price=15.1,HaveHardcopy=true},
            new Image{ UserId =2,CategoryDesc="Landscape",ImageType="jpg",ImageTags="Waterfront,Blue",FilePathWatermark="\\images_w\\004.jpg",FilePath="\\images\\protected\\004.jpg",Price=12.1,HaveHardcopy=true},
            new Image{ UserId =1,CategoryDesc="Natural",ImageType="jpg",ImageTags="Insect,Yellow",FilePathWatermark="\\images_w\\005.jpg",FilePath="\\images\\protected\\005.jpg",Price=10.1,HaveHardcopy=true},
            
            new Image{ UserId =1,CategoryDesc="Landscape",ImageType="jpg",ImageTags="Waterfront,Blue,Green",FilePathWatermark="\\images_w\\006.jpg",FilePath="\\images\\protected\\006.jpg",Price=12.1,HaveHardcopy=true},
            new Image{ UserId =1,CategoryDesc="Natural",ImageType="jpg",ImageTags="Flower,Plant,Green,White",FilePathWatermark="\\images_w\\007.jpg",FilePath="\\images\\protected\\007.jpg",Price=10.1,HaveHardcopy=true},
            new Image{ UserId =1,CategoryDesc="Natural",ImageType="jpg",ImageTags="Flower,Plant,Green",FilePathWatermark="\\images_w\\008.jpg",FilePath="\\images\\protected\\008.jpg",Price=10.1,HaveHardcopy=true},
            new Image{ UserId =1,CategoryDesc="Animal",ImageType="jpg",ImageTags="Cat,Yellow",FilePathWatermark="\\images_w\\009.jpg",FilePath="\\images\\protected\\009.jpg",Price=10.1,HaveHardcopy=true},
            new Image{ UserId =2,CategoryDesc="Animal",ImageType="jpg",ImageTags="Monkey,Green",FilePathWatermark="\\images_w\\010.jpg",FilePath="\\images\\protected\\010.jpg",Price=10.1,HaveHardcopy=true},
            
            new Image{ UserId =2,CategoryDesc="Landscape",ImageType="jpg",ImageTags="Waterfront,Blue",FilePathWatermark="\\images_w\\011.jpg",FilePath="\\images\\protected\\011.jpg",Price=12.1,HaveHardcopy=true},
            new Image{ UserId =2,CategoryDesc="Street",ImageType="jpg",ImageTags="Food",FilePathWatermark="\\images_w\\012.jpg",FilePath="\\images\\protected\\012.jpg",Price=10.1,HaveHardcopy=true},
            new Image{ UserId =2,CategoryDesc="Street",ImageType="jpg",ImageTags="Festival,Xmas",FilePathWatermark="\\images_w\\013.jpg",FilePath="\\images\\protected\\013.jpg",Price=10.1,HaveHardcopy=true},
            new Image{ UserId =2,CategoryDesc="Animal",ImageType="jpg",ImageTags="Carlton,Cat",FilePathWatermark="\\images_w\\014.jpg",FilePath="\\images\\protected\\014.jpg",Price=10.1,HaveHardcopy=true}

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
            new Cart{ UserId =1,ImageId=2,IsHardcopy=false,NoOfHardcopy=0}
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
                new Transaction{UserId =1,ImageId=1,IsHardcopy=false,NoOfHardcopy=0,PurchaseDataTime=DateTime.Parse("2005-09-01")}
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