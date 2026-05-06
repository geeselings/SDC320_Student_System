/************
Name: Samantha Mowery
Date: 4-27-2026
Course: SDC320
Assignment: Course Project
************/
using System.Data.SQLite;

public class CourseProject {
    public static void Main(string[] args) {
        Console.WriteLine("\nSamantha Mowery - 4.6 Course Project Submission\n");

        const string dbName = "Mowery.db";
        SQLiteConnection conn = SQLiteDatabase.Connect(dbName);

        if (conn != null) {
            CourseDB.CreateTable(conn);
            TeacherDB.CreateTable(conn);
            StudentDB.CreateTable(conn);
            CourseStudentDB.CreateTable(conn);

            TeacherDB.AddTeacher(conn, new Teacher("Mary", "Sue", "123-456-7890", "marysue@email.com", "Miss"));
            StudentDB.AddStudent(conn, new Student("Samantha", "Mowery", "757-386-6491", "sammow2667@email.com", "third-year", 2.9));
            StudentDB.AddStudent(conn, new Student("Adam", "Smith", "098-765-4321", "asmith@email.com", "first-year", 3.6));
            CourseDB.AddCourse(conn, new Course("BAS123", "General Studies", "Basics to School", 5, 1));

            CourseStudentDB.AddCourseStudent(conn, "BAS123", 1);
            CourseStudentDB.AddCourseStudent(conn, "BAS123", 2);
        
            int loop = 1;
            while(loop == 1) {
                Console.WriteLine("Choose An Action");
                Console.WriteLine("[ 1 ] View Course Information");
                Console.WriteLine("[ 2 ] View People");
                Console.WriteLine("[ 3 ] Exit");
                Console.WriteLine();

                string? choice = Console.ReadLine();

                if(choice == "1") {
                    // view course info
                    ViewCourses(conn);
                } else if (choice == "2") {
                    List<Person> people = ViewPeople(conn);
                    Console.WriteLine();

                    // view people
                    if(choice == "1") {
                        Console.WriteLine(people[0]);
                    } else if (choice == "2") {
                        Console.WriteLine(people[1]);
                    } else if (choice == "3") {
                        Console.WriteLine(people[2]);
                    }
                } else if (choice == "3") {
                    break;
                }
            }
        }
    }

    private static List<Person> ViewPeople(SQLiteConnection conn) {
        int i = 1;
        List<Person> people = new List<Person>();

        foreach (Student s in StudentDB.GetAllStudents(conn)) {
            people.Add(s);
        }

        foreach (Teacher t in TeacherDB.GetAllTeachers(conn)) {
            people.Add(t);
        }

        foreach(Person p in people) {
            Console.WriteLine("[ " + i + " ] : " + p.ToString());
            i++;
        }

        return people;
    }

    private static void ViewCourses(SQLiteConnection conn) {
        List<Course> courses = new List<Course>();

        foreach (Course c in CourseDB.GetAllCourses(conn)) {
            Console.WriteLine(c.CourseInfo("Teacher Name"));
        }
    }
}