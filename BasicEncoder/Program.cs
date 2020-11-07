using System;
using System.IO;
using System.Diagnostics;

namespace BasicEncoder
{
    class Program
    {
        private static string source { get; set; }
        private static string destination { get; set; }
        private static string informat { get; set; }
        private static string outformat { get; set; }
        private static string options { get; set; }
        private static string ffmpeg { get; set; }

        static void Main(string[] args)
        {
            for (int i = 0; i<args.Length-1; i++)
            {
                if (args[i] == "-i")
                {
                    source = args[++i];
                }
                else if (args[i] == "-o")
                {
                    destination = args[++i];
                }
                else if (args[i] == "-if")
                {
                    informat = args[++i];
                }
                else if (args[i] == "-of")
                {
                    outformat = args[++i];
                }
                else if (args[i] == "-opt")
                {
                    options = args[++i];
                }
                else if (args[i] == "-ffmpeg")
                {
                    ffmpeg = args[++i];
                }
            }


            if (source == destination || source == null || destination == null)
            {
                return;
            }

            if (outformat == null)
            {
                outformat = "opus";
            }
            if (informat == null)
            {
                informat = "wav";
            }
            if (options == null)
            {
                options = "";
            }
            if (ffmpeg == null)
            {
                ffmpeg = "ffmpeg.exe";
            }


            try
            {
                Console.WriteLine($"Starting convertion from {informat} to {outformat}.");
                foreach (string sourceFilePath in Directory.EnumerateFiles(source, $"*.{informat}", SearchOption.AllDirectories))
                {
                    string relPath = Path.GetRelativePath(source, sourceFilePath);
                    string dirStruct = Path.GetDirectoryName(relPath);
                    string absDestPath = Path.Combine(destination, dirStruct);
                    string destFilePath = Path.Combine(destination, Path.ChangeExtension(relPath, outformat));

                    Directory.CreateDirectory(absDestPath);
                    //Console.WriteLine(sourceFilePath);
                    //Console.WriteLine(relPath);
                    //Console.WriteLine(dirStruct);
                    //Console.WriteLine(absDestPath);
                    //Console.WriteLine(destFilePath);
                    if (!File.Exists(destFilePath))
                    {
                        Convert($"-hide_banner -i {sourceFilePath} {options} {destFilePath}");
                        Console.WriteLine($"Done converting file [{sourceFilePath}]");
                    }
                }
                Console.WriteLine("Done converting!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        private static void Convert(string parameters)
        {
            using (Process p = new Process())
            {
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = ffmpeg;
                p.StartInfo.Arguments = parameters;
                p.Start();
                p.WaitForExit();
            }
            return;
        }

    }
}
