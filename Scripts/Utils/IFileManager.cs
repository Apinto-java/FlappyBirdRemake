namespace FlappyBirdRemake.Utils
{
    public interface IFileManager<T>
    {

        public void Save(T data);

        public T Load();
    }
}
