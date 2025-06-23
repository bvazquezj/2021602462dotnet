using System.Text.RegularExpressions;

namespace program
{
    public class Persona
{
    public string nombre { get; set; }
    private string teléfono;
    private string email;

    public string Teléfono
    {
        get { return teléfono; }

        set
        {
            Regex regex = new Regex(@"[0-9 + \s]{3}[0-9 + \s]{5}[0-9]{4}");
            if (!regex.IsMatch(value.ToString()))
            {
                teléfono = "Error: usa XX-XXXX-XXXX";
            }
            teléfono = value;
        }
    }
    public string Email
    {
        get { return email; }
        set
        {
            Regex regex = new Regex(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}");
            if (!regex.IsMatch(value.ToString()))
            {
                email = "Error: usa un email valido mailt@mail.com";
            }
            email = value;
        }
    }
    public Persona() { }

    public Persona(string nombre, string teléfono, string email)
    {
        this.nombre = nombre;
        this.teléfono = teléfono;
        this.email = email;
    }

    public override string ToString()
    {
        return $"Nombre: {nombre}, Teléfono: {teléfono}, Email: {email}";
    }
}    
}
