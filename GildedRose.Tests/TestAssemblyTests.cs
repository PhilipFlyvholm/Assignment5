using Xunit;
using GildedRose.Console;
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
            new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
            new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
            new Item{ Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = 15,Quality = 20},
            new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6},
            new Item {Name = "Test product", SellIn = 1, Quality = 0}
        }.AsReadOnly();
        

        public TestAssemblyTests(){
            program = new Program(){
                Items = new List<Item>(_items.Count)
            };
            foreach (var item in _items){
                program.Items.Add(new Item{Name = item.Name, SellIn = item.SellIn, Quality = item.Quality});
            }
            program.UpdateQuality();
            System.Console.WriteLine("ET ELLER ANDET");
        }
        
        [Fact]
        public void never_more_than_50()
        {
            for (var i = 0; i < program.Items.Count; i++)
            {
                if(_items[i].Quality < 50){
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
                if(_items[i].SellIn > 0){ 
                    var expected = _items[i].SellIn - 1;
                    Assert.Equal(expected, program.Items[i].SellIn);
                }
            }
        }
    }
}