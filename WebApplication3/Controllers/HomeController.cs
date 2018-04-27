using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public static object lockObjet = new object();
        public ActionResult Index()
        {
       
            var helloProtected = MachineKey.Protect(Encoding.UTF8.GetBytes("hello"),
                "testprotector");
            var base64HelloProtected = Convert.ToBase64String(helloProtected);
            lock (lockObjet)
            {
                System.IO.File.WriteAllText("c:\\keys\\protectedFile.txt", base64HelloProtected);
            }
            if (!MyDataProtectionStartup.DataProtectionLoaded)
            {
                return Json("Dataprotection not set", JsonRequestBehavior.AllowGet);
            }
            return Json(base64HelloProtected, JsonRequestBehavior.AllowGet);
        }
    }
}
