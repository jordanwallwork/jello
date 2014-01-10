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

        public bool TryGet(string key, out object value)
        {
            foreach (var dataSource in Settings.DataSources)
            {
                if (dataSource.TryGet(key, out value)) return true;
            }
            value = null;
            return false;
        }

        public bool TrySet(string key, bool isNew, out object value)
        {
            foreach (var dataSource in Settings.DataSources)
            {
                if (dataSource.TrySet(key, isNew, out value)) return true;
            }
            value = null;
            return false;
        }
    }
}