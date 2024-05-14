namespace Storage;

abstract class StorageUnit
{
    private double weight;

    protected static Dictionary<string, string> ParamLocalizedNames = new()
    {
        { nameof(Width), "ширина"},
        { nameof(Height), "высота"},
        { nameof(Depth), "глубина"},
        { nameof(Weight), "вес"},
        { nameof(ExpirationDate), "срок годности" }
    };

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
            CheckSize(value, nameof(Weight));
            weight = value;
        }
    }

    public abstract double Volume { get; }

    public abstract DateOnly ExpirationDate { get; }

    public StorageUnit(int id, double width, double height, double depth)
    {
        ID = id;

        CheckSize(width, nameof(Width));
        Width = width;

        CheckSize(height, nameof(Height));
        Height = height;

        CheckSize(depth, nameof(Depth));
        Depth = depth;
    }

    protected abstract void CheckSize(double size, string paramName);
}
