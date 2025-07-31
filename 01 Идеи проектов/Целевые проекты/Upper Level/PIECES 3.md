
```cs
public struct StringDistance {
    public int dChar { get; set; }
    public int dLength { get; set; }
    //public static StringDistance Normalize(StringDistance dist) => dist /
    
}
```

```cs
[
    Theory,
    InlineData("aaa", "aaa", (0, 0))
    InlineData("aaa", "zzz", (3, 0))
    InlineData("aaa", "aaaaaaa", (0, 4))
    InlineData("aaa", "zzzzzzz", (3, 4))
    InlineData("", "zzz", (0, 3))
    InlineData(null, "zzz", (int.MaxValue, int.MaxValue))
    InlineData(null, "", (int.MaxValue, int.MaxValue))
] public void GetAbsoluteStringDistance(string? str1, string? str2, (int, int) expected) {
    
    int bound = Math.Min(str1.Lenght, str2.Length);
    int dc = 0;
    int dl = Math.Abs(str1.Lenght - str2.Lenght);
    
    for (int i = 0; i < bound; i++) {
        dc += str1[i] == str2[i] ? 0 : 1;
    }

    Assert.Equal((dc, dl), expected);
}

[
    Theory,
    InlineData("aaa", "aaa", (0, 0))
    InlineData("aaa", "zzz", (1, 0))
    InlineData("aaa", "aaaaaaa", (0, 4/6))
    InlineData("aaa", "zzzzzzz", (1, 4/6))
    InlineData("", "zzz", (0, 1/3))
    InlineData(null, "zzz", (1, 1))
    InlineData(null, "", (1, 1))
] public void GetNormalizedStringDistance(string? str1, string? str2, (double, double) expected) {

    var minLen = Math.Min(str1.Lenght, str2.Lenght);
    var maxLen = Math.Max(str1.Lenght, str2.Lenght);
    sd = new StringDistance(str1, str2);
    
    sd.dChar   = sd.dChar / minLen;
    sd.dLenght = sd.dLenght / maxLen;
    
    Assert.Equal((sd.dChar, sd.dLenght), expected);
}
```

#MERGE_NOTES
