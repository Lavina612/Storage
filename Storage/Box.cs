namespace Storage;

class Box : StorageUnit
{
    private DateOnly ProducationDate { get; set; }
    private DateOnly expirationDate;

    public override DateOnly ExpirationDate
    {
        get
        {
            return expirationDate == DateOnly.MinValue ? ProducationDate.AddDays(100) : expirationDate;
        }
        set
        {
            if (value < ProducationDate)
                throw new ArgumentOutOfRangeException(nameof(ExpirationDate), $"Срок годности коробки №{ID} не может быть меньше даты производства.");
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

    public Box(int id, double width, double height, double depth, double weight, DateOnly productionDate)
        : base(id, width, height, depth)
    {
        Weight = weight;
        ProducationDate = productionDate;
    }

    protected override void CheckSize(double size, string? paramName, string russianParamName)
    {
        if (size <= 0)
            throw new ArgumentOutOfRangeException(paramName, $"Параметр \"{russianParamName}\" у коробки №{ID} должен быть положительным.");
    }

    public override string ToString()
    {
        return $"Коробка №{ID}: ширина = {Width}, высота = {Height}, глубина = {Depth}, " +
            $"вес = {Weight}, объём = {Volume}, срок годности = {ExpirationDate}";
    }
}
