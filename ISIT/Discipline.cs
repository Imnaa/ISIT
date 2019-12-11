using System;
using System.Collections.Generic;
using System.Text;

namespace ISIT
{
    class Discipline
    {
        public int id { get; set; } // номер дисциплины
        public int credits { get; set; } // количество кредитов предмета
        public int exams { get; set; } // Экзамен в конце
        public int zachets { get; set; } // Зачет в конце
        public float[] influence { get; set; } // Влияние?
        public string name { get; set; } // Название предмета
        public Discipline(int _id, int _credits, int _exams, int _zachets, string _name)
        {
            id = _id;
            credits = _credits;
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
