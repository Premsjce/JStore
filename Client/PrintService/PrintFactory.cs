namespace Client.PrintService
{
    public class PrintFactory
    {
        //Default Print to screen
        public IPrint GetPrintType(PrintType printType)
        {
            switch (printType)
            {
                case PrintType.Screen:
                    return new PrintToScreen();
                case PrintType.File:
                    return new PrintToFile();
                case PrintType.Paper:
                    return new PrintToPaper();
                default:
                    return new PrintToScreen();
            }
        }
    }
}