namespace WareHouse_test;

using System.Text;
using Models;
[TestClass]
public class BoxesTest
{
    [TestMethod]
    public void BoxesCreationTest1()
    {
        Box box= new Box(Guid.NewGuid(),10,10,10,10,DateOnly.FromDateTime(DateTime.Now));
    }
    [TestMethod]
    public void BoxesCreationTest2()
    {
        Box box= new Box(Guid.NewGuid(),10,10,10,10,null,DateOnly.MaxValue);
    }
    [TestMethod]
    [ExpectedException (typeof (ArgumentException))]
    public void BoxesCreationTest3()
    {
        Box box= new Box(Guid.NewGuid(),10,10,0,10,null,DateOnly.MaxValue);
    }
    [TestMethod]
    [ExpectedException (typeof (ArgumentException))]
    public void BoxesCreationTest4()
    {
        Box box= new Box(Guid.NewGuid(),10,-10,10,10,null,DateOnly.MaxValue);
    }
    [TestMethod]
    public void BoxesCreationFromConsoleTest1()
    {
        Guid tmp = Guid.NewGuid();
        StringBuilder testInput = new StringBuilder($"{tmp.ToString()} 10 10 10 10");
        testInput.AppendLine($"\nFrom-{DateOnly.MaxValue}");
        var input = new StringReader(testInput.ToString());
        Console.SetIn(input);
        Box box1 = new Box(tmp,10,10,10,10,DateOnly.MaxValue),
            box2 = BoxCreator.ConsoleCreate();
        Assert.AreEqual (box1.PalletId, box2.PalletId);
        Assert.AreEqual (box1.Length, box2.Length);
        Assert.AreEqual (box1.Height, box2.Height);
        Assert.AreEqual (box1.Width, box2.Width);
    }
    [TestMethod]
    [ExpectedException (typeof (ArgumentException))]
    public void BoxesCreationFromConsoleTest2()
    {
        Guid tmp = Guid.NewGuid();
        StringBuilder testInput = new StringBuilder($"{tmp.ToString()} 0 10 10 10");
        testInput.AppendLine($"\nFrom-{DateOnly.MaxValue}");
        var input = new StringReader(testInput.ToString());
        Console.SetIn(input);
        BoxCreator.ConsoleCreate();
    }
    [TestMethod]
    [ExpectedException (typeof (ArgumentException))]
    public void BoxesCreationFromConsoleTest3()
    {
        Guid tmp = Guid.NewGuid();
        StringBuilder testInput = new StringBuilder($"{tmp.ToString()} 10 10 10");
        testInput.AppendLine($"\nFrom-{DateOnly.MaxValue}");
        var input = new StringReader(testInput.ToString());
        Console.SetIn(input);
        BoxCreator.ConsoleCreate();
    }
    [TestMethod]
    [ExpectedException (typeof (ArgumentException))]
    public void BoxesCreationFromConsoleTest4()
    {
        Guid tmp = Guid.NewGuid();
        StringBuilder testInput = new StringBuilder($"{tmp.ToString()} 10 10 10 10");
        testInput.AppendLine($"\nFrom {DateOnly.MaxValue}");
        var input = new StringReader(testInput.ToString());
        Console.SetIn(input);
        BoxCreator.ConsoleCreate();
    }
    [TestMethod]
    [ExpectedException (typeof (ArgumentException))]
    public void BoxesCreationFromConsoleTest5()
    {
        Guid tmp = Guid.NewGuid();
        StringBuilder testInput = new StringBuilder($"{tmp.ToString()} 10 10 10 10");
        testInput.AppendLine($"\n");
        var input = new StringReader(testInput.ToString());
        Console.SetIn(input);
        BoxCreator.ConsoleCreate();
    }
    [TestMethod]
    public async Task AddBoxToPalletTest1()
    {
        using (WareHouseDbContext db = new WareHouseDbContext())
        {
            PalletsRepository palletsRepository = new PalletsRepository(db);
            BoxesRepository boxesRepository = new BoxesRepository(db);
            Pallet pallet = new Pallet(100,100,100);
            Box box1 = new Box(pallet.Id,10,10,10,10,DateOnly.FromDateTime(DateTime.Now)), 
                box2 = new Box(pallet.Id,10,10,10,10,null,DateOnly.FromDateTime(DateTime.Now));
            await palletsRepository.AddAsync(pallet);
            await boxesRepository.AddAsync(box1);
            await boxesRepository.AddAsync(box2);
            Assert.IsTrue(pallet.Boxes.Contains(box1));
            Assert.IsTrue(pallet.Boxes.Contains(box2));
        }
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task AddBoxToPalletTest2()
    {
        using (WareHouseDbContext db = new WareHouseDbContext())
        {
            PalletsRepository palletsRepository = new PalletsRepository(db);
            BoxesRepository boxesRepository = new BoxesRepository(db);
            Pallet pallet = new Pallet(100,100,100);
            Box box1 = new Box(pallet.Id,10,10,10,10,DateOnly.FromDateTime(DateTime.Now));
            await palletsRepository.AddAsync(pallet);
            await boxesRepository.AddAsync(box1);
            await boxesRepository.AddAsync(box1);
            Assert.IsTrue(pallet.Boxes.Contains(box1));
        }
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task AddBoxToPalletTest3()
    {
        using (WareHouseDbContext db = new WareHouseDbContext())
        {
            PalletsRepository palletsRepository = new PalletsRepository(db);
            BoxesRepository boxesRepository = new BoxesRepository(db);
            Pallet pallet = new Pallet(100,100,100);
            Box box1 = new Box(pallet.Id,1000,10,1000,10,DateOnly.FromDateTime(DateTime.Now));
            await palletsRepository.AddAsync(pallet);
            await boxesRepository.AddAsync(box1);
            Assert.IsTrue(pallet.Boxes.Contains(box1));
        }
    }
}