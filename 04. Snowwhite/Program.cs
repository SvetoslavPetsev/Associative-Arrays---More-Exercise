using System;
using System.Linq;
using System.Collections.Generic;

namespace _04._Snowwhite
{
    public class Dwarf
    {
        public string Name { get; set; }
        public int Physics { get; set; }

        public Dwarf(string name, int physics)
        {
            this.Name = name;
            this.Physics = physics;
        }
    }
    class Program
    {
        static void Main()
        {
            Dictionary<string, List<Dwarf>> colorsDwarf = new Dictionary<string, List<Dwarf>>();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "Once upon a time")
                {
                    break;
                }

                string[] info = input.Split(" <:> ").ToArray();
                string dwarfName = info[0];
                string dwarfColor = info[1];
                int dwarfPfysics = int.Parse(info[2]);


                if (!colorsDwarf.ContainsKey(dwarfColor))
                {
                    List<Dwarf> listOfDwarfs = new List<Dwarf>();
                    Dwarf newDwarf = new Dwarf(dwarfName, dwarfPfysics);
                    listOfDwarfs.Add(newDwarf);
                    colorsDwarf.Add(dwarfColor, listOfDwarfs);
                    continue;
                }
                else if (!colorsDwarf[dwarfColor].Exists(x => x.Name == dwarfName))
                {
                    Dwarf newDwarf = new Dwarf(dwarfName, dwarfPfysics);
                    var listOfDwarfs = colorsDwarf[dwarfColor];
                    listOfDwarfs.Add(newDwarf);
                    continue;
                }
                else if (colorsDwarf[dwarfColor].Exists(x => x.Name == dwarfName))
                {
                    Dwarf pesho = colorsDwarf[dwarfColor].Find(x => x.Name == dwarfName);
                    if (pesho.Physics < dwarfPfysics)
                    {
                        pesho.Physics = dwarfPfysics;
                    }
                }
            }
            Dictionary<string, int> sortedDwarfs = new Dictionary<string, int>();
            foreach (var hatColor in colorsDwarf.OrderByDescending(x => x.Value.Count()))
            {
                foreach (var dwarf in hatColor.Value)
                {
                    sortedDwarfs.Add($"({hatColor.Key}) {dwarf.Name} <-> ", dwarf.Physics);
                }
            }
            foreach (var dwarf in sortedDwarfs.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"{dwarf.Key}{dwarf.Value}");
            }
        }
    }
}
