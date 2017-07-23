using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

//задача
//На вход программе подаются сведения о сдаче экзаменов учениками 9-х классов некоторой средней школы. В первой строке сообщается количество учеников N, которое не меньше 10, но не превосходит 100, каждая из следующих N строк имеет следующий формат:
//<Фамилия> <Имя> <оценки>,
//где <Фамилия> – строка, состоящая не более чем из 20 символов, <Имя> – строка, состоящая не более чем из 15 символов, <оценки> – через пробел три целых числа, соответствующие оценкам по пятибалльной системе. <Фамилия> и <Имя>, а также <Имя> и <оценки> разделены одним пробелом. Пример входной строки:
//Иванов Петр 4 5 3
//Требуется написать как можно более эффективную программу, которая будет выводить на экран фамилии и имена трех худших по среднему баллу учеников. Если среди остальных есть ученики, набравшие тот же средний балл, что и один из трех худших, то следует вывести и их фамилии и имена.


namespace WorstPupils
{
    class WorstPupils
    {
        List<string> name = new List<string>(); //список учеников
        List<decimal> averageMark = new List<decimal>(); //список средних оценок
        decimal[] minAverageMark = new decimal [3]; //три минимальные средние оценки

        bool status = false; //true, если формат файла корректен

        //читает файл с логинами и паролями
        //формат файла
        //[количество_учеников]\n
        //[Фамилия] [Имя] [Оценка1] [Оценка2] [Оценка3]\n
        public WorstPupils(string filename)
        {
            StreamReader sr;

            //Если файл существует
            if (File.Exists(filename))
            {
                //Считываем все строки из файла 
                sr = new StreamReader(filename);


                Console.WriteLine("Список учеников с оценками");
                string s;
                int currentLine = 0; //указатель на текущую строку
                int totalLines = 0; //всего строк в файле на одну больше, чем учеников
 
                string sName; //фамилия+имя ученика
                decimal sAverageMark; //средняя оценка

                while (!sr.EndOfStream)  // Пока не конец потока (файла)
                {

                    s = sr.ReadLine();

                    if (currentLine == 0) //в первой строке - количество учеников
                    {
                        if (!int.TryParse(s, out totalLines)) //пробуем спарсить количество учеников, если ошибка выход
                        {
                            sr.Close(); //закрыть файл
                            return; 
                        }

                        if (totalLines < 10 || totalLines > 100) //по условиям задачи учеников в списке от 10 до 100
                        {
                            sr.Close(); //закрыть файл
                            return; 
                        }
                        currentLine++;
                    }
                    else //в последующих строках - имя + оценки
                    {
                        Console.WriteLine(s);
                        if (!Recognize(s, out sName, out sAverageMark)) //распознать строку, если строка некорректна - выход
                        {
                            sr.Close(); //закрыть файл
                            return; 
                        }

                        name.Add(sName); //добавляем в список фамилию + имя ученика
                        averageMark.Add(sAverageMark); //добавляем в список среднюю оценку


                        currentLine++;
                    }

                    if (currentLine > totalLines) break; //обработали всех заявленных в первой строке учеников
                }

                FindMinimalMarks(); //найти три минимальные средние оценки

                status = true; //файл обработан корректно
                sr.Close(); //закрыть файл
            }
            else Console.WriteLine("Error load file");
        }


        private void FindMinimalMarks() //найти три минимальные средние оценки
        {

            List<decimal> cAverageMark = new List<decimal>(averageMark); //делаем копию списка оценок

            cAverageMark.Sort(); //сортируем по возрастанию

            for (int n = 1; n < cAverageMark.Count; n++) //убираем дубли
                if (cAverageMark[n - 1] == cAverageMark[n])
                {
                    cAverageMark.RemoveAt(n);
                    n--;
                }

            //обнуляем список минимальных оценок
            minAverageMark[0] = 0;
            minAverageMark[1] = 0;
            minAverageMark[2] = 0;

            //копируем в список минимальные оценки
            for (int n = 0; n < cAverageMark.Count; n++)
            {
                minAverageMark[n] = cAverageMark[n];
                if (n == 2) break;
            }
        }


        private bool Recognize(string s, out string sName, out decimal sAverageMark) //распознать строку, и вытащить из неё фамилию-имя и оценки
        {
            sName = "";
            sAverageMark = 0;
            bool ignoreSpace = true; //флаг, что надо игнорировать пробел

            int n = 0, i;

            for (i = 0; i < s.Length; i++) //всё, что до второго пробела - фамилия и имя
            {
                if (ignoreSpace) //можно игнорировать пробел?
                {
                    sName += s[i];
                    if (s[i] == ' ') ignoreSpace = false; //больше не игнорировать пробел
                }
                else if (s[i] != ' ') sName += s[i];
                else
                {
                    n = i + 1;
                    break;
                }
            }

         //   Console.WriteLine(sName);

            if (n == 0 || s[n + 1] != ' ' || s[n + 3] != ' ') return false; //проверка на корректность формата строки
            
            int currentMark; //текущая оценка хранится здесь
            if (!int.TryParse(s.Substring(n, 1), out currentMark)) return false;
            sAverageMark = currentMark;
            if (!int.TryParse(s.Substring(n + 2, 1), out currentMark)) return false;
            sAverageMark += currentMark;
            if (!int.TryParse(s.Substring(n + 4, 1), out currentMark)) return false;
            sAverageMark += currentMark;
            sAverageMark /= 3;

        //    Console.WriteLine(sAverageMark);
            return true;
        }


        public bool Status
        {
            get
            {
                return status;
            }
        }

        public override string ToString() //перегрузим метод чтобы writeline() мог напечатать результат
        {
            string result="";
            int i = 0;
            do //пройдемся по каждой из низких оценок
            {
                for (int n = 0; n < name.Count; n++)
                    if (averageMark[n] == minAverageMark[i]) result += name[n] + " " + minAverageMark[i].ToString("0.00") + "\n"; //если совпадает с низкой оценкой - добавить в список

                if (i < 2 && minAverageMark[i+1] == 0) return result; //если низкая оценка не определена, выход
                i++; //перейдем к следующей низкой оценке
            } while (i <= 2); 

            return result;
        }
 
    }
    class Program
    {
        static void Main(string[] args)
        {
            var ar = new WorstPupils("pupils.txt");
            if (!ar.Status)
            {
                Console.WriteLine("файл с оценками повреждён");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nСамые низкие оценки у");
            Console.WriteLine(ar);

            Console.ReadKey();
        }
    }
}
