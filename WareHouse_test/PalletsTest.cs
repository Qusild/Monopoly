namespace WareHouse_test;

using System.Text;
using Models;
[TestClass]
public class PalletsTest
{
    [TestMethod]
    public void PalletsCreationTest1()
    {
        Pallet pallet = new Pallet(10,10,10);
    }
    [TestMethod]
    [ExpectedException (typeof (ArgumentException))]
    public void PalletsCreationTest2()
    {
        Pallet pallet = new Pallet(10,0,10);
    }
    [TestMethod]
    [ExpectedException (typeof (ArgumentException))]
    public void PalletsCreationTest3()
    {
        Pallet pallet = new Pallet(-10,10,10);
    }
    [TestMethod]
    public void PalletsCreationFromConsoleTest1()
    {
        StringBuilder testInput = new StringBuilder("10 10 10");
        var input = new StringReader(testInput.ToString());
        Console.SetIn(input);
        Pallet pallet1 = new Pallet(10,10,10),
               pallet2 = PalletCreator.ConsoleCreate();
        Assert.AreEqual (pallet1.Width, pallet2.Width);
        Assert.AreEqual (pallet1.Length, pallet2.Length);
        Assert.AreEqual (pallet1.Height, pallet2.Height);
    }
    [TestMethod]
    [ExpectedException (typeof (ArgumentException))]
    public void PalletsCreationFromConsoleTest2()
    {
        StringBuilder testInput = new StringBuilder("10 0 10");
        var input = new StringReader(testInput.ToString());
        Console.SetIn(input);
        Pallet pallet1 = new Pallet(10,10,10),
               pallet2 = PalletCreator.ConsoleCreate();
        Assert.AreEqual (pallet1.Width, pallet2.Width);
        Assert.AreEqual (pallet1.Length, pallet2.Length);
        Assert.AreEqual (pallet1.Height, pallet2.Height);
    }
    [TestMethod]
    [ExpectedException (typeof (ArgumentException))]
    public void PalletsCreationFromConsoleTest3()
    {
        StringBuilder testInput = new StringBuilder("10  10");
        var input = new StringReader(testInput.ToString());
        Console.SetIn(input);
        Pallet pallet1 = new Pallet(10,10,10),
               pallet2 = PalletCreator.ConsoleCreate();
        Assert.AreEqual (pallet1.Width, pallet2.Width);
        Assert.AreEqual (pallet1.Length, pallet2.Length);
        Assert.AreEqual (pallet1.Height, pallet2.Height);
    }
    [TestMethod]
    [ExpectedException (typeof (ArgumentException))]
    public void PalletsCreationFromConsoleTest4()
    {
        StringBuilder testInput = new StringBuilder("10 10");
        var input = new StringReader(testInput.ToString());
        Console.SetIn(input);
        Pallet pallet1 = new Pallet(10,10,10),
               pallet2 = PalletCreator.ConsoleCreate();
        Assert.AreEqual (pallet1.Width, pallet2.Width);
        Assert.AreEqual (pallet1.Length, pallet2.Length);
        Assert.AreEqual (pallet1.Height, pallet2.Height);
    }
    [TestMethod]
    public async Task AddPalletToDbTest1()
    {
        using (WareHouseDbContext db = new WareHouseDbContext())
        {
            PalletsRepository palletsRepository = new PalletsRepository(db);
            Pallet pallet = new Pallet(100,100,100);
            await palletsRepository.AddAsync(pallet);
            List<Pallet> pallets = await palletsRepository.GetAll();
            Assert.IsTrue(pallets.Contains(pallet));
        }
    }
    [TestMethod]
    [ExpectedException (typeof (ArgumentException))]
    public async Task AddPalletToDbTest2()
    {
        using (WareHouseDbContext db = new WareHouseDbContext())
        {
            PalletsRepository palletsRepository = new PalletsRepository(db);
            Pallet pallet = new Pallet(100,100,100);
            await palletsRepository.AddAsync(pallet);
            await palletsRepository.AddAsync(pallet);
            List<Pallet> pallets = await palletsRepository.GetAll();
            Assert.IsTrue(pallets.Contains(pallet));
        }
    }
    [TestMethod]
    public async Task GetThreePalletsTest1()
    {
        using (WareHouseDbContext db = new WareHouseDbContext())
        {
            PalletsRepository palletsRepo = new PalletsRepository(db);
            BoxesRepository boxesRepo = new BoxesRepository(db);
            Pallet pallet1 = new Pallet(100,100,100),
                pallet2 = new Pallet(10,10,10),
                pallet3 = new Pallet(100,100,100),
                pallet4 = new Pallet(1000,1000,1000);
            Box box1 = new Box(pallet1.Id,10,10,10,10,DateOnly.FromDateTime(DateTime.Now).AddYears(1000)),
                box2 = new Box(pallet2.Id,10,10,10,10,DateOnly.FromDateTime(DateTime.Now).AddYears(1010)),
                box3 = new Box(pallet3.Id,100,10000,100,10,DateOnly.FromDateTime(DateTime.Now).AddYears(1001)),
                box4 = new Box(pallet4.Id,1000,1000,1000,1000,DateOnly.FromDateTime(DateTime.Now));
            await palletsRepo.AddAsync(pallet1);
            await boxesRepo.AddAsync(box1);
            await palletsRepo.AddAsync(pallet2);
            await boxesRepo.AddAsync(box2);
            await palletsRepo.AddAsync(pallet3);
            await boxesRepo.AddAsync(box3);
            await palletsRepo.AddAsync(pallet4);
            await boxesRepo.AddAsync(box4);
            List<Pallet> ans = await palletsRepo.GetPalletsWithTheLongestLifeTimeBoxes();
            Assert.IsTrue(ans[1].Id == pallet1.Id);
            Assert.IsTrue(ans[0].Id == pallet2.Id);
            Assert.IsTrue(ans[2].Id == pallet3.Id);
            Assert.IsFalse(ans.Contains(pallet4));
        }
    }
    [TestMethod]
    public async Task GetAllPalletsByExpDateAndWeightTest1()
    {
        using (WareHouseDbContext db = new WareHouseDbContext())
        {
            PalletsRepository palletsRepository = new PalletsRepository(db);
            BoxesRepository boxesRepository = new BoxesRepository(db);
            Pallet pallet1 = new Pallet(10,10,10),
                pallet2 = new Pallet(10,10,10),
                pallet3 = new Pallet(10,10,10),
                pallet4 = new Pallet(10,10,10);
            Box box1 = new Box(pallet1.Id,10,10,10,100,DateOnly.FromDateTime(DateTime.Now).AddYears(2000)),
                box2 = new Box(pallet2.Id,10,10,10,10000,DateOnly.FromDateTime(DateTime.Now).AddYears(2010)),
                box3 = new Box(pallet3.Id,10,10000,10,1000,DateOnly.FromDateTime(DateTime.Now).AddYears(2001)),
                box4 = new Box(pallet4.Id,10,1000,10,10001,DateOnly.FromDateTime(DateTime.Now).AddYears(2010));
            await palletsRepository.AddAsync(pallet1);
            await boxesRepository.AddAsync(box1);
            await palletsRepository.AddAsync(pallet2);
            await boxesRepository.AddAsync(box2);
            await palletsRepository.AddAsync(pallet3);
            await boxesRepository.AddAsync(box3);
            await palletsRepository.AddAsync(pallet4);
            await boxesRepository.AddAsync(box4);
            List<Pallet> ans = await palletsRepository.GetAll();
            Assert.IsTrue(ans[0].Id == pallet1.Id);
            Assert.IsTrue(ans[2].Id == pallet2.Id);
            Assert.IsTrue(ans[1].Id == pallet3.Id);
            Assert.IsTrue(ans[3].Id == pallet4.Id);
        }
    }
}