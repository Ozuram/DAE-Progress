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