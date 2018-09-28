using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, Room> Exits { get; set; }

    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      Exits = new Dictionary<string, Room>();
      Items = new List<Item>();

    }

    public void AddItem(Item item)
    {
      Items.Add(item);
    }

    public Room Go(string direction)
    {
      if (Exits.ContainsKey(direction))
      {
        return Exits[direction];
      }
      else
      {
        return this;
      }
    }
  }
}
