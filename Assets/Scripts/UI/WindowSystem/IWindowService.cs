namespace JHI.Dict.UI
{
    public interface IWindowService
    {
        void OpenWindow<T>() where T : BaseWindow;
    }
}