using System;
using System.Collections.Generic;
using System.Linq;

namespace Jello.Nodes
{
    public abstract class Node<T>
    {
        protected Jello Jello;
        protected Lexer Lexer;

        protected List<ParseError> Errors = new List<ParseError>(); 

        protected Node() {}
        protected Node(Jello jello, Lexer lexer)
        {
            Jello = jello;
            Lexer = lexer;
        }

        public T Parse(Jello jello, Lexer lexer)
        {
            Jello = jello;
            Lexer = lexer;
            return Parse();
        }

        public T Parse()
        {
            if (Lexer == null) throw new Exception("No lexer");
            return ParseNode();
        }

        protected abstract T ParseNode();

        public bool Expect(string type, out Token token)
        {
            var nextTok = Lexer.Next();
            if (nextTok.Type != type)
            {
                Errors.Add(new ParseError() { Message = "Expected " + type});
                token = null;
                return false;
            }
            token = nextTok;
            return true;
        }

        public bool Expect<T>(out T node) where T : Node<T>
        {
            var currPos = Lexer.Pos;
            var _node = Activator.CreateInstance<T>().Parse(Jello, Lexer);
            if (_node.Errors.Any())
            {
                Errors = _node.Errors;
                node = null;
                Lexer.ResetPos(currPos);
                return false;
            }
            node = _node;
            return true;
        }
    }
}