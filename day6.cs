using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

class Day6Class
{
    public void Start()
    {

        // https://adventofcode.com/2020/day/6

        Console.WriteLine("****** DIA 6 ******");
        
        string[] lines = File.ReadAllLines(@"./inputs/inputs_dia6.txt");
        
        Console.WriteLine("****** FASE 1 ******");
        List<string> inputs = new List<string>();
        string cadena = "";
        for (ushort i = 0; i < lines.Length; i++)
        {
            if (lines[i] != "")
            {
                cadena += lines[i];
            }
            
            if (lines[i] == "" || i == lines.Length - 1)
            {

                // Console.WriteLine(cadena);
                inputs.Add(cadena);
                cadena = "";
            }
        }

        uint sumagrupal = 0;
        
        for (ushort i = 0; i < inputs.Count; i++)
        {
            uint contador = 0;

            for (ushort j = 97; j < 123; j++)
            {

                char caracter = (char)j;
                if (inputs[i].Contains(caracter) == true)
                {
                    contador++;
                }
            }
            
            // Console.WriteLine("contador=" + contador);
            sumagrupal += contador;

        }

        Console.WriteLine("Suma TOtal=" + sumagrupal);

        Console.WriteLine("****** FASE 2 ******");
        //linq-u

        uint contadorGeneral = (uint)string.Join("\n", lines.ToList())
            .Split("\n\n")
            .Select(eachinput => eachinput.Split("\n").Select(element => element.ToCharArray().Distinct()))
            .Select(element => element.Aggregate((currentElement, nextElement) => currentElement.Intersect(nextElement).ToList()).Count())
            .Sum();

        Console.WriteLine("contadorgeneral=" + contadorGeneral);

    }

}
