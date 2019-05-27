using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Security.Claims;
using fyp.Model;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using fyp.Models;

namespace fyp.Controllers
{
    public class AccountController : Controller
    {
        private const string LOGIN_SQL = @" SELECT* FROM  activity
            WHERE NRIC = '{0}' )";


        private const string LOGIN_VIEW = "UserLogin";
        private const string LASTLOGIN_SQL = @"UPDATE activity SET LstLogin=GETDATE() WHERE NRIC='{0}'";

        private const string REDIRECT_CNTR = "Account";
        private const string REDIRECT_ACTN = "Account";

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            TempData["ReturnUrl"] = returnUrl;
            return View(LOGIN_VIEW);
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UserLogin user)
        {
            if (!AuthenticateUser(user.NRIC, out ClaimsPrincipal principal))
            {
                ViewData["Message"] = "Incorrect NRIC";
                ViewData["MsgType"] = "warning";
                return View(LOGIN_VIEW);
            }
            else
            {
                HttpContext.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                   principal,
               new AuthenticationProperties
               {
                   IsPersistent = user.RememberMe
               });

                // Update the Last Login Timestamp of the User
                DBUtl.ExecSQL(LASTLOGIN_SQL, user.NRIC);

                if (TempData["returnUrl"] != null)
                {
                    string returnUrl = TempData["returnUrl"].ToString();
                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                }

                return RedirectToAction(REDIRECT_ACTN, REDIRECT_CNTR);
            }
        }

        [Authorize]
        public IActionResult Logoff(string returnUrl = null)
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction(REDIRECT_ACTN, REDIRECT_CNTR);
        }

        [AllowAnonymous]
        public IActionResult Forbidden()
        {
            return View();
        }




        [AllowAnonymous]
        public IActionResult VerifyUserID(string NRIC)
        {
            string select = $"SELECT * FROM activity WHERE NRIC='{NRIC}'";
            if (DBUtl.GetTable(select).Rows.Count > 0)
            {
                return Json($"[{NRIC}] already in use");
            }
            return Json(true);
        }

       

    }
}