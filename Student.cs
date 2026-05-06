/************
Name: Samantha Mowery
Date: 4-23-2026
Course: SDC320
Assignment: Course Project
************/

public class Student : Person {
    public string Year { get; set; }
    public double GPA { get; set; }

    public Student(int id, string first, string last, string phone, string email, string year, double gpa)
        : base(id, first, last, phone, email) {
            Year = year;
            GPA = gpa;
        }
    
    public Student(string first, string last, string phone, string email, string year, double gpa)
        : base(first, last, phone, email) {
            Year = year;
            GPA = gpa;
        }

    public override string ToString() {
        return base.ToString() +
            "   Academic Year: " + Year + "\n" +
            "   GPA: " + GPA + "\n";
    }
}