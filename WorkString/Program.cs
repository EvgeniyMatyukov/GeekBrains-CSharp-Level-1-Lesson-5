using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Задача
//2. Разработать методы для решения следующих задач. Дано сообщение:
//а) Вывести только те слова сообщения,  которые содержат не более чем  n букв;
//б) Удалить из сообщения все слова, которые заканчиваются на заданный символ;
//в) Найти самое длинное слово сообщения;
//г) Найти все самые длинные слова сообщения.
//Постарайтесь разработать класс MyString.
//Автор Матюков Евгений

namespace WorkString
{
    class Program
    {
        class MyString //класс для работы со строками
        {
            string message; //сообщение
            string answer;  //здесь храним результат

            public MyString(string message) //конструктор сообщения
            {
                this.message = message;
                this.answer = message;
            }

            public void ShortWords(int n) //записать в ответ только те слова в которых не больше n букв
            {
                string[] str = message.Split(' '); //создать массив из слов (слова разделены пробелом)

                answer = ""; //обнуляем ответ
               
                foreach (var word in str) //пройдемся по всем словам
                    if (word.Length <= n) answer += word + " "; //если слово не длиннее n символов, добавить его к ответу, разделять слова в ответе пробелами
            }

            public void DeleteWords(char ch) //записать в ответ только те слова, у которых в конце нет буквы ch
            {
                string[] str = message.Split(' '); //создать массив из слов (слова разделены пробелом)

                answer = ""; //обнуляем ответ

                foreach (var word in str) //пройдемся по всем словам
                    if (word[word.Length-1] != ch) answer += word + " "; //если последняя буква в слове не равна ch, добавить это слово к ответу, разделять слова в ответе пробелами
            }

            public void LongestWord() //записать в ответ только самое длинное слово
            {
                string[] str = message.Split(' '); //создать массив из слов (слова разделены пробелом)

                answer = ""; //обнуляем ответ
                
                foreach (var word in str) //пройдемся по всем словам
                    if (answer.Length < word.Length) answer = word; //если ответ короче текущего слова, записать в ответ текущее слово
            }

            public void LongestWords() //записать в ответ самые длинные слова
            {
                string[] str = message.Split(' '); //создать массив из слов (слова разделены пробелом)

                answer = ""; //обнуляем ответ
                int maxLen = 0; //слово максимальной длины

                foreach (var word in str) //пройдемся по всем словам
                {
                    if (maxLen < word.Length) //если находим более длинное слово
                    {
                        maxLen = word.Length; //запоминаем длину более длинного слова
                        answer = word; //обнуляем ответ, и записавыем в него более длинное слово
                    }

                    else if (maxLen == word.Length) answer += " " + word; //если слово такой же длины, что и самое длинное слово, то добавить его в ответ
                }
            }

            public override string ToString() //перегружаем ToString() чтобы Console.WriteLine могла напечатать наш ответ
            {
                return answer;
            }
        }


        static void Main(string[] args)
        {
            Console.Write("Введите сообщение или нажмите enter: ");
            string message = Console.ReadLine();

            if (message == "")
            {
                message = "rty ghj ghjz uioz zzz tyur";
                Console.WriteLine(message);
            }

            var str = new MyString(message); //создаем объект

            str.ShortWords(3); //в ответе будут все слова, не длиннее трёх букв 
            Console.WriteLine("\nВсе слова, не длиннее трёх букв");
            Console.WriteLine(str);

            str.DeleteWords('z'); //в ответе будут все слова, кроме тех у которых последняя буква z 
            Console.WriteLine("\nВсе слова, кроме тех у которых последняя буква z");
            Console.WriteLine(str);

            str.LongestWord(); //в ответе будет самое длинное слово 
            Console.WriteLine("\nСамое длинное слово");
            Console.WriteLine(str);

            str.LongestWords(); //в ответе будут самые длинные слова 
            Console.WriteLine("\nСамые длинные слова");
            Console.WriteLine(str);

            Console.ReadKey();

        }
    }
}
