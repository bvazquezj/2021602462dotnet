using System;

namespace HelloWorld;

class Program{
    static void Main(string[] args){
        Console.WriteLine("Calcular area y perimetro de la figura");
        double l = 2;
        double area, perimetro, ar1, ar2, ar3, ar4, ar5;

        ar1 = l * l;
        ar2 = (l*l)/2;
        ar3 = (l*l)/2;
        ar4 = (((3*l)/2)*l)/2;
        ar5 = (l*2)*(l/4);

        area = ar1 + ar2 + ar3 + ar4 + ar5;
        perimetro = (3*l) + (2*(l/4)) + (2*l) + 2*Math.Sqrt((Math.Pow(l,2)+Math.Pow(l,2))) + Math.Sqrt((Math.Pow(l,2)+Math.Pow((l*(3/2)),2))) + (l*(3/2));

        Console.WriteLine("La area es " + area);
        Console.WriteLine("El perimetro es " + perimetro);
    }
}