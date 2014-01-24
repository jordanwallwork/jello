using System;
using Jello.DataSources;
using Jello.Utils;

namespace Jello.Nodes
{
    public class Identifier : BinaryTreeNode<Identifier>
    {
        public string Name { get; set; }

        protected override Identifier ParseNode()
        {
            object name;
            if (ExpectToken("identifier", out name))
            {
                Name = name.ToString();
            }
            return this;
        }

        public override object GetValue(IDataSource dataSource)
        {
            object value;
            if (dataSource.TryGet(Name, out value)) return value;
            throw new Exception("Unrecognised identifier: " + Name);
        }

        public override ValueType Type(IDataSource dataSource)
        {
            var type = GetValue(dataSource).GetValueType();
            if (type.HasValue) return type.Value;
            throw new Exception("Unrecognized type");
        }
    }
}