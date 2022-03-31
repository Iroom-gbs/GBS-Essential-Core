using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

using static System.Linq.Enumerable;
using static GBS_Essential_Core.Data.Comsi.Utils.ComsiUtil;

namespace GBS_Essential_Core.Data.Comsi;

public static class Comsi
{
    private static Dictionary<string, ClassInfoData[][]> classes { get; } = new();
    private static bool isUpdating = false;

    public static string GetTeacherClass(string subject, string teacher)
    {
        var sb = new StringBuilder("[");
        sb.Append(string.Join(',',
            from x in classes.Values
            from y in x
            from z in y
            where z.Subject == subject && z.Teacher == teacher
            select z.ToJson()));
        sb.Append(']');
        return sb.ToString();
    }

    public static string GetJsonOf(string cls)
    {
        var sb = new StringBuilder();
        sb.Append('[');
        foreach (var x in Range(0, 5))
        {
            sb.Append(GetJsonOf(cls, x));
            sb.Append(',');
        }

        sb.Append(']');
        return sb.ToString();
    }

    public static string GetJsonOf(string cls, int date)
    {
        var sb = new StringBuilder();
        sb.Append("{\"Day\":");
        sb.Append(date);
        sb.Append(",[");
        foreach (var x in classes[cls][date])
        {
            sb.Append(x.ToJson());
            sb.Append(',');
        }
        sb.Append("]}");
        return sb.ToString();
    }

    private static FirefoxDriver InitializeFirefox()
    {
        var options = new FirefoxOptions();
        options.AddArgument("--private-window");
        return new FirefoxDriver(options);
    }

    private static ChromeDriver InitializeChrome()
    {
        var options = new ChromeOptions();
        options.AddArgument("--incognito");
        options.AddArgument("--headless");
        return new ChromeDriver(options);
    }

    public static RenewResult ParseAll()
    {
        if (isUpdating) return RenewResult.AlreadyRunning;
        isUpdating = true;
        IWebDriver driver = InitializeChrome();
        try
        {
            driver.Url = "http://comci.kr:4082/st";
            driver.Navigate().GoToUrl("http://comci.kr:4082/st");

            driver.FindElement(By.XPath("//*[@id=\"sc\"]")).SendKeys("경기북과학고");
            driver.ExecJs("sc2_search();");
            while (driver.FindElements(By.XPath("/html/body/div[3]/table[2]/tbody/tr[2]/td[2]/a")).Count == 0)
            {
            }

            driver.ExecJs("sc_disp(12045);");

            foreach (var c in classList)
            {
                if (c == "3-5")
                    MoveToPreviousClass(driver);
                IWebElement[] classInfoHtml;
                while (true)
                {
                    try
                    {
                        classInfoHtml = driver.FindElement(By.XPath("/html/body/div[4]/div[1]/table/tbody"))
                            .FindElements(By.TagName("tr")).ToArray()[2..];
                        break;
                    }
                    catch (Exception)
                    {
                    }

                    Thread.Sleep(10);
                }

                classes[c] = new ClassInfoData[5][];
                for (var i = 0; i < 5; i++)
                    classes[c][i] = new ClassInfoData[7];
                for (var cls = 0; cls < 7; cls++)
                {
                    var dc = classInfoHtml[cls].FindElements(By.TagName("td"));
                    while (dc.Count == 0)
                        dc = classInfoHtml[cls].FindElements(By.TagName("td"));
                    for (var day = 0; day < 5; day++)
                    {
                        var k = dc[day + 1].Text.Split('\n');
                        if (k.Length < 2)
                            classes[c][day][cls] = new ClassInfoData
                            {
                                Day = day,
                                Time = cls,
                                cls = c
                            };
                        else
                            classes[c][day][cls] = new ClassInfoData
                            {
                                Day = day,
                                Subject = GetFullSubjectName(k[0]),
                                Teacher = GetFullTeacherName(k[0], k[1]),
                                Time = cls,
                                cls = c
                            };
                    }
                }

                if (c != "3-5")
                    MoveToNextClass(driver);
            }

            return RenewResult.Ok;
        }
        catch (Exception)
        {
            return RenewResult.Failed;
        }
        finally
        {
            driver.Quit();
            isUpdating = false;
        }
    }

    private static void MoveToNextClass(IWebDriver driver)
        => driver.ExecJs("ba_NextDisp(1);");
    
    private static void MoveToPreviousClass(IWebDriver driver)
        => driver.ExecJs("ba_NextDisp(-1);");

    private static List<string> classList = new()
    {
        "1-1", "1-2", "1-3", "1-4", "1-5",
        "2-1", "2-2", "2-3", "2-4", "2-5",
        "3-1", "3-2", "3-3", "3-4", "3-5"
    };
    
    static void ExecJs(this IWebDriver driver, string script) =>
        (driver as IJavaScriptExecutor)?.ExecuteScript(script);
}
