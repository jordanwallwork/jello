namespace Jello.DataSources
{
    public class CompositeDataSource : IDataSource
    {
        private readonly IDataSource[] _dataSources;

        public CompositeDataSource(params IDataSource[] dataSources)
        {
            _dataSources = dataSources;
        }

        public bool TryGet(string key, out object value)
        {
            foreach (var dataSource in _dataSources)
            {
                if (dataSource.TryGet(key, out value)) return true;
            }
            value = null;
            return false;
        }

        public bool TrySet(string key, bool isNew, object value)
        {
            foreach (var dataSource in _dataSources)
            {
                if (dataSource.TrySet(key, isNew, value)) return true;
            }
            return false;
        }
    }
}