using System.Text;

if (args.Length == 0)
{
    Console.WriteLine("Usage: HexExporter.exe [Full path of file]");
    Environment.Exit(0);
}

string filePath = args[0];

FileStream st = File.OpenRead(filePath);
FileStream ot = File.OpenWrite("output.txt");
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
int line = 1;
byte[] buf = new byte[16];
while (st.Read(buf) != 0)
{
    StringBuilder resultHex = new();
    StringBuilder resultStr = new();

    foreach (var b in buf)
    {
        resultHex.Append(b.ToString("X2") + " ");
        resultStr.Append(b is >= 21 and <= 220 ? (char)b : '.');
    }
    ot.Write(Encoding.UTF8.GetBytes($"[{line++:X8}] {resultHex} {resultStr}"));
    ot.Write("\n"u8);
}
ot.Flush();