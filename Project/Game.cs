using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
  public class Game : IGame
  {
    bool playing = true;

    public Room CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }

    public void GetUserInput()
    {
      System.Console.WriteLine("What would you like to do?");
      var userInput = Console.ReadLine().ToLower();
      switch (userInput.ToLower())
      {
        case "west":
          Go("west");
          if (CurrentRoom.Name == "Cavern")
          {
            Lose();
          }
          break;
        case "east":
          Go("east");
          break;
        case "take key":
          TakeItem("key");
          break;
        case "use key":
          UseItem("key");
          break;
        case "look":
          Look();
          break;
        case "inventory":
          Inventory();
          break;
        case "help":
          Help();
          break;
        case "quit":
          Quit();
          break;
        default:
          System.Console.WriteLine("Invalid Selection");
          break;
      }
    }

    public void Go(string direction)
    {
      CurrentRoom = CurrentRoom.Go(direction);
      System.Console.WriteLine($"{CurrentRoom.Name}. {CurrentRoom.Description}.");
    }

    public void Help()
    {
      System.Console.WriteLine(@"
      Type east or west to move between rooms.
      Type take <ItemName> to take an item
      Type use <ItemName> to use an item.
      Type look for description of the current room.
      Type inventory to see your inventory.
      Type help to see all commands.
      Type quit to quit the game.
      ");
    }

    public void Inventory()
    {
      if (CurrentPlayer.Inventory.Count > 0)
      {

        CurrentPlayer.Inventory.ForEach(Item =>
         {
           System.Console.WriteLine($"{Item.Name} : {Item.Description}");
         });
      }
      else
      {
        System.Console.WriteLine("No Items");
      }
    }

    public void Look()
    {
      System.Console.WriteLine($"{CurrentRoom.Name}.{CurrentRoom.Description}");
    }

    public void Quit()
    {
      System.Console.WriteLine("Are you sure you want to quit? Y/N?");
      var input = Console.ReadLine().ToLower();
      if (input == "y")
      {
        playing = false;
      }
      else
      {

      }

    }

    public void Lose()
    {
      System.Console.WriteLine("Would you like to play again? Y/N?");
      var input = Console.ReadLine().ToLower();
      if (input == "y")
      {
        Setup();
        StartGame();
      }
      else
      {
        Quit();
      }
    }

    public void Win()
    {
      System.Console.WriteLine("Congratulations! You are on the road to a better life!");
      System.Console.WriteLine("Would you like to play again? Y/N?");
      var input = Console.ReadLine().ToLower();
      if (input == "y")
      {
        Setup();
        StartGame();
      }
      else
      {
        Quit();
      }
    }

    public void Setup()
    {
      Room Cavern = new Room("Cavern", "You enter the cavern and water starts to fill the cavern. Sorry you died");
      Room Cave = new Room("Cave", "You are now in a cave. It is dark and dank but you see a glimmer to one side. It's a key! You also see a cavern leading farther into the cave..");
      Room Forest = new Room("Forest", "You are surrounded by trees. You can see a cave to the west and a glade to the east");
      Room Glade = new Room("Glade", "You find yourself in the middle of a glade. To your west is a forest and to your east is a Meadow");
      Room Meadow = new Room("Meadow", "You are now in a meadow. You can see a locked chest");

      Item Key = new Item("Key", "What does it unlock?");

      Cave.Exits.Add("west", Cavern);
      Cave.Exits.Add("east", Forest);
      Forest.Exits.Add("west", Cave);
      Forest.Exits.Add("east", Glade);
      Glade.Exits.Add("west", Forest);
      Glade.Exits.Add("east", Meadow);
      Meadow.Exits.Add("west", Glade);

      Cave.AddItem(Key);

      CurrentRoom = Glade;
      CurrentPlayer = new Player();
    }

    public void StartGame()
    {
      Console.Clear();
      Setup();
      System.Console.WriteLine("Welcome to The Game!");
      Help();
      System.Console.WriteLine(CurrentRoom.Description);
      while (playing)
      {
        GetUserInput();
      }
    }

    public void TakeItem(string itemName)
    {
      var takenItem = CurrentRoom.Items.Find(Item => Item.Name.ToLower() == itemName.ToLower());
      if (takenItem != null)
      {
        CurrentRoom.Items.Remove(takenItem);
        CurrentPlayer.Inventory.Add(takenItem);
        System.Console.WriteLine("Key added to inventory.");
      }
      else
      {
        System.Console.WriteLine("Sorry nothing there.");
      }
    }

    public void UseItem(string itemName)
    {
      // System.Console.WriteLine(CurrentRoom.Name);
      var theKey = CurrentPlayer.Inventory.Find(Item => Item.Name.ToLower() == itemName.ToLower());
      // System.Console.WriteLine($"{theKey}");
      if (theKey != null)
      {
        if (CurrentRoom.Name == "Meadow")
        {
          CurrentPlayer.Inventory.Remove(theKey);
          System.Console.WriteLine("You unlocked the chest to find a Learning C# book. Lucky you!");
          Win();
        }
        else
        {
          System.Console.WriteLine("Key cannot be used here.");
        }
      }
      else
      {
        System.Console.WriteLine("You do not have a key.");
      }
    }
  }
}