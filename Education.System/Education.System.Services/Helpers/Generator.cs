namespace Education.System.Services.Helpers;

public class Generator
{
    public static string GenerateEmail()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[8];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new string(stringChars);
        return finalString + "@mail.com";
    }

    public static string GeneratePassword()
    {
        var charscapital = "ABCDEFGHIJKLMNOPQRSTOVPYXZ";
        var charsmall = charscapital.ToLower();
        var numbers = "1234567890";
        var special = "!@#$%^&*";
        var stringChars = new char[11];
        var random = new Random();

        for (int i = 0; i < 5; i++)
        {
            stringChars[i] = numbers[random.Next(numbers.Length)];
        }
        for (int i = 5; i < 7; i++)
        {
            stringChars[i] = charscapital[random.Next(charscapital.Length)];
        }
        for (int i = 7; i < 9; i++)
        {
            stringChars[i] = charsmall[random.Next(charsmall.Length)];
        }
        for (int i = 9; i < 11; i++)
        {
            stringChars[i] = special[random.Next(special.Length)];
        }

        var finalString = new string(stringChars);
        return finalString;
    }
}