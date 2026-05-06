/************
Name: Samantha Mowery
Date: 4-23-2026
Course: SDC320
Assignment: Course Project
************/

public abstract class Person {
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public Person(int id, string first, string last, string phone, string email) {
        ID = id;
        FirstName = first;
        LastName = last;
        Phone = phone;
        Email = email;
    }

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
        return "Name: " + GetName() + "\n" +
            "   Phone: " + Phone + "\n" +
            "   Email: " + Email + "\n";
    }
}