using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Day11Class
{
    string[] lines = File.ReadAllLines(@"./inputs/inputs_dia11.txt");

    char[,] mapa = new char[1, 1];
    char[,] mapatemporal = new char[1, 1];
    
    public void Start()
    {
        // https://adventofcode.com/2020/day/11
        Console.WriteLine("****** DIA 11 ******");

        mapa = InicializarMapa(lines[0].Length, lines.Length, lines);
        mapatemporal = new char[lines[0].Length, lines.Length];

        bool isParte2 = false;

        Console.WriteLine("****** FASE 1 ******");

        int contador = 0;
        int resultadoParte1 = 0;
        int resultadoParte2 = 0;
        bool isMapaNotEstable = true;
        int vecinosMaximoProximos = 4;
        int vueltas = 0;

        while (isMapaNotEstable == true)
        {

            // vueltas++;
            // Console.WriteLine("vueltas=" + vueltas);

            int numeroAsientosOcupados = ActualizarAsientos(altura: lines.Length, ancho: lines[0].Length, vecinosMaximoProximos, isParte2);
            mapa = ClonarMapa(mapatemporal);
            
            if (numeroAsientosOcupados == contador)
            {

                if (isParte2 == true)
                {
                    isMapaNotEstable = false;
                    resultadoParte2 = contador;
                }
                else
                {
                    resultadoParte1 = contador;
                    isParte2 = true;
                    vecinosMaximoProximos = 5;
                    mapa = InicializarMapa(ancho: lines[0].Length, alto: lines.Length, lines);
                    
                    contador = 0;
                    numeroAsientosOcupados = 0;

                }

            }
            else
            {
                contador = numeroAsientosOcupados;
            }
        }
        
        Console.WriteLine("El resultado es=" + resultadoParte1);

        Console.WriteLine("****** FASE 2 ******");

        Console.WriteLine("El resultado es=" + resultadoParte2);
    }

    private char[,] InicializarMapa(int ancho, int alto, string[] lines)
    {
        char[,] mapa = new char[ancho, alto];
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                mapa[x, y] = lines[y][x];
            }

        }

        return mapa;

    }


    private int ActualizarAsientos(int altura, int ancho, int vecinosMaximo, bool isParte2)
    {
        int asientosOcupados = 0;
        int asientosVecinosOcupados = 0;
        // char[,] mapaTemporal = new char[ancho, altura];

        for (int y = 0; y < altura; y++)
        {
            for (int x = 0; x < ancho; x++)
            {
                asientosVecinosOcupados = 0;
                for (int i = 0; i < 8; i++)
                {
                    asientosVecinosOcupados += CheckOtherSeats(altura, ancho, i, x, y, isParte2);

                }

                if (mapa[x, y] == 'L' && asientosVecinosOcupados == 0)
                {
                    mapatemporal[x, y] = '#';
                    asientosOcupados++;
                }
                else if (mapa[x, y] == '#' && asientosVecinosOcupados >= vecinosMaximo)
                {
                    mapatemporal[x, y] = 'L';

                }
                else if (mapa[x, y] == '#' && asientosVecinosOcupados < vecinosMaximo)
                {
                    asientosOcupados++;

                }
            }
        }
        return asientosOcupados;


    }

    private int CheckOtherSeats(int altura, int ancho, int i, int x, int y, bool isParte2)
    {

        int[,] direcciones = {
            {  0, -1 },
            {  1, -1 },
            {  1,  0 },
            {  1,  1 },
            {  0,  1 },
            { -1,  1 },
            { -1,  0 },
            { -1, -1 }
        };

        int numVecinosOcupados = 0;
        int currentPosX = direcciones[i, 0] + x;
        int currentPosY = direcciones[i, 1] + y;
        bool noEncontradoAsiento = true;

        do
        {
            if ((currentPosX >= 0 && currentPosX < ancho) && 
                (currentPosY >= 0 && currentPosY < altura))
            {
                if (mapa[currentPosX, currentPosY] == 'L')
                {
                    noEncontradoAsiento = false;

                }
                else if (mapa[currentPosX, currentPosY] == '#')
                {
                    numVecinosOcupados++;
                    noEncontradoAsiento = false;
                }
                currentPosX += direcciones[i, 0];
                currentPosY += direcciones[i, 1];
            }
            else
            {
                noEncontradoAsiento = false;

            }
        } 
        while (isParte2 && noEncontradoAsiento);

        return numVecinosOcupados;
    }

    private char[,] ClonarMapa(char[,] mapatemporal)
    {

        return (char[,])mapatemporal.Clone();

    }


}