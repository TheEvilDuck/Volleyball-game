namespace DI
{
    public interface IDIContainer
    {
        public T Get<T>(string tag = "");
        public bool ContainsRegistration<T>(string tag = "");
    }
}
