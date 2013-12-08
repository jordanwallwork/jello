using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jello.DataSources;

namespace Jello.Tests.DataSources
{
    public class TestDataSource : ReadWriteDataSource
    {
        private readonly IDictionary<string, object> _data;

        public TestDataSource(IDictionary<string, object> data = null)
        {
            _data = data ?? new Dictionary<string, object>();
        }

        public override bool TryGet(string key, out object value)
        {
            return _data.TryGetValue(key, out value);
        }

        public override bool TrySet(string key, out object value)
        {
            throw new NotImplementedException();
        }
    }
}
