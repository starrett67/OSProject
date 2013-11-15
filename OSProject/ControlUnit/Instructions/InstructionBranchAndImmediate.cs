using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSProject.ControlUnit
{
    class InstructionBranchAndImmediate : Instruction
    {
        public InstructionBranchAndImmediate(Instruction instruction)
        {
            this.opCode = instruction.opCode;
            this.format = instruction.format;
            this.parameter = instruction.parameter;
            this.BReg = Convert.ToInt32(instruction.parameter.Substring(0, 4), 2);
            this.DReg = Convert.ToInt32(instruction.parameter.Substring(4, 4), 2);
            this.Address = Convert.ToInt32(instruction.parameter.Substring(8, this.parameter.Length - 8), 2);
        }
    }
}
