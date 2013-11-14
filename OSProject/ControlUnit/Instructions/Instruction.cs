using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSProject.ControlUnit
{
    class Instruction
    {
        public String format;
        public String opCode;
        public String parameter;

        public Instruction()
        {

        }

        public Instruction(string binaryString)
        {
            format = binaryString.Substring(0, 2);
            opCode = binaryString.Substring(2, 6);
            parameter = binaryString.Substring(8, binaryString.Length - 8);
        }
    }
}
