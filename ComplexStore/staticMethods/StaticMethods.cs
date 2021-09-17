using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComplexStore.staticMethods
{
    static class StaticMethods
    {
        static public string getConnectionString(string connectionString = "Default_Connection")
        {
            string res = string.Empty;

            var builder = new ConfigurationBuilder()
                .SetBasePath(@"C:\C#\Desktop\ComplexStore\ComplexStore")
                .AddJsonFile("appsettings.json");

            var config = builder.Build();
            res = config.GetConnectionString(connectionString);

            return res;
        }
        static public string[] identifiers = { "Shoe Size", "Color", "Storage", "RAM", "Resolution", 
        "Clothe Size", "Screen Diagonal", "Display Size", "Processor", "Graphics"};
    }
}
