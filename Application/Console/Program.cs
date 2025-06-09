// See https://aka.ms/new-console-template for more information
using Kata.Checkout;
using System.Runtime.InteropServices;

var checkout = CheckoutFactory.CreateStandardItems();

void PlayScanSound()
{
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        Console.Beep(1000, 100); // For Windows OS, Frequency: 1000Hz, Duration: 100ms
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
    {
        System.Diagnostics.Process.Start("afplay", "/System/Library/Sounds/Hero.aiff");
    }
    else
    {
        Console.Write("\a"); // ASCII bell character for other platforms
    }
}

async Task ScanWithDelay(string sku)
{
    checkout.Scan(sku);
    PlayScanSound();
    Console.WriteLine("----------------------------------------");
    Console.WriteLine($"Item scanned: {sku}");
    Console.WriteLine($"Current total: ${checkout.GetTotalPrice():F2}");
    Console.WriteLine("----------------------------------------");
    await Task.Delay(1000); // 1 second delay
}

Console.WriteLine("Starting checkout scan...");
Console.WriteLine("----------------------------------------");

await ScanWithDelay("A");
await ScanWithDelay("B");
await ScanWithDelay("C");
await ScanWithDelay("D");
await ScanWithDelay("A");
await ScanWithDelay("B");
await ScanWithDelay("A");

Console.WriteLine("----------------------------------------");
Console.WriteLine($"Final total: ${checkout.GetTotalPrice():F2}");
Console.WriteLine("----------------------------------------");
