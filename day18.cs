using System;
using System.IO;
using System.Collections.Generic;

class Day18Class
{

    public class Operacion
    {
        public List<long> numeros = new List<long>();
        public List<char> opers = new List<char>();

        public long CalcularOperacionFase1()
        {
            long subtotal = numeros[0];

            for (short i = 1; i < numeros.Count; i++)
            {

                if (opers[i - 1] == '+')
                {
                    subtotal += numeros[i];
                }
                else if (opers[i - 1] == '*')
                {
                    subtotal *= numeros[i];
                }

            }

            return subtotal;
        }


        public long CalcularOperacionFase2()
        {

            long sumaoperacion = numeros[0];

            List<long> additionsFirstResult = new List<long>();

            for (ushort i = 1; i < numeros.Count; i++)
            {
                if (opers[i - 1] == '+')
                {
                    sumaoperacion += numeros[i];

                }
                else if (opers[i - 1] == '*')
                {
                    additionsFirstResult.Add(sumaoperacion);
                    sumaoperacion = 0;
                    sumaoperacion += numeros[i];
                }
            }

            additionsFirstResult.Add(sumaoperacion);

            long subtotal = 1;
            for (ushort i = 0; i < additionsFirstResult.Count; i++)
            {
                subtotal *= additionsFirstResult[i];
            }

            return subtotal;
        }

    }

    public void Start()
    {
        // https://adventofcode.com/2020/day/18
        Console.WriteLine("****** DIA 18 ******");
        var lines = File.ReadAllLines(@"./inputs/inputs_dia18.txt");

        Console.WriteLine("****** FASE 1 ******");

        List<Operacion> operaciones = new List<Operacion>();
        long total = 0;

        for (ushort i = 0; i < lines.Length; i++)
        {

            char[] fila = lines[i].Replace(" ", "").ToCharArray();

            operaciones.Add( new Operacion());

            for (ushort p = 0; p < fila.Length; p++)
            {

                switch(fila[p])
                {

                    case '+':
                        operaciones[operaciones.Count - 1].opers.Add('+');
                    break;

                    case '*':
                        operaciones[operaciones.Count - 1].opers.Add('*');
                    break;

                    case '(':
                        operaciones.Add(new Operacion());
                    break;

                    case ')':
                        long t = operaciones[operaciones.Count - 1].CalcularOperacionFase1();
                        operaciones.RemoveAt(operaciones.Count - 1);
                        operaciones[operaciones.Count - 1].numeros.Add(t);
                    break;

                    default:
                        operaciones[operaciones.Count - 1].numeros.Add(long.Parse(fila[p].ToString()));
                    break;

                }

            }

            total += operaciones[operaciones.Count - 1].CalcularOperacionFase1();
            operaciones.RemoveAt(operaciones.Count - 1);


        }


        Console.WriteLine("La suma es=" + total);

        Console.WriteLine("****** FASE 2 ******");

        operaciones.Clear();
        total = 0;

        for (ushort i = 0; i < lines.Length; i++)
        {

            char[] fila = lines[i].Replace(" ", "").ToCharArray();

            operaciones.Add(new Operacion());

            for (ushort p = 0; p < fila.Length; p++)
            {

                switch (fila[p])
                {

                    case '+':
                        operaciones[operaciones.Count - 1].opers.Add('+');
                        break;

                    case '*':
                        operaciones[operaciones.Count - 1].opers.Add('*');
                        break;

                    case '(':
                        operaciones.Add(new Operacion());
                        break;

                    case ')':
                        long t = operaciones[operaciones.Count - 1].CalcularOperacionFase2();
                        operaciones.RemoveAt(operaciones.Count - 1);
                        operaciones[operaciones.Count - 1].numeros.Add(t);
                        break;

                    default:
                        operaciones[operaciones.Count - 1].numeros.Add(long.Parse(fila[p].ToString()));
                        break;

                }

            }

            total += operaciones[operaciones.Count - 1].CalcularOperacionFase2();
            operaciones.RemoveAt(operaciones.Count - 1);


        }

        Console.WriteLine("El resultado es=" + total);

    }


}