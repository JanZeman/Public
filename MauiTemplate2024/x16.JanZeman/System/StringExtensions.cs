namespace JanZeman.System;

public static class StringExtensions
{
    private static readonly Random Random = new();

    public static string GetRandomString(int length = 16)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    public static string UppercaseFirst(this string s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;
        var charArray = s.ToCharArray();
        charArray[0] = char.ToUpper(charArray[0]);
        return new string(charArray);
    }
}