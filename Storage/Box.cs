namespace Storage;

class Box(int id, double width, double height, double depth, double weight, DateOnly productionDate) 
    : StorageUnit(id, width, height, depth)
{
    public override double Weight { get; set; } = weight;
    private DateOnly ProducationDate { get; set; } = productionDate;
    private DateOnly expirationDate;
    public override DateOnly ExpirationDate
    {
        get
        {
            return expirationDate > ProducationDate ? expirationDate : ProducationDate.AddDays(100);
        }
        set
        {
            expirationDate = value;
        }
    }

    public override double Volume
    {
        get
        {
            return Width * Height * Depth;
        }
    }

    public override string ToString()
    {
        return $"Коробка №{ID}: ширина = {Width}, высота = {Height}, глубина = {Depth}, " +
            $"вес = {Weight}, объём = {Volume}, срок годности = {ExpirationDate}";
    }
}
