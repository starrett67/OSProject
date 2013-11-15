using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSProject.ControlUnit
{
    class InstructionIO : Instruction
    {
        public InstructionIO(Instruction instruction)
        {
            this.opCode = instruction.opCode;
            this.format = instruction.format;
            this.parameter = instruction.parameter;
            this.Reg1 = Convert.ToInt32(instruction.parameter.Substring(0, 4), 2);
            this.Reg2 = Convert.ToInt32(instruction.parameter.Substring(4, 4), 2);
            this.Address = Convert.ToInt32(instruction.parameter.Substring(8, instruction.parameter.Length - 8), 2);
        }
    }
}
