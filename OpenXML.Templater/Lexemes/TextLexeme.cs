﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenXML.Templater.Lexemes
{
    public record TextLexeme(string content) : Lexem(content)
    {
    }
}
