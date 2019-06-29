using System;


namespace R5T.NetStandard.Logging.SimpleFile
{
    public class SimpleFileLoggerOptions
    {
        public string LogFilePath { get; set; }
        public bool Overwrite { get; set; } = true;
    }
}
