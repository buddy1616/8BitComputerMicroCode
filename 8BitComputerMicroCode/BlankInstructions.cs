namespace _8BitComputerMicroCode
{
    public class BlankInstructionsBuilder : MicroCodeInstructionBuilder
    {
        public override List<int> Build()
        {
            InstructionList.AddRange(GetFillerInstructions(InstructionList.Count));

            return InstructionList;
        }
    }
}
