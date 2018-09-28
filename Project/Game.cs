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
      playing = false;
    }

    public void Reset()
    {

    }

    public void Setup()
    {
      Room Cave = new Room("Cave", "You are now in a cave. It is dark and dank but you see a glimmer to one side. It's a key.");
      Room Forest = new Room("Forest", "You are surrounded by trees. You can see a cave to the west.");
      Room Glade = new Room("Glade", "You find yourself in the middle of a glade. To your west is a forest and to your east is a Meadow.");
      Room Meadow = new Room("Meadow", "You are now in a meadow. You can see a locked chest.");

      Item Key = new Item("Key", "What does it unlock?");

      Cave.Exits.Add("east", Forest);
      Forest.Exits.Add("west", Cave);
      Forest.Exits.Add("east", Glade);
      Glade.Exits.Add("west", Forest);
      Glade.Exits.Add("east", Meadow);
      Meadow.Exits.Add("west", Glade);

      Cave.AddItem(Key);
      // Cave.Items.Add(Key); //use this?

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
      // System.Console.WriteLine("Would you like to see the HELP guide? Y/N?");
      while (playing)
      {
        // System.Console.WriteLine("What would you like to do?");
        GetUserInput();

        // var userHelp = Console.ReadLine().ToUpper();
        // if (userHelp == "Y")
        // {
        //   Help();
        // }
        // else if (userHelp == "N")
        // {
        //   //how to continue past?
        //   continue;
        // }
        // else
        // {
        //   System.Console.WriteLine("Invalid Selection");
        // }
        // System.Console.WriteLine("Ready to Play? Y/N?");
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
          System.Console.WriteLine("You unlocked the chest to find...");
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