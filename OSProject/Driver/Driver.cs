using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OSProject.ControlUnit;
using OSProject.Memory;
using OSProject.Loader;
using OSProject.ProcessControl;

namespace OSProject.Driver
{
    class Driver
    {
        public static int Main(string[] args)
        {
            HardDrive disk = HardDrive.GetInstance();
            PCB processControl = PCB.GetInstance();
            Ram memory = Ram.GetInstance();
            Loader.Loader fileLoader = Loader.Loader.GetInstance();
            fileLoader.Load();
            return 0;
        }
    }
}
