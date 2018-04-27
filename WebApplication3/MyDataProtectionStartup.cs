using System.IO;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.SystemWeb;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication3
{
    public class MyDataProtectionStartup : DataProtectionStartup
    {
        public static bool DataProtectionLoaded = false;
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection()
                .SetApplicationName("company")
                .PersistKeysToFileSystem(new DirectoryInfo("c:\\keys"))
                .ProtectKeysWithDpapi();
            DataProtectionLoaded = true;
        }



    }
}