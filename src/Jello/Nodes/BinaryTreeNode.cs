using System;
using System.Data;
using Jello.DataSources;

namespace Jello.Nodes
{
    public abstract class BinaryTreeNode<T> : Node<T> where T : class
    {
        public INode LHS { get; set; }
        public string Operator { get; set; }
        public INode RHS { get; set; }

        public override INode GetSingleChild()
        {
            return RHS == null ? LHS : null;
        }

        protected object Evaluate(Func<object, object, object> func, IDataSource dataSource)
        {
            return func(LHS.GetValue(dataSource), RHS.GetValue(dataSource));
        } 
    }
}