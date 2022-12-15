using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIXY.Data;
using PIXY.Models;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Drawing.Printing;

namespace PIXY.Controllers
{
    public class ImagesController : Controller
    {
        private readonly PIXYContext _context;

        public ImagesController(PIXYContext context)
        {
            _context = context;
        }

        // GET: Images
        public async Task<IActionResult> Index(string ImageCategory, string SearchString, int? PageNumber)
        {

            // Get Categories List
            IQueryable<string> CategoryQuery = from m in _context.Images
                                               orderby m.CategoryDesc
                                               select m.CategoryDesc;

            if (string.IsNullOrEmpty(SearchString))
            {
                SearchString = "";
            }

            // Paging Feature
            int PageSize = 2;    // No of record per page   ******** May Change Config Later ******** 
            PageNumber ??= 1;   // PageNumber default = 1
            
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                // Haven't login

                var query = from i in _context.Images
                            where i.ImageTags.Contains(SearchString)
                            join u in _context.Users on i.UserId equals u.ID
                            select new ImageVM
                            {
                                ID = i.ID,
                                UserId = i.UserId,
                                CategoryDesc = i.CategoryDesc,
                                ImageType = i.ImageType,
                                FilePathWatermark = i.FilePathWatermark,
                                FilePath = i.FilePath,
                                Price = i.Price,
                                HaveHardcopy = i.HaveHardcopy,
                                IsPurchased = false,
                                AuthorName = u.FirstName + " " + u.LastName,
                                ImageTags = i.ImageTags
                            };

                //Apply Filter to Image List
                if (!string.IsNullOrEmpty(ImageCategory))
                {
                    query = query.Where(i => i.CategoryDesc == ImageCategory);
                }

                var count = await query.CountAsync();       // Paging Feature

                var imageCategoryVM = new ImagesCategoryView
                {
                    Categories = new SelectList(await CategoryQuery.Distinct().ToListAsync()),                              // Execute query for Categories List
                    ImagesVM = await query.Skip((int)(PageNumber - 1) * PageSize).Take(PageSize).ToListAsync(),            // Execute query for Image List
                    SearchTags = SearchString,
                    PageIndex = (int)PageNumber,                                    // Paging Feature
                    TotalPages = (int)Math.Ceiling(count / (double)PageSize)        // Paging Feature
                };
                
                return View(imageCategoryVM);
            }
            else
            {
                // Have login

                int UserID = (int)HttpContext.Session.GetInt32("UserID");

                var query = from i in _context.Images
                            where i.ImageTags.Contains(SearchString)
                            from u in _context.Users.Where(u => u.ID == i.UserId)
                            from c in _context.Carts.Where(c => c.ImageId == i.ID && c.UserId == UserID).DefaultIfEmpty() //Left Join
                            from p in _context.PurchasedItems.Where(p=> i.ID==p.ImageId && p.UserId == UserID ).DefaultIfEmpty() //Left Join
                            select new ImageVM
                            {
                                ID = i.ID,
                                UserId = i.UserId,
                                CategoryDesc = i.CategoryDesc,
                                ImageType = i.ImageType,
                                FilePathWatermark = i.FilePathWatermark,
                                FilePath = i.FilePath,
                                Price = i.Price,
                                HaveHardcopy = i.HaveHardcopy,
                                IsPurchased = p == null ? false: true,
                                IsInCart = c == null ? false : true,
                                AuthorName = u.FirstName + " " + u.LastName,
                                ImageTags = i.ImageTags
                            };

                //Apply Filter to Image List
                if (!string.IsNullOrEmpty(ImageCategory))
                {
                    query = query.Where(i => i.CategoryDesc == ImageCategory);
                }

                var count = await query.CountAsync();       // Paging Feature

                var imageCategoryVM = new ImagesCategoryView
                {
                    Categories = new SelectList(await CategoryQuery.Distinct().ToListAsync()),  // Execute query for Categories List
                    ImagesVM = await query.Skip((int)(PageNumber - 1) * PageSize).Take(PageSize).ToListAsync(),   // Execute query for Image List
                    SearchTags = SearchString,
                    PageIndex = (int)PageNumber,                                    // Paging Feature
                    TotalPages = (int)Math.Ceiling(count / (double)PageSize)        // Paging Feature
                };

                return View(imageCategoryVM);
            }
        }
        
        // Assign file path to purchased image
  //      [Authorize]
        [Route("images/protected/{FileName}")]
        public IActionResult ShowProtectedImage(string FileName)
        {
            int ImageId = Int32.Parse(Left(FileName, 3));       // image id = frist 3 digit of file name

            if (HttpContext.Session.GetInt32("UserID") != null && ImageId != 0)
            {
#pragma warning disable CS8629 // Nullable value type may be null.
                int UserID = (int)HttpContext.Session.GetInt32("UserID");
#pragma warning restore CS8629 // Nullable value type may be null.

                // Check if the image is purchased by the user
                var query = from p in _context.PurchasedItems
                            join i in _context.Images on p.ImageId equals i.ID
                            where p.UserId == UserID && i.ID == ImageId
                            select new 
                            {
                                ID = p.ID,
                                ImageId= i.ID
                            };

                var result = query.FirstOrDefault();

                if (result != null)
                {
                    // Assign file path
                    var file = Path.Combine(Directory.GetCurrentDirectory(),
                                            "images_p", FileName);
                    string ext = System.IO.Path.GetExtension(FileName).ToLower();
                    Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
                    //get the mimetype of the file
                    string mimeType = regKey.GetValue("Content Type").ToString();

                    return PhysicalFile(file, mimeType);
                }
            }
            // return new ForbidResult();       // Image is not purchased
            return RedirectToAction("UnAuthorizedAccess", "Images");
        }

        public IActionResult UnAuthorizedAccess()
        {
            return View();
        }

        private string Left(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }


        public FileResult DownLoadZip(int ImageId)
        {
            var webRoot = Directory.GetCurrentDirectory();
            var fileName = "PIXY_Images.zip";
            var tempOutput = webRoot + "/Download/" + fileName;

            using (ZipOutputStream IzipOutputStream = new ZipOutputStream(System.IO.File.Create(tempOutput)))
            {
                IzipOutputStream.SetLevel(9);
                byte[] buffer = new byte[4096];
                var imageList = new List<string>();

                imageList.Add(Path.Combine(Directory.GetCurrentDirectory(),
                        "images_p", ImageId.ToString().PadLeft(3, '0') + ".jpg"));
                //imageList.Add(webRoot + "/Images/1data.png");


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


        private bool ImageExists(int id)
        {
          return _context.Images.Any(e => e.ID == id);
        }
    }
}
