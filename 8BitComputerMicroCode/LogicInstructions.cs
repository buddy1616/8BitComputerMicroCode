namespace _8BitComputerMicroCode
{
    public class LogicInstructionsBuilder : MicroCodeInstructionBuilder
    {
        public override List<int> Build()
        {
            for (int i = 0;i<16;i++)
            {
                BuildLogicInstructionSet(i << 8);
            }

            InstructionList.AddRange(GetFillerInstructions(InstructionList.Count));

            return InstructionList;
        }

        private void BuildLogicInstructionSet(int logicBits)
        {
            List<int> instructions = new List<int>
            {
                { NEXT_BYTE },
                { LOAD_ARG }
            };
            instructions.Add(logicBits | MODE | INP_FROM_ARG | OUT_FROM_ALU);
            instructions.Add(NEXT_BYTE | FLGS);
            instructions.Add(END_OP);
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(instructions, true, false));
        }
    }
}
