using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSProject.ProcessControl
{
    class ProcessData
    {
        //variables
        //Process identification number
        private int PrcessId;
        //Instruction disk location and length
        private int ProcessDiskStart;
        //Instruction memory location and length
        private int ProcessMemoryStart;
        //Given job instruction count
        private int ProcessCount;
        //Instruction base register
        private int ProcessBaseRegister;
        //Data dist start location
        private int DataDiskStart;
        //Data memory start location
        private int DataMemoryStart;
        //Data disk size
        private int DataDiskSize;
        //Given job priority
        private int JobPriority;

        //Number of words in each buffer
        private int InputBuffer;
        private int OutputBuffer;
        private int TempBuffer;

        //Process Status
        private enum Status
        {
            read,
            waiting,
            running,
            terminated,
            created,
        }
        private int ProcessState;

        private State State;

        public sealed static int PROCESS_READY = 0;
        public sealed static int PROCESS_WAIT = 1;
        public sealed static int PROCESS_RUNNING = 2;
        public sealed static int PROCESS_TERMINATED = 3;
        public sealed static int PROCESS_DEFAULT = 4;

        //default constructor
        public ProcessData()
        {
        }
    }
}
