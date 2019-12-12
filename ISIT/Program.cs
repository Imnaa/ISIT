using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ISIT
{
    class Program
    {
        static float[,] massVajn;
        static void Main(string[] args)
        {
            string name, outString;
            int credit, exams, zachets;
            int countSem, countCred;
            StreamReader sr = new StreamReader("../../../input.txt");
            StreamWriter sw = new StreamWriter("../../../output.txt");

            Console.Write("Количество предметов: ");
            int count = int.Parse(Console.ReadLine());
            Console.Write("Количество семестров: ");
            countSem = int.Parse(Console.ReadLine());
            Console.Write("Максимальное количество кредитов: ");
            countCred = int.Parse(Console.ReadLine());
            Console.Write("Введите коэффициент отсеивания: ");
            double coef = Convert.ToDouble(Console.ReadLine());
            // Создаем массив для всех важностей
            massVajn = new float[count, count];
            StreamReader srMassVajn = new StreamReader("../../../MassiveVajnostei.txt");
            // Перебираем файл в важностями
            for (int i = 0; i < count; ++i)
            {
                // запомнили строку
                string s = srMassVajn.ReadLine();
                // разделили строку на элементы
                string[] t = s.Split('\t');
                // по очереди присваиваем в наш массив
                for (int j = 0; j < count; ++j)
                {
                    massVajn[i, j] = float.Parse(t[j]);
                }
            }
            /*объявление всех предметов*/
            sr.ReadLine(); // skip first line
            Discipline[] shedule = new Discipline[count];
            for (int i = 0; i < count; i++)
            {
                // запомнили строку
                string s = sr.ReadLine();
                // разделили строку на элементы
                string[] t = s.Split('\t');
                // 
                name = t[0];
                credit = int.Parse(t[1]);
                exams = int.Parse(t[2]);
                zachets = int.Parse(t[3]);
                /* считвыаем из файла и кидаем в ()*/
                shedule[i] = new Discipline(i + 1, credit, exams, zachets, name);
                /* заполняем влияние предмета на предметы*/
                shedule[i].influence = new float[count];
                for (int j = 0; j < count; ++j)
                {
                    shedule[i].influence[j] = massVajn[i, j];
                }

            }
            // ?
            int[] semestrs = new int[countSem];
            int cred;
            int curSem;
            double sum;
            // Перебор + оценка 
            foreach (var arr in AllPermutations(shedule))
            {
                cred = 0;
                curSem = 0;
                sum = 0;
                // получили один из возможных последовательностей предметов
                for (int i = 0; i < count; ++i) // разбили на семестры
                {
                    cred += arr[i].credits;

                    if (cred > countCred)
                    {
                        semestrs[curSem] = i - 1;
                        cred = 0;
                        curSem++;
                        if (curSem == countSem && i < count - 1)
                        {
                            curSem = -1;
                            break;
                        }
                    }

                    if (curSem == 0)
                    {
                        sum += 0.5f;
                    }
                    else
                    {
                        sum += 1.0 / (1.0 + Math.Exp(summMass(semestrs[curSem], semestrs[curSem - 1], arr[i].id)));
                    }
                }
                if (curSem == -1 || sum < coef * count) continue;
                outString = "";
                curSem = 0;

                for (int i = 0; i < count; ++i)
                {
                    outString += arr[i].name + " ";
                    if (i == semestrs[curSem++])
                    {
                        outString += "| ";
                    }
                }
                sw.Write(outString);
            }

            /*создаем массив семестров, в каждом семестре - массив предметов
             * при каждом цикле смотрим, в каком семестре предмет - i
             * вычисляем важность предмета относительно семестра i-1
             * если в 1м семестре, то важность срезу 0.5
             */

            /*перестановка всех предметов
             *   проверка того, что предмет удволетворяет критериям
             *      если удволетворяет, то выводим в файл
             */

            /*Критерии
             *  Ограничение по кредитам - 30 шт.
             *  Название предметов в семестре не должны повторяться
             *  Коэф влияния итоговый > 0.75*количество предметов
             */


            Console.Read();
        }
        static float summMass(int end, int start, int id)
        {
            float boof = 0;
            for (int i = start; i < end; ++i)
                boof += massVajn[i, id];
            return boof;
        }

        static bool Next(ref Discipline[] arr)
        {
            int k, j, l;
            for (j = arr.Length - 2; (j >= 0) && (arr[j].id >= arr[j + 1].id); j--) { }
            if (j == -1)
            {
                arr = arr.OrderBy(c => c).ToArray();
                return false;
            }
            for (l = arr.Length - 1; (arr[j].id >= arr[l].id) && (l >= 0); l--) { }
            var tmp = arr[j];
            arr[j] = arr[l];
            arr[l] = tmp;
            for (k = j + 1, l = arr.Length - 1; k < l; k++, l--)
            {
                tmp = arr[k];
                arr[k] = arr[l];
                arr[l] = tmp;
            }
            return true;
        }
        static IEnumerable<Discipline[]> AllPermutations(Discipline[] arr)
        {
            do yield return arr;
            while (Next(ref arr));
        }
    }
}
