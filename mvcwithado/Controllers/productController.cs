using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcwithado.dal;
using mvcwithado.Models;

namespace mvcwithado.Controllers
{
    public class productController : Controller
    {
        productdal productdal = new productdal();
        // GET: product
        public ActionResult Index()
        {
            var listitem = productdal.getallitem();
            if (listitem.Count == 0)
            {
                TempData["infomsg"] = "currently no data available";
            }
            return View(listitem);
        }

        // GET: product/Details/5
        public ActionResult Details(int id)
        {
            var product = productdal.getitembyid(id).FirstOrDefault();
            return View(product);
        }

        // GET: product/Create
        public ActionResult Create()
        {
            return View();
        }


        //[HttpPost]
        //public ActionResult Upload(UploadModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Upload image
        //        string imagePath = Server.MapPath("~/Images/");
        //        string imageFileName = Path.GetFileName(model.ImageFile.FileName);
        //        string imageFullPath = Path.Combine(imagePath, imageFileName);
        //        model.ImageFile.SaveAs(imageFullPath);

        //        // Upload resume
        //        string resumePath = Server.MapPath("~/Resumes/");
        //        string resumeFileName = Path.GetFileName(model.ResumeFile.FileName);
        //        string resumeFullPath = Path.Combine(resumePath, resumeFileName);
        //        model.ResumeFile.SaveAs(resumeFullPath);

        //        // Save file information to database
        //        // ...

        //        return RedirectToAction("Index");
        //    }

        //    return View(model);
        //}



        // POST: product/Create
        [HttpPost]
        public ActionResult Create(product product,HttpPostedFileBase file)
        {
            bool isinserted = false;
            try
            {


                if (ModelState.IsValid)
                //{
                //    // Upload image
                //    string imagePath = Server.MapPath("~/photo/");
                //    string imageFileName = Path.GetFileName(product.photo.FileName);
                //    string imageFullPath = Path.Combine(imagePath, imageFileName);
                //    product.photo.SaveAs(imageFullPath);

                //    // Upload resume
                //    string resumePath = Server.MapPath("~/resume/");
                //    string resumeFileName = Path.GetFileName(product.resume.FileName);
                //    string resumeFullPath = Path.Combine(resumePath, resumeFileName);
                //    product.resume.SaveAs(resumeFullPath);
                    {
                        isinserted = productdal.insertitems(product);
                        if (isinserted)
                        {
                            TempData["successmessage"] = "inserted successfully";
                        }
                        else
                        {
                            TempData["errormessage"] = "unable to insert";
                        }

                    }
                    
                
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
                return View();
            }
        }


        //    if (ModelState.IsValid)
        //{
        //    if (file.ContentLength > 0)
        //    {
        //        string photo = Path.GetFileName(file.FileName);
        //        var s = Server.MapPath("~/photo");
        //        string pa = Path.Combine(s, photo);
        //        file.SaveAs(pa);
        //        var fullpath = Path.Combine("~\\photo", photo);
        //        product.photo = fullpath;
        //    }
        //}

        //{


        //if (ModelState.IsValid)


        // GET: product/Edit/5
        public ActionResult Edit(int id)
        {
            var products = productdal.getitembyid(id).FirstOrDefault();
            if (products == null)
            {
                TempData["infomsg"] = "product not available";
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // POST: product/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult update(product product)
        {
            if (ModelState.IsValid)
            {
                bool isupdated = productdal.update(product);
                if(isupdated)
                 {
                    TempData["sucessmsg"] = "saved";
                }
                else
                {
                    TempData["errormsg"] = "already available";
                }
            }
            return RedirectToAction("Index");
        }

        // GET: product/Delete/5
        public ActionResult Delete(int id)
        {
            var product = productdal.getitembyid(id).FirstOrDefault();
            if (product == null)
            {
                TempData["infomsg"] = "product not available";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // POST: product/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult Deleteconf(int id)
        {
            try
            {
                string result = productdal.deleteproduct(id);
                if (result.Contains("deleted"))
                {
                    TempData["sucessmsg"] = result;
                }
                else
                {
                    TempData["errormsg"] = result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View();
            }
        }
    }
}
