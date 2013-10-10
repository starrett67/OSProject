using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace OSProject.ProcessControl
{
    class PCB
    {
        private static PCB pcb;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public PCB GetInstance()
        {
            if (pcb == null)
            {
                pcb = new PCB();
            }
            return pcb;
        }

        private PCB()
        {
        }
    }
}
