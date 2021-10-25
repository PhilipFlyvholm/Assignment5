using Xunit;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        Program program;
        ReadOnlyCollection<Item> _items = new List<Item>{
            new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
            new AgedBrie {Name = "Aged Brie", SellIn = 2, Quality = 0},
            new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
            new Sulfuras {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
            new Sulfuras { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
            new BackstagePass { Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = 15,Quality = 20},
            new BackstagePass { Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = -5,Quality = 20},
            new BackstagePass { Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = 10,Quality = 20},
            new BackstagePass { Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = 5,Quality = 20},
            new Conjured {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6},
            new Item {Name = "Test product", SellIn = 1, Quality = 0},
            new Item {Name = "Test product 2", SellIn = 0, Quality = 20}
        }.AsReadOnly();


        public TestAssemblyTests()
        {
            program = new Program()
            {
                Items = new List<Item>{
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new AgedBrie {Name = "Aged Brie", SellIn = 2, Quality = 0},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Sulfuras {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new Sulfuras { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
                    new BackstagePass { Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = 15,Quality = 20},
                    new BackstagePass { Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = -5,Quality = 20},
                    new BackstagePass { Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = 10,Quality = 20},
                    new BackstagePass { Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = 5,Quality = 20},
                    new Conjured {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6},
                    new Item {Name = "Test product", SellIn = 1, Quality = 0},
                    new Item {Name = "Test product 2", SellIn = 0, Quality = 20}
                }
            };
            program.UpdateQuality();
        }

        [Fact]
        public void Run_Main()
        {
            Program.Main(new string[] { "" });

        }

        [Fact]
        public void never_more_than_50()
        {
            for (var i = 0; i < program.Items.Count; i++)
            {
                if (_items[i].Quality < 50)
                {
                    Assert.True(program.Items[i].Quality <= 50);
                }
            }
        }

        [Fact]
        public void never_negative()
        {
            foreach (var item in program.Items)
            {
                Assert.True(item.Quality >= 0);
            }
        }

        [Fact]
        public void sell_date_lowers()
        {
            for (var i = 0; i < program.Items.Count; i++)
            {
                if (!program.Items[i].Name.Contains("Sulfuras"))
                {
                    var expected = _items[i].SellIn - 1;
                    Assert.Equal(expected, program.Items[i].SellIn);
                }
            }
        }

        [Fact]
        public void sulfuras_does_not_lower_in_sell_date_and_quality()
        {
            for (var i = 0; i < program.Items.Count; i++)
            {
                if (program.Items[i].Name.Contains("Sulfuras"))
                {
                    var expectedSellin = _items[i].SellIn;
                    Assert.Equal(expectedSellin, program.Items[i].SellIn);
                    var expectedQuality = _items[i].Quality;
                    Assert.Equal(expectedQuality, program.Items[i].Quality);
                }
            }
        }

        [Fact]
        public void quality_decreases_unless_special_or_passed_sellin()
        {
            for (var i = 0; i < program.Items.Count; i++)
            {

                var item = program.Items[i];
                if (!item.Name.Contains("Sulfuras") && !item.Name.Contains("Backstage pass") && !item.Name.Contains("Aged Brie") && _items[i].Quality > 0 && item.SellIn >= 0)
                {
                    var expectedQuality = _items[i].Quality - 1;
                    if(item.Name.Contains("Conjured")){
                        expectedQuality--;
                    }
                    Assert.Equal(expectedQuality, item.Quality);
                }
            }
        }

        [Fact]
        public void quality_decreases_faster_unless_special_if_passed_sellin()
        {
            for (var i = 0; i < program.Items.Count; i++)
            {

                var item = program.Items[i];
                if (!item.Name.Contains("Sulfuras") && !item.Name.Contains("Backstage pass") && !item.Name.Contains("Aged Brie") && _items[i].Quality > 0 && item.SellIn < 0)
                {
                    var expectedQuality = _items[i].Quality - 2;
                    Assert.Equal(expectedQuality, item.Quality);
                }
            }
        }

        [Fact]
        public void Backstage_passes_increase()
        {
            for (var i = 0; i < program.Items.Count; i++)
            {

                var item = program.Items[i];
                if (item.Name.Contains("Backstage pass") && _items[i].Quality < 50)
                {
                    if (item.SellIn > 10)
                    {
                        //+11 days

                        var expectedQuality = _items[i].Quality + 1;
                        Assert.Equal(expectedQuality, item.Quality);
                    }
                    else if (item.SellIn > 5)
                    {
                        //6-10 days

                        var expectedQuality = _items[i].Quality + 2;
                        Assert.Equal(expectedQuality, item.Quality);
                    }
                    else if (item.SellIn > 0)
                    {
                        //0-5 days

                        var expectedQuality = _items[i].Quality + 3;
                        Assert.Equal(expectedQuality, item.Quality);
                    }
                    else
                    {

                        //-1 days
                        var expectedQuality = 0;
                        Assert.Equal(expectedQuality, item.Quality);
                    }
                }
            }
        }

        [Fact]
        public void Aged_Brei_increase()
        {
            for (var i = 0; i < program.Items.Count; i++)
            {
                var item = program.Items[i];
                if (item.Name.Contains("Aged Brie"))
                {
                    if (_items[i].Quality != 50)
                    {
                        var expectedQuality = _items[i].Quality + 1;
                        Assert.Equal(expectedQuality, item.Quality);
                    }
                    else
                    {
                        var expectedQuality = _items[i].Quality;
                        Assert.Equal(expectedQuality, item.Quality);
                    }
                }
            }
        }
    }
}