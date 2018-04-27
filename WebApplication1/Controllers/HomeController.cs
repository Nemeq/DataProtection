using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataProtectionProvider _provider;

        public HomeController(IDataProtectionProvider provider)

        {
            _provider = provider;
        }
        public IActionResult Index()
        {
            int count = 0;
            while (!System.IO.File.Exists("c:\\keys\\protectedFile.txt") && count < 100)
            {
                Thread.Sleep(100);
                count++;
            }

            if (count >= 100)
            {
                return NotFound("File not found.");
            }
            var protector = _provider.CreateProtector("testprotector");
            string stringMsg;
            try
            {
                var expectedHello = System.IO.File.ReadAllText("c:\\keys\\protectedFile.txt");
                var msgUnprotected = protector.Unprotect(Convert.FromBase64String(expectedHello));
                stringMsg = Encoding.UTF8.GetString(msgUnprotected);
            }
            catch (Exception e)
            {
                return Json(e);
            }
            return Json(stringMsg);
        }
    }
}