
using System;
using System.IO;
using System.Collections.Generic;

class Day15Class

{

    public void Start()
    {
        // https://adventofcode.com/2020/day/15
        Console.WriteLine("****** DIA 15 ******");
        
        List<int> inputs = new List<int>()
        {
            0,14,6,20,1,4 
            // 3,1,2 //=> 1836
            //3,2,1 //=> 438
            //2,3,1 //=> 78
            // 1,2,3 //=> 27
            //2,1,3 //=> 10
            //1,3,2 //=> 1
            // 0,3,6, //0, 
        };

        Console.WriteLine("****** FASE 1 ******");
        int resultado = GenerarMatriz(2020, inputs);
        Console.WriteLine("resultado=" + resultado);

        Console.WriteLine("****** FASE 2 ******");
        inputs.Clear();

        inputs = new List<int>()
        {
            0,14,6,20,1,4 
        };

var s1 = System.Diagnostics.Stopwatch.StartNew();
s1.Start();
        resultado = GenerarMatrizFase2(30000000, inputs.ToArray());
s1.Stop();
Console.WriteLine("tardao ms=" + s1.Elapsed.TotalMilliseconds);

        Console.WriteLine("resultado=" + resultado);


    }


    public int GenerarMatrizFase2(int contadormaximo, int[] inputs)
    {
        int[] listado = new int[contadormaximo];
        Array.Fill(listado, -1);

        for (int i = 1; i < inputs.Length + 1; i++)
        {
            listado[inputs[i - 1]] = i;

        }

        int resultado = 0;

        for (int j = inputs.Length + 1; j < contadormaximo; j++)
        {
            var prevTime = listado[resultado];
            listado[resultado] = j;
            
            if (prevTime != -1)
            {
                resultado = j - prevTime;
            }
            else
            {
                resultado = 0;
            }
        }

        return resultado;

    }

    public int GenerarMatriz(int maximocontador, List<int> inputs)
    {


        for (ushort turno = 0; turno < maximocontador; turno++)
        {

            // next number = ?
            int lastnumber = inputs[inputs.Count - 1];

            bool encontrado = false;
            for (ushort i = 0; i < inputs.Count - 1; i++)
            {
                if (inputs[i] == lastnumber)
                {
                    encontrado = true;
                    break;
                }
            }

            if (encontrado == false)
            {
                inputs.Add(0);
            }
            else
            {
                int min = -1;
                for (int i = inputs.Count - 2; i >= 0; i--)
                {
                    if (inputs[i] == lastnumber)
                    {
                        min = i;
                        break;
                    }

                }

                int max = -1;
                for (int i = inputs.Count - 1; i >= 0; i--)
                {
                    if (i == min)
                        continue;

                    if (inputs[i] == lastnumber)
                    {
                        max = i;
                        break;
                    }

                }

                inputs.Add(max - min);

            }



        }

        // TextWriter tw = new StreamWriter(@"./inputs/inputs_dia15_f.txt");
        // foreach (var s in inputs)
        //     tw.WriteLine(s);
        // tw.Close();

        return inputs[maximocontador - 1];


    }


    

}


