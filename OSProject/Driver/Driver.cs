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
            int totalRequiredRamSpace;
            HardDrive disk = HardDrive.GetInstance();
            PCB processControl = PCB.GetInstance();
            Ram memory = Ram.GetInstance();
            Scheduler scheduler = Scheduler.GetInstance();
            Loader.Loader fileLoader = Loader.Loader.GetInstance();
            ReadyQueue rQue = ReadyQueue.GetInstance();
            fileLoader.Load();
            while (!processControl.isDone())
            {
                ProcessData temp = processControl.getCurrentProcess();
                totalRequiredRamSpace = temp.GetProcessCount() + temp.GetDataDiskSize() + temp.GetTempBuffer();
                int ramStartLoc = memory.GetAvailableSlotStartLocation(totalRequiredRamSpace);
                if (ramStartLoc != -1)
                {
                    //we have ram memory to work with let the long term scheduler bring a process into memory
                    scheduler.LongTermScheduler(ramStartLoc);
                }
                else
                {
                    //we have brought as many jobs into memory as possible, start the short term scheduler
                    if (!ReadyQueue.GetInstance().isEmpty() && !Dispatcher.GetInstance().isBusy())
                    {
                        scheduler.ShortTermScheduler();
                    }
                    if (Dispatcher.GetInstance().isBusy())
                    {
                        CPU.GetInstance().run();
                    }
                }
            }
            return 0;
        }
    }
}
