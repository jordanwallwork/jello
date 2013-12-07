namespace Jello.Nodes
{
    public abstract class TerminalNode<T> : Node<T> where T : class
    {
        public override INode GetSingleChild()
        {
            return null;
        }
    }
}