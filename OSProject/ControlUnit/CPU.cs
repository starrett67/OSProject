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
            String binary = Convert.ToString(Convert.ToInt32(currentHexInstruction, 16), 2);
            currentInstruction = new Instruction(binary);
        }

        private void execute()
        {
            string format = currentInstruction.format;
            if (format.Equals("00"))
            {
                currentInstruction = new InstructionArithmetic();
                //arithmetic
            }
            else if (format.Equals("01"))
            {
                currentInstruction = new InstructionBranchAndImmediate();
                //Branch and Immediate
            }
            else if (format.Equals("10"))
            {
                currentInstruction = new InstructionJump();
                //Jump
            }
            else if (format.Equals("11"))
            {
                currentInstruction = new InstructionIO();
                //IO
            }
            else
            {
                throw new InvalidOperationException("Instruction format is invalid");
            }
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
