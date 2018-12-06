using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GadgetWorld.Migrations;
using GadgetWorld.Models;
using Microsoft.Ajax.Utilities;

namespace GadgetWorld.Controllers
{
    public class AdminController : Controller
    {
        private GwDbContext db = new GwDbContext();

        // GET: Admin


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }




        [Authorize]
        public ActionResult Index()
        {


            return View();
        }





        [Authorize]
        public ActionResult UserList()
        {

            List<User> users = db.Users.ToList();
            return View();
        }






        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }



        // GET: Admin/Create
        [Authorize]
        public ActionResult Create()
        {



            ViewBag.Gender = db.Genders.Select(c => new SelectListItem
            {
                Value = c.GenderType.ToString(),
                Text = c.GenderType
            }).ToList();



            User user = new User();
            user.Type = "Admin";
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {

            bool Status = false;
            string message = "";
            user.Type = "Admin";

            ViewBag.Gender = db.Genders.Select(c => new SelectListItem
            {
                Value = c.GenderType.ToString(),
                Text = c.GenderType
            }).ToList();


            //Model Validation
            if (ModelState.IsValid)
            {
                //Email is already Exists
                var isExist = IsEmailExist(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExists", "Email already exists");
                    ViewBag.Message = message;
                    ViewBag.Status = Status;
                    return View(user);
                }





                #region Password Hashing            

                user.Password = Crypto.Hash(user.Password);
                user.RepeatPassword = Crypto.Hash(user.RepeatPassword);

                #endregion



                #region Save to Database

                using (GwDbContext context = new GwDbContext())
                {



                    context.Users.Add(user);
                    context.SaveChanges();

                    //send email to user
                    //SendVerificationEmail(user.Email);
                    //message = "Registration Successful"+"An email has been to your Email:"+user.Email;
                    message = "Registered successfully";
                    Status = true;

                }

                #endregion
            }
            else
            {
                message = "Invalid Request";

            }

            TempData["Message"] = message;
            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        
        }






        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,Type,Name,Email,ContactNumber,Address,Gender,DateOfBirth,Password")]
            User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }




        [HttpGet]
        [Authorize]
        public ActionResult CreateCategory()
        {
            return View();

        }






        [HttpPost]
        [Authorize]
        public ActionResult CreateCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            ModelState.Clear();
            ViewBag.Message = "successfully added";
            ViewBag.Status = true;
            return View();

        }






        [HttpGet]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateProduct()
        {
            var model = new ProductCreateViewModel();
     

            ViewBag.Category = db.Categories.Select(c => new SelectListItem
            {
                Value = c.CatagoryType.ToString(),
                Text = c.CatagoryType
            }).ToList();



            ViewBag.Color = db.Colors.Select(d => new SelectListItem
            {
                Value = d.ColorType.ToString(),
                Text = d.ColorType
            }).ToList();



            



            return View();

        }



   

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {

            string message = null;
            bool Status = false;

            string FileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
            string Extensiton = Path.GetExtension(product.ImageFile.FileName);
            FileName = FileName + DateTime.Now.ToString("yymmssfff") + Extensiton;
            product.ImagePath = "~/Content/Image/" + FileName;
            FileName = Path.Combine(Server.MapPath("~/Content/Image/"), FileName);
            product.ImageFile.SaveAs(FileName);

            ViewBag.Category=db.Categories.Select(c => new SelectListItem
            {
                Value = c.CatagoryType.ToString(),
                Text = c.CatagoryType
            }).ToList();

            ViewBag.Color = db.Colors.Select(d => new SelectListItem
            {
                Value = d.ColorType.ToString(),
                Text = d.ColorType
            }).ToList();


            var isExist = IsProductExist(product.ProductName);
            if (isExist)
            {
                {
                    ModelState.AddModelError("ProductsExists", "Product already exists. Please update it");
                    ViewBag.Message = message;
                    ViewBag.Status = Status;
                    return View(product);
                   

                }
            }
         

            db.Products.Add(product);
            db.SaveChanges();
            ModelState.Clear();
            ViewBag.Message = "successfully updated";
            ViewBag.Status  = true;
            return View();

        }








        [HttpGet]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult ProductList()
        {


            //Product product = new Product();

            //product = db.Products.Where(x => x.ImagePath == ImagePath).FirstOrDefault();


            // ViewBag.Product = product;

            List<Product> products = db.Products.ToList();

            ViewBag.Products = products;

            return View(products);

        }






        [NonAction]
        public bool IsProductExist(string product)
        {
            using (GwDbContext context = new GwDbContext())
            {
                var v = context.Products.Where(a => a.ProductName == product).FirstOrDefault();
                return v!=null;
            }

        }



        //Logout

        [NonAction]
        public bool IsEmailExist(string email)
        {
            using (GwDbContext context = new GwDbContext())
            {
                var v = context.Users.Where(a => a.Email == email).FirstOrDefault();
                return v != null;
            }

        }

        [NonAction]
        public void SendVerificationEmail(string email)
        {
            //var scheme = Request.Url.Scheme;
            //var host = Request.Url.Host;
            //var port = Request.Url.Port;

            //string url= scheme + "://"  + host +

            var varifyUrl = "/User/VerifyAccount/";
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, varifyUrl);
            var fromEmail = new MailAddress("hmuzzal.diu@gmail.com", "Gadget World");
            var toEmail = new MailAddress(email);

            var fromEmailPassword = "**********";
            string subject = "Your account is successfully created!";
            string body =
                "<br/><br/> We are happy to tell you that your Gadget World account is successfully create. Now you can update your profile information" +
                "<a href='" + link + "'>" + link + "</a>";

            var smtp = new SmtpClient

            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };


            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            })
                smtp.Send(message);

        }





    }
}





