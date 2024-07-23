﻿

using Mini_App.Helpers;

namespace Mini_App;

public class Student
{
    private static int _id = 1;
    public int Id { get;  private set; }
    public string Name { get; set; }
    public string SurName { get; set; }

    public Student(string name, string surName)
    {
        if (!Helper.IsValidName(name) || !Helper.IsValidSurname(surName))
        {
            Console.WriteLine("Xeta Baş verdi");
            return;
        }
        Id = _id++;
        Name =name;
        SurName=surName;
    }
    //public void SetId(int id)
    //{
    //    Id = id;
    //}

}
