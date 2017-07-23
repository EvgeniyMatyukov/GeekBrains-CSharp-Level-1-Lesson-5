using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions; 

//Условия задачи
//1.	Создать программу, которая будет проверять корректность ввода логина. Корректным логином будет строка от 2-х до 10-ти символов, содержащая только буквы или цифры, и при этом цифра не может быть первой.
//а) без использования регулярных выражений;
//б) **с использованием регулярных выражений.
//Автор Матюков Евгений

namespace CorrectLogin
{
    
    class Program
    {
        static bool isCorrectLoginA(string login) //проверка корректности логина без использования регулярных выражений
        {
            if (login.Length < 2 || login.Length > 10) return false; //проверка длины логина

            if (char.IsDigit(login[0])) return false; //проверка, что логин не начинается с цифры

            foreach (var ch in login)  //проверка на соответствие на содержание только букв и цифр в логине
                if (!char.IsLetterOrDigit(ch)) return false;

            return true;
        }

        static bool isCorrectLoginB(string login) //проверка корректности логина c использованием регулярных выражений
        {
            Regex myReg = new Regex(@"^[A-Za-z]+[A-Za-z0-9]{1,9}$"); //символ начала строки && любой символ от A до Z или от a до z && любой символ от A до Z или от a до z или от 0 до 9 && повторенный от 1 до 9 раз && символ конца строки 
            if (myReg.IsMatch(login)) return true;

            return false;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Программа проверит корректоность введенного логина");
            Console.WriteLine("Логин должен быть длиной от 2 до 10 символов, содержать только буквы или цифры");
            Console.WriteLine("Логин не может начинаться с цифры");
            Console.WriteLine("Для выхода введите q");
            
            string login;

            do
            {
                Console.Write("\nЛогин: ");
                login = Console.ReadLine();

                if (isCorrectLoginA(login)) Console.WriteLine("функция 1: Логин корректен");
                else Console.WriteLine("функция 1: Логин не корректен");

                if (isCorrectLoginB(login)) Console.WriteLine("функция 2: Логин корректен");
                else Console.WriteLine("функция 2: Логин не корректен");
            }
            while (login != "q"); 

        }
    }
}
