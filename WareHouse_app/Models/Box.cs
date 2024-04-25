using System;
namespace Models;
public class Box : AbstractThing
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

    public override (double,double) GetWeightAndArea() => (Weight,Width*Height*Length);

    public DateOnly? CreationDate { get; set; } = null;

    public DateOnly ExperationDate { get; set; } = DateOnly.MinValue;

    public Guid PalletId { get; set; }
    
    public Pallet? Pallet { get; set; }
}