using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Jello.DataSources;
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

        public bool TrySet(string key, out object value)
        {
            foreach (var dataSource in Settings.DataSources)
            {
                if (dataSource.TrySet(key, out value)) return true;
            }
            value = null;
            return false;
        }
    }
}