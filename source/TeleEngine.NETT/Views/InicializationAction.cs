namespace TeleEngine.NET.Views
{
    public struct InicializationAction<T>
    {
        public Action<T> InicializateAction { get; set; }

        public InicializationAction(Action<T> action) 
        {
            InicializateAction = action;
        }
    }
}
