using NultBolts;

public interface IHole
{
    bool CanPin { get; }
    void Pin(Screw screw);
    void UnPin();
}