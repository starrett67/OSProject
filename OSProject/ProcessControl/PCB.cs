using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace OSProject.ProcessControl
{
    class PCB
    {
        private List<ProcessData> dataList = new List<ProcessData>();
        private static PCB pcb;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static PCB GetInstance()
        {
            if (pcb == null)
            {
                pcb = new PCB();
            }
            return pcb;
        }

        private PCB()
        {
            ProcessData temp = new ProcessData;
            temp.
        }
    }
}
