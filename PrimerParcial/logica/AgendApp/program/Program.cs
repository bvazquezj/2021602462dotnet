using System.Collections;
using System.Text.RegularExpressions;

namespace program
{
    public class Program
    {
        public ArrayList personas = new ArrayList();

        public static void Main(string[] args)
        {
            int opcion;
            Program program = new Program();
            do
            {
                Console.WriteLine("Bienvenido a AgendApp");
                Console.WriteLine("Menu principal");
                Console.WriteLine("------------------------- \n\n");
                Console.WriteLine("1. Agregar registro");
                Console.WriteLine("2. Editar registros");
                Console.WriteLine("3. Eliminar registros");
                Console.WriteLine("4. Mostrar registros");
                Console.WriteLine("5. Salir");
                Console.WriteLine("\n\n");
                Console.Write("Elige una opción: ");
                opcion = int.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        program.AgregarRegistro();
                        break;
                    case 2:
                        program.EditarRegistro();
                        break;
                    case 3:
                        program.EliminarRegistro();
                        break;
                    case 4:
                        program.MostrarRegistros();
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            } while (opcion != 5);
        }
        public void AgregarRegistro()
        {
            Console.WriteLine("Agregar registro");
            Console.WriteLine("------------------------- \n\n");


            Persona persona = new Persona();
            Console.WriteLine("ingresa el Nombre: ");
            persona.nombre = Console.ReadLine();
            Console.WriteLine("ingresa el Teléfono: ");
            persona.Teléfono = Console.ReadLine();
            Console.WriteLine("ingresa el Email: ");
            persona.Email = Console.ReadLine();

            personas.Add(persona);
            Console.WriteLine(personas.Count.ToString());
        }

        public void MostrarRegistros()
        {
            Console.WriteLine("Mostrar registros");
            Console.WriteLine("------------------------- \n\n");
            Console.WriteLine("Registros: {0}", personas.Count.ToString());
            int i = 0;
            foreach (Persona persona in personas)
            {
                Console.WriteLine("numero: {0} persona: {1}", i, persona.ToString());
                i++;
            }
            Console.WriteLine("\n ----------------------- \n\n");
        }

        public void EliminarRegistro()
        {
            Console.WriteLine("Eliminar registro");
            Console.WriteLine("------------------------- \n\n");
            MostrarRegistros();
            Console.WriteLine("Elige el registro a eliminar: ");
            int opcion = int.Parse(Console.ReadLine());
            personas.RemoveAt(opcion);
        }

        public void EditarRegistro()
        {
            int opcion;
            Console.WriteLine("Editar registro");
            Console.WriteLine("------------------------- \n\n");
            MostrarRegistros();
            Console.WriteLine("Elige el registro a editar: ");
            opcion = int.TryParse(Console.ReadLine(), out opcion) ? opcion : throw new Exception("Error: ingresa un número");

            Persona persona = (Persona)personas[opcion];
            Console.WriteLine("Ingresa el nombre: ");
            persona.nombre = Console.ReadLine();
            Console.WriteLine("Ingresa el teléfono: ");
            persona.Teléfono = Console.ReadLine();
            Console.WriteLine("Ingresa el email: ");
            persona.Email = Console.ReadLine();

            personas[opcion] = persona;
        }
    }

}



