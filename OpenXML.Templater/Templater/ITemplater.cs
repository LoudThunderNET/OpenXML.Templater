namespace OpenXML.Templater.Templater
{
    public interface ITemplater
    {
        void Render(string templateFileName, DataModel dataModel, string outpurFileName);
    }
}
