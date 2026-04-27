/************
Name: Samantha Mowery
Date: 4-27-2026
Course: SDC320
Assignment: Course Project
************/

public class CourseProject {
    public static void Main(string[] args) {
        Console.WriteLine("\nSamantha Mowery - 3.8 Course Project Submission\n");

        Teacher teach = new Teacher("Mary", "Sue", "123-456-7890", "marysue@email.com", "Miss");
        Student s1 = new Student("Samantha", "Mowery", "757-386-6491", "sammow2667@email.com", "third-year", 2.9);
        Student s2 = new Student("Adam", "Smith", "098-765-4321", "asmith@email.com", "first-year", 3.6);

        List<Person> people = new List<Person>{teach, s1, s2};
        Course c = new Course("BAS123", "General Studies", "Basics to School", 5, teach);
        c.AddStudent(s1);
        c.AddStudent(s2);

        int loop = 1;
        while(loop == 1) {
            Console.WriteLine("Choose An Action");
            Console.WriteLine("[ 1 ] View Course Information");
            Console.WriteLine("[ 2 ] View People");
            Console.WriteLine("[ 3 ] Exit");
            Console.WriteLine();

            string? choice = Console.ReadLine();

            if(choice == "1") {
                Console.WriteLine(c);
            } else if (choice == "2") {
                Console.WriteLine("Choose A Person");
                ViewPeople(people);
                Console.WriteLine();
                choice = Console.ReadLine();
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

    private static void ViewPeople(List<Person> people) {
        int i = 1;
        foreach(Person p in people) {
            Console.WriteLine("[ " + i + " ] : " + p.GetName());
            i++;
        }
    }
}