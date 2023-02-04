using System.Text;

partial class Program
{
    /// <summary>
    /// Метод, проверяющий корректность файла на
    /// 1. Соответствие кодировке
    /// 2. Пустой файл, пустые строки
    /// 3. Корректность всех строк в файле
    /// 4. Корректность имени файла.
    /// </summary>
    public static bool ValidateFile(string fileName, out List<string> numbers)
    {
        Encoding enc;
        
        try
        {
            // Считываем кодировку файла.
            using (var rdr = new StreamReader(fileName, Encoding.Default, true))
            {
                enc = rdr.CurrentEncoding;
            }

            // ВНИМАНИЕ! Автор проекта пишет на маке, у меня кодировка вин-1251 просто не поддерживается. Мне разрешили пользоваться UTF-8.
            // Если кодировка файла не равна utf-8, выдаем исключение
            if (enc.BodyName != "utf-8")
            {
                throw new IncorrectEncoding();
            }

            // Считываем все данные в лист.
            numbers = File.ReadAllLines(fileName).ToList();

            // Преобразовываем все точки в запятые, удаляем лишние пробелы.
            // Это необходимо, чтобы корректно обрабатывать числа с русской локалью.
            numbers = numbers.Select(x => x.Replace('.', ',')
                .Replace(" ", String.Empty)).ToList();

            // Проверка на пустой файл.
            if (numbers.Count == 0)
            {
                throw new EmptyFile();
            }

            // Проверка на то, что какая либо строка - пустая.
            if (numbers.Any(String.IsNullOrWhiteSpace))
            {
                throw new EmptyRow();
            }

            // Проверка на то, что все строки можно преобразовать в числа.
            if (!numbers.All(x => double.TryParse(x, out double temp)))
            {
                throw new IncorrectData();
            }

            return true;
        }
        catch (IncorrectEncoding ex)
        {
            Console.WriteLine("У файла неверная кодировка. Измените кодировку и попробуйте еще раз. \n");
            numbers = null;
            return false;
        }
        catch (EmptyFile ex)
        {
            Console.WriteLine("Вы ввели пустой файл. Измените файл и попробуйте еще раз. \n");
            numbers = null;
            return false;
        }
        catch (EmptyRow ex)
        {
            Console.WriteLine("Одна из строк в представленном файле пустая. Измените файл и попробуйте еще раз. \n");
            numbers = null;
            return false;
        }
        catch (IncorrectData ex)
        {
            Console.WriteLine("Файл содержит строку, которую нельзя преобразовать в число. Измените файл и попробуйте еще раз. \n");
            numbers = null;
            return false;
        }
        catch (PathTooLongException ex)
        {
            Console.WriteLine("Имя файла слишком длинное. Измените имя и попробуйте еще раз. \n");
            numbers = null;
            return false;
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("Файл не найден. Проверьте корректность ввода и попробуйте еще раз. \n");
            numbers = null;
            return false;
        }
        catch (IOException ex)
        {
            Console.WriteLine("Ошибка при работе с файлом. Проверьте корректность ввода и попробуйте еще раз. \n");
            numbers = null;
            return false;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Ошибка при работе с файлом. Проверьте корректность ввода и попробуйте еще раз. \n");
            numbers = null;
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Возникла ошибка. Проверьте корректность ввода и попробуйте еще раз. \n");
            numbers = null;
            return false;
        }
    }
}