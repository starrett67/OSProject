using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSProject.ControlUnit.Instructions
{
    class InstructionJump : Instruction
    {
        public String Address;
        public InstructionJump()
        {
            this.Address = this.parameter;
        }
    }
}
