namespace Storage;

class Box : StorageUnit
{
    private const int ExpirationDays = 100;

    private readonly DateOnly expirationDate;

    private DateOnly ProductionDate { get; }

    public override DateOnly ExpirationDate => expirationDate;

    public override double Volume => Width * Height * Depth;

    public Box(int id, double width, double height, double depth, double weight, DateOnly productionDate, DateOnly? expirationDate = null)
        : base(id, width, height, depth)
    {
        Weight = weight;
        ProductionDate = productionDate;

        if (expirationDate != null)
        {
            if (expirationDate < ProductionDate)
                throw new ArgumentOutOfRangeException(nameof(expirationDate), $"Параметр \"{ParamLocalizedNames[nameof(ExpirationDate)]}\" у коробки №{ID} не может быть раньше даты производства.");
            else
                this.expirationDate = (DateOnly)expirationDate;
        }
        else
            this.expirationDate = ProductionDate.AddDays(ExpirationDays);
    }

    protected override void CheckSize(double size, string paramName)
    {
        if (size <= 0)
            throw new ArgumentOutOfRangeException(paramName, $"Параметр \"{ParamLocalizedNames[paramName]}\" у коробки №{ID} должен быть положительным.");
    }

    public override string ToString()
    {
        return $"Коробка №{ID}: ширина = {Width}, высота = {Height}, глубина = {Depth}, вес = {Weight}, объём = {Volume}, срок годности = {ExpirationDate}";
    }
}
