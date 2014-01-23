using System;
using System.Collections.Generic;
using System.Linq;
using Jello.DataSources;
using Jello.Errors;
using Jello.Utils;

namespace Jello.Nodes
{
    public abstract class Node<T> : INode where T : class
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
            return ParseNode();
        }

        protected abstract T ParseNode();

        public bool ExpectKeyword(string keyword)
        {
            var nextTok = Lexer.Next();
            if (nextTok.Type != "keyword" || nextTok.Value as string != keyword)
            {
                Errors.Add(new ParseError("Expected keyword " + keyword, nextTok.LineNo, nextTok.Col));
                return false;
            }
            return true;
        }

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
                Errors.Add(new ParseError("Expected " + type, nextTok.LineNo, nextTok.Col));
                value = null;
                return false;
            }
            value = nextTok.Value;
            return true;
        }

        public bool ExpectNode<T>(out INode node) where T : Node<T>
        {
            node = ExpectNode<T>();
            return node != null;
        } 

        public INode ExpectNode<T>() where T : Node<T>
        {
            var currPos = Lexer.Pos;
            var _node = Activator.CreateInstance<T>().Parse(Jello, Lexer);
            if (_node.Errors.Any())
            {
                Errors.AddRange(_node.Errors);
                Lexer.ResetPos(currPos);
                return null;
            }
            return _node.GetSingleChild() ?? _node;
        }

        public bool AcceptKeyword(string keyword)
        {
            var currPos = Lexer.Pos;
            var nextTok = Lexer.Next();
            if (nextTok.Type != "keyword" && nextTok.Value as string != keyword)
            {
                Lexer.ResetPos(currPos);
                return false;
            }
            return true;
        }

        public bool AcceptToken(string type)
        {
            object discardVal;
            return AcceptToken(type, out discardVal);
        }

        public bool AcceptToken(out object valueOrType, params string[] types)
        {
            var currPos = Lexer.Pos;
            var nextTok = Lexer.Next();
            foreach (var type in types)
            {
                if (nextTok.Type == type)
                {
                    valueOrType = nextTok.Value ?? nextTok.Type;
                    return true;
                }
            }
            valueOrType = null;
            Lexer.ResetPos(currPos);
            return false;
        }

        public bool AcceptToken(string type, out object valueOrType)
        {
            var currPos = Lexer.Pos;
            var nextTok = Lexer.Next();
            if (nextTok.Type != type)
            {
                valueOrType = null;
                Lexer.ResetPos(currPos);
                return false;
            }
            valueOrType = nextTok.Value ?? nextTok.Type;
            return true;
        }

        public bool AcceptNode<T>(out INode node) where T : Node<T>
        {
            var currPos = Lexer.Pos;
            var _node = Activator.CreateInstance<T>().Parse(Jello, Lexer);
            if (_node.Errors.Any())
            {
                node = null;
                Lexer.ResetPos(currPos);
                return false;
            }
            node = _node.GetSingleChild() ?? _node;
            return true;
        }

        public T NoMatches(params string[] expected)
        {
            Errors.Add(new ParseError("Expected " + expected.ToReadableOrList(), Lexer.LineNo, Lexer.Col));
            return this as T;
        }

        public abstract INode GetSingleChild();

        public virtual object GetValue(IDataSource dataSource)
        {
            var singleChild = GetSingleChild();
            if (singleChild != null) return singleChild.GetValue(dataSource);
            throw new NotImplementedException();
        }

        public virtual ValueType Type(IDataSource dataSource)
        {
            var sc = GetSingleChild();
            if (sc != null) return sc.Type(dataSource);
            throw new Exception("Cannot get type information");
        }
    }
}