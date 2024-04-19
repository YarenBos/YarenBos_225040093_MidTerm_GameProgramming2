using System;
using System.Threading.Tasks;

namespace YarenBos_225040093_MidTerm_GameProgramming2
{
    // The Character class defines the basic properties and actions of both player and enemy.
    internal class Character
    {
        // Properties representing health points, attack power, and healing amount of the character.
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int HealAmount { get; set; }

        // Constructor to set initial values for the character's properties.
        public Character(int hp, int attack, int healAmount)
        {
            Hp = hp;
            Attack = attack;
            HealAmount = healAmount;
        }

        // Method for attacking and dealing damage.
        public virtual void AttackEnemy(Character enemy)
        {
            enemy.Hp -= Attack;
            Console.WriteLine("Enemy attacks and deals " + Attack + " damage!");
        }

        // Method for healing the character.
        public virtual void Heal()
        {
            Hp += HealAmount;
            Console.WriteLine("Enemy restores " + HealAmount + " health points!");
        }
    }

    // The Player class, derived from the Character class, represents a player character in the game.
    internal class Player : Character
    {

        // Constructor to set initial values for player's properties.
        public Player(int hp, int attack, int healAmount) : base(hp, attack, healAmount)
        {
        }

        // Override method for attacking the enemy with increased damage.
        public override void AttackEnemy(Character enemy)
        {
            enemy.Hp -= Attack * 2;
            Console.WriteLine("You attack and deal " + (Attack * 2) + " damage!");
        }

        // Override method for healing the player with increased health points.
        public override void Heal()
        {
            Hp += HealAmount * 2;
            Console.WriteLine("You restore " + (HealAmount * 2) + " health points!");
        }
    }

    internal class Game
    {
        private Player player;
        private Character enemy;
        private Random random;

        public Game()
        {
            player = new Player(40, 5, 5); // Create a player character.
            enemy = new Character(25, 7, 5); // Create an enemy character.
            random = new Random(); // Create a random number generator.
        }

        // Method to start the game and run the game loop asynchronously.
        public async Task StartGameAsync()
        {
            // Loop continues until either player's or enemy's health points reach zero.
            while (player.Hp > 0 && enemy.Hp > 0)
            {
                await PlayerTurnAsync();
                if (enemy.Hp > 0)
                    await EnemyTurnAsync(); // Enemy's turn if enemy's health points are still above zero.
            }

            // If player's health points reach zero, player loses; if enemy's health points reach zero, player wins.
            if (player.Hp > 0)
            {
                Console.WriteLine("Congratulations, you have won!");
            }
            else
            {
                Console.WriteLine("You lose!");
            }
        }

        // Method to manage player's turn asynchronously.
        private async Task PlayerTurnAsync()
        {
            Console.WriteLine("-- Player turn --");
            Console.WriteLine("Your Hp - " + player.Hp + ". Enemy Hp - " + enemy.Hp);
            Console.WriteLine("Enter 'a' to attack or 'h' to heal.");

            string choice = Console.ReadLine() ?? "";

            // Perform attack or heal based on player's choice.
            if (choice == "a")
            {
                player.AttackEnemy(enemy); // Player attacks the enemy.
            }
            else if (choice == "h")
            {
                player.Heal(); // Player heals themselves.
            }
            else
            {
                Console.WriteLine("Invalid choice!");
            }

            await Task.Delay(1000); // Delays the program execution for 1000 milliseconds (1 second).
        }

        // Method to manage enemy's turn asynchronously.
        private async Task EnemyTurnAsync()
        {
            Console.WriteLine("-- Enemy turn --");
            Console.WriteLine("Your Hp - " + player.Hp + ". Enemy Hp - " + enemy.Hp);
            int enemyChoice = random.Next(0, 2); // Randomly choose whether enemy attacks or heals.

            if (enemyChoice == 0)
            {
                enemy.AttackEnemy(player); // Enemy attacks the player.
            }
            else
            {
                enemy.Heal(); // Enemy heals themselves.
            }

            await Task.Delay(1000); // Delays the program execution for 1000 milliseconds (1 second).
        }
    }

    // Main class to initiate the program and start the game.
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Game game = new Game();
            await game.StartGameAsync(); // Start the game and run asynchronously.
        }
    }
}