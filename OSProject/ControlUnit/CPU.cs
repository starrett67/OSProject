using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace OSProject.ControlUnit
{
    class CPU
    {
        private static CPU cpu;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public CPU GetInstance()
        {
            if (cpu == null)
            {
                cpu = new CPU();
            }
            return cpu;
        }

        private CPU()
        {
        }
    }
}
