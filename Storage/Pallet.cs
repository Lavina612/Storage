using System.Text;

namespace Storage;

class Pallet : StorageUnit
{
    private const int DefaultWeight = 30;
    private List<Box> boxes;

    public List<Box> Boxes
    {
        get
        {
            return boxes;
        }
        set
        {
            foreach (Box box in value)
            {
                if (box.Width > Width)
                    throw new ArgumentOutOfRangeException(nameof(Width), $"Ширина коробки №{box.ID} не должна превышать ширину паллеты №{ID}.");
                if (box.Depth > Depth)
                    throw new ArgumentOutOfRangeException(nameof(Depth), $"Глубина коробки №{box.ID} не должна превышать глубину паллеты №{ID}.");
            }
            boxes = value.Where(x => x != null).ToList();
        }
    }

    public override double Weight
    {
        get
        {
            return Boxes.Select(x => x.Weight).Sum() + DefaultWeight;
        }
    }

    public override double Volume
    {
        get
        {
            return Weight * Height * Depth + Boxes.Select(x => x.Volume).Sum();
        }
    }

    public override DateOnly ExpirationDate
    {
        get
        {
            return Boxes.Select(x => x.ExpirationDate).DefaultIfEmpty().Min();
        }
        set
        {
            throw new ArgumentOutOfRangeException(nameof(ExpirationDate), 
                $"Срок годности паллеты №{ID} вычисляется автоматически на основе вложенных коробок, его нельзя изменить.");
        }
    }

    public Pallet(int id, double width, double height, double depth, List<Box> boxes) 
        : base(id, width, height, depth)
    {
        Width = width;
        Depth = depth;
        Boxes = boxes ?? [];
    }

    protected override void CheckSize(double size, string? paramName, string russianParamName)
    {
        if (size <= 0)
            throw new ArgumentOutOfRangeException(paramName, $"Параметр \"{russianParamName}\" у палеты №{ID} должен быть положительным.");
    }

    public override string ToString()
    {
        var palletInformation = new StringBuilder($"Паллета №{ID}: ширина = {Width}, высота = {Height}, глубина = {Depth}, ");
        palletInformation.Append($"вес = {Weight}, объём = {Volume}, срок годности = {ExpirationDate}, коробки:{Environment.NewLine}");

        foreach (Box box in Boxes)
        {
            palletInformation.Append('\t').Append(box.ToString()).Append(Environment.NewLine);
        }

        return palletInformation.ToString();
    }
}
