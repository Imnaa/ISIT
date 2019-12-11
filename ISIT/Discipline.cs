using System;
using System.Collections.Generic;
using System.Text;

namespace ISIT
{
    class Discipline
    {
        public int number { get; set; } // номер дисциплины
        private int credits { get; set; } // количество кредитов предмета
        private int exams { get; set; } // Экзамен в конце
        private int zachets { get; set; } // Зачет в конце
        private int[] influence { get; set; } // Влияние?
        private string name { get; set; } // Название предмета
        public Discipline(int _number, int _exams, int _zachets, string _name)
        {
            number = _number;
            exams = _exams;
            zachets = _zachets;
            name = _name;
        }

        //метод ВернутьЗнач(i, j)
        //{
        //    return mas[i][j];
        //}
            


    }
}
