using System;
using System.Collections.Generic;
using System.Linq;
using Jello.DataSources;
using Jello.Operators;
using Jello.Utils;

namespace Jello.Nodes
{
    public class MultiplicativeExpression : BinaryTreeNode<MultiplicativeExpression>
    {
        public static List<IBinaryOperator> Operators = new List<IBinaryOperator>()
        {
            new BasicBinaryOperator("*", ValueType.Number, (l,r) => l.AsNumber() * r.AsNumber()),
            new BasicBinaryOperator("/", ValueType.Number, (l,r) => l.AsNumber() / r.AsNumber()),
        };

        protected override MultiplicativeExpression ParseNode()
        {
            INode node;
            if (ExpectNode<PrimaryExpression>(out node))
            {
                LHS = node;
                object op;
                if (AcceptToken(out op, "*", "/"))
                {
                    Operator = op.ToString();
                    RHS = ExpectNode<MultiplicativeExpression>();
                }
            }
            return this;
        }

        public override object GetValue(IDataSource dataSource)
        {
            var op = GetOperator(dataSource);
            return op != null ? Evaluate(op.Evaluate, dataSource) : LHS.GetValue(dataSource);
        }

        private IBinaryOperator GetOperator(IDataSource dataSource)
        {
            if (RHS == null) return null;
            var ops = Operators.Where(x => x.Operator == Operator && x.LHS == LHS.Type(dataSource) && x.RHS == RHS.Type(dataSource));
            if (!ops.Any()) throw new Exception("No valid operator found");
            if (ops.Count() > 1) throw new Exception("Ambiguous reference: Multiple operators found");
            return ops.Single();
        }

        public override ValueType Type(IDataSource dataSource)
        {
            var op = GetOperator(dataSource);
            return op != null ? op.ReturnType : LHS.Type(dataSource);
        }
    }
}