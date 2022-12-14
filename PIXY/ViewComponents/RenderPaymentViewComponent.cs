using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIXY.Data;
using PIXY.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PIXY.ViewComponents
{
    public class RenderPaymentViewComponent : ViewComponent
    {
        private readonly PIXYContext _context;

        public RenderPaymentViewComponent(PIXYContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            PaymentVM result = new PaymentVM();

            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                // Haven't login
                return View(result);
            }
            else
            {
                // Have login
#pragma warning disable CS8629 // Nullable value type may be null.
                int UserID = (int)HttpContext.Session.GetInt32("UserID");
#pragma warning restore CS8629 // Nullable value type may be null.
                /*
                var user =  _context.Users.FirstOrDefaultAsync(e => e.ID == UserID);

                if (user != null)
                {
                    result.Address = user.;
                }
                */

                var tmp = (from u in _context.Users
                            where u.ID == UserID
                            select new {
                                Address = u.Address
                            }).FirstOrDefault();

                if (tmp != null)
                {
                    result.Address = tmp.Address;
                }

                return View(result);
            }

           
        }
    }
}

