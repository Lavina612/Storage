namespace Storage;

abstract class StorageUnit(int id, double width, double height, double depth)
{
    public int ID { get; } = id;
    public virtual double Width { get; set; } = width;
    protected double Height { get; set; } = height;
    public virtual double Depth { get; set; } = depth;
    public virtual double Weight { get; set; }
    public abstract double Volume { get; }
    public abstract DateOnly ExpirationDate { get; set; }
}
