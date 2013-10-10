using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace OSProject.ProcessControl
{
    class ReadyQueue
    {
        private static ReadyQueue queue;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ReadyQueue GetInstance()
        {
            if (queue == null)
            {
                queue = new ReadyQueue();
            }
            return queue;
        }

        private ReadyQueue()
        {
        }
    }
}
