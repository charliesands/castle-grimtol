using System.Collections.Generic;

namespace CastleGrimtol.Project
{
  public class Player : IPlayer
  {
    public string PlayerName { get; set; }
    public List<Item> Inventory { get; set; } = new List<Item>();


  }
}