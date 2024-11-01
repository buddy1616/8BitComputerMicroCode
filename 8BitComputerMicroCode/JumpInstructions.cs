namespace _8BitComputerMicroCode
{
    public class JumpInstructionsBuilder : MicroCodeInstructionBuilder
    {
        private List<int> GetJumpInstructions()
        {
            return new List<int>
            {
                { NEXT_BYTE },
                { DEC_STACK },
                { INP_TO_STACK | OUT_FROM_PROG },
                { NEXT_BYTE },
                { INP_TO_PCL | OUT_FROM_PROG },
                { INP_TO_PCH | OUT_FROM_STACK },
                { INC_STACK }
            };
        }

        public override List<int> Build()
        {
            // JUMP
            InstructionList.AddRange(BuildInstructionSet_FlagIndependent(GetJumpInstructions()));

            //JMPC
            InstructionList.AddRange(BuildInstructionSet_CarryFlagDependent(new List<int> { NEXT_BYTE, NEXT_BYTE, NEXT_BYTE }, GetJumpInstructions()));

            //JPNC
            InstructionList.AddRange(BuildInstructionSet_CarryFlagDependent(GetJumpInstructions(), new List<int> { NEXT_BYTE, NEXT_BYTE, NEXT_BYTE }));

            //JMPZ
            InstructionList.AddRange(BuildInstructionSet_ZeroFlagDependent(new List<int> { NEXT_BYTE, NEXT_BYTE, NEXT_BYTE }, GetJumpInstructions()));

            //JPNZ
            InstructionList.AddRange(BuildInstructionSet_ZeroFlagDependent(GetJumpInstructions(), new List<int> { NEXT_BYTE, NEXT_BYTE, NEXT_BYTE }));

            //JMPE
            InstructionList.AddRange(BuildInstructionSet_EqualFlagDependent(new List<int> { NEXT_BYTE, NEXT_BYTE, NEXT_BYTE }, GetJumpInstructions()));

            //JPNE
            InstructionList.AddRange(BuildInstructionSet_EqualFlagDependent(GetJumpInstructions(), new List<int> { NEXT_BYTE, NEXT_BYTE, NEXT_BYTE }));

            //JMPG
            InstructionList.AddRange(BuildInstructionSet_GreaterFlagDependent(new List<int> { NEXT_BYTE, NEXT_BYTE, NEXT_BYTE }, GetJumpInstructions()));

            //JPNG
            InstructionList.AddRange(BuildInstructionSet_GreaterFlagDependent(GetJumpInstructions(), new List<int> { NEXT_BYTE, NEXT_BYTE, NEXT_BYTE }));

            //JMPL
            InstructionList.AddRange(GetJMPLInstructions());

            //JPNL
            InstructionList.AddRange(GetJPNLInstructions());

            InstructionList.AddRange(GetFillerInstructions(InstructionList.Count));

            return InstructionList;
        }

        private List<int> GetJMPLInstructions()
        {
            List<int> instructions = new List<int>();
            List<int> jsteps = GetJumpInstructions();

            instructions.AddRange(BuildMicroInstruction_WithDuplicates(FETCH, 16));

            for (int i=0;i<4;i++)
            {
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(NEXT_BYTE, 1));
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(NEXT_BYTE, 3));
            }
            for (int i = 0; i < 4; i++)
            {
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(INP_TO_PCH | OUT_FROM_PROG, 1));
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(END_OP, 3));
            }
            for (int i = 0; i < 4; i++)
            {
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(NEXT_BYTE, 1));
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(0, 3));
            }
            for (int i = 0; i < 4; i++)
            {
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(INP_TO_PCL | OUT_FROM_PROG, 1));
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(0, 3));
            }
            for (int i = 0; i < 4; i++)
            {
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(NEXT_BYTE, 1));
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(0, 3));
            }
            for (int i = 0; i < 4; i++)
            {
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(END_OP, 1));
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(0, 3));
            }
            instructions.AddRange(GetFillerSteps(instructions.Count));
            return instructions;
        }

        private List<int> GetJPNLInstructions()
        {
            List<int> instructions = new List<int>();
            List<int> jsteps = GetJumpInstructions();

            instructions.AddRange(BuildMicroInstruction_WithDuplicates(FETCH, 16));
            instructions.AddRange(BuildMicroInstruction_WithDuplicates(NEXT_BYTE, 16));

            for (int i = 0; i < 4; i++)
            {
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(END_OP, 1));
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(INP_TO_PCH | OUT_FROM_PROG, 3));
            }
            for (int i = 0; i < 4; i++)
            {
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(0, 1));
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(NEXT_BYTE, 3));
            }
            for (int i = 0; i < 4; i++)
            {
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(0, 1));
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(INP_TO_PCL | OUT_FROM_PROG, 3));
            }
            for (int i = 0; i < 4; i++)
            {
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(0, 1));
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(NEXT_BYTE, 3));
            }
            for (int i = 0; i < 4; i++)
            {
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(0, 1));
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(END_OP, 3));
            }
            instructions.AddRange(GetFillerSteps(instructions.Count));
            return instructions;
        }
    }
}
