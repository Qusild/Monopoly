public class ErrorMsg
{
    public static void ShowMsg(Exception ex)
    {
        Console.Clear();
        Console.WriteLine("ОШИБКА:" + ex.Message);
        Console.WriteLine("НАЖМИТЕ ENTER ДЛЯ ПРОДОЛЖЕНИЯ");
        Console.ReadLine();
        Console.Clear();
    }
}