﻿using System;
using Jello.Nodes;

namespace Jello
{
    public class Jello
    {
        public T Parse<T>(string input) where T : Node<T>
        {
            var jello = new Jello();
            var lexer = new Lexer(input);

            var node = Activator.CreateInstance<T>();
            node.Parse(jello, lexer);
            return node;
        }
    }
}