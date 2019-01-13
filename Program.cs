using System;
using CastleGrimtol.Project;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.Clear();
      Console.WriteLine("HAa ha ha! Hello and Welcome to 'Fun House'!!!!! HA HA!");
      bool validName = false;
      string user = "";

      while (!validName)
      {
        Console.WriteLine("What is your name?");
        string userName = Console.ReadLine().Trim();
        if (userName.Length < 1)
        {
          Console.WriteLine("You need to tell me your name!");
        }
        else
        {
          user = userName;
          validName = true;
        }
      }
      Player player = new Player(user);
      GameService gameService = new GameService();
      gameService.CurrentPlayer = player;
      gameService.Setup();

    }





  }
}
