namespace Jello.DataSources
{
    public abstract class ReadonlyDataSource : IDataSource
    {
        public abstract bool TryGet(string key, out object value);

        public bool TrySet(string key, bool isNew, object value)
        {
            value = null;
            return false;
        }
    }
}