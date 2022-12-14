using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIXY.Data;
using PIXY.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PIXY.ViewComponents
{
    public class RenderCartViewComponent : ViewComponent
    {
        private readonly PIXYContext _context;

        public RenderCartViewComponent(PIXYContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                // Haven't login
                return View();

            }
            else
            {
                // Have login
#pragma warning disable CS8629 // Nullable value type may be null.
                int UserID = (int)HttpContext.Session.GetInt32("UserID");
#pragma warning restore CS8629 // Nullable value type may be null.

                var query = from c in _context.Carts
                            join i in _context.Images on c.ImageId equals i.ID
                            where c.UserId == UserID
                            select new CartVM
                            {
                                ID = c.ID,
                                UserId = c.UserId,
                                ImageId = c.ImageId,
                                IsHardcopy = c.IsHardcopy,
                                NoOfHardcopy = c.NoOfHardcopy,
                                FilePathWatermark = i.FilePathWatermark
                            };

                CartVM[] results = query.ToArray();

                return View(results);
            }

        }
    }
}

