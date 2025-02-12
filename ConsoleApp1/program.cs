class Program
{
    struct Person
    {
    public string fname;
    public string lname;
    }
    static void Main(string[] args)
    {
        Person person1 = new Person { fname = "John", lname = "Doe" };
        Console.WriteLine(person1.lname);
    }
}








/*using System;

struct Person
{
    public string fname;
    public string lname;
}

Person person1 = new Person { fname = "John", lname = "Doe" };
Console.WriteLine(person1.lname)

void Demo()
{
    Console.WriteLine("Hello, World!");
    Console.WriteLine("Hi");
    Console.WriteLine("This is text that I have written.");
    Console.WriteLine("Eat my shorts.");

    // Using person1
    Console.WriteLine($"Person's First Name: {person1.fname}");
    Console.WriteLine($"Person's Last Name: {person1.lname}");
}

Demo();
*/