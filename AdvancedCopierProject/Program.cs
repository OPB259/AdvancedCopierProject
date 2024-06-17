using System;

class Program
{
    static void Main(string[] args)
    {
        var mfd = new MultifunctionalDevice();
        mfd.PowerOn();

        IDocument doc1 = new PDFDocument("aaa.pdf");
        mfd.Print(in doc1);

        IDocument doc2;
        mfd.Scan(out doc2);

        mfd.SendFax(in doc1);

        Console.WriteLine($"Power on count: {mfd.Counter}");
        Console.WriteLine($"Print count: {mfd.PrintCounter}");
        Console.WriteLine($"Scan count: {mfd.ScanCounter}");
        Console.WriteLine($"Fax count: {mfd.FaxCounter}");
    }
}