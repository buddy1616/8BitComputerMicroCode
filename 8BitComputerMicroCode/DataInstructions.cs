namespace _8BitComputerMicroCode
{
    public class DataInstructionsBuilder : MicroCodeInstructionBuilder
    {
        public override List<int> Build()
        {
            // NOOP
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>{ NEXT_BYTE }, true));

            // HALT
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int> { { HALT } }, true));

            // RSET
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int> { { RSET } }, true));


            // MOVE
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { NEXT_BYTE },
                { LOAD_ARG },
                { INP_FROM_ARG | OUT_FROM_ARG },
                { NEXT_BYTE }
            }));
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { NEXT_BYTE },
                { LOAD_ARG },
                { NEXT_BYTE },
                { INP_FROM_ARG | OUT_FROM_PROG },
                { NEXT_BYTE }
            }));
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { GET_ZERO | INP_TO_RAM_PAGE | OUT_FROM_ALU },
                { NEXT_BYTE },
                { LOAD_ARG },
                { NEXT_BYTE },
                { INP_TO_RAM_ADDR | OUT_FROM_PROG },
                { INP_FROM_ARG | OUT_FROM_RAM },
                { NEXT_BYTE }
            }));
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { GET_ZERO | INP_TO_RAM_PAGE | OUT_FROM_ALU },
                { NEXT_BYTE },
                { INP_TO_RAM_ADDR | OUT_FROM_PROG },
                { NEXT_BYTE },
                { LOAD_ARG },
                { INP_TO_RAM | OUT_FROM_ARG },
                { NEXT_BYTE }
            }));
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { GET_ZERO | INP_TO_RAM_PAGE | OUT_FROM_ALU },
                { NEXT_BYTE },
                { INP_TO_RAM_ADDR | OUT_FROM_PROG },
                { NEXT_BYTE },
                { INP_TO_RAM | OUT_FROM_PROG },
                { NEXT_BYTE }
            }));

            // POKE
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { NEXT_BYTE },
                { INP_TO_RAM_PAGE | OUT_FROM_PROG },
                { NEXT_BYTE },
                { INP_TO_RAM_ADDR | OUT_FROM_PROG },
                { NEXT_BYTE },
                { INP_TO_RAM | OUT_FROM_PROG },
                { NEXT_BYTE }
            }));
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { NEXT_BYTE },
                { INP_TO_RAM_PAGE | OUT_FROM_PROG },
                { NEXT_BYTE },
                { INP_TO_RAM_ADDR | OUT_FROM_PROG },
                { NEXT_BYTE },
                { LOAD_ARG },
                { INP_TO_RAM | OUT_FROM_ARG },
                { NEXT_BYTE }
            }));
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { GET_ZERO | INP_TO_RAM_PAGE | OUT_FROM_ALU },
                { NEXT_BYTE },
                { INP_TO_RAM_ADDR | OUT_FROM_PROG },
                { NEXT_BYTE },
                { INP_TO_RAM | OUT_FROM_PROG },
                { NEXT_BYTE }
            }));
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { GET_ZERO | INP_TO_RAM_PAGE | OUT_FROM_ALU },
                { NEXT_BYTE },
                { INP_TO_RAM_ADDR | OUT_FROM_PROG },
                { NEXT_BYTE },
                { LOAD_ARG },
                { INP_TO_RAM | OUT_FROM_ARG },
                { NEXT_BYTE }
            }));

            // PEEK
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { NEXT_BYTE },
                { INP_TO_RAM_PAGE | OUT_FROM_PROG },
                { NEXT_BYTE },
                { INP_TO_RAM_ADDR | OUT_FROM_PROG },
                { NEXT_BYTE }
            }));
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { GET_ZERO | INP_TO_RAM_PAGE | OUT_FROM_ALU },
                { NEXT_BYTE },
                { INP_TO_RAM_ADDR | OUT_FROM_PROG },
                { NEXT_BYTE }
            }));

            InstructionList.AddRange(GetFillerInstructions(InstructionList.Count));

            return InstructionList;
        }
    }
}
