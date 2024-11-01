namespace _8BitComputerMicroCode
{
    public class ArithmeticInstructionsBuilder : MicroCodeInstructionBuilder
    {
        public override List<int> Build()
        {
            // MATH
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { NEXT_BYTE },
                { INP_TO_A | OUT_FROM_PROG },
                { NEXT_BYTE },
                { INP_TO_B | OUT_FROM_PROG },
                { NEXT_BYTE }
            }));
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { GET_ZERO | INP_TO_RAM_PAGE | OUT_FROM_ALU },
                { NEXT_BYTE },
                { INP_TO_RAM_ADDR | OUT_FROM_PROG },
                { INP_TO_A | OUT_FROM_RAM },
                { NEXT_BYTE },
                { INP_TO_RAM_ADDR | OUT_FROM_PROG },
                { INP_TO_B | OUT_FROM_RAM },
                { NEXT_BYTE }
            }));
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { GET_ZERO | INP_TO_RAM_PAGE | OUT_FROM_ALU },
                { NEXT_BYTE },
                { INP_TO_A | OUT_FROM_PROG },
                { NEXT_BYTE },
                { INP_TO_RAM_ADDR | OUT_FROM_PROG },
                { INP_TO_B | OUT_FROM_RAM },
                { NEXT_BYTE }
            }));
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { GET_ZERO | INP_TO_RAM_PAGE | OUT_FROM_ALU },
                { NEXT_BYTE },
                { INP_TO_RAM_ADDR | OUT_FROM_PROG },
                { INP_TO_A | OUT_FROM_RAM },
                { NEXT_BYTE },
                { INP_TO_B | OUT_FROM_PROG },
                { NEXT_BYTE }
            }));

            // ADD (Addition)
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { NEXT_BYTE },
                { LOAD_ARG },
                { ALU_ADD | FLGS | CARY | INP_FROM_ARG | OUT_FROM_ALU },
                { NEXT_BYTE  }
            }));

            // ADDC (Addition with Carry)
            InstructionList.AddRange(BuildInstructionSet_CarryFlagDependent(new List<int>
            {
                { NEXT_BYTE },
                { LOAD_ARG },
                { ALU_ADD | FLGS | CARY | INP_FROM_ARG | OUT_FROM_ALU },
                { NEXT_BYTE }
            }, new List<int>
            {
                { NEXT_BYTE },
                { LOAD_ARG },
                { ALU_ADD | FLGS | INP_FROM_ARG | OUT_FROM_ALU },
                { NEXT_BYTE }
            }, true, false));

            // INC (Increment)
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { NEXT_BYTE },
                { LOAD_ARG },
                { INP_FROM_ARG | FLGS | OUT_FROM_ALU },
                { NEXT_BYTE }
            }, true, false));

            // SUB (Subtraction)
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { NEXT_BYTE },
                { LOAD_ARG },
                { ALU_SUB | FLGS | INP_FROM_ARG | OUT_FROM_ALU },
                { NEXT_BYTE }
            }));

            // SUBB (Subtraction with Borrow)
            InstructionList.AddRange(BuildInstructionSet_CarryFlagDependent(new List<int>
            {
                { NEXT_BYTE },
                { LOAD_ARG },
                { ALU_SUB | FLGS | INP_FROM_ARG | OUT_FROM_ALU },
                { NEXT_BYTE }
            }, new List<int>
            {
                { NEXT_BYTE },
                { LOAD_ARG },
                { ALU_SUB | FLGS | CARY | INP_FROM_ARG | OUT_FROM_ALU },
                { NEXT_BYTE }
            }));

            // DEC (Decrement)
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { NEXT_BYTE },
                { LOAD_ARG },
                { ALU_DEC | FLGS | INP_FROM_ARG | OUT_FROM_ALU },
                { NEXT_BYTE }
            }));

            // SHFL (Shift Left)
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { NEXT_BYTE },
                { LOAD_ARG },
                { ALU_SHFT | FLGS | CARY | INP_FROM_ARG | OUT_FROM_ALU },
                { NEXT_BYTE }
            }));

            // ROTL (Rotate Left)
            InstructionList.AddRange(BuildMicroInstruction_WithDuplicates(FETCH, 16));
            InstructionList.AddRange(BuildMicroInstruction_WithDuplicates(NEXT_BYTE, 16));
            InstructionList.AddRange(BuildMicroInstruction_WithDuplicates(LOAD_ARG, 16));
            InstructionList.AddRange(BuildMicroInstruction_WithDuplicates(ALU_SHFT | CARY | FLGS, 16));
            InstructionList.AddRange(BuildMicroInstruction_WithDuplicates(ALU_SHFT | CARY | FLGS | INP_FROM_ARG | OUT_FROM_ALU, 8));
            InstructionList.AddRange(BuildMicroInstruction_WithDuplicates(ALU_SHFT | FLGS | INP_FROM_ARG | OUT_FROM_ALU, 8));
            InstructionList.AddRange(BuildMicroInstruction_WithDuplicates(NEXT_BYTE, 16));
            InstructionList.AddRange(BuildMicroInstruction_WithDuplicates(END_OP, 16));

            InstructionList.AddRange(GetFillerInstructions(InstructionList.Count));

            return InstructionList;
        }
    }
}
