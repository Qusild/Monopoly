using Models;

public class PalletCreator : ICreator<Pallet>
{
    public static Pallet ConsoleCreate()
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
}