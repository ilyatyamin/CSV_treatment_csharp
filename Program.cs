using RefineAndFormat;

// Выстрадано Тямином Ильей БПИ226

partial class Program
{
    public static void Main(string[] args)
    { 
        string fileName = String.Empty;
        List<string> numbers;
        int numberOfDigits;

        do
        {
            // Печатаем меню.
            Console.WriteLine("Введите имя файла. Убедитесь, что \n" +
                              "1) Файл находится в той же папке, что и сама программа. \n" +
                              "2) Файл соответствует следующей структуре: числа, каждое в новой строке. \n" +
                              "3) Файл представлен в кодировке UTF-8. (!!!) \n" +
                              "Программа сохранит 2 файла: с отформатированными целыми числами и с остальными значениями. \n" +
                              "Если вы хотите завершить работу программы, то введите слово exit");
            
            fileName = Console.ReadLine();

            // Если пользователь ввел exit, то выходим из программы.
            if (fileName == "exit")
            {
                break;
            }

            Console.WriteLine("Введите точность числовых значений");
            // Проверяем, что число можно преобразовать в int, и оно больше 1.
            if (!(int.TryParse(Console.ReadLine(), out numberOfDigits)))
            {
                Console.WriteLine("Введено неправильное значение точности. Повторите ввод \n");
                continue;
            }

            // Если файл прошел все условия валидации, то работает со значениями.
            if (ValidateFile(fileName, out numbers))
            {
                // Открываем 2 потока на запись, один с целыми числами, другой со всеми
                StreamWriter sw1 = new StreamWriter($"ints.txt");
                StreamWriter sw2 = new StreamWriter($"all.txt");
                try
                {
                    // Создаем объект-фильтр класса IntPreciseFilter.
                    IntPreciseFilter ipf = new IntPreciseFilter(numberOfDigits);
                    DoublePreciseFilter dpf = new DoublePreciseFilter(numberOfDigits);
                    foreach (string item in numbers)
                    {
                        string result = ipf.Filter(item);
                        // Если фильтр отдал не пустую строку, значит это значение типа int или double, но с вещественностью  частью .0
                        if (result != String.Empty)
                        {
                            sw1.WriteLine(result);
                            sw2.WriteLine(result);
                        }
                        // Других видов значений быть не может, так как числа бывают или целые, или вещественные,
                        // ошибочные значения мы проверяем и не допускаем в методе валидации.
                        else
                        {
                            // Иначе создаем объект-фильтр для вещественных чисел.
                                
                            result = dpf.Filter(item);

                            // Записываем только во второй файл.
                            sw2.WriteLine(result);
                        }
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Введено неправильное значение точности. Повторите ввод \n");
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Возникла ошибка. Повторите ввод \n");
                    continue;
                }
                // Закрываем оба потока.
                sw1.Close();
                sw2.Close();
                Console.WriteLine("Данные были отфильтрованы и записаны в 2 файла: ints.txt и all.txt. \n " +
                                  "ints.txt содержит целочисленные значения, all.txt содержит все значения. \n" +
                                  "\n Нажмите любую клавишу для продолжения работы.");
                Console.ReadKey(); 
            }

        } while (true);
    }
}

/// <summary>
/// Классы-исключения для удобства их обработки в методе ValidateFile().
/// </summary>
class IncorrectEncoding : Exception
{
    public IncorrectEncoding(): base() { }
}

class EmptyFile : Exception
{
    public EmptyFile() : base() {}
}

class EmptyRow : Exception
{
    public EmptyRow() : base() {}
}

class IncorrectData : Exception
{
    public IncorrectData() : base() {}
    public IncorrectData(string message) : base(message) { }
    public IncorrectData(string message, Exception exception) : base(message, exception) { }
}
