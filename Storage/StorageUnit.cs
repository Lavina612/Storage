namespace Storage;

abstract class StorageUnit
{
    private double width;
    private double height;
    private double depth;
    private double weight;

    public int ID { get; }

    public double Width
    {
        get
        {
            return width;
        }
        set
        {
            CheckSize(value, nameof(Width), "ширина");
            width = value;
        }
    }

    protected double Height
    {
        get
        {
            return height;
        }
        set
        {
            CheckSize(value, nameof(Height), "высота");
            height = value;
        }
    }

    public double Depth
    {
        get
        {
            return depth;
        }
        set
        {
            CheckSize(value, nameof(Depth), "глубина");
            depth = value;
        }
    }

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

    public abstract DateOnly ExpirationDate { get; set; }

    public StorageUnit(int id, double width, double height, double depth)
    {
        ID = id;
        Width = width;
        Height = height;
        Depth = depth;
    }

    protected abstract void CheckSize(double size, string? paramName, string russianParamName);
}
