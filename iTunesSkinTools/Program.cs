//
//   iTunes skin creation tool : 
//   Inject modified resource files into iTunes\iTunes.Resources\iTunes.dll
//
//   https://github.com/Apophenic
//   
//   Copyright (c) 2015 Justin Dayer (jdayer9@gmail.com)
//   
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//   
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//   
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestris.ResourceLib;

namespace iTunesSkinTools
{
    class Program
    {
        /// <summary>
        /// Defines what operation should be performed based on cmd args
        /// </summary>
        public enum Operation
        {
            Extract, Inject
        }

        // Represents generic RT_RCDATA type in resources assembly //
        private static ResourceId RT_RCDATA = new ResourceId(10);

        // Flag to parse .dll with an English charset //
        private static UInt16 LANG_ENGLISH = (UInt16) 1033;

        // [global] vars //
        private static Operation op;
        private static string dll;
        private static string workingDir;
        private static bool isCreateBackup;

        static void Main(string[] args)
        {
            readCmdArgs(args);

            switch(op)
            {
                case (Operation.Extract):
                    Console.WriteLine("Beginning Extraction Operation...");
                    extractResources();
                    break;
                case (Operation.Inject):
                    Console.WriteLine("Beginning Injection Operation...");
                    injectResources();
                    break;
                default:
                    Console.WriteLine("Unsupported Operation");
                    break;
            }

            Console.WriteLine("Operation finished. Press any key to exit");
            Console.ReadKey();
        }

        public static void extractResources()
        {
            var resInfo = new ResourceInfo();
            resInfo.Load(dll);

            var resources = resInfo.Resources[RT_RCDATA];
            foreach(var resource in resources)
            {
                try
                {
                    byte[] data = resource.WriteAndGetBytes();
                    File.WriteAllBytes(workingDir + "\\" + resource.Name + ".png", data);
                    Console.WriteLine("Successfully extracted: " + resource.Name);
                }
                catch
                {
                    Console.WriteLine("Failed to extract: " + resource.Name);
                }
            }
        }

        public static void injectResources()
        {
            if(isCreateBackup) 
                File.Copy(dll, dll + ".bak", false);    // Don't overwrite

            DirectoryInfo d = new DirectoryInfo(workingDir);
            foreach (var file in d.GetFiles())
            {
                try
                {
                    // The resource ID MUST be parsed as a uint to replace the current resource, otherwise
                    // it will be added as a new resource which will be ignored by iTunes.
                    var res = new GenericResource
                        (RT_RCDATA, new ResourceId(uint.Parse(Path.GetFileNameWithoutExtension(file.Name))), LANG_ENGLISH);

                    res.Data = File.ReadAllBytes(file.FullName);
                    res.SaveTo(dll);

                    Console.WriteLine("Injection Successful: " + file.Name);
                }
                catch
                {
                    Console.WriteLine("Injection Failed: " + file.Name);
                }
            }
        }

        public static void readCmdArgs(string[] args)
        {
            foreach (var arg in args)
            {
                var temp = arg.Split('=');
                string flag = temp[0]; string value = temp[1];
                switch (flag)
                {
                    case ("-op"):
                        if (value == "extract")
                            op = Operation.Extract;
                        else if (value == "inject")
                            op = Operation.Inject;
                        break;

                    case ("-itunesdir"):
                        dll = value.Replace("\"", "") + "\\iTunes.Resources\\iTunes.dll";
                        break;
                    case ("-workingdir"):
                        workingDir = value.Replace("\"", "");
                        break;
                    case ("-createbackup"):
                        isCreateBackup = Boolean.Parse(value);
                        break;
                    default:
                        Console.WriteLine("Unsupported flag: " + flag);
                        break;
                }
            }
        }

    }
}
