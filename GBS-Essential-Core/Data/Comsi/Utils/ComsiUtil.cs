using System.Diagnostics;

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
        "박동" => subject switch
        {
            "수학" => "박동우",
            _ => "박동"
        },
        "전은" => "전은선",
        "감순" => "감순천",
        "오상" => "오상림",
        "임석" => "임석영",
        _ => teacher
    };

    public static string GetFullSubjectName(string subject) => subject switch
    {
        "통화" or "화Ⅱ" or "AP화" or "화학2" => "화학",
        "통물" or "물Ⅱ" or "AP물" or "물리2" => "물리",
        "영Ⅱ" or "영Ⅰ" => "영어",
        "국사" => "한국사",
        "AP미" or "심수Ⅱ" => "수학",
        "중어" => "중국어",
        "통생" or "AP생" or "생명2" => "생명과학",
        "체2" or "체3" => "체육",
        "문학" => "국어",
        "지Ⅱ" or "통지" or "지구2" => "지구과학",
        "정보2" => "정보",
        "정법" => "정치와 법",
        "통사" => "통합사회",
        _ => subject
    };
}