﻿using System.Collections.ObjectModel;
using System.Text;

namespace Storage;

class Pallet : StorageUnit
{
    private const int DefaultWeight = 30;

    private readonly List<Box> boxes = [];

    public override double Weight => boxes.Select(x => x.Weight).Sum() + DefaultWeight;

    public override double Volume => Weight * Height * Depth + boxes.Select(x => x.Volume).Sum();

    public override DateOnly ExpirationDate => boxes.Select(x => x.ExpirationDate).DefaultIfEmpty().Min();

    public Pallet(int id, double width, double height, double depth, List<Box> boxes)
        : base(id, width, height, depth)
    {
        SetBoxes(boxes);
    }

    protected override void CheckSize(double size, string paramName)
    {
        if (size <= 0)
            throw new ArgumentOutOfRangeException(paramName, $"Параметр \"{ParamLocalizedNames[paramName]}\" у палеты №{ID} должен быть положительным.");
    }

    public ReadOnlyCollection<Box> GetBoxes()
    {
        return boxes.AsReadOnly();
    }

    public void SetBoxes(List<Box> boxes)
    {
        if (boxes != null)
        {
            foreach (Box box in boxes)
            {
                AddBox(box);
            }
        }
    }

    public void AddBox(Box box)
    {
        if (box != null)
        {
            if (box.Width > Width || box.Width > Depth || box.Depth > Depth || box.Depth > Width)
                throw new ArgumentOutOfRangeException(nameof(box), $"Размеры коробки №{box.ID} не должны превышать размер паллеты №{ID} (ширина и глубина).");
            boxes.Add(box);
        }
    }

    public override string ToString()
    {
        var palletInformation = new StringBuilder($"Паллета №{ID}: ширина = {Width}, высота = {Height}, глубина = {Depth}, ");
        palletInformation.Append($"вес = {Weight}, объём = {Volume}, срок годности = {ExpirationDate}, коробки:{Environment.NewLine}");

        foreach (Box box in boxes)
        {
            palletInformation.Append('\t').Append(box.ToString()).Append(Environment.NewLine);
        }

        return palletInformation.ToString();
    }
}
