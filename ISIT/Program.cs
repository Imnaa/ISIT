using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ISIT
{
    class Program
    {
        static void Main(string[] args)
        {
            string name;
            int credit, exams, zachets;
            StreamReader sr = new StreamReader("../../../input.txt");
            Console.Write("Количество предметов: ");
            int count = int.Parse(Console.ReadLine());
            // Создаем массив для всех важностей
            float[,] massVajn = new float[count, count];
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
            Discipline[] shedule = new Discipline[count];
            for (int i = 0; i < count; i++)
            {
                name = sr.ReadLine();
                credit = int.Parse(sr.ReadLine());
                exams = int.Parse(sr.ReadLine());
                zachets = int.Parse(sr.ReadLine());
                /* считвыаем из файла и кидаем в ()*/
                shedule[i] = new Discipline(i + 1, credit, exams, zachets, name);
                /*заполняем влияние предмета на предметы*/
                shedule[i].influence = new float[count];
                for (int j = 0; j < count; ++j)
                {
                    shedule[i].influence[j] = massVajn[i, j];
                }
                
            }
            // Перебор + оценка 
            foreach (var arr in AllPermutations(shedule))
            {
                foreach (var i in arr)
                    Console.Write(i + " ");
                Console.WriteLine();
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
