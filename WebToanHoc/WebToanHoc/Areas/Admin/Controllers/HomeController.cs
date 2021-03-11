using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebToanHoc.Areas.Admin.Models;
using WebToanHoc.Models;

namespace WebToanHoc.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        DbContext_WebTaiLieu db = new DbContext_WebTaiLieu();
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.listLink = new List<string>();
            ViewBag.fileName = new List<string>();
            //ViewBag.list_link = new List<string>();
            //ViewBag.count = 0;
            var category = db.tbl_category.ToList();
            ViewBag.category = category;
            return View();
        }
          [HttpPost]
          public ActionResult Index(HttpPostedFileBase[] file, string folderName, string cate)
          {
               //GoogleDriveAPIHelper.CreateFolder(folderName);
               string id = GoogleDriveAPIHelper.GetLinkFolder(folderName);
               if (id == "")
               {
                    GoogleDriveAPIHelper.CreateFolder(folderName);
                    id = GoogleDriveAPIHelper.GetLinkFolder(folderName);
               }
               //GoogleDriveAPIHelper.UplaodFileOnDrive(file);

               foreach (var item in file)
               {
                    try
                    {
                         //GoogleDriveAPIHelper.UplaodFileOnDrive(item);
                         GoogleDriveAPIHelper.FileUploadInFolder(id, item);
                    }
                    catch
                    {
                         ViewBag.Success = "File Upload fail on Google Drive";
                         ViewBag.listLink = new List<string>();
                         ViewBag.fileName = new List<string>();
                         return View();
                    }
               }

               var lnk = GoogleDriveAPIHelper.linkDrive;
               var fileName = GoogleDriveAPIHelper.fileName;
               ViewBag.Success = "File Uploaded on Google Drive";
               //List<String> list_link = new List<String>();
               //foreach (var item in lnk)
               //{
               //    string[] temp = item.ToString().Split(' ');
               //    list_link.Add(temp[0]);
               //}
               //ViewBag.list_link = list_link;
               ViewBag.listLink = lnk;
               ViewBag.fileName = fileName;
               GoogleDriveAPIHelper.DeleteFolder();
               int count = lnk.Count();
               //ViewBag.count = count;sau đó thêm sản phẩm vào  danh mục ý
               // Ý tưởng viết tiếp
               //cho chọn danh mục 
               for (int i = 0; i < count; i++)
               {
                    tbl_file new_file = new tbl_file();
                    new_file.file_name = fileName[i];
                    new_file.id_cate = Convert.ToInt32(cate);
                    new_file.link_drive = lnk[i];
                    new_file.status = 0;
                    db.tbl_file.Add(new_file);
                    db.SaveChanges();

               }
               var category = db.tbl_category.ToList();
               ViewBag.category = category;

               return View();
          }
          // GET: Admin/Home/Details/5
          public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
