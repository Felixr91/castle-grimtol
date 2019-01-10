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
      Console.WriteLine("What is your name?");
      Console.WriteLine("HAa ha ha! Hello and Welcome to 'Fun House'!!!!! HA HA!");
      Console.Write("Whats your name?! HA HA HA HA HA!!!!!!");
      string userName = Console.ReadLine();
      Player player = new Player(userName);
      GameService gameService = new GameService();
      gameService.CurrentPlayer = player;
      gameService.Setup();

    }





  }
}
