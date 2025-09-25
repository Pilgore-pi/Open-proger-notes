using Sandbox;

namespace TestProject1;

public class UnitTest1 {
    [
        Theory,
        InlineData("", "", 0),
        InlineData(null, null, 0),
        InlineData(null, "", int.MaxValue),
        InlineData("TEXT_TEXT", "TEXT_TEX", 1),
        InlineData("TEXT_text", "TEXT_TEXT", 4),
        InlineData("TEXT_text", "TEXT_", 4),
        InlineData("TEXT_TEXT", "TEXTTEXT", 1),
        InlineData("TEXT_TEXT", "TEXTtext", 5),
    ]
    public void StringIntersectionTest(string? str1, string str2, int expected) {
        var actual =  API.StringIntersection(str1, str2);
        Assert.Equal(expected, actual);
    }
}
