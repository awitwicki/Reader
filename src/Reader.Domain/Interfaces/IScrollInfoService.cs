namespace Reader.Domain.Interfaces;

public interface IScrollInfoService
{
    event EventHandler<float> OnScrollEnd; 
    float YPercentValue { get; }
}
