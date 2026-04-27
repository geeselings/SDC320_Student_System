/************
Name: Samantha Mowery
Date: 4-23-2026
Course: SDC320
Assignment: Course Project
************/

public class Teacher : Person {
    private string Honorific { get; set; }

    public Teacher(string first, string last, string phone, string email, string honor)
        : base(first, last, phone, email) {
            Honorific = honor;
        }

    public override string ToString() {
        return base.ToString() +
            "   Honorific: " + Honorific + "\n";
    }
}