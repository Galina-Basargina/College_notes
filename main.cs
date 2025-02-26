/******************************************************************************

                            Online C# Compiler.
                Code, Compile, Run and Debug C# program online.
Write your code in this editor and press "Run" button to execute it.

*******************************************************************************/

using System;
class HelloWorld {
  static void Main() {
    string[] array = new string[9];
    
    Console.WriteLine($"0\t|1\t|2\t");
    Console.WriteLine("-------------------------");
    Console.WriteLine($"3\t|4\t|5\t");
    Console.WriteLine("-------------------------");
    Console.WriteLine($"6\t|7\t|8\t");
    
    // Console.WriteLine($"{array[0]}\t|{array[1]}\t|{array[2]}");
    // Console.WriteLine("-------------------------");
    // Console.WriteLine($"{array[3]}\t|{array[4]}\t|{array[5]}");
    // Console.WriteLine("-------------------------");
    // Console.WriteLine($"{array[6]}\t|{array[7]}\t|{array[8]}");
    
    Random rnd = new Random();
    int step = 1;
    string myAnswer = "0";
    string compAnswer = "X";
    Console.WriteLine("Кто ты? 0 или Х?");
    if (Console.ReadLine() != "0")
    {
        myAnswer = "X";
        compAnswer = "0";
    }    
    while (true)
    {
        Console.WriteLine($"Номер хода: {step}");
        
        int value = rnd.Next(0, 8);
        while (array[value] == compAnswer || array[value] == myAnswer)
        {
            value = rnd.Next(0, 8);
        }
        array[value] = compAnswer;
        Console.WriteLine("Ход противника:");
        Console.WriteLine($"\t{array[0]}|\t{array[1]}|\t{array[2]}");
        Console.WriteLine("-------------------------");
        Console.WriteLine($"\t{array[3]}|\t{array[4]}|\t{array[5]}");
        Console.WriteLine("-------------------------");
        Console.WriteLine($"\t{array[6]}|\t{array[7]}|\t{array[8]}");
        
        Console.WriteLine("\nКуда ставим?");
        int myTurn = int.Parse(Console.ReadLine());
        while (myTurn >= 9)
            myTurn = int.Parse(Console.ReadLine());
            
        while(array[myTurn] == compAnswer || array[myTurn] == myAnswer)
            myTurn = int.Parse(Console.ReadLine());
        array[myTurn] = myAnswer;
        Console.WriteLine($"\t{array[0]}|\t{array[1]}|\t{array[2]}");
        Console.WriteLine("-------------------------");
        Console.WriteLine($"\t{array[3]}|\t{array[4]}|\t{array[5]}");
        Console.WriteLine("-------------------------");
        Console.WriteLine($"\t{array[6]}|\t{array[7]}|\t{array[8]}");
        
        if (array[0] == array[1] && array[1] == array[2] && array[0] == myAnswer ||
            array[3] == array[4] && array[4] == array[5] && array[3] == myAnswer ||
            array[6] == array[7] && array[7] == array[8] && array[6] == myAnswer ||
            array[0] == array[3] && array[3] == array[6] && array[0] == myAnswer ||
            array[1] == array[4] && array[4] == array[7] && array[1] == myAnswer ||
            array[2] == array[5] && array[5] == array[8] && array[2] == myAnswer ||
            array[0] == array[4] && array[4] == array[8] && array[0] == myAnswer ||
            array[2] == array[4] && array[4] == array[6] && array[2] == myAnswer )
            
            {   
                Console.WriteLine($"{myAnswer} WIN!");
                break;
            }
        else if (array[0] == array[1] && array[1] == array[2] && array[0] == compAnswer ||
            array[3] == array[4] && array[4] == array[5] && array[3] == compAnswer ||
            array[6] == array[7] && array[7] == array[8] && array[6] == compAnswer ||
            array[0] == array[3] && array[3] == array[6] && array[0] == compAnswer ||
            array[1] == array[4] && array[4] == array[7] && array[1] == compAnswer ||
            array[2] == array[5] && array[5] == array[8] && array[2] == compAnswer ||
            array[0] == array[4] && array[4] == array[8] && array[0] == compAnswer ||
            array[2] == array[4] && array[4] == array[6] && array[2] == compAnswer )
            
            {   
                Console.WriteLine($"{compAnswer} WIN!");
                break;
            }
        Console.WriteLine();
        step++;
    }
        
    }
}
