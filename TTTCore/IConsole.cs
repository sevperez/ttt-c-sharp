namespace TTTCore
{
    public interface IConsole
    {
        void Write(string value);
        void Write(string value, object arg0);
        void Write(string value, object arg0, object arg1);
        void Write (string value, object[] args);
        void Clear();
        string ReadLine();
    }
}
