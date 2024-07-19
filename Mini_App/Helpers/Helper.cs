
namespace Mini_App.Helpers;

public static class Helper
{
    public static bool IsValidName(string name)
    {
        return !string.IsNullOrEmpty(name) && name.Length >= 3 && name.Split(' ').Length == 1 && char.IsUpper(name[0]);
    }

    public static bool IsValidSurname(string surname)
    {
        return !string.IsNullOrEmpty(surname) && surname.Length >= 3 && surname.Split(' ').Length == 1 && char.IsUpper(surname[0]);
    }

    public static bool IsValidClassroomName(string name)
    {
        return !string.IsNullOrEmpty(name) && name.Length == 5 && char.IsUpper(name[0]) && char.IsUpper(name[1]) && char.IsDigit(name[2]) && char.IsDigit(name[3]) && char.IsDigit(name[4]);
    }
}
