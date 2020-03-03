using System;
using System.Linq;
using System.Collections.Generic;

namespace _05._Dragon_Army
{
    public class Dragon
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int Health { get; set; }
        public int Armor { get; set; }

        public Dragon(string name, int damage, int health, int armor)
        {
            this.Name = name;
            this.Damage = damage;
            this.Health = health;
            this.Armor = armor;
        }
    }
    class Program
    {
        static void Main()
        {
            Dictionary<string, List<Dragon>> dragonsArmy = new Dictionary<string, List<Dragon>>();
            int numberOfInput = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfInput; i++)
            {
                string[] info = Console.ReadLine().Split().ToArray();
                string currType = info[0];
                string currName = info[1];
                int currDamage = 0;
                int currHealth = 0;
                int currArmor = 0;
                if (info[2] != "null")
                {
                    currDamage = int.Parse(info[2]);
                }
                else
                {
                    currDamage = 45;
                }
                if (info[3] != "null")
                {
                    currHealth = int.Parse(info[3]);
                }
                else
                {
                    currHealth = 250;
                }
                if (info[4] != "null")
                {
                    currArmor = int.Parse(info[4]);
                }
                else
                {
                    currArmor = 10;
                }

                Dragon currDragon = new Dragon(currName, currDamage, currHealth, currArmor);
                if (!dragonsArmy.ContainsKey(currType))
                {
                    List<Dragon> dragonsList = new List<Dragon>();
                    dragonsList.Add(currDragon);
                    dragonsArmy.Add(currType, dragonsList);
                }
                else 
                {
                    List<Dragon> currList = dragonsArmy[currType];
                    if (!currList.Exists(x => x.Name == currName))
                    {
                        currList.Add(currDragon);
                    }
                    else
                    {
                        var thisDragon = currList.Find(x => x.Name == currName);
                        thisDragon.Health = currHealth;
                        thisDragon.Damage = currDamage;
                        thisDragon.Armor = currArmor;
                    }
                }
            }

            foreach (var type in dragonsArmy.Keys)
            {
                List<Dragon> currList = dragonsArmy[type];
                var allHelth = currList.Select(x => x.Health).ToList();
                double avrHelth = allHelth.Sum() / (double)allHelth.Count;
                var allDamage = currList.Select(x => x.Damage).ToList();
                double avrDamage = allDamage.Sum() / (double)allDamage.Count;
                var allArmor = currList.Select(x => x.Armor).ToList();
                double avrArmor = allArmor.Sum() / (double)allArmor.Count;

                Console.WriteLine($"{type}::({avrDamage:F2}/{avrHelth:F2}/{avrArmor:F2})");
                foreach (var dragon in currList.OrderBy(x => x.Name))
                {
                    Console.WriteLine($"-{dragon.Name} -> damage: {dragon.Damage}, health: {dragon.Health}, armor: {dragon.Armor}");
                }
            }
        }
    }
}
