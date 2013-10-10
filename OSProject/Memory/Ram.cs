using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace OSProject.Memory
{
    class Ram
    {
        //variables
        private String[] ram;               //memory object to save data in
        private int nextMemoryLocation;     //Current writing index for ram array
        private static Ram RAM;             //Ram object

        private Ram()
        {
            ram = new String[1024];
            nextMemoryLocation = 0;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Ram GetInstance()
        {
            if (RAM == null)
            {
                RAM = new Ram();
            }
            return RAM;
        }

        //read ram
        public String Read(int index)
        {
            if (index > 0 && index < 1024)
            {
                return ram[index];
            }
            else
            {
                throw new ArgumentException("memory location does not exist");
            }
        }

        public bool Write(String data)
        {
            if (data != null)
            {
                if (nextMemoryLocation < 1024 && nextMemoryLocation > -1)
                {
                    ram[nextMemoryLocation] = data;
                    nextMemoryLocation++;
                    return true;
                }
                else
                {
                    throw new ArgumentException("next write location is out of bounds");
                }
            }
            else
            {
                throw new ArgumentException("cannot write null data");
            }
            return false;
        }

        public bool WriteLocation(String data, int loc)
        {
            if (data != null && loc > 0 && loc < 1024)
            {
                ram[loc] = data;
                return true;
            }
            else
            {
                throw new ArgumentException("either data is null or write location is invalid. Location : " + loc);
            }
            return false;
        }

        public string MemoryDump()
        {
            String memoryDump = "Ram Memory Dump: ";
            for (int i = 0; i < ram.Length; i++)
            {
                memoryDump += "\n\tLocation[" + i + "]:\t" + ram[i];
            }
            return memoryDump;
        }

        public void WipeRam()
        {
            for (int i = 0; i < ram.Length; i++)
            {
                ram[i] = "";
            }
        }

        public int GetFreeSpace()
        {
            return ram.Length - nextMemoryLocation;
        }

        public int GetCurrentMemoryLocation()
        {
            return nextMemoryLocation;
        }

    }
}
