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
            ProcessData temp = new ProcessData();
        }

        public int addJob(string line)
        {
            String[] info = line.Split(' ');
            ProcessData temp = new ProcessData();
            temp.SetProcessId(int.Parse(info[2], System.Globalization.NumberStyles.HexNumber));
            temp.SetProcessCount(int.Parse(info[3], System.Globalization.NumberStyles.HexNumber));
            temp.setJobPriority(int.Parse(info[4], System.Globalization.NumberStyles.HexNumber));
            dataList.Add(temp);
            return int.Parse(info[2], System.Globalization.NumberStyles.HexNumber);
        }
        public void addData(string line, int jobId)
        {
            ProcessData temp = getProcessData(jobId);
            String[] info = line.Split(' ');
            temp.SetInputBuffer(int.Parse(info[2], System.Globalization.NumberStyles.HexNumber));
            temp.SetOutputBuffer(int.Parse(info[3], System.Globalization.NumberStyles.HexNumber));
            temp.SetTempBuffer(int.Parse(info[4], System.Globalization.NumberStyles.HexNumber));
                       
        }
        public ProcessData getProcessData(int jobId)
        {
            ProcessData temp = null;
            foreach (ProcessData data in dataList)
            {
                if (data.GetProcessId() == jobId)
                {
                    temp = data;
                }
            }
            if (temp == null)
            {
                throw new ArgumentException("Something went wrong, processdata not retrieved");
            }
            return temp;
        }
    }
}
