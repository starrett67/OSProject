using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSProject.ControlUnit
{
    class InstructionArithmetic : Instruction
    {

        public InstructionArithmetic(Instruction instruction)
        {
            this.opCode = instruction.opCode;
            this.format = instruction.format;
            this.parameter = instruction.parameter;
            this.SReg1 = Convert.ToInt32(instruction.parameter.Substring(0, 4), 2);
            this.SReg2 = Convert.ToInt32(instruction.parameter.Substring(4, 4), 2);
            this.DReg = Convert.ToInt32(instruction.parameter.Substring(8, 4), 2); ;
        }
    }
}
