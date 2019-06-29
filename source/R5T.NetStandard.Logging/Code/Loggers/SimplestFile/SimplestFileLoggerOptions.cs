using System;


namespace R5T.NetStandard.Logging.SimplestFile
{
    public class SimplestFileLoggerOptions
    {
        public string LogFilePath { get; set; }
        public bool Overwrite { get; set; } = true;
    }
}
