using System;
using Jello.Nodes;

namespace Jello
{
    public class Jello
    {
        public JelloSettings Settings { get; set; }

        public Jello(JelloSettings settings = null)
        {
            Settings = settings ?? new JelloSettings();
        }

        public T Parse<T>(string input) where T : Node<T>
        {
            var lexer = new Lexer(input);

            var node = Activator.CreateInstance<T>();
            node.Parse(this, lexer);
            return node;
        }
    }
}