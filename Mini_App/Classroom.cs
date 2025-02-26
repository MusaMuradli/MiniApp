﻿
using Mini_App.Helpers;
using Mini_App.Helpers.Enums;
using System.ComponentModel.Design;
using Newtonsoft.Json.Converters;

namespace Mini_App;

public class Classroom
{
    private static int _id = 1;
    public int Id { get; private set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; }
    public ClassType Type { get; set; } 
    public int MaxLimit { get; private set; }

    //private int _studentIdCounter;

    public Classroom(string name, ClassType type)
    {
        Id = _id++;
        Name = name;
        Students = new List<Student>();
        Type = type;
        MaxLimit = (int)type;
    }

    public bool AddStudent(Student student)
    {
        if (!Helper.IsValidName(student.Name) || !Helper.IsValidSurname(student.SurName))
        {
            Console.WriteLine("Yanlış telebe adı ve ya soyadı");
            return false;
        }
        if (Students.Count >= MaxLimit)
        {
            Console.WriteLine("Sinif doludur");
            return false;
        }
        Students.Add(student);
        return true;
    }
    public Student FindById(int id)
    {
        //foreach (var item in Students)
        //{
        //    if (item.Id==id)
        //    {
        //        return item;

        //    }
        //    Console.WriteLine($"{id}- id student tapilmadi");
        //}

        return Students.Find(s => s.Id == id);
    }

    public bool DeleteStudent(int id)
    {
        var student = FindById(id);
        if (student == null)
        {
            Console.WriteLine("Telebe tapılmadı");
            return false;
        }
        Students.Remove(student);
        return true;
    }





}
