namespace Jello.DataSources
{
    public interface IDataSource
    {
        bool TryGet(string key, out object value);
        bool TrySet(string key, out object value);
    }
}