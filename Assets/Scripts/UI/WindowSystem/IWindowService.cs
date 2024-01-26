namespace JHI.Dict.UI
{
    public interface IWindowService
    {
        void OpenWindow<T>() where T : BaseWindow;
        T GetWindow<T>() where T : BaseWindow;
    }
}