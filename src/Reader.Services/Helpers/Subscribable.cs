namespace Reader.Services.Helpers;

public class Subscribable<T>
{
    public Subscribable() { }
    public Subscribable(T initialValue) { _value = initialValue; }

    private T? _value;

    public T? Value
    {
        get => _value;
        set
        {
            _value = value;
            OnChange?.Invoke();
        }
    }

    public event Action? OnChange;
}
