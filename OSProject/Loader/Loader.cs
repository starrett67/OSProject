using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.IO;
using OSProject.ProcessControl;
using OSProject.Memory;

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
                StreamReader file = new StreamReader("Datafile2.txt");
                String line = file.ReadLine();
                while (!String.IsNullOrEmpty(line))
                {
                    if (line.Contains("JOB"))
                    {
                        JobId = PCB.GetInstance().addJob(line);
                        FirstJobLine = true;
                    }
                    else if (line.Contains("Data"))
                    {
                        PCB.GetInstance().addData(line, JobId);
                        count = 0;
                        FirstDataLine = true;
                    }
                    else if (line.Contains("END"))
                    {
                        PCB.GetInstance().getProcessData(JobId).SetDataDiskSize(count);
                        JobId = -1;
                    }
                    else
                    {
                        HardDrive.GetInstance().Write(line);
                        if (FirstJobLine)
                        {
                            PCB.GetInstance().getProcessData(JobId).SetProcessDiskStart(HardDrive.GetInstance()
                                .CurrentUsedSpace());
                            FirstJobLine = false;
                        }
                        else if (FirstDataLine)
                        {
                            PCB.GetInstance().getProcessData(JobId).SetDataDiskStart(HardDrive.GetInstance()
                                .CurrentUsedSpace());
                            FirstDataLine = false;
                        }
                        count++;
                    }
                    line = file.ReadLine();
                }
                file.Close();
                HardDrive drive = HardDrive.GetInstance();
            }
            catch (IOException ex)
            {
                System.Console.Out.WriteLine(ex.StackTrace);
                throw new IOException(ex.Message);
            }
        }
    }
}
