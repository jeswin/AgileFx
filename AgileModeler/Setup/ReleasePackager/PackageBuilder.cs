using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace ReleasePackager
{
    public class PackageBuilder
    {
        public string TargetMSIFile { get; set; }
        public string CertificateFile { get; set; }
        public string Password { get; set; }
        public string Version { get; set; }
        public string MinimumUpdateVersion { get; set; }
        public string OutputFile { get; set; }

        public bool Validate()
        {
            return !(string.IsNullOrEmpty(TargetMSIFile) || string.IsNullOrEmpty(CertificateFile)
                || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Version));
        }

        public void Create()
        {
            byte[] signature = null;
            var cert = new X509Certificate2(CertificateFile, Password);
            using (var csp = cert.PrivateKey as RSACryptoServiceProvider)
            {
                var data = File.ReadAllBytes(TargetMSIFile);
                signature = csp.SignData(data, SHA1.Create());
            }
            using (var writer = new StreamWriter(File.OpenWrite(OutputFile)))
            {
                writer.WriteLine("Version:" + Version);
                writer.WriteLine("MinimumUpdateVersion:" + MinimumUpdateVersion);
                writer.WriteLine("Signature:" + Convert.ToBase64String(signature));
            }
        }
    }
}
