using System;
class HelloWorld 
{
    static void Main() 
    {
        string[] array = new string[9];
        string[] numbs = new string[9] {"1", "2", "3", "4", "5", "6", "7", "8", "9"};
        
        Console.WriteLine($"0\t|1\t|2\t");
        Console.WriteLine("-------------------------");
        Console.WriteLine($"3\t|4\t|5\t");
        Console.WriteLine("-------------------------");
        Console.WriteLine($"6\t|7\t|8\t");
        
        Random rnd = new Random();
        int step = 1;
        string myAnswer = "0";
        string compAnswer = "X";
        Console.WriteLine("Кто ты? 0 или Х?");
        string whoanswer = Console.ReadLine();
        while(whoanswer != "X" || whoanswer != "0" || whoanswer != "x")
            {
                Console.WriteLine("Простите, но этот символ я не понимаю. Кто вы? 0 или Х?");
                whoanswer = Console.ReadLine();
            }
        if (whoanswer != "0")
        {
            myAnswer = "X";
            compAnswer = "0";
        }    
        
        while (true)
        {
            if (myAnswer == "0")
            {
                int comp = CompTurn(array, numbs, myAnswer, compAnswer, rnd);
                // Console.WriteLine(step);
                step++;
                if (comp == 1)
                    break;
                
                if (step == 9)
                {
                    Console.WriteLine("Ничья!");
                    break;
                }
                
                int pers = PersTurn(array, numbs, compAnswer, myAnswer);
                // Console.WriteLine(step);
                // Console.WriteLine();
                step++;
                if (pers == 1)
                    break;
            }
            else
            {
                int pers = PersTurn(array, numbs, compAnswer, myAnswer);
                // Console.WriteLine(step);
                step++;
                if (pers == 1)
                    break;
                
                if (step == 9)
                {
                    Console.WriteLine("Ничья!");
                    break;
                }
                
                int comp = CompTurn(array, numbs, myAnswer, compAnswer, rnd);
                // Console.WriteLine(step);
                // Console.WriteLine();
                step++;
                if (comp == 1)
                    break;
            }
        }
    }
    
    public static int CompTurn(string[] array, string[] numbs, string myAnswer, string compAnswer, Random rnd)
    {
        int value = rnd.Next(0, 9);                                             
        while (array[value] == compAnswer || array[value] == myAnswer)
        {
            value = rnd.Next(0, 9);
        }
        array[value] = compAnswer;
        numbs[value] = "";
        Console.WriteLine("Ход противника:");
        Console.WriteLine($"\t{array[0]}|\t{array[1]}|\t{array[2]}\t\t\t{numbs[0]}|\t{numbs[1]}|\t{numbs[2]}");
        Console.WriteLine("-------------------------\t\t-------------------------");
        Console.WriteLine($"\t{array[3]}|\t{array[4]}|\t{array[5]}\t\t\t{numbs[3]}|\t{numbs[4]}|\t{numbs[5]}\t\t");
        Console.WriteLine("-------------------------\t\t-------------------------");
        Console.WriteLine($"\t{array[6]}|\t{array[7]}|\t{array[8]}\t\t\t{numbs[6]}|\t{numbs[7]}|\t{numbs[8]}");
        
        if (array[0] == array[1] && array[1] == array[2] && array[0] == compAnswer ||
            array[3] == array[4] && array[4] == array[5] && array[3] == compAnswer ||
            array[6] == array[7] && array[7] == array[8] && array[6] == compAnswer ||
            array[0] == array[3] && array[3] == array[6] && array[0] == compAnswer ||
            array[1] == array[4] && array[4] == array[7] && array[1] == compAnswer ||
            array[2] == array[5] && array[5] == array[8] && array[2] == compAnswer ||
            array[0] == array[4] && array[4] == array[8] && array[0] == compAnswer ||
            array[2] == array[4] && array[4] == array[6] && array[2] == compAnswer )
            {   
                Console.WriteLine($"\t{array[0]}|\t{array[1]}|\t{array[2]}");
                Console.WriteLine("-------------------------");
                Console.WriteLine($"\t{array[3]}|\t{array[4]}|\t{array[5]}");
                Console.WriteLine("-------------------------");
                Console.WriteLine($"\t{array[6]}|\t{array[7]}|\t{array[8]}");
                Console.WriteLine($"{compAnswer} WIN!");
                return 1;
            }
        return 0;
    }
    
    public static int PersTurn(string[] array, string[] numbs, string compAnswer, string myAnswer)
    {
        Console.WriteLine("\nКуда ставим?");                                    
        int myTurn = int.Parse(Console.ReadLine());
        while (myTurn >= 10)
            myTurn = int.Parse(Console.ReadLine());
            
        while(array[myTurn-1] == compAnswer || array[myTurn-1] == myAnswer)
            myTurn = int.Parse(Console.ReadLine());
        array[myTurn-1] = myAnswer;
        numbs[myTurn-1] = "";
        if (array[0] == array[1] && array[1] == array[2] && array[0] == myAnswer ||
            array[3] == array[4] && array[4] == array[5] && array[3] == myAnswer ||
            array[6] == array[7] && array[7] == array[8] && array[6] == myAnswer ||
            array[0] == array[3] && array[3] == array[6] && array[0] == myAnswer ||
            array[1] == array[4] && array[4] == array[7] && array[1] == myAnswer ||
            array[2] == array[5] && array[5] == array[8] && array[2] == myAnswer ||
            array[0] == array[4] && array[4] == array[8] && array[0] == myAnswer ||
            array[2] == array[4] && array[4] == array[6] && array[2] == myAnswer )
            {   
                Console.WriteLine($"\t{array[0]}|\t{array[1]}|\t{array[2]}");
                Console.WriteLine("-------------------------");
                Console.WriteLine($"\t{array[3]}|\t{array[4]}|\t{array[5]}");
                Console.WriteLine("-------------------------");
                Console.WriteLine($"\t{array[6]}|\t{array[7]}|\t{array[8]}");
                Console.WriteLine($"{myAnswer} WIN!");
                return 1;
            }
        return 0;
    }
}
