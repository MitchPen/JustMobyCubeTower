namespace Core.Services.ScreenBorderProvider
{
    public interface IScreenBorderProvider
    {
        public ScreenBorderProviderData GetScreenBorder();
        public ScreenBorderProviderData GetScreenToWorldBorder();
    }
}
