using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using OSProject.ProcessControl;
using OSProject.Memory;

namespace OSProject.ControlUnit
{
    class CPU
    {
        private static CPU cpu;
        private int[] register;
        private List<String> ProgramCache = new List<string>();
        private int pc;
        private String currentHexInstruction;
        private Instruction currentInstruction;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static CPU GetInstance()
        {
            if (cpu == null)
            {
                cpu = new CPU();
            }
            return cpu;
        }

        private CPU()
        {
            register = new int[20];
            pc = 0;
            currentHexInstruction = "";
        }

        private void fetch()
        {
            currentHexInstruction = ProgramCache.ElementAt(pc);
        }

        private void decode()
        {

        }

        private void execute()
        {
 
        }

        public void loadCache(ProcessControl.ProcessData currentProcess)
        {
            int processMemoryStart = currentProcess.GetProcessDiskStart();
            int processMemoryEnd = currentProcess.GetProcessDiskStart() + currentProcess.GetProcessCount();
            int i = processMemoryStart;
            while (i < processMemoryEnd)
            {
                ProgramCache.Add(Ram.GetInstance().Read(i));
                i++;
            }
        }

        public void run()
        {
            fetch();
            decode();
            execute();
            pc++;
            if (pc == ProgramCache.Count)
            {
                //we have executed the last instruction
                //things to do: Remove from ram, save status, save state, and free dispatcher
            }
        }
    }
}
