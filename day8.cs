using System;
using System.IO;
using System.Collections.Generic;

class Day8Class
{

    public class Inputs
    {
        public string command;
        public int argument;
        public int countRepeated;

        public Inputs(string command, int argument, int count)
        {
            this.command = command;
            this.argument = argument;
            this.countRepeated = count;
        }

    }

    public void Start()
    {
        // https://adventofcode.com/2020/day/8
        Console.WriteLine("****** DIA 8 ******");
        
        string[] lines = File.ReadAllLines(@"./inputs/inputs_dia8.txt");
        int accumulator = 0;
        int currentLine = 0;
        List<Inputs> inputs = new List<Inputs>();

        //format inputs
        for (ushort i = 0; i < lines.Length; i++)
        {
            var raw = lines[i].Split(" ");
            string command = raw[0];
            int argument = int.Parse(raw[1]);

            inputs.Add(new Inputs(command, argument, 0));
        }

        bool repeated = false;
        int maxRange = inputs.Count - 1;

        Console.WriteLine("****** FASE 1 ******");

        while( repeated == false)
        {

            inputs[currentLine].countRepeated++;

            

            if (inputs[currentLine].countRepeated > 1 || currentLine >= maxRange)
            {
                repeated = true;
                break;
            }

            switch (inputs[currentLine].command)
            {
                case "acc":

                    accumulator += inputs[currentLine].argument;
                    currentLine++;
                break;

                case "nop":

                    currentLine++;
                break;
                case "jmp":
                    
                    currentLine += inputs[currentLine].argument;

                break;

            }

            // Console.WriteLine("currentline=" + currentLine + " acumulador=" + accumulator);
            //por si acaso
            // System.Threading.Thread.Sleep(1000);
        }


        Console.WriteLine("acumulador=" + accumulator);


        Console.WriteLine("****** FASE 2 ******");

        List<int> positionJump = new List<int>();
        List<int> positionNop = new List<int>();

        inputs.Clear();

        
        //format inputs
        for (ushort i = 0; i < lines.Length; i++)
        {
            var raw = lines[i].Split(" ");
            string command = raw[0];
            int argument = int.Parse(raw[1]);

            if (command == "jmp")
            {
                positionJump.Add(i);
            }

            if (command == "nop")
            {
                positionNop.Add(i);
            }


            inputs.Add(new Inputs(command, argument, 0));
        }

        int lastMaxPosition = 0;
        int currentpositionListJump = 0;
        currentLine = 0;
        accumulator = 0;
        repeated = false;

        while (repeated == false)
        {

            if (currentLine > maxRange)
            {
                Console.WriteLine(currentLine);
                repeated = true;
                break;
            }

            inputs[currentLine].countRepeated++;
            if (inputs[currentLine].countRepeated > 1 )
            {
                accumulator = 0;
                inputs.Clear();
                currentLine = 0;
                inputs = ResetInputs(lines, positionJump[currentpositionListJump]);
                currentpositionListJump++;
                repeated = false;
            }


            switch (inputs[currentLine].command)
            {
                case "acc":

                    accumulator += inputs[currentLine].argument;
                    currentLine++;
                    break;

                case "nop":

                    currentLine++;
                    break;
                case "jmp":

                    if (lastMaxPosition > currentLine)
                    {
                        lastMaxPosition = currentLine;
                    }

                    currentLine += inputs[currentLine].argument;
                    
                    break;

            }

            
            //por si acaso
            // System.Threading.Thread.Sleep(1000);
        }


        Console.WriteLine("acumulador=" + accumulator);

    }

    public List<Inputs> ResetInputs(string[] lines, int positionJumpToChange)
    {

        List<Inputs> inputs = new List<Inputs>();
        for (ushort i = 0; i < lines.Length; i++)
        {
            var raw = lines[i].Split(" ");
            string command = raw[0];
            int argument = int.Parse(raw[1]);

            inputs.Add(new Inputs(command, argument, 0));
            
            
        }

        if (inputs[positionJumpToChange].command == "jmp")
        {
            inputs[positionJumpToChange].command = "nop";
        }

        return inputs;

    }





}
