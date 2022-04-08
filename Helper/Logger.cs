using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Example.Helper
{
    
    public static class Logger{
      
        public static void Error(string message)
        {
          string filepath = "C:\\SourceCode\\log.txt";
          File.WriteAllText(filepath,$"{DateTime.Now}-ERROR:{message}");

        }
    }
}