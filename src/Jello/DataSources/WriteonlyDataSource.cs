namespace Jello.DataSources
{
    public abstract class WriteonlyDataSource : IDataSource
    {
        public bool TryGet(string key, out object value)
        {
            value = null;
            return false;
        }

        public abstract bool TrySet(string key, bool isNew, object value);
    }
}