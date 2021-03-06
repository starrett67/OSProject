﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace OSProject.Memory
{
    class HardDrive
    {
        private String[] hardDrive;
        private int nextMemoryLocation;
        private int lastMemoryLocation;

        private static HardDrive disk;

        //construct hard drive
        private HardDrive()
        {
            //need 2048 words 
            hardDrive = new String[2048];
            nextMemoryLocation = 0;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static HardDrive GetInstance()
        {
            if (disk == null)
            {
                disk = new HardDrive();
            }
            return disk;
        }

        public bool Write(String word)
        {
            if (nextMemoryLocation >= 0 && nextMemoryLocation <= 2047 && word != null)
            {
                hardDrive[nextMemoryLocation] = word;
                lastMemoryLocation = nextMemoryLocation;
                nextMemoryLocation++;
                return true;
            }
            else if (nextMemoryLocation == 2048)
            {
                throw new ArgumentOutOfRangeException("HardDisk is full");
            }
            else
            {
                throw new ArgumentException("Something went wrong, did not save");
            }
        }

        public bool WriteLocation(String word, int loc)
        {
            if (loc > 0 && loc < 2048 && word != null)
            {
                hardDrive[loc] = word;
                return true;
            }
            else
            {
                throw new ArgumentException("Something went wrong, did not save");
            }
        }

        public String GetDataFromLocation(int loc)
        {
            return hardDrive[loc];
        }

        public int CurrentUsedSpace()
        {
            return lastMemoryLocation;
        }

        public int GetSpaceRemaining()
        {
            return hardDrive.Length - nextMemoryLocation;
        }

        public String MemoryDump()
        {
            String memoryDump = "Memory Dump";
            for (int i = 0; i < nextMemoryLocation; i++)
            {
                memoryDump += "\n Memory Location: " + i + " - [" + hardDrive[i] + "]";
            }
            return memoryDump;
        }

    }
}
