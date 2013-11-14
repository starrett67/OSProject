using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSProject.ControlUnit.Instructions
{
    class InstructionIO : Instruction
    {
        public String Reg1;
        public String Reg2;
        public String Address;

        public InstructionIO()
        {
            this.Reg1 = this.parameter.Substring(0, 4);
            this.Reg2 = this.parameter.Substring(4, 4);
            this.Address = this.parameter.Substring(8, this.parameter.Length);
        }
    }
}
