namespace RefineAndFormat;

/// <summary>
/// Базовый объект-фильтр.
/// </summary>
public class PreciseFilter
{
    protected int _precize;
    public PreciseFilter(int precize)
    {
        _precize = precize;

        // Если точность меньше нуля, то выбрасываем исключение.
        if (_precize < 0)
        {
            throw new ArgumentOutOfRangeException("Точность не может быть меньше нуля");
        }
    }
    
    // Базовый класс Filter.
    internal virtual string Filter(string item)
    {
        return double.Parse(item).ToString("F3");
    }
    
}