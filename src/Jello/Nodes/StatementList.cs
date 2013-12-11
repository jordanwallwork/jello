using System.Collections.Generic;
using System.Linq;

namespace Jello.Nodes
{
    public class StatementList : Node<StatementList>
    {
        public List<INode> Statements { get; set; }

        protected override StatementList ParseNode()
        {
            Statements = new List<INode> { ExpectNode<Statement>() };
            INode stmt;
            while (AcceptNode<Statement>(out stmt))
            {
                Statements.Add(stmt);
            }
            return this;
        }

        public override INode GetSingleChild()
        {
            return Statements.Count == 1 ? Statements.First() : null;
        }
    }
}