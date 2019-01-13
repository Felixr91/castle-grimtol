using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }

    public bool playing = true;

    public void GetUserInput()
    {
      string input = System.Console.ReadLine().ToLower();
      string[] arrChoice = input.Split(" ");
      string command = arrChoice[0];
      string option = "";
      if (arrChoice.Length > 1)
      {
        option = arrChoice[1];
      }
      switch (command)
      {
        case "quit":
          Quit();
          break;
        case "help":
          Help();
          break;
        case "look":
          Look();
          break;
        case "go":
          Go(option);
          break;
        case "inventory":
          Inventory();
          break;
        case "take":
          TakeItem(option);
          break;
        case "use":
          UseItem(option);
          break;
        case "speak":
          Speak();
          break;
        default:
          System.Console.WriteLine("????? A HAHA HA HA! You're so funny!");
          break;
      }
    }

    public void Speak()
    {
      if (CurrentRoom.Name.ToString() == "The Clown with 3 wheels")
      {
        System.Console.WriteLine("The clown stops riding his bicycle. He farts. A green gas fills the room and kills you.");
        Quit();
      }
      else
      {
        System.Console.Write("Nothing to say!");
      }
    }

    public void Go(string direction)
    {
      System.Console.Clear();
      if (direction == "north" && CurrentRoom.Name.ToString() == "Waiting Room" && (CurrentPlayer.Inventory[0].ToString() != "key" || CurrentPlayer.Inventory[1].ToString() != "key"))
      {
        System.Console.WriteLine("You need a key to go in there!");
      }
      else if (CurrentRoom.Exits.ContainsKey(direction))
      {
        CurrentRoom = CurrentRoom.Exits[direction];
      }
      else
      {
        System.Console.WriteLine("You can't go that way, silly silly silly boy!!!!!!! Ha HA HA!");
      }
      System.Console.WriteLine(CurrentRoom.Name.ToString());
    }

    public void Help()
    {
      System.Console.WriteLine("Escape 'Fun House' to win!");
      System.Console.WriteLine("When the game begins you find yourself in a waiting room. With a hysterical clown by your side.");

      System.Console.WriteLine("**** COMMANDS LIST ****");
      System.Console.WriteLine("To do something in 'Fun House' you must type a command followed by a space and an option");
      System.Console.WriteLine("For example: 'go west', 'take sandwich', or 'use hammer");

      System.Console.WriteLine("**** NAVIGATION ****");
      System.Console.WriteLine("Type 'look' to look around the room you're in");

      System.Console.WriteLine("**** SPEAKING ****");
      System.Console.WriteLine("Type 'speak' to speak to the person in the current room");

      System.Console.WriteLine("////////////// BACK TO GAME ////////////////");
      System.Console.WriteLine("Hit ENTER to continue the game.");
      GetUserInput();
    }

    public void Inventory()
    {
      CurrentPlayer.Inventory.ForEach(i =>
      {
        System.Console.WriteLine("You currently have: ");
        System.Console.WriteLine($"{i.Name}");
      });
    }

    public void Look()
    {
      System.Console.WriteLine(CurrentRoom.Description.ToString());
      GetUserInput();
    }

    public void Quit()
    {
      System.Console.WriteLine("RIP");
      // System.Console.WriteLine("You have died. Would you like to try again?");
      playing = false;
    }

    public void Reset()
    {
      Setup();
    }

    public void Setup()
    {
      //create rooms
      Room waitingRoom = new Room("Waiting Room", "HA HA HAHA HA HA! What will you do? On the north side of us you see a locked door... hear that? There's a sad little clown crying back there. On the eastern side there's another door... hoo hoo! I here a little bell ringing! To the south there's another door... Its awfully quiet back there. HEE HEE! To the west I can hear water... sounds like someone's taking a shower!");
      Room northRoom = new Room("The Sad Clown", "Inside you see a small clown sitting in a chair. To the right is a very large hammer with a star on its face and a very large hammer arcade machine. ");
      // Do you speak to the clown or do you grab the hammer?
      Room eastRoom = new Room("The Clown with 3 wheels", "Inside theres an elderly clown riding a tricycle. His glare stays on the floor in front of him. Every time he completes a circle he rings the bell. ");
      // Do you speak to the clown or do you exit the room?
      Room southRoom = new Room("The Dark Room", "You step inside. The door slams from behind you and you are left alone. From the darkness you hear a voice ask 'Do you want to dance with me?'");
      // What do you respond?
      Room westRoom = new Room("The Shower Room", "Inside you see a shower is running. The person inside the shower pulls the curtain... a tall clown stands in the shower. The paint on his face has melted onto his body. He stares at you. He's wearing swimming trunks with a banana on them. He's also eating a banana. He drops the banana into the tub. ");
      // What do you want to do?

      //create items
      Item banana = new Item("banana", "A token of your guides love. A wonderfully ripe and sweet BANANA!");
      Item hammer = new Item("hammer", "large hammer with a star on its face");
      Item key = new Item("key", "Hey, remember that clown crying in the northern room? Maybe this key can help him out!");

      CurrentRoom = waitingRoom;

      waitingRoom.Items.Add(banana);
      northRoom.Items.Add(hammer);
      westRoom.Items.Add(key);

      waitingRoom.Exits.Add("north", northRoom);
      waitingRoom.Exits.Add("east", eastRoom);
      waitingRoom.Exits.Add("south", southRoom);
      waitingRoom.Exits.Add("west", westRoom);

      northRoom.Exits.Add("south", waitingRoom);
      eastRoom.Exits.Add("west", waitingRoom);
      southRoom.Exits.Add("north", waitingRoom);
      westRoom.Exits.Add("east", waitingRoom);

      System.Console.WriteLine("type 'help' for directions. Hit enter to continue!");
      GetUserInput();

      StartGame();
    }

    public void StartGame()
    {
      System.Console.Clear();
      System.Console.WriteLine($"Would you like to dance with me {CurrentPlayer.PlayerName}?");
      string danceresponse = System.Console.ReadLine().ToLower();
      if (danceresponse == "no")
      {
        System.Console.WriteLine("I hate you! DIE!!!!!!!!!");
        System.Console.WriteLine("You're dead. Hit ENTER to RESTART!");
        System.Console.ReadLine();
        Reset();
      }
      else if (danceresponse == "yes")
      {
        System.Console.WriteLine("HAHAHAAAAAA HA HE he he! hey! I can't dance right now, but here you go!");
        System.Console.WriteLine("** You are offered a banana **");
        GetUserInput();
        // Look();
        // while loop
        while (playing == true)
        {
          GetUserInput();
        }
      }
      else
      {
        StartGame();
      }
    }

    public void TakeItem(string itemName)
    {

      Item item = CurrentRoom.Items.Find(i =>
      {
        return i.Name.ToLower() == itemName;
      });

      if (item != null)
      {
        CurrentRoom.Items.Remove(item);
        CurrentPlayer.Inventory.Add(item);
        System.Console.WriteLine($"Oh you so money! You have a {item.Name}!");
      }
      else
      {
        System.Console.WriteLine("Nothing to take!");
      }
    }

    public void UseItem(string itemName)
    {
      Item usedItem = CurrentPlayer.Inventory.Find(i =>
      {
        return i.Name.ToLower() == itemName;
      });

      if (itemName == "banana" && (CurrentPlayer.Inventory[0].Name.ToString() == "banana") && CurrentRoom.Name.ToString() == "The Shower Room")
      {
        CurrentPlayer.Inventory.Remove(usedItem);
        System.Console.WriteLine("Thank you so so... so much, the shower clown says. Here... ");
        System.Console.WriteLine("** You are offered a key!**");
        GetUserInput();
        CurrentRoom.Description = "The Clown resumes his shower and joyfully chews on his fresh ripe banana! What will you do now?";
        Look();
      }

      if (itemName == "key" && (CurrentPlayer.Inventory[0].Name.ToString() == "key") && CurrentRoom.Name.ToString() == "Waiting Room")
      {
        System.Console.WriteLine("You unlock the door!");
        CurrentRoom = CurrentRoom.Exits["north"];
        Look();
        GetUserInput();
      }
      if (itemName == "hammer" && (CurrentPlayer.Inventory[0].Name.ToString() == "hammer") && CurrentRoom.Name.ToString() == "The Sad Clown")
      {
        System.Console.WriteLine("Nice hit!!!!");
        System.Console.WriteLine("The small clown laughs.  He pulls a string. A rope comes down.");
        CurrentPlayer.Inventory.Remove(usedItem);
        GetUserInput();
      }
      if (itemName == "rope" && CurrentRoom.Name.ToString() == "The Sad Clown")
      {
        System.Console.WriteLine("Nice hit!!!!");
        System.Console.WriteLine("The small clown laughs.  He pulls a string. A rope comes down.");
        CurrentPlayer.Inventory.Remove(usedItem);
        System.Console.WriteLine("You've escaped the fun house! YOU WIN!");
        System.Console.WriteLine("type 'quit' to EXIT");
        GetUserInput();
      }
    }

    //constructor
    public GameService()
    {

    }
  }
}