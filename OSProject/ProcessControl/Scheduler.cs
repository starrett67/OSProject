using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace OSProject.ProcessControl
{
    class Scheduler
    {
        private static Scheduler scheduler;

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
        }
    }
}
