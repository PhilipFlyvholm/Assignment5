using System;
using System.Collections.Generic;

namespace GildedRose
{
    public class Program
    {
        public IList<Item> Items;

        public static int maxQuality = 50;
        public static int minQuality = 0;
        public static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
            {
                Items = new List<Item>
                                          {
                new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                new AgedBrie { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                new Sulfuras { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                new Sulfuras { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
                new BackstagePass
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new BackstagePass
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 10,
                    Quality = 49
                },
                new BackstagePass
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 5,
                    Quality = 49
                },
				// this conjured item does not work properly yet
				new Conjured { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
                                          }

            };
            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                for (var j = 0; j < app.Items.Count; j++)
                {
                    Console.WriteLine(app.Items[j].Name + ", " + app.Items[j].SellIn + ", " + app.Items[j].Quality);
                }
                Console.WriteLine("");
                app.UpdateQuality();
            }

        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                item.update();
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }

        public virtual void update()
        {
            SellIn--;
            if (Quality <= Program.minQuality){
                 return;
            }
            if (Quality > Program.maxQuality)
            {
                Quality = Program.maxQuality;
            }
            if (SellIn >= 0)
            {
                Quality -= 1;
            }
            else
            {
                Quality -= 2;
            }
        }
    }
    public class AgedBrie : Item
    {
        public override void update()
        {
            SellIn--;
            if (Quality < Program.minQuality){
                Quality = 0;
                 return;
            }
            if (Quality > Program.maxQuality)
            {
                Quality = Program.maxQuality;
            }

            Quality += 1;
        }
    }
    public class BackstagePass : Item
    {
        public override void update()
        {
            SellIn--;
            if (Quality <= Program.minQuality){
                 return;
            }
            if (Quality > Program.maxQuality)
            {
                Quality = Program.maxQuality;
            }
            if (SellIn > 10)
            {
                Quality += 1;
            }
            else if (SellIn > 5)
            {
                Quality += 2;
            }
            else if (SellIn > 0)
            {
                Quality += 3;
            }
            else
            {
                Quality = 0;
            }
        }
    }
    public class Sulfuras : Item
    {
        public override void update()
        {
            //Do nothing   
        }
    }

    public class Conjured : Item
    {
        public override void update()
        {
            SellIn--;
            if (Quality <= Program.minQuality){
                 return;
            }
            if (Quality > Program.maxQuality)
            {
                Quality = Program.maxQuality;
            }
            if (SellIn >= 0)
            {
                Quality -= 2;
            }
            else
            {
                Quality -= 4;
            }
            
        }
    }

}