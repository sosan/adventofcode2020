using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Day23Class
{

    public class Nodo
    {
        public int value;
        public Nodo Next;

        public Nodo(int value)
        {
            this.value = value;
        }
    }

    public void Start()
    {
        // https://adventofcode.com/2020/day/23
        Console.WriteLine("****** DIA 23 ******");
        Console.WriteLine("****** FASE 1 ******");

        string lines = "198753462";

        string resultado = Jugar(lines, 0, 100, false);
        Console.WriteLine("El resultado es:" + resultado);

        Console.WriteLine("****** FASE 2 ******");
        resultado = Jugar(lines, 1_000_000, 10_000_000, true);
        Console.WriteLine("El resultado es=" + resultado);


    }

    private string Jugar(string cadena, int max, int rondas, bool isParte2)
    {
        List<int> inputs = new List<int>();
        for (ushort i = 0; i < cadena.Length; i++)
        {
            inputs.Add(int.Parse(cadena[i].ToString()));
        }

        int valorMaximo = inputs.Max();

        if (max > 0)
        {
            for (int j = valorMaximo + 1; j <= max ; j++)
            {
                inputs.Add(j);
            }
        }

        int indice = max + 1;
        if (max == 0)
        {
            indice = 10;
        }

        Nodo[] nodos = new Nodo[indice];

        Nodo nodoStart = new Nodo(inputs[0]);
        nodos[inputs[0]] = nodoStart;
        Nodo nodoAnterior = nodoStart;


        for (int i = 1; i < inputs.Count; i++)
        {
            Nodo nodoActual = new Nodo(inputs[i]);
            nodos[inputs[i]] = nodoActual;
            nodoAnterior.Next = nodoActual;
            nodoAnterior = nodoActual;

            if (inputs[i] > valorMaximo)
            {
                valorMaximo = inputs[i];

            }

        }

        nodoAnterior.Next = nodoStart;

        Nodo nodoCurrent = nodoStart;
        for (int j = 0; j < rondas; j++)
        {
            Nodo nodoSiguiente = nodoCurrent.Next;
            nodoCurrent.Next = nodoSiguiente.Next.Next.Next;

            int destVal = FindPositionDestination(nodoCurrent.value, nodoSiguiente, valorMaximo);

            Nodo ip = nodos[destVal];
            Nodo ipn = ip.Next;
            Nodo tail = nodoSiguiente.Next.Next;
            tail.Next = ipn;
            ip.Next = nodoSiguiente;

            nodoCurrent = nodoCurrent.Next;
        }

        string resultado = "";
        if (isParte2 == false)
        {
            resultado = nodosToString(nodoStart, 1);
        }
        else
        {
            Nodo nodoActual = nodos[1];
            ulong res = (ulong)nodoActual.Next.value * (ulong)nodoActual.Next.Next.value;
            resultado = res.ToString();
        }

        return resultado;
    }


    private int FindPositionDestination(int valorInicio, Nodo nodoCurrent, int valormasalto)
    {

        int destino = 0;
        if (valorInicio == 1)
        {
            destino = valormasalto;
        }
        else
        {
            destino = valorInicio - 1;
        }

        int valorCurrentNodo = nodoCurrent.value;
        int valorNextNodo = nodoCurrent.Next.value;
        int valorNodoLejano = nodoCurrent.Next.Next.value;

        while (destino == valorCurrentNodo || destino == valorNextNodo || destino == valorNodoLejano)
        {
            destino--;
            if (destino <= 0)
            {
                destino = valormasalto;

            }
        }

        return destino;
    }

    private string nodosToString(Nodo currentNodo, int valorInicial)
    {
        while (currentNodo.value != valorInicial)
        {
            currentNodo = currentNodo.Next;
        }
        currentNodo = currentNodo.Next;

        string str = "";
        do
        {
            str += currentNodo.value.ToString();
            currentNodo = currentNodo.Next;
        } 
        while (currentNodo.value != valorInicial);

        return str;
    }

}