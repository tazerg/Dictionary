namespace JHI.Dict.UI
{
    public interface IWindow
    {
        WindowType WindowType { get; }

        void Open();
        void Close();
    }
}