using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.IO;

namespace OSProject.Loader
{
    class Loader
    {
        private int JobId;
        private int JobLocation;
        private int count;
        private bool FirstJobLine;
        private bool FirstDataLine;
        private static Loader loader;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Loader GetInstance()
        {
            if (loader == null)
            {
                loader = new Loader();
            }
            return loader;
        }

        private Loader()
        {
            JobId = -1;
            JobLocation = -1;
            count = 0;
            FirstJobLine = false;
            FirstDataLine = false;
        }

        public void Load()
        {
            try
            {
                StreamReader file = new StreamReader("datafile2.txt");
                String line = file.ReadLine();
                while (!String.IsNullOrEmpty(line))
                {
                    if (line.Contains("JOB"))
                    {
                        //add job to pcb
                        FirstJobLine = true;
                    }
                    else if (line.Contains("Data"))
                    {
                        //add data to pcb
                        count = 0;
                        FirstDataLine = true;
                    }
                    else if (line.Contains("END"))
                    {
                        JobId = -1;
                    }
                    else
                    {
                        //save to harddrive
                        if (FirstJobLine)
                        {
                            //set process disk start location
                            FirstJobLine = false;
                        }
                        else if (FirstDataLine)
                        {
                            //set data disk start location
                            FirstDataLine = false;
                        }
                        count++;
                    }
                    line = file.ReadLine();
                }
                file.Close();
            }
            catch (IOException ex)
            {
                System.Console.Out.WriteLine(ex.StackTrace);
                throw new IOException(ex.StackTrace);
            }
        }
    }
}
