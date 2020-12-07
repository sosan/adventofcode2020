using System;
using System.Collections.Generic;

class Day3Class
{
    public void Start()
    {
        // https://adventofcode.com/2020/day/3

        Console.WriteLine("****** DIA 3 ******");

        string[] lines = System.IO.File.ReadAllLines(@"./inputs/inputs_dia3.txt");
        
        //generar matriz
        List<char[]> inputs = new List<char[]>();
        for (ushort j = 0; j < lines.Length; j++)
        {
            for (uint i = 0; i < 15; i++)
            {
                lines[j] += lines[j];
            }

            inputs.Add(lines[j].ToCharArray());
        }

        //fase 1
        Console.WriteLine("****** FASE 1 ******");
        uint contador = calcularmatriz(lines.Length, inputs, 3, 1);
        Console.WriteLine("contador=" + contador);


        //fase 2
        Console.WriteLine("****** FASE 2 ******");
        uint contador_slope1 = calcularmatriz(lines.Length, inputs, 1, 1);
        uint contador_slope2 = calcularmatriz(lines.Length, inputs, 3, 1);
        uint contador_slope3 = calcularmatriz(lines.Length, inputs, 5, 1);
        uint contador_slope4 = calcularmatriz(lines.Length, inputs, 7, 1);
        uint contador_slope5 = calcularmatriz(lines.Length, inputs, 1, 2);

        Console.WriteLine("contador slope1=" + contador_slope1);
        Console.WriteLine("contador slope2=" + contador_slope2);
        Console.WriteLine("contador slope3=" + contador_slope3);
        Console.WriteLine("contador slope4=" + contador_slope4);
        Console.WriteLine("contador slope5=" + contador_slope5);

        uint multiplicacion = (contador_slope1 * contador_slope2 * contador_slope3 * contador_slope4 * contador_slope5);
        Console.WriteLine("Multiplicacion slopes=" + multiplicacion);



        ushort calcularmatriz(int longitudLines, List<char[]> inputs, ushort sumaX, ushort sumaY)
        {
            ushort contador = 0;
            ushort posX = 0;
            ushort posY = 0;

            for (ushort i = 0; i < longitudLines; i++)
            {
                posX += sumaX;
                posY += sumaY;
                if (posY < longitudLines)
                {
                    if (inputs[posY][posX] == '#')
                    {
                        contador++;
                    }
                }

                if (posY > longitudLines)
                {
                    break;
                }

            }

            return contador;
        }


    }


}
