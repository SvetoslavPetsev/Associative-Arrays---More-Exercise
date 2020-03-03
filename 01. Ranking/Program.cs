using System;
using System.Linq;
using System.Collections.Generic;

namespace _01._Ranking
{
    class Program
    {
        static void Main()
        {
            Dictionary<string, string> contestPassword = new Dictionary<string, string>();
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "end of contests")
                {
                    break;
                }
                string[] info = input.Split(":").ToArray();
                contestPassword.Add(info[0], info[1]);
            }
           SortedDictionary <string, Dictionary<string, int>> userPoints = new SortedDictionary<string, Dictionary<string, int>>();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "end of submissions")
                {
                    break;
                }
                string[] info = input.Split("=>").ToArray();
                string contest = info[0];
                string password = info[1];
                string user = info[2];
                int points = int.Parse(info[3]);

                //is valid
                if (contestPassword.ContainsKey(contest))
                {
                    if (contestPassword[contest] == password)
                    {
                        if (!userPoints.ContainsKey(user))
                        {
                            Dictionary<string, int> currInfo = new Dictionary<string, int>();
                            currInfo.Add(contest, points);
                            userPoints.Add(user, currInfo);
                        }
                        else
                        {
                            var currInfo = userPoints[user];
                            if (!currInfo.ContainsKey(contest))
                            {
                                Dictionary<string, int> newContest = new Dictionary<string, int>();
                                newContest.Add(contest, points);
                                userPoints[user].Add(contest, points);
                            }
                            else
                            {
                                if (points > userPoints[user][contest])
                                {
                                    userPoints[user][contest] = points;
                                }
                            }
                        }
                    }
                }
            }
            int bestPoints = 0;
            string bestStudent = "";
            foreach (var student in userPoints)
            {
                int sumPoints = 0;
                foreach (var info in student.Value)
                {
                    sumPoints += info.Value;
                    if (sumPoints > bestPoints)
                    {
                        bestPoints = sumPoints;
                        bestStudent = student.Key;
                    }
                }
            }
            Console.WriteLine($"Best candidate is {bestStudent} with total {bestPoints} points.");
            Console.WriteLine("Ranking:");
            foreach (var student in userPoints)
            {
                Console.WriteLine(student.Key);

                foreach (var info in student.Value.OrderByDescending(x => x.Value))
                {
                    
                    Console.WriteLine("#  " + info.Key + " -> " + info.Value);
                }
            }
        }
    }
}
