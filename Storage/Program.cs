using Storage;

class Program
{
    const string BeaitufulDelimiter = "----------------------------------------------------------------------------------------------------------------";
    const int PalletCountForEx2 = 3;

    public static void Main(string[] args)
    {
        List<Pallet> pallets = [];

        try
        {
            pallets = GenerateData();
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Измените соответствующие размеры коробки или паллеты и попробуйте снова");
            return;
        }

        WritePalletsInformation(pallets);

        Exercise1(pallets);
        Exercise2(pallets);
    }

    private static List<Pallet> GenerateData()
    {
        List<Box> boxesForFirstPallet = [
            new(1, 20, 30, 15, 30, new DateOnly(2024, 04, 04)),
            new(2, 30, 10, 25, 20, new DateOnly(2024, 04, 10)),
            new(3, 10, 40, 30, 15, new DateOnly(2024, 04, 06)),
            new(4, 15, 15, 45, 25, new DateOnly(2024, 04, 08))
            ];

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

        List<Pallet> pallets = [
            new(1, 60, 55, 45, boxesForFirstPallet),
            new(2, 70, 60, 50, boxesForSecondPallet),
            new(3, 55, 65, 55, boxesForThirdPallet),
            new(4, 55, 55, 45, boxesForFourthPallet)
            ];

        return pallets.Where(x => x != null).ToList();
    }

    private static void Exercise1(List<Pallet> pallets)
    {
        Console.WriteLine(BeaitufulDelimiter);
        Console.WriteLine("Задание №1: cгрупированные по сроку годности паллеты, отсортированные по возрастанию срока годности, ");
        Console.WriteLine("а внутри каждой группы по весу");
        Console.WriteLine(BeaitufulDelimiter);

        var palletsForEx1 = pallets.Where(x => x.GetBoxes().Count > 0).GroupBy(x => x.ExpirationDate).OrderBy(x => x.Key);
        var groupCount = 1;

        foreach(var palletsGroup in palletsForEx1)
        {
            Console.WriteLine($"***Группа №{groupCount} со сроком годности {palletsGroup.Key} (количество паллет: {palletsGroup.Count()}):***");
            
            WritePalletsInformation(palletsGroup.OrderBy(x => x.Weight).ToList());

            groupCount++;
            Console.WriteLine();
        }
    }

    private static void Exercise2(List<Pallet> pallets)
    {
        Console.WriteLine(BeaitufulDelimiter);
        Console.WriteLine($"Задание №2: {PalletCountForEx2} паллеты с коробками с наибольшим сроком годности, отсортированные по возрастанию объёма");
        Console.WriteLine(BeaitufulDelimiter);

        var palletsForEx2 = pallets.Where(x => x.GetBoxes().Count > 0)
            .OrderByDescending(x => x.GetBoxes().Select(x => x.ExpirationDate).DefaultIfEmpty().Max())
            .ThenBy(x => x.Volume).Take(PalletCountForEx2).ToList();
        WritePalletsInformation(palletsForEx2);
    }

    private static void WritePalletsInformation(List<Pallet> pallets)
    {
        foreach (var pallet in pallets)
        {
            Console.WriteLine(pallet.ToString());
        }
    }
}
