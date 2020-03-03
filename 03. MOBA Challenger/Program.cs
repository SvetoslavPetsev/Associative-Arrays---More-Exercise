using System;
using System.Linq;
using System.Collections.Generic;

namespace _03._MOBA_Challenger
{
    class Program
    {
        static void Main()
        {
            Dictionary<string, Dictionary<string, int>> playerPositionSkill = new Dictionary<string, Dictionary<string, int>>();
            while (true)
            {
                string input = Console.ReadLine();
                bool match = false;
                if (input == "Season end")
                {
                    break;
                }
                string[] versus = input.Split().ToArray();
                if (versus.Contains("vs"))
                {
                    match = true;
                }
                if (!match)
                {
                    string[] info = input.Split(" -> ").ToArray();
                    string position = info[1];
                    string player = info[0];
                    int skill = int.Parse(info[2]);

                    if (!playerPositionSkill.ContainsKey(player))
                    {
                        Dictionary<string, int> positonSkill = new Dictionary<string, int>();
                        positonSkill.Add(position, skill);
                        playerPositionSkill.Add(player, positonSkill);
                    }
                    else
                    {
                        if (!playerPositionSkill[player].ContainsKey(position))
                        {
                            playerPositionSkill[player].Add(position, skill);
                        }
                        else
                        {
                            if (skill > playerPositionSkill[player][position])
                            {
                                playerPositionSkill[player][position] = skill;
                            }
                        }
                    }
                }
                else
                {
                    string player1 = versus[0];
                    string player2 = versus[2];

                    if (playerPositionSkill.ContainsKey(player1) && playerPositionSkill.ContainsKey(player2))
                    {
                        int player1TotalSkill = 0;
                        int player2TotalSkill = 0;

                        foreach (var position1 in playerPositionSkill[player1])
                        {
                            foreach (var position2 in playerPositionSkill[player2])
                            {
                                if (position1.Key == position2.Key)
                                {
                                    player1TotalSkill = playerPositionSkill[player1].Values.Sum();
                                    player2TotalSkill = playerPositionSkill[player2].Values.Sum();
                                    break;
                                }
                            }
                        }
                        if (player1TotalSkill > player2TotalSkill)
                        {
                            playerPositionSkill.Remove(player2);
                        }
                        else if (player2TotalSkill > player1TotalSkill)
                        {
                            playerPositionSkill.Remove(player1);
                        }
                    }
                }
            }
            foreach (var player in playerPositionSkill.OrderByDescending(x => x.Value.Sum(x => x.Value)).ThenBy(x => x.Key))
            {
                Console.WriteLine($"{player.Key}: {player.Value.Sum(x=>x.Value)} skill");
                foreach (var skill in player.Value.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
                {
                    Console.WriteLine($"- {skill.Key} <::> {skill.Value}");
                }
            }
        }
    }
}
