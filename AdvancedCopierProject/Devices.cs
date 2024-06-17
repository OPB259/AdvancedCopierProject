using System;

public interface IDevice
{
    enum State { on, off };
    State GetState();
    void PowerOn();
    void PowerOff();
    int Counter { get; }
}

public interface IPrinter : IDevice
{
    void Print(in IDocument document);
}

public interface IScanner : IDevice
{
    void Scan(out IDocument document);
}

public interface IFax : IDevice
{
    void SendFax(in IDocument document);
}

public abstract class BaseDevice : IDevice
{
    private IDevice.State state = IDevice.State.off;
    public IDevice.State GetState() => state;

    public virtual void PowerOn()
    {
        state = IDevice.State.on;
        Counter++;
    }

    public virtual void PowerOff()
    {
        state = IDevice.State.off;
    }

    public int Counter { get; private set; } = 0;
}

public class Printer : BaseDevice, IPrinter
{
    public int PrintCounter { get; private set; } = 0;

    public void Print(in IDocument document)
    {
        if (GetState() == IDevice.State.off)
            throw new InvalidOperationException("Device is off");
        PrintCounter++;
        Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
    }
}

public class Scanner : BaseDevice, IScanner
{
    public int ScanCounter { get; private set; } = 0;

    public void Scan(out IDocument document)
    {
        document = new ImageDocument($"ImageScan{ScanCounter + 1}.jpg");
        if (GetState() == IDevice.State.off)
            throw new InvalidOperationException("Device is off");
        ScanCounter++;
        Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");
    }
}

public class Copier : BaseDevice
{
    private Printer printer = new Printer();
    private Scanner scanner = new Scanner();

    public int PrintCounter => printer.PrintCounter;
    public int ScanCounter => scanner.ScanCounter;

    public void Print(in IDocument document)
    {
        if (GetState() == IDevice.State.off)
            throw new InvalidOperationException("Device is off");
        printer.Print(in document);
    }

    public void Scan(out IDocument document)
    {
        if (GetState() == IDevice.State.off)
            throw new InvalidOperationException("Device is off");
        scanner.Scan(out document);
    }

    public void ScanAndPrint()
    {
        Scan(out IDocument document);
        if (document != null)
        {
            Print(in document);
        }
    }

    public override void PowerOn()
    {
        base.PowerOn();
        printer.PowerOn();
        scanner.PowerOn();
    }

    public override void PowerOff()
    {
        base.PowerOff();
        printer.PowerOff();
        scanner.PowerOff();
    }
}

public class MultifunctionalDevice : BaseDevice, IPrinter, IScanner, IFax
{
    private Printer printer = new Printer();
    private Scanner scanner = new Scanner();

    public int PrintCounter => printer.PrintCounter;
    public int ScanCounter => scanner.ScanCounter;
    public int FaxCounter { get; private set; } = 0;

    public void Print(in IDocument document)
    {
        if (GetState() == IDevice.State.off)
            throw new InvalidOperationException("Device is off");
        printer.Print(in document);
    }

    public void Scan(out IDocument document)
    {
        if (GetState() == IDevice.State.off)
            throw new InvalidOperationException("Device is off");
        scanner.Scan(out document);
    }

    public void SendFax(in IDocument document)
    {
        if (GetState() == IDevice.State.off)
            throw new InvalidOperationException("Device is off");
        FaxCounter++;
        Console.WriteLine($"{DateTime.Now} Fax: {document.GetFileName()}");
    }

    public void ScanAndPrint()
    {
        Scan(out IDocument document);
        if (document != null)
        {
            Print(in document);
        }
    }

    public override void PowerOn()
    {
        base.PowerOn();
        printer.PowerOn();
        scanner.PowerOn();
    }

    public override void PowerOff()
    {
        base.PowerOff();
        printer.PowerOff();
        scanner.PowerOff();
    }
}