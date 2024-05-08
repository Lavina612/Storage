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
            boxes = value.Where(x => x != null).ToList();
        }
    }
    public override double Width
    {
        get
        {
            return base.Width;
        }
        set
        {
            foreach (Box box in Boxes)
            {
                if (box.Width > Width)
                    throw new InvalidDataException($"Ширина коробки №{box.ID} не должна превышать ширину паллеты №{ID}");
                else base.Width = value;
            }
        }
    }
    public override double Depth
    {
        get
        {
            return base.Depth;
        }
        set
        {
            foreach (Box box in Boxes)
            {
                if (box.Depth > Depth)
                    throw new InvalidDataException($"Глубина коробки №{box.ID} не должна превышать глубину паллеты №{ID}");
                else base.Depth = value;
            }
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
        { }
    }

    public Pallet(int id, double width, double height, double depth, List<Box> boxes) 
        : base(id, width, height, depth)
    {
        Boxes = boxes ?? [];
        Width = width;
        Depth = depth;
    }

    public override string ToString()
    {
        var palletInformation = new StringBuilder($"Паллета №{ID}: ширина = {Width}, высота = {Height}, глубина = {Depth}, " +
            $"вес = {Weight}, объём = {Volume}, срок годности = {ExpirationDate}, " +
            $"коробки:{Environment.NewLine}");
        foreach (Box box in Boxes)
        {
            palletInformation.Append('\t').Append(box.ToString()).Append(Environment.NewLine);
        }

        return palletInformation.ToString();
    }
}
