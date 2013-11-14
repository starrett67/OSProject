using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSProject.ControlUnit
{
    class InstructionArithmetic : Instruction
    {
        public String SReg1;
        public String SReg2;
        public String DReg;

        public InstructionArithmetic()
        {
            this.SReg1 = this.parameter.Substring(0, 4);
            this.SReg2 = this.parameter.Substring(4, 4);
            this.DReg = this.parameter.Substring(8, 4);
        }
    }
}
