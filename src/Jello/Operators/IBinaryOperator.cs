using System;

namespace Jello.Operators
{
    public interface IBinaryOperator
    {
        string Operator { get; }
        ValueType ReturnType { get; }
        ValueType LHS { get; }
        ValueType RHS { get; }

        object Evaluate(object lhs, object rhs);
    }

    public class BasicBinaryOperator : IBinaryOperator
    {
        public string Operator { get; private set; }
        public ValueType ReturnType { get; private set; }
        public ValueType LHS { get; private set; }
        public ValueType RHS { get; private set; }

        private readonly Func<object, object, object> _evaluator;

        public object Evaluate(object lhs, object rhs)
        {
            return _evaluator(lhs, rhs);
        }

        public BasicBinaryOperator(string @operator, ValueType returnType, ValueType lhs, ValueType rhs, Func<object, object, object> evaluator)
        {
            _evaluator = evaluator;
            Operator = @operator;
            ReturnType = returnType;
            LHS = lhs;
            RHS = rhs;
        }

        public BasicBinaryOperator(string @operator, ValueType returnType, ValueType operands, Func<object, object, object> evaluator)
        {
            _evaluator = evaluator;
            Operator = @operator;
            ReturnType = returnType;
            LHS = operands;
            RHS = operands;
        }

        public BasicBinaryOperator(string @operator, ValueType operandsAndReturnType, Func<object, object, object> evaluator)
        {
            _evaluator = evaluator;
            Operator = @operator;
            ReturnType = operandsAndReturnType;
            LHS = operandsAndReturnType;
            RHS = operandsAndReturnType;
        }
    }
}