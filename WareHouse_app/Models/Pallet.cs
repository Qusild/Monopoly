using System;
namespace Models;
public class Pallet : AbstractThing
{
    public Pallet(double width, double height, double length)
    {
        Id = Guid.NewGuid();
        if (width <=0 || height <= 0 || length <= 0)
            throw new ArgumentException("Неверно заданы параметры паллеты. Проверьте значения аргументов.");
        Width = width;
        Height = height;
        Length = length;
        Weight = 30;
    }

    public override (double, double) GetWeightAndArea()
    {
        double sumWeight = Weight, sumArea = Width*Height*Length;
        double weightToAdd, areaToAdd;
        foreach (var box in Boxes)
        {
            (weightToAdd,  areaToAdd) = box.GetWeightAndArea();
            sumWeight+=weightToAdd;
            sumArea+=areaToAdd;
        }
        return (sumWeight, sumArea);
    }

    public void PrintInfo()
    {
        double sumWeight, sumArea;
        (sumWeight, sumArea) = GetWeightAndArea();
        Console.WriteLine($"Pallet: Id:{Id}, Weight:{sumWeight}, Width:{Width}, Height:{Height}, Length:{Length}, Area:{sumArea}, ExpDate:{ExperationDate}");
        foreach (var box in Boxes)
            Console.WriteLine($"-- Id:{box.Id}, Weight:{box.Weight}, Width:{box.Width}, Height:{box.Height}, Length:{box.Length}, Area:{box.GetWeightAndArea().Item2}, ExpDate:{box.ExperationDate}");
    }
    
    public DateOnly? ExperationDate { get; set; } = null;
    
    public List<Box> Boxes { get; set; } = [];
}