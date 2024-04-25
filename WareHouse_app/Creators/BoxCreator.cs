using Models;
public class BoxCreator : ICreator<Box>
{
    public static Box ConsoleCreate()
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
}