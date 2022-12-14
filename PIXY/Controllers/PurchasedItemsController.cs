using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIXY.Data;
using PIXY.Models;

namespace PIXY.Controllers
{
    public class PurchasedItemsController : Controller
    {
        private readonly PIXYContext _context;

        public PurchasedItemsController(PIXYContext context)
        {
            _context = context;
        }

        // GET: PurchasedItems
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IActionResult> Index()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                // Haven't login
                return RedirectToAction("Login", "Users");

            }
            else
            {
                // Have login
             
#pragma warning disable CS8629 // Nullable value type may be null.
                int UserID = (int)HttpContext.Session.GetInt32("UserID");
#pragma warning restore CS8629 // Nullable value type may be null.


                var query = from p in _context.PurchasedItems
                            join i in _context.Images on p.ImageId equals i.ID
                            where p.UserId == UserID
                            select new PurchasedItemVM
                            {
                                ID = p.ID,
                                UserId = p.UserId,
                                ImageId = p.ImageId,
                                IsHardcopy = p.IsHardcopy,
                                NoOfHardcopy = p.NoOfHardcopy,
                                FilePath = i.FilePath
                            };

                PurchasedItemVM[] results = query.ToArray();

                return View(results);
            }
        }

        public FileResult DownLoadZip([Bind("SelectedImage")] PurchasedItemVM purchasedItemVM)
        {
            var webRoot = Directory.GetCurrentDirectory();
            var fileName = "PIXY_Images.zip";
            var tempOutput = webRoot + "/Download/" + fileName;

            using (ZipOutputStream IzipOutputStream = new ZipOutputStream(System.IO.File.Create(tempOutput)))
            {
                IzipOutputStream.SetLevel(9);
                byte[] buffer = new byte[4096];
                var imageList = new List<string>();

                for (int i = 0; i < purchasedItemVM.SelectedImage.Count; i++)
                { 
                    imageList.Add(Path.Combine(Directory.GetCurrentDirectory(),
                        "images_p", purchasedItemVM.SelectedImage[i].ToString().PadLeft(3,'0') + ".jpg"));
                }

                for (int i = 0; i < imageList.Count; i++)
                {
                    ZipEntry entry = new ZipEntry(Path.GetFileName(imageList[i]));
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    IzipOutputStream.PutNextEntry(entry);

                    using (FileStream oFileStream = System.IO.File.OpenRead(imageList[i]))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = oFileStream.Read(buffer, 0, buffer.Length);
                            IzipOutputStream.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }
                IzipOutputStream.Finish();
                IzipOutputStream.Flush();
                IzipOutputStream.Close();
            }

            byte[] finalResult = System.IO.File.ReadAllBytes(tempOutput);
            if (System.IO.File.Exists(tempOutput))
            {
                System.IO.File.Delete(tempOutput);
            }
            if (finalResult == null || !finalResult.Any())
            {
                throw new Exception(String.Format("Nothing found"));

            }

            return File(finalResult, "application/zip", fileName);
        }

        private bool PurchasedItemExists(int id)
        {
          return _context.PurchasedItems.Any(e => e.ID == id);
        }
    }
}
