using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp
{
   public class OutputSettings
    {
        public OutputSettings()
        {
            File = "report.txt";
        }
        public string Folder { get; set; }
        public string File { get; set; }

        public string GetReportFilePath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), Folder, File);
        }

        public string GetReportDirecory()
        {
            return Path.GetDirectoryName(GetReportFilePath());
        }
    }
    public class SiteSettings
    {
        public string Site { get; set; }
    }
}
