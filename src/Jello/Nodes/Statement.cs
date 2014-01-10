using System.Collections.Generic;
using System.Linq;

namespace Jello.Nodes
{
    public class Statement : Node<Statement>
    {
        public INode LHS { get; set; }

        protected override Statement ParseNode()
        {
            INode node;
            if (AcceptNode<ReturnStatement>(out node) || AcceptNode<IfStatement>(out node) || AcceptNode<AssignmentStatement>(out node))
            {
                LHS = node;
                return this;
            }
            return NoMatches("ReturnStatement", "IfStatement");
        }

        public override INode GetSingleChild()
        {
            return LHS;
        }
    }
}