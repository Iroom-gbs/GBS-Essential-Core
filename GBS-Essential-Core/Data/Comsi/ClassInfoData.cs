using static GBS_Essential_Core.Data.Comsi.Utils.ComsiUtil;

namespace GBS_Essential_Core.Data.Comsi;

public record ClassInfoData
{
    public string Teacher = "";
    public string Subject = "";
    public int Day = 0;
    public int Time = 0;
    public string cls = "";

    public string ToRawJson() 
        => $"{{\"Teacher\":\"{Teacher}\",\"Subject\":\"{Subject}\",\"Day\":{Day},\"Time\":{Time},\"Class\":\"{cls}\"}}";
    public string ToJson()
        => $"{{\"Teacher\":\"{GetFullTeacherName(Subject, Teacher)}\",\"Subject\":\"{GetFullSubjectName(Subject)}\",\"Day\":{Day},\"Time\":{Time},\"Class\":\"{cls}\"}}";
}