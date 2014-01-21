using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jello.Nodes;

namespace Jello.Tests
{
    public abstract class TestBase
    {
        public T Parse<T>(string str) where T : Node<T>
        {
            var t = Activator.CreateInstance<T>();
            t.Parse(new Jello(), new Lexer(str));
            return t;
        }
    }
}
