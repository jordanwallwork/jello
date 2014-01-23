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
            var val = GetValue(dataSource);
            if (val is bool) return ValueType.Bool;
            if (val.IsNumber()) return ValueType.Number;
            if (val is string) return ValueType.String;
            if (val is DateTime) return ValueType.Date;
            throw new Exception("Unrecognized type");
        }
    }
}