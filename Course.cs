/************
Name: Samantha Mowery
Date: 4-23-2026
Course: SDC320
Assignment: Course Project
************/

public class Course {
    private string Code { get; set; }
    private string Category { get; set; }
    private string Name {get; set; }
    private int MaxCapacity { get; set; }
    private Teacher Instructor { get; set; }
    private List<Student> Students { get; }

    public Course(string code, string category, string name, int max, Teacher teacher) {
        Code = code;
        Category = category;
        Name = name;
        MaxCapacity = max;
        Instructor = teacher;
        Students = new List<Student>();
    }

    public string CheckCapacity() {
        if(Students.Count < MaxCapacity) {
            int rem = MaxCapacity - Students.Count;

            return string.Format("Class Open\n{0}/{1} spots remaining.",
                rem, MaxCapacity);
        } else {
            return "Class full.";
        }
    }

    public string BasicRoster() {
        string res = "";
        foreach(Student s in Students) {
            res += "    ";
            res += s.GetName();
            res += "\n";
        }

        return res;
    }

    public string DetailRoster() {
        string res = "";
        foreach (Student s in Students) {
            res += s.ToString();
        }

        return res;
    }

    public override string ToString() {
        return "Course Code: " + Code + "\n" +
            "Category: " + Category + "\n" +
            "Name: " + Name + "\n" +
            "Instructor: " + Instructor.GetName() + "\n" +
            CheckCapacity() + "\n" +
            "------------------------------------\n" +
            "Students\n" + BasicRoster(); 
    }

    public void AddStudent(Student s) {
        Students.Add(s);
    }

    public void RemoveStudent(string first, string last) {
        Students.Remove(new Student(first, last, "", "", "", 0.0));
    }
}