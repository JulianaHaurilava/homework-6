using System;
using System.IO;

namespace task_6._1
{
    class Program
    {
        /// <summary>
        /// Определяет возраст сотрудника
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns>Возраст сотрудника</returns>
        public static int GetAge(DateTime dateOfBirth)
        {
            return DateTime.Today.Year - dateOfBirth.Year -
                ((DateTime.Today.Month > dateOfBirth.Month ||
                  DateTime.Today.Month == dateOfBirth.Month && DateTime.Today.Day >= dateOfBirth.Day) ? 0 : 1);
        }

        /// <summary>
        /// Проверяет корректность введенного ID
        /// </summary>
        /// <returns>ID сотрудника</returns>
        static string CheckAndGetID()
        {
            Console.WriteLine("ID сотрудника");
            int inputInfo;
            while (!int.TryParse(Console.ReadLine(), out inputInfo))
            {
                Console.WriteLine("Ошибка! Введите целое число.");
            }
            return Convert.ToString(inputInfo);
        }

        /// <summary>
        /// Проверяет корректность введенной даты рождения
        /// </summary>
        /// <param name="employeeInfo"></param>
        /// <returns>Дата рождения сотрудника</returns>
        static string CheckAndGetDateOfBirth(string[] employeeInfo)
        {
            Console.WriteLine("Введите дату рождения в формате дд.ММ.гггг");
            DateTime dateOfBirth;

            while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth) ||
                   GetAge(dateOfBirth) < 18 || GetAge(dateOfBirth) > 70)
            {
                Console.WriteLine("Ошибка! Введите корректную дату рождения в формате дд.ММ.гггг\n" +
                                  "Помните, что сотрудники нашей фирмы не могут быть младше 18 " +
                                  "и старше 70 лет.");
            }
            employeeInfo[3] = Convert.ToString(GetAge(dateOfBirth));

            return dateOfBirth.ToShortDateString();
        }

        /// <summary>
        /// Проверяет корректность введенного роста
        /// </summary>
        /// <returns>Рост сотрудника</returns>
        static string CheckAndGetHeight()
        {
            Console.WriteLine("Рост сотрудника (в см)");
            int employeeHeight;
            while (!int.TryParse(Console.ReadLine(), out employeeHeight) || employeeHeight < 100 || employeeHeight > 250)
            {
                Console.WriteLine("Ошибка! Введите корректный рост (в см)");
            }
            return Convert.ToString(employeeHeight);
        }

        /// <summary>
        /// Ввод информации о сотруднике
        /// </summary>
        /// <returns>Строка с информацией о сотруднике</returns>
        static string GetEmployeeInfo()
        {
            string[] employeeInfo = new string[7];
            Console.WriteLine("Введите информацию о сотруднике.\n");

            employeeInfo[0] = CheckAndGetID();
            Console.WriteLine("Ф.И.О. сотрудника");
            employeeInfo[2] = Console.ReadLine();
            employeeInfo[4] = CheckAndGetHeight();
            employeeInfo[5] = CheckAndGetDateOfBirth(employeeInfo);
            Console.WriteLine("Место рождения сотрудника");
            employeeInfo[6] = "город " + $"{Console.ReadLine()}";
            employeeInfo[1] = DateTime.Now.ToShortTimeString();
            return String.Join('#', employeeInfo);
        }

        /// <summary>
        /// Создает файл и добавляет информацию о сотруднике
        /// </summary>
        static void AddToFile()
        {
            using (StreamWriter stream = new StreamWriter(@"Сотрудники.txt", true))
            {
                stream.WriteLine(GetEmployeeInfo());
            }
        }

        /// <summary>
        /// Выводит информацию о сотруднике
        /// </summary>
        static void ReadFile()
        {
            if (File.Exists(@"Сотрудники.txt"))
            {
                using (StreamReader stream = new StreamReader(@"Сотрудники.txt"))
                {
                    while (!stream.EndOfStream)
                    {
                        string[] employeeInfo = stream.ReadLine().Split('#');

                        string pattern = "ID сотрудника: {0}\nДата и время добавления записи: {1}\n" +
                                         "Ф.И.О. сотрудника: {2}\nВозраст сотрудника: {3}\n" +
                                         "Рост сотрудника: {4} см\nДата рождения: {5}\n" +
                                         "Место рождения сотрудника: {6}\n";

                        Console.WriteLine(pattern,
                                          employeeInfo[0],
                                          employeeInfo[1],
                                          employeeInfo[2],
                                          employeeInfo[3],
                                          employeeInfo[4],
                                          employeeInfo[5],
                                          employeeInfo[6]);
                    }
                }
            }
            else Console.WriteLine("Записей о сотрудниках нет.");
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Что вы хотите сделать?" +
                                  "Выберите соответствующее число.\n" +
                                  "1 — вывести данные на экран;\n" +
                                  "2 — добавить новую запись о сотруднике.\n" +
                                  "Для того, чтобы завершить работу, введите любой другой символ.");

                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1':
                    {
                        Console.Clear();
                        ReadFile();
                        break;
                    }
                    case '2':
                    {
                        Console.Clear();
                        AddToFile();
                        break;
                    }
                    default: return;
                }

                Console.WriteLine("\nДля того, чтобы выйти в главное меню, нажмите любую клавишу...");
                Console.ReadKey(true);
                Console.Clear();
            }
        }
    }
}

