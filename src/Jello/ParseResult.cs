using System.Collections.Generic;
using Jello.Errors;
using Jello.Nodes;

namespace Jello
{
    public class ParseResult 
    {
        public bool Success { get; private set; }
        public IEnumerable<IError> Errors { get; private set; } 
        private INode _root;

        public ParseResult(INode node)
        {
            _root = node;
            Success = true;
        }

        public ParseResult(IEnumerable<IError> errors)
        {
            Success = false;
            Errors = errors;
        }
    }
}