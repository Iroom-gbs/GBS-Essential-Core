namespace GBS_Essential_Core.Data.Comsi;

public record ClassInfoData
{
    public string Teacher = "";
    public string Subject = "";
    public int Day = 0;
    public int Time = 0;
    public string cls = "";

    public string ToJson()
        => $"{{\"Teacher\":\"{Teacher}\",\"Subject\":\"{Subject}\",\"Day\":{Day},\"Time\":{Time},\"Class\":\"{cls}\"}}";
}