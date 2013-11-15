using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using OSProject.Memory;
using OSProject.ControlUnit;

namespace OSProject.ProcessControl
{
    class Scheduler
    {
        private static Scheduler scheduler;
        private int CurrentJob;
        private int CurrentDiskLoc;
        private int CurretRamLoc;
        private int RamStartLoc;
        private int ProcessRamStart;
        private int DataRamStart;
        private int InstructionCount;
        private int DataCount;
        private ProcessData tempProcessData;
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Scheduler GetInstance()
        {
            if (scheduler == null)
            {
                scheduler = new Scheduler();
            }
            return scheduler;
        }

        private Scheduler()
        {
            CurrentJob = 1;
        }
        public void LongTermScheduler(int ramStartLocation)
        {
            //Find next process so we can add it to ready queue
            CurrentJob = PCB.GetInstance().getNextProcess().GetProcessId();
            CurretRamLoc = ramStartLocation;
            RamStartLoc = ramStartLocation;
            tempProcessData = PCB.GetInstance().getProcessData(CurrentJob);
            CurrentDiskLoc = tempProcessData.GetProcessDiskStart();
            if (RamStartLoc + tempProcessData.GetProcessCount() + tempProcessData.GetDataDiskSize()
                + tempProcessData.GetOutputBuffer() + tempProcessData.GetInputBuffer()
                  + tempProcessData.GetTempBuffer() < 1024)
            {
                ReadyQueue.GetInstance().addJob(CurrentJob);
                PCB.GetInstance().getProcessData(CurrentJob).SetProcessState(ProcessData.PROCESS_READY);
                PCB.GetInstance().getProcessData(CurrentJob).SetProcessMemoryStart(Ram.GetInstance().WriteLocation(
                    HardDrive.GetInstance().GetDataFromLocation(CurrentDiskLoc), CurretRamLoc));
                CurretRamLoc++;
                CurrentDiskLoc++;
                InstructionCount = tempProcessData.GetProcessCount();
                //place the rest of the process's in the ram
                while(CurrentDiskLoc < tempProcessData.GetProcessDiskStart() + InstructionCount)
                {
                    Ram.GetInstance().Write(HardDrive.GetInstance().GetDataFromLocation(CurrentDiskLoc));
                    CurrentDiskLoc++;
                    CurretRamLoc++;
                }
                //now move on to data
                CurrentDiskLoc = tempProcessData.GetDataDiskStart();
                PCB.GetInstance().getProcessData(CurrentJob).SetDataMemoryStart(CurretRamLoc);
                int DataCount, DataTempBuffer;
                DataCount = tempProcessData.GetDataDiskSize();
                DataTempBuffer = tempProcessData.GetTempBuffer();
                while (CurrentDiskLoc < (tempProcessData.GetDataDiskStart() + DataCount))
                {
                    Ram.GetInstance().Write(HardDrive.GetInstance().GetDataFromLocation(CurrentDiskLoc));
                    CurrentDiskLoc++;
                    CurretRamLoc++;
                }
                while (DataTempBuffer > 0)
                {
                    Ram.GetInstance().Write("0x00000000");
                    DataTempBuffer--;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("Scheduluer: Ram out of bounds");
            }
        }
        public bool ShortTermScheduler()
        {
            int nextJob = ReadyQueue.GetInstance().getHighestPriority();
            if (nextJob > 0)
            {
                Console.Out.WriteLine(nextJob);
                Dispatcher.GetInstance().dispatchProcess(nextJob);
                ReadyQueue.GetInstance().removeFromReadyQueue(nextJob);
                PCB.GetInstance().getProcessData(nextJob).SetProcessState(ProcessData.PROCESS_RUNNING);
                return true;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Invalid Process Id");
            }
        }
    }
}