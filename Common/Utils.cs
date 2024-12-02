namespace AdventOfCode.Common;

public static class Utils 
{
    public static string[] ReadInputFile(string path)
    {
        return File.ReadAllLines(path);
    }
}