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
        private ProcessData currentProcess;
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

        public void setBusy(bool val)
        {
            busy = val;
        }

        public void dispatchProcess(int process)
        {
            currentProcess = PCB.GetInstance().getProcessData(process);
            instruction = currentProcess.GetProcessMemoryStart();
            busy = true;
            CPU.GetInstance().loadCache(currentProcess);
        }

        /*
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
            if ((nInstruction > currentProcess.GetProcessMemoryStart()) &&
                (nInstruction < currentProcess.GetProcessMemoryStart() + currentProcess.GetProcessCount()))
            {
                instruction = nInstruction;
            }
        }
         */
    }
}
