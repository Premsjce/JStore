namespace Client.PrintService
{
    public interface IPrint
    {
        void Print(string message);
    }

    public enum PrintType
    {
        Screen,
        Paper,
        File
    }
}
