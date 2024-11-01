using _8BitComputerMicroCode;

int romIndex = 0;
int numberOfRoms = 3;
string path = "D:\\OneDrive\\Projects\\8 Bit Computer\\";


List<int> instructions =  new List<int>();
instructions.AddRange(new DataInstructionsBuilder().Build());
instructions.AddRange(new ArithmeticInstructionsBuilder().Build());
instructions.AddRange(new LogicInstructionsBuilder().Build());
instructions.AddRange(new StackInstructionsBuilder().Build());
instructions.AddRange(new JumpInstructionsBuilder().Build());
instructions.AddRange(new JumpCloseInstructionsBuilder().Build());
instructions.AddRange(new BlankInstructionsBuilder().Build());
instructions.AddRange(new FunInstructionsBuilder().Build());

for (int r = 0; r < numberOfRoms; r++)
{
    BinaryWriter writer = new BinaryWriter(File.Open(path + "MicroCode_ROM-" + r + ".bin", FileMode.Create));

    byte[] bytes = new byte[instructions.Count];
    for (int i = 0; i < instructions.Count; i++)
    {
        bytes[i] = (byte)(instructions[i] >> (2 - r) * 8);
    }
    writer.Write(bytes);
    writer.Flush();
    writer.Close();
}