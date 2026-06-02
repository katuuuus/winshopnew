using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

class ReceiptParser
{
    public static List<Session> Parse(string json)
        
    {
        
        var result = new List<Session>();

        var root = JObject.Parse(json);

        var data = root["data"];

        if (data == null)
            return result;

        foreach (var item in data)
        {
            DateTime parsedDate;

            var rawDate =
                item["dateOfPurchase"]?.Value<DateTime>()
                ?? DateTime.Now;


            if (rawDate.Year < 2000)
            {
                parsedDate = DateTime.Now;
            }
            else
            {
                parsedDate = rawDate;
            }

            if (parsedDate.Year < 2025)
            {
                continue;
            }

            var session = new Session
            {
                amountTotal =
                    item["amountTotal"]?.Value<decimal>()
                    ?? 0,

                dateOfPurchase = parsedDate,

                customerId =
                    item["customerId"]?.Value<int>()
                    ?? 0,

                invoiceNumber =
                    item["invoiceNumber"]?.Value<int>()
                    ?? 0,

                branchId =
                    item["branchId"]?.Value<int>()
                    ?? 0
            };

            result.Add(session);
        }

        return result;
    }
}