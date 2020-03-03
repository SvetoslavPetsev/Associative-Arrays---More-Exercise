using System;
using System.Linq;
using System.Collections.Generic;

namespace _02._Judge
{
    class Program
    {
        static void Main()
        {
            Dictionary<string, Dictionary<string, int>> studentContPoints = new Dictionary<string, Dictionary<string, int>>();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "no more time")
                {
                    break;
                }
                string[] info = input.Split(" -> ").ToArray();
                string contestName = info[1];
                string student = info[0];
                int points = int.Parse(info[2]);

                if (!studentContPoints.ContainsKey(contestName))
                {
                    Dictionary<string, int> currInfo = new Dictionary<string, int>();
                    currInfo.Add(student, points);
                    studentContPoints.Add(contestName, currInfo);
                }
                else
                {
                    var currInfo = studentContPoints[contestName];
                    if (!currInfo.ContainsKey(student))
                    {
                        studentContPoints[contestName].Add(student, points);
                    }
                    else
                    {
                        if (points > studentContPoints[contestName][student])
                        {
                            studentContPoints[contestName][student] = points;
                        }
                    }
                }
            }
            foreach (var item in studentContPoints)
            {
                Console.WriteLine(item.Key + ": " + item.Value.Count + " participants");
                int counter = 0;
                foreach (var student in item.Value.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
                {
                    counter++;
                    Console.WriteLine(counter + ". " + student.Key + " <::> " + student.Value);
                }
            }
            Dictionary<string, int> finalStudents = new Dictionary<string, int>();
            foreach (var contest in studentContPoints)
            {
                foreach (var student in contest.Value)
                {
                    if (!finalStudents.ContainsKey(student.Key))
                    {
                        finalStudents.Add(student.Key, student.Value);
                    }
                    else
                    {
                        finalStudents[student.Key] += student.Value;
                    }
                }
            }
            
            Console.WriteLine("Individual standings:");

            int count = 0;
            foreach (var student in finalStudents.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                count++;
                Console.WriteLine(count + ". " + student.Key + " -> " + student.Value);
            }
        }
    }
}
