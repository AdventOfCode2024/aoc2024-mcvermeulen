using AdventOfCode.Common;

namespace AdventOfCode.Day5;


internal class Solution_day5
{
    private readonly string[] manuals;
    private readonly Dictionary<int, List<int>> page_orders = [];

    public Solution_day5()
    {
        var input = Utils.ReadInputFile("Day5/input.txt");
        
        var orders = input.Take(Array.IndexOf(input, "")).ToArray();
        foreach (var order in orders) {
            var pagenumbers = Array.ConvertAll(order.Split('|'), int.Parse);
            if (page_orders.TryGetValue(pagenumbers[0], out var befores)){
                befores.Add(pagenumbers[1]);
            } else {
                page_orders.Add(pagenumbers[0], [pagenumbers[1]]);
            }
        }
        manuals = input.Skip(Array.IndexOf(input, "") + 1).ToArray();
    }

    public int Part1(){
        int total = 0;

        foreach (var manual in manuals) {
            var pages = Array.ConvertAll(manual.Split(','), int.Parse);
            total += GetMiddleFromCorrectManual(pages);         
        }

        return total;
    }

    public int Part2(){
        int total = 0;

        foreach (var manual in manuals) {
            var pages = Array.ConvertAll(manual.Split(','), int.Parse);

            var value = 0;
            if (GetMiddleFromCorrectManual(pages) == 0){
                while (value == 0) {
                    value = SwapOrderUntilValid(pages);
                }
            }

            total += value;         
        }

        return total;
    }

    private int GetMiddleFromCorrectManual(int[] pages) {
        for (var p = 1; p < pages.Length; p++) {
            var before = pages[p-1];

            if (page_orders.TryGetValue(pages[p], out var afters) && afters.Contains(before))
            {
                return 0;
            }
        }

        return pages[pages.Length / 2];
    }

    private int SwapOrderUntilValid(int[] pages) {
        for (var p = 1; p < pages.Length; p++) {
            var before = pages[p-1];

            if (page_orders.TryGetValue(pages[p], out var afters) && afters.Contains(before))
            {
                var first = pages[p];
                pages[p] = afters[afters.IndexOf(before)];
                pages[p-1] = first;

                return 0;
            }
        }

        return pages[pages.Length / 2];
    }
}