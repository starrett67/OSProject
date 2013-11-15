using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSProject.ControlUnit
{
    class InstructionJump : Instruction
    {
        public int Address;
        public InstructionJump(Instruction instruction)
        {
            this.opCode = instruction.opCode;
            this.format = instruction.format;
            this.parameter = instruction.parameter;
            this.Address = Convert.ToInt32(instruction.parameter, 2);
        }
    }
}
