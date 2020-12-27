using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Day10Class
{

    public void Start()
    {
        // https://adventofcode.com/2020/day/10
        Console.WriteLine("****** DIA 10 ******");
        
        string[] lines = File.ReadAllLines(@"./inputs/inputs_dia10.txt");
        int[] inputs = new int[lines.Length];
        for (ushort i = 0; i < lines.Length; i++ )
        {
            int valor = ushort.Parse(lines[i]);
            inputs[i] = valor;

        }

        Console.WriteLine("****** FASE 1 ******");

        int max = (inputs[0..(inputs.Length - 1)].Max()) + 3;

        // Console.WriteLine("max=" + max);
        int currentJolt = 0;

        int sumDif1Jolt = 0;
        int sumDif2Jolt = 0;
        int sumDif3Jolt = 0;

        int joltfase = 1;
        bool final = false;

        while (max >= currentJolt + 3 && final == false)
        {

            int testvalue = currentJolt + joltfase;

            if (inputs.Contains(testvalue) == true )
            {

                switch (joltfase)
                {
                    case 1: sumDif1Jolt++; break;
                    case 2: sumDif2Jolt++; break;
                    case 3: sumDif3Jolt++; break;
                    // default: Console.WriteLine("seeee"); break;

                }


                currentJolt += joltfase;
                
                int rango = Math.Abs(max - currentJolt);
                if (rango == 1)
                {
                    sumDif1Jolt++;
                    final = true;
                }
                else if (rango == 2)
                {
                    sumDif2Jolt++;
                    final = true;

                }
                else if (rango == 3)
                {
                    sumDif3Jolt++;
                    final = true;
                }

                // Console.WriteLine("    |- currentjolt=" + currentJolt + " sumdife1=" + sumDif1Jolt + " sumdif2=" + sumDif2Jolt + " sumdif3=" + sumDif3Jolt);
                joltfase = 1;
            }
            else
            {
                joltfase++;
                if (joltfase >= 4)
                {
                    joltfase = 1;
                }
            }

        
        }

        // Console.WriteLine("sumdife1=" + sumDif1Jolt + " sumdif2=" + sumDif2Jolt + " sumdif3=" + sumDif3Jolt) ;
        int resultado = sumDif1Jolt * sumDif3Jolt;
        Console.WriteLine("El resultado es=" + resultado);
        Console.WriteLine("****** FASE 2 ******");

        List<int> ratings = new List<int>();
        for (ushort i = 0; i < lines.Length; i++)
        {
            ratings.Add( int.Parse(lines[i]) );

        }

        ratings.Add(0);
        ratings.Sort();
        ratings.Add( ratings[ratings.Count - 1] + 3 );

        int diff1 = 0;
        int diff3 = 0;
        long resultadoParte2 = 1;

        string numerosDentroRango = "";

        for (ushort i = 0; i < ratings.Count - 1; i++)
        {
            int diferencia = ratings[i + 1] - ratings[i];

            if (diferencia == 1)
            {
                diff1++;
            }
            else if (diferencia == 3)
            {
                diff3++;
            }
            else
            {
                Console.WriteLine("diferencia no contemplada");
            }

            int contadorRangos = 0;
            for (int j = 1; j <= 3; j++)
            {
                if ((i + j) < ratings.Count)
                {
                    if (ratings[i + j] - ratings[i] <= 3)
                    {

                        contadorRangos++;
                    }
                }
            }
            
            numerosDentroRango += contadorRangos;
        }


        for (int i = 0; i < numerosDentroRango.Length; i++)
        {
            if (numerosDentroRango[i] == '3')
            {
                if (numerosDentroRango[i + 1] == '3')
                {
                    resultadoParte2 *= 7;
                    i += 3;
                }
                else
                {
                    resultadoParte2 *= 4;
                    i += 2;
                }
            }
            else if (numerosDentroRango[i] == '2')
            {
                resultadoParte2 *= 2;
                i++;
            }
        }


        Console.WriteLine("El resultado es=" + resultadoParte2);

    }

}
