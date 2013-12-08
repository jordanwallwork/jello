using System.Collections.Generic;
using Jello.DataSources;

namespace Jello
{
    public class JelloSettings
    {
        public IDateParser DateParser { get; set; }
        public List<IDataSource> DataSources { get; set; }

        public JelloSettings()
        {
            DateParser = new StandardDateParser();
            DataSources = new List<IDataSource>();
        }
    }
}