using System.Collections;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace GildedRoseDotNet
{
    public class Program
    {
        public IList<Item> Items;
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
                          {
                              Items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }

                          };

            foreach(var item in app.Items)
            {
                System.Console.WriteLine("Name: {0}, SellIn: {1}, Quality: {2}", item.Name, item.SellIn, item.Quality);
            }

            app.UpdateQuality();
            System.Console.WriteLine("After Update ");  

            foreach(var item in app.Items)
            {
                System.Console.WriteLine("Name: {0}, SellIn: {1}, Quality: {2}", item.Name, item.SellIn, item.Quality);
            }

            System.Console.ReadKey();

        }

        public void UpdateQuality()
        {
            foreach(Item item in Items)
            {
                var strategy = GildedRoseStratetyFactory.GetUpdateStrategy(item);
                strategy.Update(item);
                FixQualityLimits(item);
            }
        }

        private void FixQualityLimits(Item item)
        {
            if (item.Quality < 0)
            {
                item.Quality = 0;
            }
            if (item.Quality > 50)
            {
                item.Quality = 50;
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

    public static class GildedRoseStratetyFactory
    {
        public static IUpdateStrategy GetUpdateStrategy(Item item)
        {
            if (item.Name == "Aged Brie")
            {
                return new AgedBrieUpdateStrategy();
            }
            if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
            {
                return new BackstagePassesUpdateStrategy();
            }
            if (item.Name == "Sulfuras, Hand of Ragnaros")
            {
                return new SulfurasUpdateStrategy();
            }
            if (item.Name == "Conjured Mana Cake")
            {
                return new ConjuredUpdateStrategy();
            }
            return new DefaultUpdateStrategy();
        }
    }

    public interface IUpdateStrategy
    {
        void Update(Item item);
    }

    public class DefaultUpdateStrategy : IUpdateStrategy
    {
        public void Update(Item item)
        {
            item.SellIn--;
            item.Quality--;

            if(item.SellIn < 0)
            {
                item.Quality--;
            }
        }
    }

    public class AgedBrieUpdateStrategy : IUpdateStrategy
    {
        public void Update(Item item)
        {
            item.SellIn--;
            item.Quality++;

            if (item.SellIn < 0)
            {
                item.Quality++;
            }
        }
    }

    public class SulfurasUpdateStrategy : IUpdateStrategy
    {
        public void Update(Item item)
        {
            // No rules for Sulfuras
            // we can use Liskov Substitution Principle here but it is not necessary
            // as we are not going to change the behavior of Sulfuras
            // if the requirements change we can change this class and implement
            // liskov substitution principle
        }
    }

    public class BackstagePassesUpdateStrategy : IUpdateStrategy
    {
        public void Update(Item item)
        {
            item.SellIn--;
            item.Quality++;

            if (item.SellIn <= 10)
            {
                item.Quality++;
            }

            if (item.SellIn <= 5)
            {
                item.Quality++;
            }

            if (item.SellIn < 0)
            {
                item.Quality = 0;
            }
        }
    }

    public class ConjuredUpdateStrategy : IUpdateStrategy
    {
        public void Update(Item item)
        {
            item.SellIn--;
            item.Quality -= 2;

            if (item.SellIn < 0)
            {
                item.Quality -= 2;
            }
        }
    }
}