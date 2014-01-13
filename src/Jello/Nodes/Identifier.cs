using System;
using Jello.DataSources;

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
    }
}