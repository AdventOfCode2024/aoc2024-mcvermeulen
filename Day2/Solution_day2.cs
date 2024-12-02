using AdventOfCode.Common;

namespace AdventOfCode.Day2;

internal class Solution_day2
{
    private readonly string[] input;
    private readonly int max_difference = 3;

    public Solution_day2()
    {
        input = Utils.ReadInputFile("Day2/input.txt");
    }

    public int Part1()
    {
        var safe_count = 0;

        foreach (var line in input)
        {
            var levels = Array.ConvertAll(line.Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);

            safe_count += ParseReport(levels);
        }

        return safe_count;
    }

    public int Part2()
    {
        var safe_count = 0;

        foreach (var line in input)
        {
            var levels = Array.ConvertAll(line.Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);

            safe_count += ParseReport(levels) == 1 ? 1 : ProblemDampener(levels);
        }

        return safe_count;
    }

    private int ParseReport(int[] levels)
    {
        var direction = 0;

        for (int i = 1; i < levels.Length; i++)
        {
            if (levels[i] == levels[i - 1]) return 0;

            if (Math.Abs(levels[i] - levels[i - 1]) > max_difference) return 0;

            direction += (levels[i] > levels[i - 1]) ? 1 : -1;        
        }

        return (Math.Abs(direction) < levels.Length - 1) ? 0 : 1;
    }

    private int ProblemDampener(int[] levels)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            var dampened = RemoveAt(levels, i);
            if (ParseReport(dampened) == 1) return 1;
        }
        return 0;
    }

    private static int[] RemoveAt(int[] levels, int index)
    {
        var list = levels.ToList();
        list.RemoveAt(index);
        return [.. list];
    }
}
