using AdventOfCode.Common;

namespace AdventOfCode.Day6;

internal class Solution_day6
{
    private readonly List<char[]> map = [];
    private readonly char BLOCK = '#';
    private List<string> guardPositions = [];
    private readonly HashSet<(int, int, int)> path = [];

    public Solution_day6()
    {
        var input = Utils.ReadInputFile("Day6/input.txt");
        foreach (var line in input) {
            map.Add(line.ToCharArray());
        }

        var startingPosition = FindStartingPosition('^');
        guardPositions.Add($"{startingPosition[0]},{startingPosition[1]}");
    }

    public int Part1()
    {
        _ = MapGuardRoute();

        return guardPositions.Distinct().ToArray().Length;
    }

    public int Part2(){
        int successfulObstacles = 0;

        var originalGuardRoute = new List<string>(guardPositions);
        var start = Array.ConvertAll(originalGuardRoute[0].Split(','), int.Parse);
        for (int i = 0; i < map.Count; i++) {
            for (int j = 0; j < map[0].Length; j++) {                
                if (i == start[0] && j == start[1]) continue;
                guardPositions = originalGuardRoute.Take(1).ToList();
                path.Clear();
                var originalValue = map[i][j];
                map[i][j] = '#';
                if (!MapGuardRoute()) {
                    successfulObstacles++;
                }
                map[i][j] = originalValue;
            }           
        }

        return successfulObstacles;
    }

    private bool MapGuardRoute()
    {
        var direction = Direction.UP;
        var loop_detected = false;

        int[] newPosition = [0, 0];
        while (newPosition[0] != -1 && newPosition[1] != -1)
        {
            newPosition = TryMoveGuard(direction);
            switch (newPosition[0])
            {
                case -1:
                    break;
                case -2:
                    direction = Direction.RIGHT;
                    break;
                case -3:
                    direction = Direction.DOWN;
                    break;
                case -4:
                    direction = Direction.LEFT;
                    break;
                case -5:
                    direction = Direction.UP;
                    break;
                default:
                    guardPositions.Add($"{newPosition[0]},{newPosition[1]}");
                    if (! path.Add((newPosition[0], newPosition[1], (int)direction))) {
                        loop_detected = true;
                    }
                    break;
            }
            if (loop_detected) return false;
        }
        return true;
    }

    private int[] TryMoveGuard(Direction direction) {
        var currentPosition = Array.ConvertAll(guardPositions.Last().Split(','), int.Parse);
        switch (direction) {
            case Direction.RIGHT:
                if (currentPosition[1] < map[0].Length - 1) {
                    if (map[currentPosition[0]][currentPosition[1] + 1] == BLOCK) {
                        return [-3, currentPosition[1]];
                    }
                    return [currentPosition[0], currentPosition[1] + 1];
                }
                break;
            case Direction.DOWN:
                if (currentPosition[0] < map.Count - 1) {
                    if (map[currentPosition[0] + 1][currentPosition[1]] == BLOCK) {
                        return [-4, currentPosition[1]];
                    }
                    return [currentPosition[0] + 1, currentPosition[1]];
                }
                break;
            case Direction.LEFT:
                if (currentPosition[1] > 0) {
                    if (map[currentPosition[0]][currentPosition[1] - 1] == BLOCK) {
                        return [-5, currentPosition[1]];
                    }
                    return [currentPosition[0], currentPosition[1] - 1];
                }
                break;
            default: 
                // If we can go further upwards
                if (currentPosition[0] > 0) {
                    if (map[currentPosition[0] - 1][currentPosition[1]] == BLOCK) {
                        return [-2, currentPosition[1]];
                    }
                    return [currentPosition[0] - 1, currentPosition[1]];
                }
                break;
        }

        return [-1, -1];
    }

    private int[] FindStartingPosition(char character) {
        for (int i = 0; i < map.Count; i++) {
            int j = Array.IndexOf(map[i], character);
            if (j != -1) {
                return [i, j];
            }
        }

        return [-1, -1];
    }

    private enum Direction {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }
}
