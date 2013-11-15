using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using OSProject.ProcessControl;

namespace OSProject.ControlUnit
{
    class Dispatcher
    {
        private static Dispatcher dispatcher;
        private bool busy;
        public ProcessData currentProcess;
        private int instruction;
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Dispatcher GetInstance()
        {
            if (dispatcher == null)
            {
                dispatcher = new Dispatcher();
            }
            return dispatcher;
        }

        private Dispatcher()
        {
            busy = false;
            currentProcess = null;
            instruction = -1;
        }

        public bool isBusy()
        {
            return busy;
        }

        public void terminate()
        {
            busy = false;
            OSProject.Memory.Ram ram = OSProject.Memory.Ram.GetInstance();
            int start = currentProcess.GetProcessMemoryStart();
            int end = currentProcess.GetDataMemoryStart() + currentProcess.GetDataDiskSize() + currentProcess.GetTempBuffer();
            OSProject.Memory.Ram.GetInstance().WipeRam(start, end);
        }

        public void dispatchProcess(int process)
        {
            currentProcess = PCB.GetInstance().getProcessData(process);
            instruction = currentProcess.GetProcessMemoryStart();
            busy = true;
        }

        public int getInstruction()
        {
            if (currentProcess != null )
            {
                instruction++;
                return instruction - 1;
            }
            else
            {
                throw new InvalidOperationException("Dispatcher hasnt been assigned a process yet");
            }
        }

        public void setCurrentInstruction(int nInstruction)
        {
            int start = currentProcess.GetProcessMemoryStart();
            int end = currentProcess.GetProcessMemoryStart() + currentProcess.GetProcessCount();
            if ((nInstruction > start) && (nInstruction < end))
            {
                instruction = nInstruction;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Instruction out of bounds");
            }
        }
         
    }
}
