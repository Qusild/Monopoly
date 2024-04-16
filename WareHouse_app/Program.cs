using Models;

namespace WareHouse_app;

class Program
{
    static async Task Main(string[] args)
    {
        using (WareHouseDbContext db  = new WareHouseDbContext())
        {
            //Точки вместо запятых для разделения
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            PalletsRepo palletsRepo = new PalletsRepo(db);
            BoxesRepo boxesRepo= new BoxesRepo(db);
            List<Pallet> pallets;
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Возможные действия");
                    Console.WriteLine("1) Добавить паллету");
                    Console.WriteLine("2) Добавить коробку");
                    Console.WriteLine("3) Паллеты по сроку годности и весу");
                    Console.WriteLine("4) 3 паллеты с самыми долгохранящимися коробками по объему");
                    Console.WriteLine("999) Закончить ввод данных");
                    Console.Write("Выберите номер действия: ");
                    int input;
                    if (Int32.TryParse(Console.ReadLine(), out input))
                    {
                        Console.Clear();
                        switch (input)
                        {
                            case 1: 
                                Pallet tmpPallet = Pallet.ConsoleCreatePallet();
                                await palletsRepo.AddAsync(tmpPallet);
                                break;
                            case 2:
                                Box tmpBox = Box.ConsoleCreateBox();
                                await boxesRepo.AddAsync(tmpBox);
                                break;
                            case 3:
                                pallets = await palletsRepo.GetAll();
                                foreach (var pallet in pallets)
                                    pallet.PrintInfo();
                                Console.WriteLine("Нажмите Enter для продолжения работы");
                                Console.ReadLine();
                                break;
                            case 4:
                                pallets = await palletsRepo.GetPalletsWithTheLongestLifeTimeBoxes();
                                foreach (var pallet in pallets)
                                    pallet.PrintInfo();
                                Console.WriteLine("Нажмите Enter для продолжения работы");
                                Console.ReadLine();
                                break;
                            case 999:
                                return;
                            default:
                                throw new Exception("Неправильная команда");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        throw new Exception("Неправильная команда");
                    }
                }
                catch (Exception ex)
                {
                    ErrorMsg.ShowMsg(ex);
                }
            }
        }
    }
}
