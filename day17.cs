using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Day17Class
{
    public void Start()
    {
        // https://adventofcode.com/2020/day/17
        Console.WriteLine("****** DIA 17 ******");
        string[] lines = File.ReadAllLines(@"./inputs/inputs_dia17.txt");

        Console.WriteLine("****** FASE 1 ******");

        List<Vector3> directionsTocheck3D = new List<Vector3>()
        {
            //cambiar las z +1 +0 -1
            // new Vector3(0, 0, 0),

            new Vector3(-1, -1,  1),
            new Vector3( 0, -1,  1),
            new Vector3( 1, -1,  1),
            new Vector3(-1,  0,  1),
            new Vector3( 0,  0,  1),
            new Vector3( 1,  0,  1),
            new Vector3(-1,  1,  1),
            new Vector3( 0,  1,  1),
            new Vector3( 1,  1,  1),

            new Vector3(-1, -1,  0),
            new Vector3( 0, -1,  0),
            new Vector3( 1, -1,  0),
            new Vector3(-1,  0,  0),
            new Vector3( 1,  0,  0),
            new Vector3(-1,  1,  0),
            new Vector3( 0,  1,  0),
            new Vector3( 1,  1,  0),

            new Vector3(-1, -1, -1),
            new Vector3( 0, -1, -1),
            new Vector3( 1, -1, -1),
            new Vector3(-1,  0, -1),
            new Vector3( 0,  0, -1),
            new Vector3( 1,  0, -1),
            new Vector3(-1,  1, -1),
            new Vector3( 0,  1, -1),
            new Vector3( 1,  1, -1)


        };

        

        List<Vector4> directionsTocheck4D = new List<Vector4>()
        {
            new Vector4 (-1, -1, -1, -1),
            new Vector4 (-1, -1, -1, 0),
            new Vector4 (-1, -1, -1, 1),
            new Vector4 (-1, -1, 0, -1),
            new Vector4 (-1, -1, 0, 0),
            new Vector4 (-1, -1, 0, 1),
            new Vector4 (-1, -1, 1, -1),
            new Vector4 (-1, -1, 1, 0),
            new Vector4 (-1, -1, 1, 1),
            new Vector4 (-1, 0, -1, -1),
            new Vector4 (-1, 0, -1, 0),
            new Vector4 (-1, 0, -1, 1),
            new Vector4 (-1, 0, 0, -1),
            new Vector4 (-1, 0, 0, 0),
            new Vector4 (-1, 0, 0, 1),
            new Vector4 (-1, 0, 1, -1),
            new Vector4 (-1, 0, 1, 0),
            new Vector4 (-1, 0, 1, 1),
            new Vector4 (-1, 1, -1, -1),
            new Vector4 (-1, 1, -1, 0),
            new Vector4 (-1, 1, -1, 1),
            new Vector4 (-1, 1, 0, -1),
            new Vector4 (-1, 1, 0, 0),
            new Vector4 (-1, 1, 0, 1),
            new Vector4 (-1, 1, 1, -1),
            new Vector4 (-1, 1, 1, 0),
            new Vector4 (-1, 1, 1, 1),
            new Vector4 (0, -1, -1, -1),
            new Vector4 (0, -1, -1, 0),
            new Vector4 (0, -1, -1, 1),
            new Vector4 (0, -1, 0, -1),
            new Vector4 (0, -1, 0, 0),
            new Vector4 (0, -1, 0, 1),
            new Vector4 (0, -1, 1, -1),
            new Vector4 (0, -1, 1, 0),
            new Vector4 (0, -1, 1, 1),
            new Vector4 (0, 0, -1, -1),
            new Vector4 (0, 0, -1, 0),
            new Vector4 (0, 0, -1, 1),
            new Vector4 (0, 0, 0, -1),
            new Vector4 (0, 0, 0, 1),
            new Vector4 (0, 0, 1, -1),
            new Vector4 (0, 0, 1, 0),
            new Vector4 (0, 0, 1, 1),
            new Vector4 (0, 1, -1, -1),
            new Vector4 (0, 1, -1, 0),
            new Vector4 (0, 1, -1, 1),
            new Vector4 (0, 1, 0, -1),
            new Vector4 (0, 1, 0, 0),
            new Vector4 (0, 1, 0, 1),
            new Vector4 (0, 1, 1, -1),
            new Vector4 (0, 1, 1, 0),
            new Vector4 (0, 1, 1, 1),
            new Vector4 (1, -1, -1, -1),
            new Vector4 (1, -1, -1, 0),
            new Vector4 (1, -1, -1, 1),
            new Vector4 (1, -1, 0, -1),
            new Vector4 (1, -1, 0, 0),
            new Vector4 (1, -1, 0, 1),
            new Vector4 (1, -1, 1, -1),
            new Vector4 (1, -1, 1, 0),
            new Vector4 (1, -1, 1, 1),
            new Vector4 (1, 0, -1, -1),
            new Vector4 (1, 0, -1, 0),
            new Vector4 (1, 0, -1, 1),
            new Vector4 (1, 0, 0, -1),
            new Vector4 (1, 0, 0, 0),
            new Vector4 (1, 0, 0, 1),
            new Vector4 (1, 0, 1, -1),
            new Vector4 (1, 0, 1, 0),
            new Vector4 (1, 0, 1, 1),
            new Vector4 (1, 1, -1, -1),
            new Vector4 (1, 1, -1, 0),
            new Vector4 (1, 1, -1, 1),
            new Vector4 (1, 1, 0, -1),
            new Vector4 (1, 1, 0, 0),
            new Vector4 (1, 1, 0, 1),
            new Vector4 (1, 1, 1, -1),
            new Vector4 (1, 1, 1, 0),
            new Vector4 (1, 1, 1, 1)

        };



        //rellenar
        HashSet<Vector3> cubos = new HashSet<Vector3>();

        for (int y = 0; y < lines.Length; y++)
        {

            string linea = lines[y];
            for (int x = 0; x < linea.Length; x++)
            {
                if (linea[x] == '#')
                {
                    cubos.Add( new Vector3(y, x, 0) );

                }

            }

        }

        ushort ciclosmax = 6;

        for (int j = 0; j < ciclosmax; j++)
        {
            cubos = GenerarCubos(cubos, directionsTocheck3D);
        }
        
        Console.WriteLine("El resultado es=" + cubos.Count);

        Console.WriteLine("****** FASE 2 ******");

        HashSet<Vector4> cubos4d = new HashSet<Vector4>();

        for (int y = 0; y < lines.Length; y++)
        {

            string linea = lines[y];
            for (int x = 0; x < linea.Length; x++)
            {
                if (linea[x] == '#')
                {
                    cubos4d.Add(new Vector4(y, x, 0, 0));

                }

            }

        }


        for (int j = 0; j < ciclosmax; j++)
        {
            cubos4d = GenerarCubos(cubos4d, directionsTocheck4D);
        }

        Console.WriteLine("El resultado es=" + cubos4d.Count);

    }

    private HashSet<Vector3> GenerarCubos(HashSet<Vector3> cubos, List<Vector3> directionsTocheck3D)
    {

        HashSet<Vector3> nuevoCubo = new HashSet<Vector3>();

        for(int i = 0; i < cubos.Count; i++)
        {
            //todo: optimizar
            List<Vector3> posicionesChequear = ObtenerVecinos(cubos.ElementAt(i), directionsTocheck3D);
            posicionesChequear.Add(cubos.ElementAt(i));

            for(int j = 0; j < posicionesChequear.Count; j++)
            {
                Vector3 currentPosicionChequear = posicionesChequear.ElementAt(j);

                int vecinosActivos = ContarVecinosActivos(cubos, posicionesChequear[j], directionsTocheck3D );
                if ((cubos.Contains(currentPosicionChequear) == true) && (vecinosActivos == 2 || vecinosActivos == 3) )
                {
                    nuevoCubo.Add(currentPosicionChequear);
                }
                else if ((cubos.Contains(currentPosicionChequear) == false) && (vecinosActivos == 3))
                {
                    nuevoCubo.Add(currentPosicionChequear);

                }

            }


        }

        return nuevoCubo;

    }

    private HashSet<Vector4> GenerarCubos(HashSet<Vector4> cubos, List<Vector4> directionsTocheck4D)
    {
        HashSet<Vector4> nuevoCubo = new HashSet<Vector4>();

        for (int i = 0; i < cubos.Count; i++)
        {
            //todo: optimizar
            List<Vector4> posicionesChequear = ObtenerVecinos(cubos.ElementAt(i), directionsTocheck4D);
            posicionesChequear.Add(cubos.ElementAt(i));

            for (int j = 0; j < posicionesChequear.Count; j++)
            {
                Vector4 currentPosicionChequear = posicionesChequear.ElementAt(j);

                int vecinosActivos = ContarVecinosActivos(cubos, posicionesChequear[j], directionsTocheck4D);
                if ((cubos.Contains(currentPosicionChequear) == true) && (vecinosActivos == 2 || vecinosActivos == 3))
                {
                    nuevoCubo.Add(currentPosicionChequear);
                }
                else if ((cubos.Contains(currentPosicionChequear) == false) && (vecinosActivos == 3))
                {
                    nuevoCubo.Add(currentPosicionChequear);

                }

            }


        }


        return nuevoCubo;
    }



    public List<Vector3> ObtenerVecinos(Vector3 position, List<Vector3> directionsTocheck)
    {

        List<Vector3> nuevosVecinos = new List<Vector3>();

        for (int i = 0; i < directionsTocheck.Count; i++)
        {
            Vector3 currentPos = directionsTocheck[i] + position;
            nuevosVecinos.Add(currentPos);

        }

        return nuevosVecinos;

    }

    public List<Vector4> ObtenerVecinos(Vector4 position, List<Vector4> directionsTocheck)
    {

        List<Vector4> nuevosVecinos = new List<Vector4>();

        for (int i = 0; i < directionsTocheck.Count; i++)
        {
            Vector4 currentPos = directionsTocheck[i] + position;
            nuevosVecinos.Add(currentPos);

        }

        return nuevosVecinos;

    }

    private int ContarVecinosActivos(HashSet<Vector3> cubo, Vector3 position, List<Vector3> direcciones)
    {

        int resultado = 0;
        for (int i = 0; i < direcciones.Count; i++)
        {
            var currentPos = direcciones[i] + position;
            if (cubo.Contains(currentPos) == true)
            {
                resultado++;
            }

        }

        return resultado;

    }

    private int ContarVecinosActivos(HashSet<Vector4> cubo, Vector4 position, List<Vector4> direcciones)
    {

        int resultado = 0;
        for (int i = 0; i < direcciones.Count; i++)
        {
            var currentPos = direcciones[i] + position;
            if (cubo.Contains(currentPos) == true)
            {
                resultado++;
            }

        }

        return resultado;

    }

}