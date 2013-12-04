using System;
using System.Collections.Generic;
using System.Linq;

namespace Jello.Nodes
{
    public abstract class Node<T>
    {
        protected Jello Jello;
        protected Lexer Lexer;

        public List<ParseError> Errors = new List<ParseError>(); 

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

        public bool ExpectToken(string type)
        {
            object discardVal;
            return ExpectToken(type, out discardVal);
        }

        public bool ExpectToken(string type, out object value)
        {
            var nextTok = Lexer.Next();
            if (nextTok.Type != type)
            {
                Errors.Add(new ParseError("Expected " + type));
                value = null;
                return false;
            }
            value = nextTok.Value;
            return true;
        }

        public bool ExpectNode<T>(out T node) where T : Node<T>
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

        public bool AcceptToken(string type)
        {
            object discardVal;
            return AcceptToken(type, out discardVal);
        }

        public bool AcceptToken(string type, out object value)
        {
            var nextTok = Lexer.Next();
            if (nextTok.Type != type)
            {
                value = null;
                return false;
            }
            value = nextTok.Value;
            return true;
        }
    }
}