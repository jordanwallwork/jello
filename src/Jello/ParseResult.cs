using System;
using System.Collections.Generic;
using Jello.DataSources;
using Jello.Errors;
using Jello.Nodes;
using Jello.Utils;

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

        public object Execute(IDataSource dataSource)
        {
            return _root.GetValue(dataSource);
        }

        public bool? ExecuteBool(IDataSource dataSource)
        {
            var val = _root.GetValue(dataSource);
            var valType = val.GetValueType();
            if (valType != ValueType.Bool) throw new Exception("Return value is not boolean");
            return val.AsBool();
        }
    }
}