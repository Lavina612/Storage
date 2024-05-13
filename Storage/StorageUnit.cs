namespace Storage;

abstract class StorageUnit
{
    private double weight;

    public int ID { get; }

    public double Width { get; }

    protected double Height { get; }

    public double Depth { get; }

    public virtual double Weight
    {
        get
        {
            return weight;
        }
        set
        {
            CheckSize(value, nameof(Weight), "вес");
            weight = value;
        }
    }

    public abstract double Volume { get; }

    public abstract DateOnly ExpirationDate { get; }

    public StorageUnit(int id, double width, double height, double depth)
    {
        ID = id;

        CheckSize(width, nameof(Width), "ширина");
        Width = width;

        CheckSize(height, nameof(Height), "высота");
        Height = height;

        CheckSize(depth, nameof(Depth), "глубина");
        Depth = depth;
    }

    protected abstract void CheckSize(double size, string? paramName, string russianParamName);
}
