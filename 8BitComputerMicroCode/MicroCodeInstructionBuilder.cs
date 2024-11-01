namespace _8BitComputerMicroCode
{
    public class MicroCodeInstructionBuilder
    {
        public List<int> InstructionList = new List<int>();

        public const int BLANK = 0b000000000000000000000000;

        public const
            int HALT = 0b000000010000000000000000,
                RSET = 0b000000100000000000000000,
                NEXT = 0b000001000000000000000000,
                PROG = 0b000010000000000000000000,
                STCE = 0b000100000000000000000000,
                STUD = 0b001000000000000000000000,
                nop1 = 0b010000000000000000000000,
                nop2 = 0b100000000000000000000000;

        public const
            int ALU0 = 0b000000000000000100000000,
                ALU1 = 0b000000000000001000000000,
                ALU2 = 0b000000000000010000000000,
                ALU3 = 0b000000000000100000000000,
                MODE = 0b000000000001000000000000,
                nop3 = 0b000000000010000000000000,
                FLGS = 0b000000000100000000000000,
                CARY = 0b000000001000000000000000;

        public const
            int OUT0 = 0b000000000000000000000001,
                OUT1 = 0b000000000000000000000010,
                OUT2 = 0b000000000000000000000100,
                OUT3 = 0b000000000000000000001000,
                INP0 = 0b000000000000000000010000,
                INP1 = 0b000000000000000000100000,
                INP2 = 0b000000000000000001000000,
                INP3 = 0b000000000000000010000000;

        public const
            int CAR_FLAG = 0b1000,
                ZER_FLAG = 0b0100,
                AEB_FLAG = 0b0010,
                AGB_FLAG = 0b0001;

        public const
            int FETCH = INP2 | INP1 | OUT2 | OUT1,
                NEXT_BYTE = PROG,
                LOAD_ARG = INP2 | INP1 | INP0 | OUT2 | OUT1,
                DEC_STACK = STCE,
                INC_STACK = STCE | STUD,
                END_OP = NEXT | PROG;

        public const
            int INP_FROM_ARG = INP3 | INP2 | INP1 | INP0,
                INP_TO_A = INP0,
                INP_TO_B = INP1,
                INP_TO_PCH = INP2,
                INP_TO_PCL = INP2 | INP0,
                INP_TO_RAM_PAGE = INP3,
                INP_TO_RAM_ADDR = INP3 | INP0,
                INP_TO_RAM = INP3 | INP1,
                INP_TO_STACK = INP3 | INP1 | INP0;

        public const
            int OUT_FROM_ARG = OUT3 | OUT2 | OUT1 | OUT0,
                OUT_FROM_A = OUT0,
                OUT_FROM_B = OUT1,
                OUT_FROM_ALU = OUT1 | OUT0,
                OUT_FROM_PROG = OUT2 | OUT1,
                OUT_FROM_PCH = OUT2,
                OUT_FROM_PCL = OUT2 | OUT0,
                OUT_FROM_RAM = OUT3 | OUT1,
                OUT_FROM_STACK = OUT3 | OUT1 | OUT0;

        public const
            int GET_ZERO = ALU1 | ALU0,
                ALU_ADD = ALU3 | ALU0,
                ALU_SUB = ALU2 | ALU1,
                ALU_DEC = ALU3 | ALU2 | ALU1 | ALU0 | CARY,
                ALU_SHFT = ALU3 | ALU2;


        public virtual List<int> Build()
        {
            InstructionList.AddRange(GetFillerInstructions(InstructionList.Count));
            return new List<int>();
        }

        public List<int> BuildMicroInstruction_WithDuplicates(int instruction, int times)
        {
            List<int> instructions = new List<int>();
            for (int f = 0; f < times; f++)
            {
                instructions.Add(instruction);
            }
            return instructions;
        }

        public List<int> BuildInstructionSet_FlagIndependent(List<int> steps, bool addFetchCycle = true, bool addEndOp = true)
        {
            List<int> instructions = new List<int>();
            if (addFetchCycle) { steps.Insert(0, FETCH); }
            if (addEndOp && steps.Count < 16) { steps.Add(END_OP); }

            foreach (int step in steps)
            {
                instructions.AddRange(BuildMicroInstruction_WithDuplicates(step, 16));
            }
            instructions.AddRange(GetFillerSteps(instructions.Count));
            return instructions;
        }
        public List<int> BuildInstructionSet_FlagDependent(List<int> stepsWhenFlagNotSet, List<int> stepsWhenFlagSet, int instructionsPerSet, int numberOfSets,
            bool addFetchCycle = true, bool addEndOp = true)
        {
            List<int> instructions = new List<int>();

            if (addFetchCycle)
            {
                stepsWhenFlagNotSet.Insert(0, FETCH);
                stepsWhenFlagSet.Insert(0, FETCH);
            }
            if (addEndOp && stepsWhenFlagNotSet.Count < 16) { stepsWhenFlagNotSet.Add(END_OP); }
            if (addEndOp && stepsWhenFlagSet.Count < 16) { stepsWhenFlagSet.Add(END_OP); }


            for (int i = 0; i < Math.Max(stepsWhenFlagNotSet.Count, stepsWhenFlagSet.Count); i++)
            {
                for (int x = 0; x < numberOfSets; x++)
                {
                    instructions.AddRange(BuildMicroInstruction_WithDuplicates((stepsWhenFlagNotSet.Count > i) ? stepsWhenFlagNotSet[i] : 0, instructionsPerSet));
                    instructions.AddRange(BuildMicroInstruction_WithDuplicates((stepsWhenFlagSet.Count > i) ? stepsWhenFlagSet[i] : 0, instructionsPerSet));
                }
            }
            instructions.AddRange(GetFillerSteps(instructions.Count));
            return instructions;
        }
        public List<int> BuildInstructionSet_CarryFlagDependent(List<int> stepsWhenCarryFlagNotSet, List<int> stepsWhenCarryFlagSet, bool addFetchCycle = true, bool addEndOp = true)
        {
            return BuildInstructionSet_FlagDependent(stepsWhenCarryFlagNotSet, stepsWhenCarryFlagSet, 8, 1, addFetchCycle, addEndOp);
        }
        public List<int> BuildInstructionSet_ZeroFlagDependent(List<int> stepsWhenZeroFlagNotSet, List<int> stepsWhenZeroFlagSet, bool addFetchCycle = true, bool addEndOp = true)
        {
            return BuildInstructionSet_FlagDependent(stepsWhenZeroFlagNotSet, stepsWhenZeroFlagSet, 4, 2, addFetchCycle, addEndOp);
        }
        public List<int> BuildInstructionSet_EqualFlagDependent(List<int> stepsWhenEqualFlagNotSet, List<int> stepsWhenEqual, bool addFetchCycle = true, bool addEndOp = true)
        {
            return BuildInstructionSet_FlagDependent(stepsWhenEqualFlagNotSet, stepsWhenEqual, 2, 4, addFetchCycle, addEndOp);
        }
        public List<int> BuildInstructionSet_GreaterFlagDependent(List<int> stepsWhenGreaterFlagNotSet, List<int> stepsWhenGreaterFlagSet, bool addFetchCycle = true, bool addEndOp = true)
        {
            return BuildInstructionSet_FlagDependent(stepsWhenGreaterFlagNotSet, stepsWhenGreaterFlagSet, 1, 8, addFetchCycle, addEndOp);
        }

        public List<int> GetFillerSteps(int count)
        {
            List<int> instructions = new List<int>();
            for (int i = count; i < 256; i++)
            {
                instructions.Add(BLANK);
            }
            return instructions;
        }

        public List<int> GetFillerInstructions(int count)
        {
            List<int> instructions = new List<int>();
            for (int i = count; i < 4096; i++)
            {
                instructions.Add(BLANK);
            }
            return instructions;
        }
    }
}
