using System;
using System.Threading;

///<summary>
///Author: Yusuf Yakubov
///</summary>

namespace GameOfLive
{
    class Program
    {
        public static bool[,] field;
        public static int rows = 60;
        public static int cols = 60;
        public static int gen = 0;


        static void Draw()
        {
            Console.Clear();
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < cols; j++)
                    Console.Write(field[j, i] ? " #" : "  ");
                Console.Write("\n");    
            }   
            gen++;
            Console.WriteLine($"Generation: {gen}"); 
        }

        static int CountNeighbours(int x, int y)
        {
            int count = 0;
            bool haslife = field[x, y];
            try{
                if(field[x, y+1])
                    count++;
                if(field[x, y-1])
                    count++;
                if(field[x+1, y])
                    count++;
                if(field[x-1, y])
                    count++;
                if(field[x+1, y+1])
                    count++;
                if(field[x-1, y+1])
                    count++;
                if(field[x+1, y-1])
                    count++;
                if(field[x-1, y-1])
                    count++;
            }catch{}
            return count;
        }

        static void start()
        {
            field = new bool[rows, cols];
            Random random = new Random();
            for(int i = 0; i < cols; i++)
                for(int j = 0; j < rows; j++)
                    field[i, j] = random.Next(5) == 0; 
            Draw();
        }

        static void update()
        {
            var newfield = new bool[rows, cols];
            for(int x= 0; x < cols; x++)
                for(int y = 0; y < rows; y++)
                {
                    int neighbours = CountNeighbours(x, y);
                    bool haslife = field[x, y];

                    if(!haslife && neighbours == 3)
                        newfield[x, y] = true;
                    else if(haslife && (neighbours < 2 || neighbours > 3))
                        newfield[x, y] = false;
                    else
                        newfield[x, y] = field[x, y];
                }
            field = newfield;
            Draw();
        }

        static void Main(string[] args)
        {
            Console.Clear();
            start();
            while(true)
            {
                update();
                Thread.Sleep(70);
            }
        }
    }
}
