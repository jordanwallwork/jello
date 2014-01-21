using System;
using System.Collections.Generic;
using Jello.DataSources;

namespace Jello.Tests.DataSources
{
    public class TestDataSource : IDataSource
    {
        private readonly IDictionary<string, object> _data;

        public TestDataSource(IDictionary<string, object> data = null)
        {
            _data = data ?? new Dictionary<string, object>();
        }

        public bool TryGet(string key, out object value)
        {
            return _data.TryGetValue(key, out value);
        }
    }
}
