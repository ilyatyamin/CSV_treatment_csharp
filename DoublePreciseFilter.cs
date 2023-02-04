using System.Text;

namespace RefineAndFormat;

/// <summary>
/// Объект-фильтр для вещественных чисел.
/// </summary>
public class DoublePreciseFilter : PreciseFilter
{
    public DoublePreciseFilter(int precize)  : base(precize)
    {
        _precize = precize;
    }

    // Метод-фильтр для работы с числом
    internal override string Filter(string item)
    {
        // Проверяем, что это вообще число (хоть какое-то - дробное или нет)
        if (!Double.TryParse(item, out double temp))
        {
            throw new IncorrectData("Упс. Представленная строка не является числом.");
        }
        
        return double.Parse(item).ToString($"F{_precize}");
    }
}