
using Mini_App;
using Mini_App.Exceptions;
using Mini_App.Helpers;
using Mini_App.Helpers.Enums;

string studentPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Jsons", "classrooms.json");
string result;
using(StreamReader sr = new StreamReader(studentPath))
{
    result = sr.ReadToEnd();
}

List<Classroom> classrooms = new();
List<Student> students = new List<Student>();
sinifYaradildi:
Console.WriteLine("Menu:");
Console.WriteLine("1. Classroom yarat");
Console.WriteLine("2. Student yarat");
Console.WriteLine("3. Bütün telebeleri ekrana çıxart");
Console.WriteLine("4. Seçilmiş sinifdeki telebeleri ekrana çıxart");
Console.WriteLine("5. Telebe sil");
Console.WriteLine("0. Çıxış");
while (true)
{
    var choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            {
            checkname:
                Console.WriteLine("Sinifin adını daxil edin:");
                var name = Console.ReadLine();
                if (classrooms.Exists(c => c.Name == name))
                {
                    Console.WriteLine("Bu adla sinif artıq mövcuddur.");
                    goto checkname;
                    break;
                }

                if (!Helper.IsValidClassroomName(name))
                {
                    Console.WriteLine("Xeta bash verdi");
                    break;
                }
                Console.WriteLine("Sinifin növünü seçin (Backend/FrontEnd):");
                var typeInput = Console.ReadLine();
                if (!Enum.TryParse<ClassType>(typeInput, true, out var type))
                {
                    Console.WriteLine("Yanlış sinif növü.");
                    break;
                }

                var classroom = new Classroom(name, type);
                if (classroom != null)
                {
                    classrooms.Add(classroom);
                    Console.WriteLine("Sinif yaradıldı.");
                    goto sinifYaradildi;
                }
                break;
            }
        case "2":
            {
                Console.WriteLine("Telebenin adını daxil edin:");
                var name = Console.ReadLine();
                Console.WriteLine("Telebenin soyadini daxil edin:");
                var surName = Console.ReadLine();
                Student student = new(name, surName);
                if (student == null || student.Id == 0)
                {
                    return;
                }
                Console.WriteLine("Telebenin daxil edileceyi sinfin adını daxil edin:");
                foreach (var item in classrooms)
                {
                    Console.WriteLine(item.Name);
                }
                var className = Console.ReadLine();
                var classroom = classrooms.Find(c => c.Name == className);
                if (classroom == null)
                {
                    throw new StudentNotFoundException($"Sinif '{className}' tapılmadı.");
                }

                if (classroom.AddStudent(student))
                {
                    Console.WriteLine($"Telebe {className} sinife elave olundu.");
                    goto sinifYaradildi;
                }
            }
            break;
        case "3":
            {
                Console.WriteLine("Hansi klassdaki telebeleri gormek isdeyirsiniz");
                foreach (var item in classrooms)
                {
                    Console.WriteLine($"{item.Id} {item.Name}");
                }

                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    var classroom = classrooms.Find(c => c.Id == id);
                    if (classroom == null)
                    {
                        Console.WriteLine("Sinif tapılmadı.");
                        break;
                    }

                    if (classroom.Students.Count == 0)
                    {
                        Console.WriteLine("Sinifde telebe yoxdur.");
                        break;
                    }

                    foreach (var student in classroom.Students)
                    {
                        Console.WriteLine($"ID: {student.Id}, Ad: {student.Name}, Soyad: {student.SurName}");
                        goto sinifYaradildi;
                    }
                }
                else
                {
                    Console.WriteLine("Yanlış ID daxil edildi.");
                    goto sinifYaradildi;
                }
                break;
            }
        case "4":
            {
                Console.WriteLine("Secim e: 1)Id or 2)Name");
                var subChoice = Console.ReadLine();

                switch (subChoice)
                {
                    case "1":
                        {
                            Console.WriteLine("Class Id daxil e:");
                            if (!int.TryParse(Console.ReadLine(), out int input))
                            {
                                Console.WriteLine("Yanlış Id.");
                                break;
                            }
                            var classroomById = classrooms.Find(x => x.Id == input);
                            if (classroomById == null)
                            {
                                Console.WriteLine("Sinif tapilmadi");
                                break;
                            }
                            if (classroomById.Students.Count == 0)
                            {
                                Console.WriteLine("Sinifde student yoxdu");
                                break;
                            }
                            foreach (var student in classroomById.Students)
                            {
                                Console.WriteLine($"ID: {student.Id}, Ad: {student.Name}, Soyad: {student.SurName}");
                            }
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("Class adini daxil e:");
                            var name = Console.ReadLine();
                            var classroom = classrooms.Find(x => x.Name == name);
                            if (classroom == null)
                            {
                                Console.WriteLine("Sinif tapilmadi");
                                break;
                            }
                            if (classroom.Students.Count == 0)
                            {
                                Console.WriteLine("Sinifde student yoxdu");
                                break;
                            }
                            foreach (var student in classroom.Students)
                            {
                                Console.WriteLine($"ID: {student.Id}, Ad: {student.Name}, Soyad: {student.SurName}");
                            }
                            break;
                        }
                    default:
                        Console.WriteLine("Yanlış seçim.");
                        break;
                }
                break;
            }
        case "5":
            {
                Console.WriteLine("Silinecek telebenin ID-ni daxil e:");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Yanlış ID.");
                    break;
                }
                bool isExist = false;
                foreach (var telebe in classrooms)
                {
                    if (telebe.DeleteStudent(id))
                    {
                        Console.WriteLine("Telebe silindi.");
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                    Console.WriteLine("Telebe tapılmadı.");
                break;
            }
        case "0":
            return;
        default:
            Console.WriteLine("Yanlış seçim, yeniden cehd edin.");
            goto sinifYaradildi;
            break;
    }
}












