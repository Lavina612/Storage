using Storage;

class Program
{
    static void Main(string[] args)
    {
        const string BeaitufulDelimiter = "----------------------------------------------------------------------------------------------------------------";
        const int PalletCountForEx2 = 3;

        List<Pallet> pallets = GenerateData();

        foreach (var pallet in pallets)
        {
            Console.WriteLine(pallet.ToString());
        }

        Console.WriteLine(BeaitufulDelimiter);
        Console.WriteLine($"Задание №1: отсортированные по возрастанию срока годности, а затем по весу");
        Console.WriteLine(BeaitufulDelimiter);

        var palletsForEx1 = pallets.OrderBy(x => x.ExpirationDate).ThenBy(x => x.Weight);
        foreach (var pallet in palletsForEx1)
        {
            Console.WriteLine(pallet.ToString());
        }

        Console.WriteLine(BeaitufulDelimiter);
        Console.WriteLine($"Задание №2: {PalletCountForEx2} паллеты с коробками с наибольшим сроком годности, отсортированные по возрастанию объёма");
        Console.WriteLine(BeaitufulDelimiter);

        var palletsForEx2 = pallets.OrderByDescending(x => x.Boxes.Select(x => x.ExpirationDate).DefaultIfEmpty().Max())
            .ThenBy(x => x.Volume).Take(PalletCountForEx2);
        foreach (var pallet in palletsForEx2)
        {
            Console.WriteLine(pallet.ToString());
        }
    }

    private static List<Pallet> GenerateData()
    {
        List<Box> boxesForFirstPallet = [
            new(1, 20, 30, 15, 30, new DateOnly(2024, 04, 04)),
            new(2, 30, 10, 25, 20, new DateOnly(2024, 04, 10)),
            new(3, 10, 40, 30, 15, new DateOnly(2024, 04, 06)),
            new(4, 15, 15, 45, 25, new DateOnly(2024, 04, 08))
        ];
        boxesForFirstPallet.ElementAt(0).ExpirationDate = new DateOnly(2024, 04, 06);

        List<Box> boxesForSecondPallet = [
            new(5, 10, 20, 40, 10, new DateOnly(2024, 04, 02)),
            new(6, 50, 20, 20, 15, new DateOnly(2024, 04, 11)),
            new(7, 40, 10, 35, 35, new DateOnly(2024, 04, 07)),
            new(8, 25, 15, 15, 20, new DateOnly(2024, 04, 09))
        ];

        List<Box> boxesForThirdPallet = [
            new(9, 30, 20, 10, 25, new DateOnly(2024, 04, 05)),
            new(10, 25, 15, 35, 10, new DateOnly(2024, 04, 09)),
            new(11, 15, 45, 50, 25, new DateOnly(2024, 04, 04)),
            new(12, 35, 20, 45, 15, new DateOnly(2024, 04, 03))
        ];

        List<Box> boxesForFourthPallet = [
            new(13, 40, 20, 10, 30, new DateOnly(2024, 04, 03)),
            new(14, 20, 35, 25, 25, new DateOnly(2024, 04, 11)),
            new(15, 25, 15, 20, 15, new DateOnly(2024, 04, 06)),
            new(16, 35, 10, 35, 20, new DateOnly(2024, 04, 02))
        ];

        List<Pallet> pallets = [];
        try
        {
            pallets.AddRange([
                new(1, 40, 55, 45, boxesForFirstPallet),
                new(2, 50, 60, 40, boxesForSecondPallet),
                new(3, 35, 65, 55, boxesForThirdPallet),
                new(4, 55, 55, 45, boxesForFourthPallet)
            ]);
        }
        catch (InvalidDataException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Измените соответствующие размеры коробки или паллеты и попробуйте снова");
        }

        pallets.ElementAt(0).ExpirationDate = new DateOnly(2024, 05, 30);

        return pallets.Where(x => x != null).ToList();
    }
}
