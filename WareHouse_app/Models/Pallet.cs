using System;
namespace Models;
public class Pallet
{
    public Pallet(double width, double height, double length)
    {
        Id = Guid.NewGuid();
        if (width <=0 || height <= 0 || length <= 0)
            throw new ArgumentException("Неверно заданы параметры паллеты. Проверьте значения аргументов.");
        Width = width;
        Height = height;
        Length = length;
    }
    public static Pallet ConsoleCreatePallet()
    {
        Console.WriteLine("Введите длину, высоту и глубину паллеты через пробел");
        string InputString = Console.ReadLine()??
            throw new ArgumentException("Пустая строка ввода");
        string[] inputs = InputString.Split(' ');       
        if (inputs.Length != 3)
            throw new ArgumentException("Неверно заданы параметры паллеты. Проверьте количество аргументов.");
        Double tmpWidth, tmpHeight, tmpLength;
        if (Double.TryParse(inputs[0], out tmpWidth) &&
            Double.TryParse(inputs[1], out tmpHeight) &&
            Double.TryParse(inputs[2], out tmpLength))
        {
            if (tmpWidth>0 && tmpHeight>0 && tmpLength>0)
            {
                return new Pallet(tmpWidth, tmpHeight, tmpLength);
            }
            else throw new ArgumentException("Неверно заданы параметры паллеты. Проверьте значения аргументов.");
        }
        else throw new ArgumentException("Неверно заданы параметры паллеты. Проверьте тип аргументов.");
    }
    public void PrintInfo()
    {
        double sumWeight = Weight, sumArea = Width*Height*Length;
        foreach (var box in Boxes)
        {
            sumWeight += box.Weight;
            sumArea+= box.GetArea();
        }
        Console.WriteLine($"Pallet: Id:{Id}, Weight:{sumWeight}, Width:{Width}, Height:{Height}, Length:{Length}, Area:{sumArea}, ExpDate:{ExperationDate}");
        foreach (var box in Boxes)
            Console.WriteLine($"-- Id:{box.Id}, Weight:{box.Weight}, Width:{box.Width}, Height:{box.Height}, Length:{box.Length}, Area:{box.GetArea()}, ExpDate:{box.ExperationDate}");
    }
    public Guid Id;
    public double Weight { get; set; } = 30;
    public double Width { get; set; } = 0;
    public double Height { get; set; } = 0;
    public double Length { get; set; } = 0;
    public DateOnly? ExperationDate { get; set; } = null;
    public List<Box> Boxes { get; set; } = [];
}