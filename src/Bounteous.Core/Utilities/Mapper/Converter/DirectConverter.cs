namespace Bounteous.Core.Utilities.Mapper.Converter;

public class DirectConverter<T> : IValueConverter<T>
{
    public T Convert(object value)
        => (T)value;
}