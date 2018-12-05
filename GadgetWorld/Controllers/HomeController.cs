using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using GadgetWorld.Models;

namespace GadgetWorld.Controllers
{
    public class HomeController : Controller
    {
        private GwDbContext db = new GwDbContext();





        //User Login
        [HttpGet]
        public ActionResult Index()
        {
            var message = TempData["Message"] as string;
            ViewBag.Message = message;
            return View();
        }





        //User Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserLoginModel userLoginModel, string ReturnUrl = "")
        {
            string message = "";


            using (GwDbContext context = new GwDbContext())
            {
                var v = context.Users.Where(a => a.Email == userLoginModel.Email).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(userLoginModel.Password), v.Password) == 0)
                    {
                        int timeOut = userLoginModel.RememberMe ? 525600 : 1;
                        var ticket =
                            new FormsAuthenticationTicket(userLoginModel.Email, userLoginModel.RememberMe, timeOut);
                        string enctypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, enctypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeOut);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            #region MINE

                            if (v.Type == "Admin")
                            {
                                
                                return RedirectToActionPermanent("Index", "Admin"); 

                            }


                            #endregion

                            else
                            {
                              
                                return RedirectToActionPermanent("AfterLogin", "Home");
                            }

                        }

                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }

                }
                else
                {
                    message = "Invalid credential provided";
                }

            }


            ViewBag.Message = message;
            return View();
        }






        [HttpGet]
        public ActionResult Login()
        {

            var message = TempData["Message"] as string;
            ViewBag.Message = message;
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginModel userLoginModel, string ReturnUrl = "")
        {
            string message = "";


            using (GwDbContext context = new GwDbContext())
            {
                var v = context.Users.Where(a => a.Email == userLoginModel.Email).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(userLoginModel.Password), v.Password) == 0)
                    {
                        int timeOut = userLoginModel.RememberMe ? 525600 : 1;
                        var ticket =
                            new FormsAuthenticationTicket(userLoginModel.Email, userLoginModel.RememberMe, timeOut);
                        string enctypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, enctypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeOut);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            #region MINE

                            if (v.Type == "Admin")
                            {

                                return RedirectToActionPermanent("Index", "Admin");

                            }


                            #endregion

                            else
                            {

                                return RedirectToActionPermanent("AfterLogin", "Home");
                            }

                        }

                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }

                }
                else
                {
                    message = "Invalid credential provided";
                }

            }


            ViewBag.Message = message;
            return View();
        }











        [Authorize]
        public ActionResult AfterLogin()
        {
            //string message = "Login Successfully";
            //ViewBag.Message = message;
            return View();
        }






        //User Logout
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
         





      









        public ActionResult Reg()
        {
            User user =new User();
            user.Type = "Customer";
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reg(User user)
        {
            bool Status = false;
            string message = "";
            user.Type = "Customer";

            //Model Validation
            if (ModelState.IsValid)
            {
                //Email is already Exists
                var isExist = IsEmailExist(user.Email);
                if(isExist)
                {
                    ModelState.AddModelError("EmailExists","Email already exists");
                    return View(user);
                }





                #region Password Hashing            
                user.Password = Crypto.Hash(user.Password);
                user.RepeatPassword = Crypto.Hash(user.RepeatPassword);            
                #endregion



                #region Save to Database
                using (GwDbContext context=new GwDbContext())
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
            return RedirectToAction("Index", "Home");
        }


       
    

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }





        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
            var toEmail=new MailAddress(email);

            var fromEmailPassword = "**********";
            string subject = "Your account is successfully created!";
            string body = "<br/><br/> We are happy to tell you that your Gadget World account is successfully create. Now you can update your profile information"+
                "<a href='"+link+"'>"+link+"</a>";

            var smtp = new SmtpClient        
                          
            {
                Host = "smtp.gmail.com",
                Port=587,
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