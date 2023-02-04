using System.Text;

namespace RefineAndFormat;

/// <summary>
/// Объект-фильтр для целых чисел.
/// </summary>
public class IntPreciseFilter : PreciseFilter
{
    public IntPreciseFilter(int precize) : base(precize)
    {
        _precize = precize;
    }

    // Фильтр для целого числа
    internal override string Filter(string item)
    {
        StringBuilder newNumber = new StringBuilder();

        // Проверяем, что число недробное (или дробная часть у него  = 0). Если дробное, то возвращаем пустую строку
        if (item.Contains(",") && Double.Parse(item) % 1 != 0)
        {
            return String.Empty;
        }
        
        // Количество цифр в числе без знака "минус", если он присутствует.
        int lenNumber = double.Parse(item).ToString().Replace("-", String.Empty).Length;

        // Конструируем строку: добавляем минус вначале, если он есть.
        if (item.Contains('-'))
        {
            newNumber.Append('-');
        }
        
        // Циклом добавляем нули в начало.
        for (int i = lenNumber; i < _precize; i++)
        {
            newNumber.Append('0');
        }

        // Если число ,0 - то отрезаем эту вещественную часть, сначала отрезав минус.
        if (!item.Contains(','))
        {
            newNumber.Append(item.Replace("-", String.Empty));
        }
        else
        {
            item = item.Replace("-", String.Empty);
            newNumber.Append(item.Substring(0, item.IndexOf(',')));
        }
        return newNumber.ToString();
    }
}