using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReleasePackager
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new PackageBuilder { MinimumUpdateVersion = "0.0.0", OutputFile = "latest.txt" };
            for (int i=0; i<args.Length; i+=2)
            {
                switch (args[i])
                {
                    case "/t":
                        builder.TargetMSIFile = args[i + 1];
                        break;
                    case "/c":
                        builder.CertificateFile = args[i + 1];
                        break;
                    case "/p":
                        builder.Password = args[i + 1];
                        break;
                    case "/v":
                        builder.Version = args[i + 1];
                        break;
                    case "/mv":
                        builder.MinimumUpdateVersion = args[i + 1];
                        break;
                    case "/o":
                        builder.OutputFile = args[i + 1];
                        break;
                }
            }
            if (builder.Validate()) builder.Create();
            else ShowUsage();
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Creates the deployment package for AgileModeler.");
            Console.WriteLine("ReleasePackage [options]");
            Console.WriteLine("Options:");
            Console.WriteLine("/t [target msi file]");
            Console.WriteLine("/c [certificate (.pfx) file]");
            Console.WriteLine("/p [password to certificate file]");
            Console.WriteLine("/v [package version eg: 1.0.0]");
            Console.WriteLine("/mv [minimum update version]");
            Console.WriteLine("/o [output release file]");
        }
    }
}
