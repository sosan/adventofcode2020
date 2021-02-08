using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Day9Class
{

    public class Inputs
    {
        public double numero;
        public bool isUsed;

        public Inputs(double numero, bool isUsed)
        {
            this.numero = numero;
            this.isUsed = isUsed;

        }

    }

    public void Start()
    {
        // https://adventofcode.com/2020/day/9
        Console.WriteLine("****** DIA 9 ******");
        
        string[] lines = File.ReadAllLines(@"./inputs/inputs_dia9.txt");
        
        List<Inputs> inputs = new List<Inputs>();
        List<Inputs> preambulo = new List<Inputs>();
        double[] input = new double[lines.Length];

        ushort maxNumeroPReambulo = 25;
        
        for (ushort i = 0; i < lines.Length; i++)
        {

            double valor = long.Parse(lines[i]);
            input[i] = valor;
            inputs.Add(new Inputs(valor, false));
            if (i < maxNumeroPReambulo)
            {
                preambulo.Add(new Inputs(valor, false));
            }


        }


        Console.WriteLine("****** FASE 1 ******");

        double resultado = 0;

        for (ushort i = maxNumeroPReambulo; i < inputs.Count; i++)
        {

            
            double currentNumber = inputs[i].numero;
            double valor1 = 0;
            double valor2 = 0;
            bool encontrado = false;

            for (ushort j = 0; j < preambulo.Count; j++)
            {
                if (encontrado == true)
                {
                    break;
                }
                for (ushort k = 0; k < preambulo.Count; k++)
                {
                    if (encontrado == true)
                    {
                        break;
                    }

                    if (preambulo[k].numero != preambulo[j].numero)
                    {
                        if (currentNumber == (preambulo[k].numero + preambulo[j].numero))
                        {
                            
                            // Console.WriteLine(preambulo[k].numero + " + " + preambulo[j].numero + "=" + currentNumber);
                            valor1 = preambulo[k].numero;
                            valor2 = preambulo[j].numero;
                            preambulo[k].isUsed = true;
                            preambulo[j].isUsed = true;

                            preambulo.RemoveAt(0);
                            preambulo.Add(new Inputs( inputs[i].numero, false));
                            inputs[i].isUsed = true;
                            encontrado = true;
                        }
                    }
                }

            }

            if (encontrado == false)
            {
                resultado = currentNumber;
                break;
            }

        }

        Console.WriteLine("Resultado=" + resultado);

        Console.WriteLine("****** FASE 2 ******");
        
        int start = 0;
        int end = 1;
        
        while (end < input.Length)
        {

            double sum = 0;
            for (var i = start; i < end; i++)
            {
                sum += input[i];
            }

            if (sum == resultado)
            {
                // double min = input[start];
                // double max = input[end];
                // Console.WriteLine( "Min" + min + " max=" + max + " suma=" + (min + max) );
                resultado = input[start..end].Min() + input[start..end].Max();
                break;
            }


            if (sum < resultado)
            {
                end++;

            }

            if (sum > resultado)
            {
                start++;

            }
        }

        Console.WriteLine("Resultado=" + resultado);

    }




}
