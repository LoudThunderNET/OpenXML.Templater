namespace OpenXML.Docx.Templater
{
    internal static class VDomNodeCollectionExtensions
    {
        public static IEnumerable<VDomNode> NotRendered(this IEnumerable<VDomNode> vDomNodes) =>
            vDomNodes.Where(node => node.Target == null);
    }
}
