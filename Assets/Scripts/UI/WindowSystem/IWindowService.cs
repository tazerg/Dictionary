namespace JHI.Dict.UI
{
    public interface IWindowService
    {
        void ShowStartWindow();
        void OpenWindow<T>() where T : IWindow;
    }
}