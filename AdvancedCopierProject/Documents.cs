public interface IDocument
{
    string GetFileName();
    enum FormatType { TXT, PDF, JPG }
    FormatType GetFormatType();
}

public abstract class AbstractDocument : IDocument
{
    public string fileName { get; private set; }
    public AbstractDocument(string fileName)
    {
        this.fileName = fileName;
    }

    public string GetFileName() => fileName;
    public abstract IDocument.FormatType GetFormatType();
}

public class PDFDocument : AbstractDocument
{
    public PDFDocument(string fileName) : base(fileName) { }
    public override IDocument.FormatType GetFormatType() => IDocument.FormatType.PDF;
}

public class TextDocument : AbstractDocument
{
    public TextDocument(string fileName) : base(fileName) { }
    public override IDocument.FormatType GetFormatType() => IDocument.FormatType.TXT;
}

public class ImageDocument : AbstractDocument
{
    public ImageDocument(string fileName) : base(fileName) { }
    public override IDocument.FormatType GetFormatType() => IDocument.FormatType.JPG;
}