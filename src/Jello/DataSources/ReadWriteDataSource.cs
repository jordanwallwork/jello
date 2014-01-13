namespace Jello.DataSources
{
    public abstract class ReadWriteDataSource : IDataSource
    {
        public abstract bool TryGet(string key, out object value);
        public abstract bool TrySet(string key, bool isNew, object value);
    }
}