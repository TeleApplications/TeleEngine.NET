namespace TeleEngine.NET.Intefaces
{
    public interface IBindable
    {
        public async Task BindAsync() { return Task.CompletedTask; }
    }
}
