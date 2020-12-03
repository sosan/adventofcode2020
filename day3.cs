using System;
using System.Collections.Generic;

class DayThreeClass
{
    // public static void DayThree(string[] args)
    public static void Main(string[] args)
    {

        string[] lines = System.IO.File.ReadAllLines(@"./inputs_dia3.txt");
        
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



        ulong contador_slope1 = calcularmatriz(lines.Length, inputs, 1, 1);
        ulong contador_slope2 = calcularmatriz(lines.Length, inputs, 3, 1);
        ulong contador_slope3 = calcularmatriz(lines.Length, inputs, 5, 1);
        ulong contador_slope4 = calcularmatriz(lines.Length, inputs, 7, 1);
        ulong contador_slope5 = calcularmatriz(lines.Length, inputs, 1, 2);

        Console.WriteLine("contador slope1=" + contador_slope1);
        Console.WriteLine("contador slope2=" + contador_slope2);
        Console.WriteLine("contador slope3=" + contador_slope3);
        Console.WriteLine("contador slope4=" + contador_slope4);
        Console.WriteLine("contador slope5=" + contador_slope5);

        ulong multiplicacion = (contador_slope1 * contador_slope2 * contador_slope3 * contador_slope4 * contador_slope5);
        Console.WriteLine("Resultado final=" + multiplicacion);

        //fase 1        
        // ushort contador = 0;
        // for (ushort i = 0; i < lines.Length; i++ )
        // {
        //     posX += 3;
        //     posY++;
        //     if (posY < lines.Length)
        //     {
        //         if (inputs[posY][posX] == '#')
        //         {
        //             contador++;
        //         }
        //     }

        //     if (posY > lines.Length)
        //     {
        //         break;
        //     }

        // }

        // Console.WriteLine("contador=" + contador);


    }

    public static ushort calcularmatriz(int longitudLines, List<char[]> inputs, ushort sumaX, ushort sumaY)
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
