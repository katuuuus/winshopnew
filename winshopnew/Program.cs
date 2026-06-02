using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("🚀 START");

            // =========================
            // LOGIN
            // =========================

            string token = await WinShopAuth.GetToken(
                "3031f9be-7e68-425e-9862-afa9875ad3d0",
                "qXy105YzBKRp2mdFbTFd8cMIvN6kDCP92RqEsMsymLs9Tj930kXOndB1wZd3tFTD"
            );

            Console.WriteLine("🔐 TOKEN OK");

            // =========================
            // LOAD DATA
            // =========================

            var contio = new ContioClient(token);

            var rawJson =
                await contio.GetReceiptsRaw();
 

            Console.WriteLine("📥 JSON loaded");

            // =========================
            // PARSE
            // =========================

            var sessions =
                ReceiptParser.Parse(rawJson);

            Console.WriteLine(
                $"📦 Parsed sessions: {sessions.Count}"
            );

            // =========================
            // SEND TO WINSHOP
            // =========================

            var winshop =
                new WinShopClient(token);

            int success = 0;
            int failed = 0;

            foreach (var s in sessions)
            {
                try
                {
                    await winshop.SendSession(s);

                    success++;

                    Console.WriteLine(
                        $"✅ Invoice {s.invoiceNumber} | " +
                        $"{s.amountTotal} Kč | " +
                        $"{s.dateOfPurchase:dd.MM.yyyy}"
                    );
                }
                catch
                {
                    failed++;

                    Console.WriteLine(
                        $"❌ Failed invoice {s.invoiceNumber}"
                    );
                }
            }

            Console.WriteLine();
            Console.WriteLine("🏁 IMPORT FINISHED");
            Console.WriteLine($"✅ Success: {success}");
            Console.WriteLine($"❌ Failed: {failed}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ ERROR:");
            Console.WriteLine(ex.ToString());
        }

        Console.ReadKey();
    }
}
