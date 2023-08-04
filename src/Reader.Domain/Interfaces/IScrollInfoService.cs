namespace Reader.Domain.Interfaces;

public interface IScrollInfoService
{
    event EventHandler<bool> OnScroll;
    event EventHandler<float> OnScrollEnd;
    float YPercentValue { get; }
}
