using System.Collections;
using System.Xml;
using GBS_Essential_Core.Extensions;

namespace GBS_Essential_Core.Data.Meal;

public static class Meal
{
    public static async Task<string> GetMealString()
    {
        using var wc = new HttpClient();
        var xml = new XmlDocument();
        xml.LoadXml(await wc.GetStringAsync(
            $"https://open.neis.go.kr/hub/mealServiceDietInfo?ATPT_OFCDC_SC_CODE=J10&SD_SCHUL_CODE=7530851&MLSV_YMD={DateTime.Now:yyyyMMdd}"));

        // ReSharper disable once SuspiciousTypeConversion.Global
        (xml["mealServiceDietInfo"]!.SelectNodes("row")! as IEnumerable<XmlElement>)!.ForEach(x =>
        {
            x["DDISH_NM"].InnerText.Split("<br/>").Select(x =>
            {
                var k = x.Split('*');
                var kk = k[1].TrimEnd('.').Split('.').Select(int.Parse).ToArray();
                if (kk[0] > 20) kk[0] %= 10;
                return (k[0], kk);
            }).ForEach(x =>
            {
                Console.WriteLine(x.Item1);
                Console.WriteLine(string.Join(' ', x.kk));
                Console.WriteLine();
            });
        });

        return "";
    }
}