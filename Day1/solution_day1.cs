namespace AdventOfCode.Day1;

public class solution_day1
{
    private readonly List<int> Left = [];

    private readonly List<int> Right = [];

    public solution_day1()
    {
        var input = ReadInputFile();

        foreach (var line in input) {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Left.Add(int.Parse(parts[0]));
            Right.Add(int.Parse(parts[1]));
        }
    }

    public string Part1() 
    {
        Left.Sort();
        Right.Sort();
        
        int total_distance = 0;

        for (var i = 0; i < Left.Count; i++) {
            total_distance += Math.Abs(Left[i] - Right[i]);
        }

        return total_distance.ToString();
    }

    public string Part2() 
    {
        int similarity = 0;

        for (var i = 0; i < Left.Count; i++) {
            int count = Right.Count(n => n == Left[i]);
            similarity += Left[i] * count;
        }

        return similarity.ToString();
    }

    private string[] ReadInputFile()
    {
        return File.ReadAllLines("Day1/input.txt");
    }

}
