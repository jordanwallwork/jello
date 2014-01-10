namespace Jello.DataSources
{
    public interface IDataSource
    {
        bool TryGet(string key, out object value);
        bool TrySet(string key, bool isNew, out object value);
    }
}