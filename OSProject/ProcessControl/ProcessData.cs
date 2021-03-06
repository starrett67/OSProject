﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSProject.ProcessControl
{
    class ProcessData
    {
        //variables
        //Process identification number
        private int ProcessId;
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

        public static int PROCESS_READY = 0;
        public static int PROCESS_WAIT = 1;
        public static int PROCESS_RUNNING = 2;
        public static int PROCESS_TERMINATED = 3;
        public static int PROCESS_DEFAULT = 4;

        //default constructor
        public ProcessData()
        {
            ProcessId = -1;
            ProcessDiskStart = -1;
            ProcessMemoryStart = -1;
            ProcessCount = 0;
            ProcessBaseRegister = -1;
            DataDiskSize = -1;
            DataMemoryStart = -1;
            DataDiskSize = 0;
            DataMemoryStart = -1;
            JobPriority = -1;
            InputBuffer = -1;
            OutputBuffer = -1;
            TempBuffer = -1;
            ProcessState = 4;
            State = new State();
        }
        public int GetProcessId()
        {
            return ProcessId;
        }
        public void SetProcessId(int id)
        {
            ProcessId = id;
        }
        public int GetProcessDiskStart()
        {
            return ProcessDiskStart;
        }
        public void SetProcessDiskStart(int start)
        {
            ProcessDiskStart = start;
        }
        public int GetProcessMemoryStart()
        {
            return ProcessMemoryStart;
        }
        public void SetProcessMemoryStart(int start)
        {
            ProcessMemoryStart = start;
        }
        public int GetDataDiskStart()
        {
            return DataDiskStart;
        }
        public void SetDataDiskStart(int start)
        {
            DataDiskStart = start;
        }
        public int GetDataMemoryStart()
        {
            return DataMemoryStart;
        }
        public void SetDataMemoryStart(int start)
        {
            DataMemoryStart = start;
        }
        public int GetProcessCount()
        {
            return ProcessCount;
        }
        public void SetProcessCount(int count)
        {
            ProcessCount = count;
        }
        public int GetProcessBaseRegister()
        {
            return ProcessBaseRegister;
        }
        public void SetProcessBaseRegister(int register)
        {
            ProcessBaseRegister = register;
        }
        public int GetDataDiskSize()
        {
            return DataDiskSize;
        }
        public void SetDataDiskSize(int size)
        {
            DataDiskSize = size;
        }
        public int GetJobPriority()
        {
            return JobPriority;
        }
        public void setJobPriority(int priority)
        {
            JobPriority = priority;
        }
        public void SetProcessState(int state)
        {
            ProcessState = state;
        }
        public int GetProcessState()
        {
            return ProcessState;
        }
        public State GetState()
        {
            return State;
        }
        public void SetState(State state)
        {
            State = state;
        }
        public void SetInputBuffer(int bufferSize)
        {
            InputBuffer = bufferSize;
        }
        public int GetInputBuffer()
        {
            return InputBuffer;
        }
        public void SetOutputBuffer(int bufferSize)
        {
            OutputBuffer = bufferSize;
        }
        public int GetOutputBuffer()
        {
            return OutputBuffer;
        }
        public void SetTempBuffer(int bufferSize)
        {
            TempBuffer = bufferSize;
        }
        public int GetTempBuffer()
        {
            return TempBuffer;
        }
    }
}
