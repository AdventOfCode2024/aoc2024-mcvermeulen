using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode.Day3;

internal class Solution_day3
{
    private readonly string[] input;

    public Solution_day3()
    {
        input = Utils.ReadInputFile("Day3/input.txt");
    }

    public int Part1() 
    {
        var pattern = @"mul\(\d{1,3},\d{1,3}\)";
        var sum = 0;
        foreach (var line in input) {
            foreach (Match match in Regex.Matches(line, pattern, RegexOptions.None)) {
                sum += CalculateMatch(match.Value);
            }
        }

        return sum;
    }

    public int Part2() 
    {
        var pattern = @"mul\(\d{1,3},\d{1,3}\)|don't\(\)|do\(\)";
        var sum = 0;
        var enabled = true;

        foreach (var line in input) {
            foreach (Match match in Regex.Matches(line, pattern, RegexOptions.None)) {
                if (match.Value == "do()") {
                    enabled = true; 
                } else if (match.Value == "don't()") { 
                    enabled = false;
                } else {
                    if (enabled) sum += CalculateMatch(match.Value);
                }
            }
        }

        return sum;
    }

    private int CalculateMatch(string match){
        var a = match[(match.IndexOf('(')+1)..match.IndexOf(',')];
        var b = match[(match.IndexOf(',')+1)..match.IndexOf(')')];

        return int.Parse(a) * int.Parse(b);
    }

}