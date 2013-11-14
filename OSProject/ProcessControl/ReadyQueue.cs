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
        List<int> readyQueue = new List<int>();

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

        public void addJob(int jobID)
        {
            readyQueue.Add(jobID);
        }

        public void removeFromReadyQueue(int processID)
        {
            readyQueue.Remove(processID);
        }

        public int getHighestPriority()
        {
            int priority = 0;
            int temp = 0;
            foreach (int id in readyQueue)
            {
                temp = PCB.GetInstance().getProcessData(id).GetJobPriority();
                if (temp > priority)
                {
                    priority = id;
                }
            }
            return priority;
        }
    }
}
