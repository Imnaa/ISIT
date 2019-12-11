using System;
using System.Collections.Generic;
using System.Text;

namespace ISIT
{
    class Discipline
    {
        public int id { get; set; }
        public int credits { get; set; }
        public int exams { get; set; }
        public int zachets { get; set; }
        public float[] influence { get; set; }
        public string name { get; set; }

        public Discipline(int _id, int _credits, int _exams, int _zachets, string _name)
        {
            id = _id;
            credits = _credits;
            exams = _exams;
            zachets = _zachets;
            name = _name;

            // здесь чтение из файла
        }

        /*метод ВернутьЗнач(i, j)
        {
            return mas[i][j];
        }*/
            


    }
}
