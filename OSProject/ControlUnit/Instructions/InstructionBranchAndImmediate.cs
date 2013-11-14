using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSProject.ControlUnit
{
    class InstructionBranchAndImmediate : Instruction
    {
        public String BReg;
        public String DReg;
        public String Address;

        public InstructionBranchAndImmediate()
        {
            this.BReg = this.parameter.Substring(0, 4);
            this.DReg = this.parameter.Substring(4, 4);
            this.Address = this.parameter.Substring(8, this.parameter.Length);
        }
    }
}
