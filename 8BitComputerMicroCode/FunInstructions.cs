namespace _8BitComputerMicroCode
{
    public class FunInstructionsBuilder : MicroCodeInstructionBuilder
    {
        public override List<int> Build()
        {
            // KAOS
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(new List<int>
            {
                { INP_TO_A | OUT_FROM_PCH },
                { INP_TO_B | OUT_FROM_PCL },
                { MODE | INP_TO_PCL | OUT_FROM_ALU },
                { MODE | ALU2 | ALU0 | INP_TO_PCH | OUT_FROM_ALU },
                { NEXT_BYTE }
            }));

            InstructionList.AddRange(GetFillerInstructions(InstructionList.Count));

            return InstructionList;
        }
    }
}
