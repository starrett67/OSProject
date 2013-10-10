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
            HardDrive.GetInstance();
            PCB.GetInstance();
            Dispatcher.GetInstance();
            Ram.GetInstance();
            CPU.GetInstance();
            Loader.Loader.GetInstance();
            return 0;
        }
    }
}
