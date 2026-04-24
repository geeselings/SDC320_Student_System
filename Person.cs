/************
Name: Samantha Mowery
Date: 4-23-2026
Course: SDC320
Assignment: Course Project
************/

public abstract class Person {
    private string FirstName { get; set; }
    private string LastName { get; set; }
    private string Phone { get; set; }
    private string Email { get; set; }

    public Person(string first, string last, string phone, string email) {
        FirstName = first;
        LastName = last;
        Phone = phone;
        Email = email;
    }

    public string GetName() {
        return FirstName + " " + LastName;
    }

    public override string ToString() {
        return "Name: " + FirstName + LastName + "\n" +
            "   Phone: " + Phone + "\n" +
            "   Email: " + Email + "\n";
    }
}