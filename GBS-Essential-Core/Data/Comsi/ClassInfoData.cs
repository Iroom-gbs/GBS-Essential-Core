namespace GBS_Essential_Core.Data.Comsi;

public class ClassInfoData
{
    public string Teacher = "";
    public string Subject = "";
    public int Day = 0;
    public int Time = 0;

    public string ToJson()
        => $"{{\"Teacher\":\"{Teacher}\",\"Subject\":\"{Subject}\",\"Day\":{Day},\"Time\":{Time}}}";
}