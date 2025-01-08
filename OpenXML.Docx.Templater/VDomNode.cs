namespace OpenXML.Docx.Templater
{
    public class VDomNode
    {
        public VDomNode? Parent { get; set; }
        public List<VDomNode> Children { get; set; } = new List<VDomNode>();
        public required object Template { get; set; }
        public object? Target { get; set; }
    }
}