//static void CreateClassroom(List<Classroom> classrooms)
//{
//repeat:
//    Console.WriteLine("Sinifin adını daxil e:");
//    var name = Console.ReadLine();
//    if (!Helper.IsValidClassroomName(name))
//    {
//        Console.WriteLine("Xeta bash verdi");
//        goto repeat;
//    }
//classType:
//    Console.WriteLine("Sinifin növünü seçin (Backend/FrontEnd):");
//    var typeInput = Console.ReadLine();
//    if (!Enum.TryParse<ClassType>(typeInput, true, out var type))
//    {
//        Console.WriteLine("Yanlış sinif növü.");
//        goto classType;
//    }

//    var classroom = new Classroom(name, type);
//    if (classroom != null)
//    {
//        classrooms.Add(classroom);
//        Console.WriteLine("Sinif yaradıldı.");

//    }
//}

//static void CreateStudent(List<Student> students, List<Classroom> classrooms)
//{
//    Console.WriteLine("Telebenin adını daxil e:");
//    var name = Console.ReadLine();
//    Console.WriteLine("Telebenin soyadini daxil e:");
//    var surName = Console.ReadLine();
//    Student student = new(name, surName);
//    if (student == null || student.Id == 0)
//    {
//        return;
//    }
//    Console.WriteLine("Telebenin daxil edileceyi sinfin adını daxil edin:");
//    var className = Console.ReadLine();
//    var classroom = classrooms.Find(c => c.Name == className);
//    if (classroom == null)
//    {
//        throw new StudentNotFoundException($"Sinif '{className}' tapılmadı.");
//    }

//    if (classroom.AddStudent(student))
//    {
//        Console.WriteLine($"Telebe {className}  sinife elave olundu.");
//    }
//}

//static void AllStudents(List<Classroom> classrooms)
//{
//    Console.WriteLine("Hansi klassdaki studetleri gormek isdeyirsiniz");
//    foreach (var item in classrooms)
//    {
//        Console.WriteLine(item.Name);

//    }



//    //if (students.Count == 0)
//    //{
//    //    Console.WriteLine("Heç bir telebe yoxdur.");
//    //    return;
//    //}
//    //foreach (var student in students)
//    //{
//    //    Console.WriteLine($"ID: {student.Id}, Ad: {student.Name}, Soyad: {student.SurName}");
//    //}
//}

//static void StudentsInClassroom(List<Classroom> classrooms)
//{

//    Console.WriteLine("Secim e: 1)Id or 2)Name");
//    var choice = Console.ReadLine();

//    switch (choice)
//    {
//        case "1":
//            Console.WriteLine("Class Id daxil e:");
//            int input = int.Parse(Console.ReadLine());
//            var classroomByid = classrooms.Find(x => x.Id == input);
//            if (classroomByid == null)
//            {
//                Console.WriteLine("Sinif tapilmadi");
//                return;
//            }
//            if (classroomByid.Students.Count == 0)
//            {
//                Console.WriteLine("Sinifde student yoxdu");
//                return;
//            }
//            foreach (var student in classroomByid.Students)
//            {
//                Console.WriteLine($"ID: {student.Id}, Ad: {student.Name}, Soyad: {student.SurName}");
//            }

//            break;
//        case "2":
//            Console.WriteLine("Class adini daxil e:");
//            var name = Console.ReadLine();
//            var classroom = classrooms.Find(x => x.Name == name);
//            if (classroom == null)
//            {
//                Console.WriteLine("Sinif tapilmadi");
//                return;
//            }
//            if (classroom.Students.Count == 0)
//            {
//                Console.WriteLine("Sinifde student yoxdu");
//                return;
//            }
//            foreach (var student in classroom.Students)
//            {
//                Console.WriteLine($"ID: {student.Id}, Ad: {student.Name}, Soyad: {student.SurName}");
//            }
//            break;
//        default:
//            break;
//    }


//}
//static void DeleteStudent(List<Classroom> classrooms)
//{
//    Console.WriteLine("Silinecek telebenin ID-ni daxil e:");
//    if (!int.TryParse(Console.ReadLine(), out int id))
//    {
//        Console.WriteLine("Yanlış ID.");
//        return;
//    }

//    foreach (var telebe in classrooms)
//    {
//        if (telebe.DeleteStudent(id))
//        {
//            Console.WriteLine("Telebe silindi.");
//            return;
//        }
//    }
//    Console.WriteLine("Telebe tapılmadı.");
//}