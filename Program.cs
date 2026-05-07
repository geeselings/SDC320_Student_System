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
            StudentDB.AddStudent(conn, new Student("Adam", "Smith", "098-765-4321", "asmith@email.com", "first-year", 3.6));
            CourseDB.AddCourse(conn, new Course("BAS123", "General Studies", "Basics to School", 5, 1));

            CourseStudentDB.AddCourseStudent(conn, "BAS123", 1);
        
            int loop = 1;
            while(loop == 1) {
                Console.WriteLine("Choose An Action");
                Console.WriteLine("[ 1 ] : Course Actions");
                Console.WriteLine("[ 2 ] : Person Actions");
                Console.WriteLine("[ 3 ] : Exit");
                Console.WriteLine();

                string? choice = Console.ReadLine();
                if(choice == "1") {
                    Console.WriteLine();
                    Console.WriteLine("Choose An Action");
                    Console.WriteLine("[ 1 ] : View All Courses");
                    Console.WriteLine("[ 2 ] : Add Course");
                    Console.WriteLine("[ 3 ] : Update Course");
                    Console.WriteLine("[ 4 ] : Delete Course");
                    Console.WriteLine("[ 5 ] : Enroll in Course");
                    Console.WriteLine("[ 6 ] : Remove from course");
                    Console.WriteLine("[ 7 ] : Return");
                    Console.WriteLine();

                    choice = Console.ReadLine();

                    if(choice == "1") {
                        ViewCourses(conn);
                    } else if (choice == "2") {
                        Console.WriteLine(AddCourse(conn));
                    } else if (choice == "3") {
                        Console.WriteLine();
                        Console.WriteLine(UpdateCourse(conn));
                    } else if (choice == "4") {
                        Console.WriteLine();
                        Console.WriteLine(DeleteCourse(conn));
                    } else if (choice == "5") {
                        Console.WriteLine();
                        Console.WriteLine(Enroll(conn));
                    } else if (choice == "6") {
                        Console.WriteLine();
                        Console.WriteLine(Unenroll(conn));
                    }
                } else if (choice == "2") {
                    Console.WriteLine();
                    Console.WriteLine("Choose An Action");
                    Console.WriteLine("[ 1 ] : View All People");
                    Console.WriteLine("[ 2 ] : Add Person");
                    Console.WriteLine("[ 3 ] : Update Person");
                    Console.WriteLine("[ 4 ] : Delete Person");
                    Console.WriteLine("[ 5 ] : Enroll in Course");
                    Console.WriteLine("[ 6 ] : Remove from course");
                    Console.WriteLine("[ 7 ] : Return");
                    Console.WriteLine();

                    choice = Console.ReadLine();

                    if(choice == "1") {
                        List<Person> people = ViewPeople(conn);
                        Console.WriteLine();
                        Console.WriteLine("Select a Person");
                        PrintNames(people);
                        Console.WriteLine();

                        choice = Console.ReadLine();
                        if(int.TryParse(choice, out int result) && result <= people.Count) {
                            Console.WriteLine(people[result - 1]);
                        }
                    } else if (choice == "2") {
                        Console.WriteLine(AddPerson(conn));
                    } else if (choice == "3") {
                        Console.WriteLine();
                        Console.WriteLine(UpdatePerson(conn));
                    } else if (choice == "4") {
                        Console.WriteLine();
                        Console.WriteLine(DeletePerson(conn));
                    } else if (choice == "5") {
                        Console.WriteLine();
                        Console.WriteLine(Enroll(conn));
                    } else if (choice == "6") {
                        Console.WriteLine();
                        Console.WriteLine(Unenroll(conn));
                    }
                } else if (choice == "3") {
                    break;
                }
            }
        }
    }

    // view all people in the database for the school. includes teachers and students
    private static List<Person> ViewPeople(SQLiteConnection conn) {
        // list to store all people
        List<Person> people = new List<Person>();

        // retrieve all students from database, store them in list
        foreach (Student s in StudentDB.GetAllStudents(conn)) {
            people.Add(s);
        }

        // retrieve all teachers from database, store them in list
        foreach (Teacher t in TeacherDB.GetAllTeachers(conn)) {
            people.Add(t);
        }
        return people;
    }

    // print names of people in database, formatted for options
    private static void PrintNames(List<Person> people) {
        int i = 1;
        foreach(Person p in people) {
            Console.WriteLine("[ " + i + " ] : " + p.GetName());
            i++;
        }
    }

    // view all courses currently in database, with basic information about them
    private static void ViewCourses(SQLiteConnection conn) {
        foreach (Course c in CourseDB.GetAllCourses(conn)) {
            // get teacher for course based on the instructor id stored in database
            Teacher t = TeacherDB.GetTeacher(conn, c.InstructorID);

            // get students currently in course based on the CourseStudent table
            List<int> students = CourseStudentDB.GetStudentsByCourse(conn, c.Code);
            foreach(int s in students) {
                c.AddStudent(StudentDB.GetStudent(conn, s));
            }

            Console.WriteLine();
            Console.WriteLine(c.CourseInfo(t.GetName()));
        }
    }

    // create a new course
    private static string AddCourse(SQLiteConnection conn) {
        Console.WriteLine();
        string code = string.Empty;
        string category = string.Empty;
        string name = string.Empty;
        int capacity = -1;
        int instructor = -1;

        // obtain user input
        Console.WriteLine("Course Code - follows format ABC123: ");
        code = Console.ReadLine();

        Console.WriteLine("Category: ");
        category = Console.ReadLine();

        Console.WriteLine("Course Name: ");
        name = Console.ReadLine();

        Console.WriteLine("Maximum Students - integer value: ");
        capacity = int.Parse(Console.ReadLine());

        Console.WriteLine("Select an instructor: ");
        List<Teacher> teachers = TeacherDB.GetAllTeachers(conn);
        int i = 1;
        foreach(Teacher t in teachers) {
            Console.WriteLine("[ " + i + " ] : " + t.GetName());
            i++;
        }
        instructor = int.Parse(Console.ReadLine());

        // validate input
        List<Course> courses = CourseDB.GetAllCourses(conn);
        List<string> codes = new List<string>();
        foreach (Course c in courses) {
            codes.Add(c.Code);
        }

        if (code.Length != 6 || codes.Contains(code)) {
            return "Course not saved.\nPlease ensure that course code follows the format ABC123, and that course code does not currently exist.\n";
        } else if (category.Length == 0) {
            return "Course not saved.\nPlease enter a category.\n";
        } else if (name.Length == 0) {
            return "Course not saved.\nPlease enter a name.\n";
        } else if (capacity < 1) {
            return "Course not saved.\nCourse must allow at least one student.\n";
        } else if (instructor < 0 || instructor > teachers.Count - 1) {
            return "Course not saved.\nInstructor must be selected from numbered list.\n";
        } else {
            CourseDB.AddCourse(conn, new Course(code, category, name, capacity, instructor));
            return "Course saved.\n";
        }
    }

    // create a new person
    private static string AddPerson(SQLiteConnection conn) {
        string first;
        string last;
        string phone;
        string email;
        int type;

        // determine type of person + get values
        Console.WriteLine("\nSelect Type of Person");
        Console.WriteLine("[ 1 ] : Teacher\n[ 2 ] : Student");
        type = int.Parse(Console.ReadLine());

        if (type != 1 && type != 2) {
            return "Invalid type chosen.\n";
        }

        Console.WriteLine("First Name: ");
        first = Console.ReadLine();

        Console.WriteLine("Last Name: ");
        last = Console.ReadLine();

        Console.WriteLine("Phone Number: ");
        phone = Console.ReadLine();

        Console.WriteLine("Email Address: ");
        email = Console.ReadLine();

        // validate input and create object based on previous decision
        if (type == 1) {
            string honorific;
            Console.WriteLine("Honorific - Miss, Mister, etc.");
            honorific = Console.ReadLine();

            if (first == string.Empty || last == string.Empty || phone == string.Empty || email == string.Empty || honorific == string.Empty) {
                return "Teacher not saved.\nPlease ensure all fields have a value before submitting.\n";
            } else {
                TeacherDB.AddTeacher(conn, new Teacher(first, last, phone, email, honorific));
                return "Teacher saved.\n";
            }
        } else if (type == 2) {
            string year;
            double gpa;

            Console.WriteLine("Academic Year - first-year, second-year, etc.");
            year = Console.ReadLine();

            Console.WriteLine("Current GPA - decimal value: ");
            gpa = double.Parse(Console.ReadLine());

            if (first == string.Empty || last == string.Empty || phone == string.Empty || email == string.Empty || year == string.Empty) {
                return "Student not saved.\nPlease ensure all fields have a value before submitting.\n";
            } else if (gpa < 0) {
                return "Student not saved.\nNegative GPA is not allowed.\n";
            } else {
                StudentDB.AddStudent(conn, new Student(first, last, phone, email, year, gpa));
                return "Student saved.\n";
            }
        }
        return "Unexpected error encountered.\n";
    }

    // update existing person
    private static string UpdatePerson(SQLiteConnection conn) {
        // determine type of person being updated
        Console.WriteLine("Pick a type of person to update");
        Console.WriteLine("[ 1 ] : Teacher\n[ 2 ] : Student");
        int type = int.Parse(Console.ReadLine());

        if (type != 1 && type != 2) {
            return "Invalid type chosen.\n";
        }

        // determine who is being updated
        // if type 1 was chosen, only teachers are shown. validation is based on teachers
        // if type 2 was chosen, only students are shown. validation is based on students
        Console.WriteLine("\nPick a person to update");
        if (type == 1) {
            List<Teacher> teachers = TeacherDB.GetAllTeachers(conn);
            int i = 1;
            foreach (Teacher t in teachers) {
                Console.WriteLine("[ " + i + " ] : " + t.GetName());
                i++;
            }
            
            int id = int.Parse(Console.ReadLine());
            Teacher upd = TeacherDB.GetTeacher(conn, id);

            Console.WriteLine("\nPick a field to update");
            Console.WriteLine("[ 1 ] : First Name\n[ 2 ] : Last Name\n[ 3 ] : Phone Number\n[ 4 ] : Email Address\n[ 5 ] : Honorific");
            string? choice = Console.ReadLine();
            
            if (choice == "1") {
                Console.WriteLine("\nEnter the new first name: ");
                string? first = Console.ReadLine();
                if (first != string.Empty) {
                    upd.FirstName = first;
                }
            } else if (choice == "2") {
                Console.WriteLine("\nEnter the new last name: ");
                string? last = Console.ReadLine();
                if (last != string.Empty) {
                    upd.LastName = last;
                }
            } else if (choice == "3") {
                Console.WriteLine("\nEnter the new phone number: ");
                string? phone = Console.ReadLine();
                if (phone != string.Empty) {
                    upd.Phone = phone;
                }
            } else if (choice == "4") {
                Console.WriteLine("\nEnter the new email address: ");
                string? email = Console.ReadLine();
                if (email != string.Empty) {
                    upd.Email = email;
                }
            } else if (choice == "5") {
                Console.WriteLine("\nEnter the new honorific: ");
                string? honor = Console.ReadLine();
                if (honor != string.Empty) {
                    upd.Honorific = honor;
                }
            }

            TeacherDB.UpdateTeacher(conn, upd);
        } else if (type == 2) {
            List<Student> students = StudentDB.GetAllStudents(conn);
            int i = 1;
            foreach (Student s in students) {
                Console.WriteLine("[ " + i + " ] : " + s.GetName());
                i++;
            }
            
            int id = int.Parse(Console.ReadLine());
            Student upd = StudentDB.GetStudent(conn, id);

            Console.WriteLine("\nPick a field to update");
            Console.WriteLine("[ 1 ] : First Name\n[ 2 ] : Last Name\n[ 3 ] : Phone Number\n[ 4 ] : Email Address\n[ 5 ] : Academic Year\n[ 6 ] : GPA");
            string? choice = Console.ReadLine();
            
            if (choice == "1") {
                Console.WriteLine("\nEnter the new first name: ");
                string? first = Console.ReadLine();
                if (first != string.Empty) {
                    upd.FirstName = first;
                }
            } else if (choice == "2") {
                Console.WriteLine("\nEnter the new last name: ");
                string? last = Console.ReadLine();
                if (last != string.Empty) {
                    upd.LastName = last;
                }
            } else if (choice == "3") {
                Console.WriteLine("\nEnter the new phone number: ");
                string? phone = Console.ReadLine();
                if (phone != string.Empty) {
                    upd.Phone = phone;
                }
            } else if (choice == "4") {
                Console.WriteLine("\nEnter the new email address: ");
                string? email = Console.ReadLine();
                if (email != string.Empty) {
                    upd.Email = email;
                }
            } else if (choice == "5") {
                Console.WriteLine("\nEnter the new academic year: ");
                string? year = Console.ReadLine();
                if (year != string.Empty) {
                    upd.Year = year;
                }
            } else if (choice == "6") {
                Console.WriteLine("\nEnter the new GPA: ");
                double gpa = double.Parse(Console.ReadLine());
                if (gpa >= 0) {
                    upd.GPA = gpa;
                }
            }

            StudentDB.UpdateStudent(conn, upd);
        }
        return "Changes saved.";
    }

    // delete person by id. only allows for students to be deleted
    private static string DeletePerson (SQLiteConnection conn) {
        Console.WriteLine("\nPick a student to delete - note : teachers cannot be deleted");
        List<Student> students = StudentDB.GetAllStudents(conn);
        int i = 1;
        foreach (Student s in students) {
            Console.WriteLine("[ " + i + " ] : " + s.GetName());
            i++;
        }
        
        int id = int.Parse(Console.ReadLine());
        // removes from student database
        StudentDB.DeleteStudent(conn, id);
        // removes from joining database between classes and students
        CourseStudentDB.DeleteByStudent(conn, id);
        return "Student deleted.";
    }

    // update course information
    private static string UpdateCourse (SQLiteConnection conn) {
        Console.WriteLine("Pick a course to update");
        List<Course> courses = CourseDB.GetAllCourses(conn);
        int i = 1;
        foreach(Course c in courses) {
            Console.WriteLine("[ " + i + " ] : " + c.Code + " - " + c.Name);
        }

        int id = int.Parse(Console.ReadLine());
        Course upd = CourseDB.GetCourse(conn, courses[id - 1].Code);

        Console.WriteLine("\nPick a field to update");
        Console.WriteLine("[ 1 ] : Category\n[ 2 ] : Name\n[ 3 ] : Max Capacity\n[ 4 ] : Teacher");
        string? choice = Console.ReadLine();

        if (choice == "1") {
            Console.WriteLine("\nEnter the new category: ");
            string? cat = Console.ReadLine();
            if (cat != string.Empty) {
                upd.Category = cat;
            }
        } else if (choice == "2") {
            Console.WriteLine("\nEnter the new course name: ");
            string? name = Console.ReadLine();
            if (name != string.Empty) {
                upd.Name = name;
            }
        } else if (choice == "3") {
            Console.WriteLine("\nEnter the new maximum capacity - integer values only: ");
            int max = int.Parse(Console.ReadLine());
            if (max > 0) {
                upd.MaxCapacity = max;
            }
        } else if (choice == "4") {
            Console.WriteLine("\nChoose the new teacher");
            List<Teacher> teachers = TeacherDB.GetAllTeachers(conn);
            int ti = 1;
            foreach(Teacher t in teachers) {
                Console.WriteLine("[ " + ti + " ] : " + t.GetName());
            }

            int teach = int.Parse(Console.ReadLine());

            if (teach > 0 && teach <= teachers.Count) {
                upd.InstructorID = teach;
            }
        }
        CourseDB.UpdateCourse(conn, upd);
        return "Changes saved.\n";
    }

    // delete course by id
    private static string DeleteCourse (SQLiteConnection conn) {
        Console.WriteLine("\nPick a course to delete");
        List<Course> courses = CourseDB.GetAllCourses(conn);
        int i = 1;
        foreach (Course c in courses) {
            Console.WriteLine("[ " + i + " ] : " + c.Code + " - " + c.Name);
            i++;
        }

        int id = int.Parse(Console.ReadLine());
        string del = courses[id - 1].Code;
        // removes from course table
        CourseDB.DeleteCourse(conn, del);
        // removes from joining database between courses and students
        CourseStudentDB.DeleteByCourse(conn, del);
        return "Course deleted.";
    }

    // enrolls student in class
    private static string Enroll (SQLiteConnection conn) {
        Console.WriteLine("Pick a course to enroll in - filled classes are not shown");
        List<Course> courses = CourseDB.GetAllCourses(conn);
        List<Course> available = new List<Course>();
        int i = 1;
        
        foreach (Course c in courses) {
            // add students to proper class
            List<int> cStudents = CourseStudentDB.GetStudentsByCourse(conn, c.Code);
            foreach(int s in cStudents) {
                c.AddStudent(StudentDB.GetStudent(conn, s));
            }

            // determine if class is full. displays only open classes
            string capacity = c.CheckCapacity();
            if (capacity != "Class full.") {
                available.Add(c);
                Console.WriteLine("[ " + i + " ] : " + c.Code + " - " + c.Name);
                Console.WriteLine("\t" + capacity);
                i++;
            }
        }

        int id = int.Parse(Console.ReadLine());
        string code = available[id - 1].Code;

        // pick student to be added
        Console.WriteLine("\nPick the student being enrolled in " + code);
        List<Student> students = StudentDB.GetAllStudents(conn);

        i = 1;
        foreach (Student s in students) {
            Console.WriteLine("[ " + i + " ] : " + s.GetName());
            i++;
        }

        id = int.Parse(Console.ReadLine());

        // adds student and course to joining table
        CourseStudentDB.AddCourseStudent(conn, code, id);
        return "Student enrolled.";
    }

    // removes student from class without deleting entire student
    private static string Unenroll (SQLiteConnection conn) {
        Console.WriteLine("Pick a course to remove a student from");
        List<Course> courses = CourseDB.GetAllCourses(conn);
        int i = 1;
        
        foreach (Course c in courses) {
            Console.WriteLine("[ " + i + " ] : " + c.Code + " - " + c.Name);
            i++;
        }

        int id = int.Parse(Console.ReadLine());
        string code = courses[id - 1].Code;

        // pick student to be removed
        Console.WriteLine("\nPick the student being removed from " + code);
        List<int> sId = CourseStudentDB.GetStudentsByCourse(conn, code);
        List<Student> cStudents = new List<Student>();

        i = 1;
        foreach (int n in sId) {
            Student s = StudentDB.GetStudent(conn, n);
            Console.WriteLine("[ " + i + " ] : " + s.GetName());
            cStudents.Add(s);
            i++;
        }

        int stu = int.Parse(Console.ReadLine());
        id = cStudents[stu - 1].ID;

        // adds student and course to joining table
        CourseStudentDB.DeleteCourseStudent(conn, code, id);
        return "Student removed from course.";
    }
}