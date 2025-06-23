public class Persona{
    public string name { get; set; }
    public int age { get; set; }
    public string cellFone { get; set; }

    public Persona(string name, int age, string cellFone){
        this.name = name;
        this.age = age;
        this.cellFone = cellFone;
    }

    
    public override string ToString()
    {
        return $"Name: {name}, Age: {age}, CellFone: {cellFone}";
    }
}