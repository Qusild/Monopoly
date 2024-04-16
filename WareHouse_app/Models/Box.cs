using System;
namespace Models;
public class Box
{
    public Box(Guid palletId, double width, double height, double length, double weight, DateOnly? creationDate, DateOnly? experationDate = null)
    {
        PalletId = palletId;
        Id = Guid.NewGuid();
        if (width <=0 || height <= 0 || length <= 0 || weight <= 0)
            throw new ArgumentException("Неверно заданы параметры коробки. Проверьте значения аргументов.");
        CreationDate = creationDate;
        Width = width;
        Height = height;
        Length = length;
        Weight = weight;
        if (creationDate!=null)
            {
                DateOnly tmp = creationDate ?? DateOnly.MinValue;
                try {ExperationDate = tmp.AddDays(100);}
                catch (ArgumentOutOfRangeException)
                {
                    ExperationDate = DateOnly.MaxValue;
                }

            }
        else ExperationDate = experationDate ?? DateOnly.MinValue;
    }
    public Box(){}
    public static Box ConsoleCreateBox()
    {
        Console.WriteLine("Введите айди соответсвующего паллета, длину, высоту, глубину, вес коробки через пробел");
        string InputString = Console.ReadLine()??
            throw new ArgumentException("Пустая строка ввода");
        string[] inputs = InputString.Split(' ');       
        if (inputs.Length != 5)
            throw new ArgumentException("Неверно заданы параметры коробки. Проверьте количество аргументов.");
        Console.WriteLine("Введите дату производства или срок годности коробки в формате From-DD.MM.YYYY или Upto-DD.MM.YYYY соответсвенно");
        string DateString = Console.ReadLine()??
            throw new ArgumentException(/*"Пустая строка ввода"*/InputString);
        Double tmpWidth, tmpHeight, tmpLength, tmpWeight;
        Guid tmpGuid;
        if (Guid.TryParse(inputs[0], out tmpGuid) &&
            Double.TryParse(inputs[1], out tmpWidth) &&
            Double.TryParse(inputs[2], out tmpHeight) &&
            Double.TryParse(inputs[3], out tmpLength) &&
            Double.TryParse(inputs[4], out tmpWeight))
        {
            if (tmpWidth>0 && tmpHeight>0 && tmpLength>0 && tmpWeight>0)
            {
                inputs = DateString.Split('-');
                if (inputs.Length!=2)
                    throw new ArgumentException("Неверно задана дата. Проверьте соответсвие шаблону.");
                DateOnly tmpDate;
                if (DateOnly.TryParse(inputs[1], out tmpDate))
                {
                    if (inputs[0]=="From")
                        return new Box(tmpGuid,tmpWidth, tmpHeight, tmpLength, tmpWeight, tmpDate);
                    else if (inputs[0]=="Upto")
                        return new Box(tmpGuid,tmpWidth, tmpHeight, tmpLength, tmpWeight, null, tmpDate);
                    else throw new ArgumentException("Неверно задано время. Проверьтре правильность указания характеристики даты.");
                }
                else throw new ArgumentException("Неверно задано время. Проверьте правильность ввода даты.");
            }
            else throw new ArgumentException("Неверно заданы параметры коробки. Проверьте значения аргументов.");
        }
        else throw new ArgumentException("Неверно заданы параметры коробки. Проверьте тип аргументов.");
    }
    public double GetArea() => Width*Height*Length;
    public Guid Id;
    public double Weight { get; set; } = 0;
    public double Width { get; set; } = 0;
    public double Height { get; set; } = 0;
    public double Length { get; set; } = 0;
    public DateOnly? CreationDate { get; set; } = null;
    public DateOnly ExperationDate { get; set; } = DateOnly.MinValue;

    public Guid PalletId { get; set; }
    public Pallet? Pallet { get; set; }
}