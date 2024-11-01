namespace _8BitComputerMicroCode
{
    public class StackInstructionsBuilder : MicroCodeInstructionBuilder
    {
        public override List<int> Build()
        {
            // PUSH
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { DEC_STACK },
                { LOAD_ARG },
                { INP_TO_STACK | OUT_FROM_ARG },
                { NEXT_BYTE }
            }));
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { DEC_STACK },
                { NEXT_BYTE },
                { INP_TO_STACK | OUT_FROM_PROG },
                { NEXT_BYTE }
            }));
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { DEC_STACK },
                { GET_ZERO | INP_TO_RAM_PAGE | OUT_FROM_ALU },
                { NEXT_BYTE },
                { INP_TO_RAM_ADDR | OUT_FROM_PROG },
                { INP_TO_STACK | OUT_FROM_RAM },
                { NEXT_BYTE }
            }));

            // POP
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { NEXT_BYTE },
                { LOAD_ARG },
                { INP_FROM_ARG | OUT_FROM_STACK },
                { INC_STACK },
                { NEXT_BYTE }
            }));
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { GET_ZERO | INP_TO_RAM_PAGE | OUT_FROM_ALU },
                { NEXT_BYTE },
                { INP_TO_RAM_ADDR | OUT_FROM_PROG },
                { INP_TO_RAM | OUT_FROM_STACK },
                { INC_STACK },
                { NEXT_BYTE }
            }));

            // SWAP (Swap value in A and B)
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { DEC_STACK },
                { INP_TO_STACK | OUT_FROM_A },
                { INP_TO_A | OUT_FROM_B },
                { INP_TO_B | OUT_FROM_STACK },
                { INC_STACK },
                { NEXT_BYTE }
            }));

            // STSH (Stash AB register values)
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { DEC_STACK },
                { INP_TO_STACK | OUT_FROM_A },
                { DEC_STACK },
                { INP_TO_STACK | OUT_FROM_B },
                { NEXT_BYTE }
            }));

            // RSTR (Restore AB register values)
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { INP_TO_B | OUT_FROM_STACK },
                { INC_STACK },
                { INP_TO_A | OUT_FROM_STACK },
                { INC_STACK },
                { NEXT_BYTE }
            }));

            // NPSH (Step the stack counter without pushing)
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { DEC_STACK },
                { NEXT_BYTE }
            }));

            // NPOP (Step the stack counter without popping)
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { INC_STACK },
                { NEXT_BYTE }
            }));

            // CALL (Call subroutine)
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { DEC_STACK },
                { INP_TO_STACK | OUT_FROM_A },
                { DEC_STACK },
                { INP_TO_STACK | OUT_FROM_B },
                { DEC_STACK },
                { INP_TO_STACK | OUT_FROM_PCH },
                { DEC_STACK },
                { INP_TO_STACK | OUT_FROM_PCL },
                { NEXT_BYTE },
                { INP_TO_PCH | OUT_FROM_PROG },
                { NEXT_BYTE },
                { INP_TO_PCL | OUT_FROM_PROG },
                { NEXT_BYTE }
            }));

            // RTRN (Return from subroutine)
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { INP_TO_PCL | OUT_FROM_STACK },
                { INC_STACK },
                { INP_TO_PCH | OUT_FROM_STACK },
                { INC_STACK },
                { INP_TO_B | OUT_FROM_STACK },
                { INC_STACK },
                { INP_TO_A | OUT_FROM_STACK },
                { INC_STACK },
                { NEXT_BYTE }
            }));

            InstructionList.AddRange(GetFillerInstructions(InstructionList.Count));

            return InstructionList;
        }
    }
}
