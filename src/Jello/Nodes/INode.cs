using Jello.DataSources;

namespace Jello.Nodes
{
    public interface INode
    {
        object GetValue(IDataSource dataSource);
        ValueType Type(IDataSource dataSource);
    }
}