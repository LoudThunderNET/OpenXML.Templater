using NPOI.XWPF.Model;
using NPOI.XWPF.UserModel;

XWPFDocument doc = new XWPFDocument();

// Starts with no header
XWPFHeaderFooterPolicy policy = doc.GetHeaderFooterPolicy();

// Add a default header
policy = doc.CreateHeaderFooterPolicy();
XWPFHeader header = policy.CreateHeader(XWPFHeaderFooterPolicy.DEFAULT);
header.CreateParagraph().CreateRun().SetText("Hello, Header World!");
header.CreateParagraph().CreateRun().SetText("Paragraph 2");
using var fs = new FileStream("HeaderWithTwoParagraph", FileMode.CreateNew, FileAccess.Write);
doc.Write(fs);

Console.WriteLine("Press any key ...");
Console.ReadKey();
