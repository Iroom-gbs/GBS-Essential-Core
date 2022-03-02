namespace GBS_Essential_Core.Data.Comsi.Utils;

public class ComsiUtil
{
    public static string GetFullTeacherName(string subject, string teacher) => teacher switch
    {
        "조아" => "조아름",
        "박현" => "박현종",
        "박규" => "박규",
        "이기" => "이기성",
        "우홍" => "우홍균",
        "이지" => "이지현",
        "김현" => "김현철",
        "이은" => "이은실",
        "박동" => "박동우",
        "전은" => "전은선",
        "감순" => "감순천",
        _ => teacher
    };

    public static string GetFullSubjectName(string subject) => subject switch
    {
        "화학2" => "화학",
        "물리2" => "물리",
        "영Ⅰ" => "영어",
        "국사" => "한국사",
        "심수Ⅱ" => "수학",
        "중어" => "중국어",
        "생명2" => "생명과학",
        "체2" => "체육",
        "문학" => "국어",
        "지구2" => "지구과학",
        "정보2" => "정보",
        _ => subject
    };
}