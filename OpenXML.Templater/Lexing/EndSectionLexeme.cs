﻿using OpenXML.Templater.Parsing;
using OpenXML.Templater.Primitives;

namespace OpenXML.Templater.Lexing
{
    public record EndSectionLexeme : Lexem
    {
        public EndSectionLexeme(StringSpan content) : base(content) 
        { 
        }

        public override void Accept(Parser parser)
        {
            parser.Visit(this);
        }
    }
}
