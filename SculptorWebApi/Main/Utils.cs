using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SculptorWebApi.Main
{
    public static class Utils
    {
        public static void WriteToFile(string fileName, string content)
        {
            StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine(content);
            sw.Close();
        }

        public static void WriteToFile(string fileName, List<string> content)
        {
            StreamWriter sw = new StreamWriter(fileName);
            content?.ForEach(line => sw.WriteLine(line));
            sw.Close();
        }

        public static async void WriteToFileAsync(string fileName, string content)
        {
            StreamWriter sw = new StreamWriter(fileName, true);
            await sw.WriteAsync(content);
            sw.Close();
        }
    }
}