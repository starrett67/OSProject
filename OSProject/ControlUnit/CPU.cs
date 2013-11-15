using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using OSProject.ProcessControl;
using OSProject.Memory;

namespace OSProject.ControlUnit
{
    class CPU
    {
        private static CPU cpu;
        private int[] register;
        private String programCache;
        private Instruction currentInstruction;

        private const int Accumulator = 0;
        private const int ZeroReg = 1;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static CPU GetInstance()
        {
            if (cpu == null)
            {
                cpu = new CPU();
            }
            return cpu;
        }

        private CPU()
        {
            register = new int[16];
            programCache = "";
        }

        private void fetch()
        {
            int instructionAddress = Dispatcher.GetInstance().getInstruction();
            programCache = Ram.GetInstance().Read(instructionAddress);
        }

        private string hextobinary(char hexChar)
        {
            string binVal;
            binVal = Convert.ToString(Convert.ToInt32(hexChar.ToString(), 16), 2).PadLeft(4, '0');
            return binVal;
        }

        private void decode()
        {
            int length;
            Console.Out.WriteLine(programCache.Trim());
            Dispatcher dis = Dispatcher.GetInstance();
            Ram rm = Ram.GetInstance();
            String hexValue = programCache.Trim().Substring(2);
            String binary = "";
            for (int i = 0; i < hexValue.Length; i++)
            {
                binary += hextobinary(hexValue[i]);
            }
            currentInstruction = new Instruction(binary);
            length = binary.Length;
        }

        private void execute()
        {
            string format = currentInstruction.format;
            if (currentInstruction.opCode.Equals("010011"))//nop
            {
                //do nothing
            }
            else if (format.Equals("00"))
            {
                currentInstruction = new InstructionArithmetic(currentInstruction);
                Arithmetic();
            }
            else if (format.Equals("01"))
            {
                currentInstruction = new InstructionBranchAndImmediate(currentInstruction);
                BranchAndImmidiate();
            }
            else if (format.Equals("10"))
            {
                currentInstruction = new InstructionJump(currentInstruction);
                Jump();
            }
            else if (format.Equals("11"))
            {
                currentInstruction = new InstructionIO(currentInstruction);
                InputOutput();
            }
            else
            {
                throw new InvalidOperationException("Instruction format is invalid");
            }
        }

        private void Arithmetic()
        {
            String opCode = currentInstruction.opCode;
            int sReg1 = currentInstruction.SReg1;
            int sReg2 = currentInstruction.SReg2;
            int dReg = currentInstruction.DReg;
            switch (opCode)
            {
                case "000100":  //MOV
                    register[sReg1] = register[sReg2];
                    break;
                case "000101":  //ADD
                    register[dReg] = register[sReg1] + register[sReg2];
                    break;
                case "000110":  //SUB
                    register[dReg] = register[sReg1] - register[sReg2];
                    break;
                case "000111":  //MUL
                    register[dReg] = register[sReg1] * register[sReg2];
                    break;
                case "001000":  //DIV
                    register[dReg] = register[sReg1] / register[sReg2];
                    break;
                case "001001":  //AND
                    if (register[sReg1] != 0 && register[sReg2] != 0)
                        register[dReg] = 1;
                    else
                        register[dReg] = 0;
                    break;
                case "001010":  //OR
                    if (register[sReg1] == 0 && register[sReg2] == 0)
                        register[dReg] = 0;
                    else
                        register[dReg] = 1;
                    break;
                case "010000":  //SLT
                    if (register[sReg1] < register[sReg2])
                        register[dReg] = 1;
                    else
                        register[dReg] = 0;
                    break;
                case default:
                    break;
            }
        }

        private void BranchAndImmidiate()
        {
            String opCode = currentInstruction.opCode;
            int bReg = currentInstruction.BReg;
            int dReg = currentInstruction.DReg;
            int address = currentInstruction.Address;

            switch (opCode)
            {
                case "001011":  //MOVI
                    register[dReg] = register[bReg];
                    break;
                case "001100":  //ADDI
                    register[dReg] = register[bReg] + address;
                    break;
                case "001101":  //MULI
                    register[dReg] = register[bReg] * address;
                    break;
                case "001110":  //DIVI
                    register[dReg] = register[bReg] / address;
                    break;
                case "001111":  //LDI
                    register[dReg] = address;
                    break;
                case "010001":  //SLTI
                    if (register[bReg] < address)
                        register[dReg] = 1;
                    else
                        register[dReg] = 0;
                    break;
                case "010101":  //BEQ
                    if (register[dReg] == register[bReg])
                        Dispatcher.GetInstance().setCurrentInstruction(address);
                    break;
                case "010110":  //BNE
                    if (register[dReg] != register[bReg])
                        Dispatcher.GetInstance().setCurrentInstruction(address);
                    break;
                case "010111":  //BEZ
                    if (register[dReg] == register[ZeroReg])
                        Dispatcher.GetInstance().setCurrentInstruction(address);
                    break;
                case "011000":  //BNZ
                    if (register[dReg] != register[ZeroReg])
                        Dispatcher.GetInstance().setCurrentInstruction(address);
                    break;
                case "011001":  //BGZ
                    if (register[dReg] > register[ZeroReg])
                        Dispatcher.GetInstance().setCurrentInstruction(address);
                    break;
                case "011010":  //BLZ
                    if (register[dReg] < register[ZeroReg])
                        Dispatcher.GetInstance().setCurrentInstruction(address);
                    break;
            }
        }

        private void Jump()
        {
            String opCode = currentInstruction.opCode;
            int address = currentInstruction.Address;
            switch (opCode)
            {
                case "010010":  //HLT
                    //End of program
                    Dispatcher.GetInstance().terminate();
                    break;
                case "010100":  //JMP
                    Dispatcher.GetInstance().setCurrentInstruction(address);
                    break;
            }
        }

        private void InputOutput()
        {
            String opCode = currentInstruction.opCode;
            int reg1 = currentInstruction.Reg1;
            int reg2 = currentInstruction.Reg2;
            int address = currentInstruction.Address;
            string hex = "0";
            ProcessData process = Dispatcher.GetInstance().currentProcess;
            switch (opCode)
            {
                case "000000":  //RD
                    //register[reg1] Convert.ToInt32(Ram.GetInstance().Read(
                    if (reg1 != 0 && reg2 != 0)
                    {
                        hex = Ram.GetInstance().Read(process.GetDataMemoryStart() + (reg2));
                        hex = hex.Substring(2);
                    }
                    else
                    {
                        hex = Ram.GetInstance().Read(process.GetProcessMemoryStart() + (address / 4));
                        hex = hex.Substring(2);
                    }
                    register[Accumulator] += int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
                    break;
                case "000001":  //WR
                    String write = "0x" + register[Accumulator].ToString("X8");
                    break;
            }
        }

        /*
        public void loadCache(ProcessControl.ProcessData currentProcess)
        {
            int processMemoryStart = currentProcess.GetProcessDiskStart();
            int processMemoryEnd = currentProcess.GetProcessDiskStart() + currentProcess.GetProcessCount();
            int i = processMemoryStart;
            while (i < processMemoryEnd)
            {
                ProgramCache.Add(Ram.GetInstance().Read(i));
                i++;
            }
        }
        */

        public void run()
        {
            fetch();
            decode();
            execute();
        }
    }
}
