using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace AutomaticUpdate
{
    public partial class UpdateForm : Form
    {
#if VS_2008
        const string RELEASE_DOC_URL = "http://www.agilefx.org/public/agilefx.org/releases/latest-vs2008.txt";
        const string LATEST_MSI_URL = "http://www.agilefx.org/public/agilefx.org/releases/latest-vs2008.msi";
#endif
#if VS_2010
        const string RELEASE_DOC_URL = "http://www.agilefx.org/public/agilefx.org/releases/latest-vs2010.txt";
        const string LATEST_MSI_URL = "http://www.agilefx.org/public/agilefx.org/releases/latest-vs2010.msi";
#endif

        public UpdateForm()
        {
            InitializeComponent();
            LoadRegistryKey();
        }

        RegistryKey currentInstallKey = null;
        private void LoadRegistryKey()
        {
            string rootKeyName = null;
            if (Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") == "AMD64")
                rootKeyName = @"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            else
                rootKeyName = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
            var rootKey = Registry.LocalMachine.OpenSubKey(rootKeyName);
            currentInstallKey = rootKey.GetSubKeyNames().Select(subKey => rootKey.OpenSubKey(subKey))
                .Where(subkey => (subkey.GetValue("DisplayName") as string) == "AgileModeler")
                .OrderByDescending(subkey => new Version(subkey.GetValue("DisplayVersion") as string)).FirstOrDefault();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Application.Idle += new EventHandler(OnLoaded);
        }

        void OnLoaded(object sender, EventArgs e)
        {
            Application.Idle -= new EventHandler(OnLoaded);
            PerformUpdate();
        }

        private void PerformUpdate()
        {
            try
            {
                var currentVersion = GetCurrentVersion();

                //wait for a second so that the user sees the connection message
                Thread.Sleep(1000);

                var latestReleaseInfo = GetLatestReleaseInfo();
                var latestVersion = latestReleaseInfo["Version"];
                statusLabel.Text = "The latest version is " + latestVersion + ".";
                Thread.Sleep(1000);

                if (currentVersion == new Version(latestVersion))
                {
                    MessageBox.Show("You already have the latest version.", "AgileModeler Update");
                }
                else
                {
                    if (MessageBox.Show("Do you want to download and install the latest version",
                        "AgileModeler Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var latestMSI = DownloadFromUrl(LATEST_MSI_URL, true);
                        if (ValidateDownload(latestMSI, latestReleaseInfo["Signature"], GetPublickeyFile()))
                        {
                            var windowsDir = Environment.GetEnvironmentVariable("windir");
                            var msiexecPath = Path.Combine(windowsDir, @"system32\msiexec.exe");
                            if (currentVersion >= new Version(latestReleaseInfo["MinimumUpdateVersion"]))
                            {
                                var p2 = System.Diagnostics.Process.Start(msiexecPath, string.Format("/x {0} /qn", latestMSI));
                                p2.WaitForExit();
                            }
                            var p = System.Diagnostics.Process.Start(msiexecPath, string.Format("/i {0} /qn", latestMSI));
                            p.WaitForExit();
                            MessageBox.Show("AgileFx updated successfully.", "AgileModeler Update");
                        }
                        else
                        {
                            MessageBox.Show("Unable to validate the setup files. Please manually download and install from www.agilefx.org.",
                                "AgileModeler Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Unable to download updates. Please manually download and install from www.agilefx.org.",
                    "AgileModeler Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }

        private bool ValidateDownload(string latestZip, string signatureString, string publickeyFile)
        {
            var data = File.ReadAllBytes(latestZip);
            var signature = Convert.FromBase64String(signatureString);
            var publicKey = File.ReadAllBytes(publickeyFile);
            using (var csp = new RSACryptoServiceProvider())
            {
                csp.ImportCspBlob(publicKey);
                return csp.VerifyData(data, SHA1.Create(), signature);
            }
        }

        private string GetPublickeyFile()
        {
            if (currentInstallKey != null)
                return Path.Combine(currentInstallKey.GetValue("InstallLocation") as string, "public.key");
            else
                return "public.key";
        }

        private Version GetCurrentVersion()
        {
            if (currentInstallKey != null)
                return new Version(currentInstallKey.GetValue("DisplayVersion") as string);
            else
                return new Version(0, 0, 0);
        }

        private Dictionary<string, string> GetLatestReleaseInfo()
        {
            var releaseInfoFile = DownloadFromUrl(RELEASE_DOC_URL, false);
            var data = new Dictionary<string, string>();
            foreach (var line in File.ReadAllLines(releaseInfoFile))
            {
                var kvp = line.Split(':');
                data.Add(kvp[0], kvp[1]);
            }

            return data;
        }

        private string DownloadFromUrl(string url, bool isBinary)
        {
            var req = WebRequest.Create(url);
            var response = req.GetResponse();
            var stream = response.GetResponseStream();
            var filename = Path.GetTempFileName();
            if (isBinary)
            {
                var buffer = new byte[600000];//[2048];
                using (var writer = new BinaryWriter(File.OpenWrite(filename)))
                {
                    var numRead = 0;
                    while ((numRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                        writer.Write(buffer, 0, numRead);
                }
            }
            else
            {
                File.WriteAllText(filename, new StreamReader(stream).ReadToEnd());
            }
            return filename;
        }
    }
}
