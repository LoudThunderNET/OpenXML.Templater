using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenXML.Templater.Tests
{
    internal class TemplateSamples
    {
        public const string Empty = "";
        public const string Whitespace = " ";
        public const string One = "1";
        public const string Two = "2";
        public const string Three = "3";
        public const string Four = "4";
        public const string Five = "5";
        public const string Six = "6";
        public const string Seven = "7";
        public const string Eigth = "8";
        public const string Nine = "9";
        public const string Ten = "0";
        public const string WhitespaceIdentifierWhitespace = " hello123 ";
        public const string WhitespaceWroneIdentifierWhitespace = " 21hello123 ";
        public const string OpenTag = "{{";
        public const string CloseTag = "}}";
        public const string CloseTagOpenTag = "}}{{";
        public const string OpenTagOpenTagCloseTag = "{{{{}}";
        public const string TextSpaceOpenTagSpaceInlineSpaceCloseTag = "hello {{ world }}";
        public const string TextSpaceOpenTagSpaceInlineSpaceCloseTagSpaceTextSpace = "hello {{ beautiful }} word ";
        public const string OpenTagSpaceSectionSpaceCloseTag = "{{ #beautiful }}";
        public const string OpenTagSectionCloseTag = "{{ #beautiful }}";
        public const string OpenTagSpaceSectionCloseTag = "{{ #beautiful }}";
        public const string OpenTagSpaceInvertedSectionSpaceCloseTag = "{{ ^beautiful }}";
        public const string OpenTagSpaceHorizSectionSpaceCloseTag = "{{ >beautiful }}";
        public const string OpenTagSpaceEndSectionSpaceCloseTag = "{{ /beautiful }}";
        public const string OpenTagSectionSpaceCloseTagSpaceOpenTagIdentifierCloseTagSpaceOpenTagEndSectionCloseTag = "{{#beautiful }} {{soul}} {{/beautiful}}";
        public const string OpenTagDigitsCloseTag = "{{435}}"; //****
        public const string OpenTagTextSpaceCloseTag = "{{dsfsd132 {{#section}} }}";
        public const string OpenTagSectionCloseTagSpaceOpenTagInlineCloseTag = "{{#section}} {{fdfd}}";
        public const string OpenTagTextCloseTag = "{{#section dsf}}";
        public const string OpenTagInvertedSectionSpaceCloseTagSpaceOpenTagIdentifierCloseTagSpaceOpenTagEndSectionCloseTag = "{{^beautiful }} {{soul}} {{/beautiful}}";
        public const string OpenTagHorizSectionSpaceCloseTagSpaceOpenTagIdentifierCloseTagSpaceOpenTagEndSectionCloseTag = "{{>beautiful }} {{soul}} {{/beautiful}}";
    }
}
