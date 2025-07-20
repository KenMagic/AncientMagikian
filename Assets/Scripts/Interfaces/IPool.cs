public interface IPool<T>
{

    T GetObject();
    void ReturnObject(T obj);
    void ClearPool();
}