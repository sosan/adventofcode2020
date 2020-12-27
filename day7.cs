
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Day7Class
{

    public void Start()
    {
        // https://adventofcode.com/2020/day/7
        Console.WriteLine("****** DIA 7 ******");
        
        string[] lines = File.ReadAllLines(@"./inputs/inputs_dia7.txt");

        Console.WriteLine("****** FASE 1 ******");
        
        List<string> miBolsa = new List<string>() { "shiny gold" };
        HashSet<string> BolsasQueContiene = new HashSet<string>();

        for (ushort o = 0; o < miBolsa.Count; o++)
        {

            for(ushort i = 0; i < lines.Length; i++)
            {
                string[] contenidoraw = lines[i].Split(" bags contain ");
                string textocolor = contenidoraw[0];
                string contenido = contenidoraw[1] ;

                for (ushort k = 0; k < miBolsa.Count; k++)
                {

                    if (contenido.Contains(miBolsa[k]) == true)
                    {

                        if (BolsasQueContiene.Contains(textocolor) == false)
                        {
                            BolsasQueContiene.Add(textocolor);
                            miBolsa.Add(textocolor);
                            // Console.WriteLine("textocolor=" + textocolor);

                        }

                    }

                }

                

            }

        }

        Console.WriteLine("El resultado es=" + BolsasQueContiene.Count);

        Console.WriteLine("****** FASE 2 (Recursion) ******");

        List<string> inputs = File.ReadAllLines("./inputs/inputs_dia7.txt").ToList();

        int indiceIndex = -1;
        for(ushort i = 0; i < inputs.Count; i++)
        {
            if (inputs[i].StartsWith("shiny gold bags") == true)
            {
                indiceIndex = i;
                break;
            }

        }

        if (indiceIndex == -1)
            throw new Exception("error no existe el comienzo");

        string reglaPrincipal = inputs[indiceIndex];
        string bolsasQueContieneMiBolsa = reglaPrincipal.Substring(reglaPrincipal.IndexOf("contain") + 8).Replace(".", "").Replace("bags", "bag");

        string[] bolsasDentro = bolsasQueContieneMiBolsa.Split(", ");
        
        int resultado = EncontrarBolsas(bolsasDentro, inputs);
        Console.WriteLine("El resultado es=" + resultado);

    }


    private int EncontrarBolsas(string[] bolsas, List<string> inputs)
    {
        int total = 0;

        for (ushort i = 0; i < bolsas.Length; i++)
        {
            int cantidad = int.Parse(bolsas[i].Substring(0, bolsas[i].IndexOf(" ") + 1));
            string nombre = bolsas[i].Substring(bolsas[i].IndexOf(" ") + 1);
            int indiceBolsa = EncontrarIndice(inputs, nombre);
            string reglaBolso = inputs[indiceBolsa];

            reglaBolso = reglaBolso.Substring(reglaBolso.IndexOf("contain") + 8);
            total += cantidad;

            if (reglaBolso != "no other bags.")
            {
                string bolsosDentroRaw = reglaBolso.Replace(".", "").Replace("bags", "bag");
                string[] bolsosDentro = bolsosDentroRaw.Split(", ");
                total += cantidad * EncontrarBolsas(bolsosDentro, inputs);
            }

        }

        return total;

    }

    private int EncontrarIndice(List<string> inputs, string nombreBolso)
    {

        int indiceIndex = -1;
        for (ushort i = 0; i < inputs.Count; i++)
        {
            if (inputs[i].StartsWith(nombreBolso) == true)
            {
                indiceIndex = i;
                break;
            }

        }

        if (indiceIndex == -1)
            throw new Exception("error no existe el nombreBolso" + nombreBolso);
        
        return indiceIndex;

    }

}
