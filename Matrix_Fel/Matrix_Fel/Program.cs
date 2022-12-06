using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_Fel
{
    class UserArray
    {
        private int inputX = 1;
        private int inputY = 1;
        private double[,] s;
        private double[] Even;
        private double[] Odd;
        public UserArray()
        {
            Console.Write("Hány soros legyen a mátrix? ");
            inputY = Int32.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Hány oszlopos legyen a mátrix? ");
            inputX = Int32.Parse(Console.ReadLine());
            Console.WriteLine();
            s = new double[inputY, inputX];
        }

        public void FillWithRandom(int start, int end) // array contains from start to end values, included both
        {
            Random r = new Random();
            for (int i = 0; i < inputY; i++)
                for (int j = 0; j < inputX; j++)
                    s[i, j] = r.Next(start, end + 1) / 10.0;
        }

        public void FillWithUserInput()
        {
            for (int i = 0; i < inputY; i++)
                for (int j = 0; j < inputX; j++)
                    s[i, j] = Int32.Parse(Console.ReadLine());
        }

        public double Min()
        {
            double min = s[0, 0];
            for (int i = 0; i < inputY; i++)
                for (int j = 0; j < inputX; j++)
                    if (s[i, j] < min)
                        min = s[i, j];

            return min;
        }

        public double Max()
        {
            double max = s[0, 0];
            for (int i = 0; i < inputY; i++)
                for (int j = 0; j < inputX; j++)
                    if (max < s[i, j])
                        max = s[i, j];

            return max;
        }

        public double Avg()
        {
            double sum = 0;
            for (int i = 0; i < inputY; i++)
                for (int j = 0; j < inputX; j++)
                    sum += s[i, j];

            return sum / (inputX * inputY);
        }

        public bool Contains(double num)
        {
            for (int i = 0; i < inputY; i++)
                for (int j = 0; j < inputX; j++)
                    if (s[i, j] == num)
                        return true;
            return false;
        }

        private void MySort_Switch(ref int i, ref int j, ref int z, ref int k)
        {
            double temp = s[i, j];
            s[i, j] = s[z, k];
            s[z, k] = temp;
        }

        public void MySort(bool asc)
        {
            for (int i = 0; i < inputY; i++)
                for (int j = 0; j < inputX; j++)
                    for (int z = i; z < inputY; z++)
                        for (int k = (i == z) ? j : 0; k < inputX; k++)
                        {
                            if (asc)
                            {
                                if (s[i, j] < s[z, k])
                                {
                                    MySort_Switch(ref i, ref j, ref z, ref k);
                                }
                            }
                            else if (s[i, j] > s[z, k])
                            {
                                MySort_Switch(ref i, ref j, ref z, ref k);
                            }
                        }
        }

        public void WriteOut()
        {
            for (int i = 0; i < inputY; i++)
            {
                for (int j = 0; j < inputX; j++)
                    Console.Write(s[i, j] + " ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void Odd_Even()
        {
            int[] IndexY = new int[inputY * inputX];
            int[] IndexX = new int[inputY * inputX];
            int countInd = 0;
            for (int i = 0; i < inputY; i++)
                for (int j = 0; j < inputX; j++)
                    if (s[i, j] % 2.0 == 0.0)
                    {
                        IndexY[countInd] = i;
                        IndexX[countInd] = j;
                        countInd++;
                    }
            IndexY[countInd + 1] = -1;
            Even = new double[countInd];
            Odd = new double[IndexX.Length - countInd];

            for (int i = 0; i < Even.Length; i++)
                Even[i] = s[IndexY[i], IndexX[i]];

            int tempInd = 0;
            int OddInd = 0;
            for (int i = 0; i < inputY; i++)
                for (int j = 0; j < inputX; j++)
                {
                    if (i != IndexY[tempInd] || j != IndexX[tempInd])
                    {
                        Odd[OddInd] = s[i, j];
                        OddInd++;
                    }
                    else
                        tempInd++;
                }

            WriteResult();
        }

        private void WriteResult()
        {
            Console.Write("Páros: ");
            for (int i = 0; i < Even.Length; i++)
                Console.Write(Even[i] + " ");
            Console.WriteLine();
            Console.Write("Páratlan: ");
            for (int i = 0; i < Odd.Length; i++)
                Console.Write(Odd[i] + " ");
            Console.WriteLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            UserArray arr = new UserArray();

            Console.WriteLine(
                "Hogy töltse fel a tömböt? \n" +
                "0. Te adod meg \n" +
                "1. Random");
            if (Console.ReadLine() == "0")
                arr.FillWithUserInput();
            else
                arr.FillWithRandom(-500, 500);
            Console.Clear();
            
            int Input = 666;
            do
            {
                Console.WriteLine(
                    "Mit akarsz csinálni a tömbbel? \n" +
                    "1. Minimum elem keresése \n" +
                    "2. Maximum elem keresése \n" +
                    "3. Elemek átlaga \n" +
                    "4. Tartalmaz-e egy számot? \n" +
                    "5. Rendezzük növekvő sorba \n" +
                    "6. Rendezzük csökkenő sorba \n" +
                    "7. Válogassuk szét páros és páratlan számokra (befejezi a programot) \n" +
                    "~ Minden más input befejezi a programot \n");

                switch (Input)
                {
                    case 1: Console.WriteLine(arr.Min()); break;
                    case 2: Console.WriteLine(arr.Max()); break;
                    case 3: Console.WriteLine(arr.Avg()); break;
                    case 4: Console.Write("A szám amit keresel: "); Console.WriteLine((arr.Contains(double.Parse(Console.ReadLine()))) ? "Megtalálható benne a szám" : "Nem található meg benne a szám"); break;
                    case 5: arr.WriteOut(); arr.MySort(false); arr.WriteOut(); break;
                    case 6: arr.WriteOut(); arr.MySort(true); arr.WriteOut(); break;
                    case 7: arr.Odd_Even(); break;
                    case 666: break;
                    default: System.Environment.Exit(1); break;
                }

                Console.Write("Input: ");
                Input = Int32.Parse(Console.ReadLine());
                Console.Clear();
            } while (1 <= Input && Input <= 7);
        }
    }
}
