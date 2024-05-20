using Xunit;
using GildedRoseDotNet;

namespace GildedRoseNetCoreTest;
    
public class GildedRoseDotNetTests
{
    [Fact]
    public void Should_Be_DegradesNormally()
    {
        IList<Item> items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 } };
        Program app = new Program { Items = items };
        app.UpdateQuality();
        Assert.Equal(9, items[0].SellIn);
        Assert.Equal(19, items[0].Quality);
    }

    [Fact]
    public void Should_Be_AgedBrie_IncreasesInQuality()
    {
        IList<Item> items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 } };
        Program app = new Program { Items = items };
        app.UpdateQuality();
        Assert.Equal(1, items[0].SellIn);
        Assert.Equal(1, items[0].Quality);
    }

    [Fact]
    public void Should_Be_AgedBrie_MaxQualityIs50()
    {
        IList<Item> items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 2, Quality = 50 } };
        Program app = new Program { Items = items };
        app.UpdateQuality();
        Assert.Equal(1, items[0].SellIn);
        Assert.Equal(50, items[0].Quality);
    }

    [Fact]
    public void Should_Be_Sulfuras_NeverDecreasesInQualityOrSellIn()
    {
        IList<Item> items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 } };
        Program app = new Program { Items = items };
        app.UpdateQuality();
        Assert.Equal(0, items[0].SellIn);
        Assert.Equal(50, items[0].Quality);
    }

    [Fact]
    public void Should_Be_BackstagePasses_IncreaseInQuality()
    {
        IList<Item> items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 } };
        Program app = new Program { Items = items };
        app.UpdateQuality();
        Assert.Equal(14, items[0].SellIn);
        Assert.Equal(21, items[0].Quality);
    }

    [Fact]
    public void Should_Be_BackstagePasses_IncreaseInQualityBy2_When10DaysOrLess()
    {
        IList<Item> items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20 } };
        Program app = new Program { Items = items };
        app.UpdateQuality();
        Assert.Equal(9, items[0].SellIn);
        Assert.Equal(22, items[0].Quality);
    }

    [Fact]
    public void Should_Be_BackstagePasses_IncreaseInQualityBy3_When5DaysOrLess()
    {
        IList<Item> items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 20 } };
        Program app = new Program { Items = items };
        app.UpdateQuality();
        Assert.Equal(4, items[0].SellIn);
        Assert.Equal(23, items[0].Quality);
    }

    [Fact]
    public void BackstagePasses_QualityDropsToZero_AfterConcert()
    {
        IList<Item> items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20 } };
        Program app = new Program { Items = items };
        app.UpdateQuality();
        Assert.Equal(-1, items[0].SellIn);
        Assert.Equal(0, items[0].Quality);
    }

    [Fact]
    public void Should_Be_ConjuredItems_DegradeTwiceAsFast()
    {
        IList<Item> items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 } };
        Program app = new Program { Items = items };
        app.UpdateQuality();
        Assert.Equal(2, items[0].SellIn);
        Assert.Equal(4, items[0].Quality);
    }

    [Fact]
    public void Should_Be_ConjuredItems_DegradeTwiceAsFast_AfterSellInDate()
    {
        IList<Item> items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 0, Quality = 6 } };
        Program app = new Program { Items = items };
        app.UpdateQuality();
        Assert.Equal(-1, items[0].SellIn);
        Assert.Equal(2, items[0].Quality);
    }
}