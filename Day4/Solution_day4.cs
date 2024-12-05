using AdventOfCode.Common;

namespace AdventOfCode.Day4;

internal class Solution_day4
{
    private readonly string[] input;
    private readonly string[] vertical;
    private readonly string[] diagonalLR;
    private readonly string[] diagonalRL;

    public Solution_day4()
    {
        input = Utils.ReadInputFile("Day4/input.txt");

        vertical = Enumerable.Repeat(string.Empty, input.Length).ToArray();
        // words need to fit, so we have less diagonals
        var possible_diagonals = (input.Length * 2) - 7;
        diagonalLR = Enumerable.Repeat(string.Empty, possible_diagonals).ToArray();
        diagonalRL = Enumerable.Repeat(string.Empty, possible_diagonals).ToArray();

        FillArrays();
    }

    public int Part1(){
        int total = 0;

        // horizontal
        foreach (var line in input)
        {
            total += CountWords(line, "XMAS") + CountWords(line, "SAMX");
        }

        // vertical
        foreach (var line in vertical) {
            total += CountWords(line, "XMAS") + CountWords(line, "SAMX");
        }

        foreach (var line in diagonalLR) {
            total += CountWords(line, "XMAS") + CountWords(line, "SAMX");
        }

        foreach (var line in diagonalRL) {
            total += CountWords(line, "XMAS") + CountWords(line, "SAMX");
        }

        return total;
    }

    public int Part2(){
        int total = 0;

        for (int line = 1; line < input.Length - 1; line++) {
            for (int row = 1; row < input.Length - 1; row++) {
                if (input[line][row] == 'A') {
                    char LB = '.';
                    char RB = '.';
                    if (input[line - 1][row - 1] == 'S') {
                        LB = 'S';
                    } else if (input[line - 1][row - 1] == 'M') {
                        LB = 'M';
                    }

                    if (input[line - 1][row + 1] == 'S') {
                        RB = 'S';
                    } else if (input[line - 1][row + 1] == 'M') {
                        RB = 'M';
                    }

                    if (LB == 'M' || LB == 'S' && RB == 'M' || RB == 'S') {
                        if ((LB == 'M' && input[line + 1][row + 1] == 'S' ||
                            LB == 'S' && input[line + 1][row + 1] == 'M') &&
                            (RB == 'M' && input[line + 1][row - 1] == 'S' ||
                            RB == 'S' && input[line + 1][row - 1] == 'M')) {
                                total++;
                            }
                    }
                }
            }
        }

        return total;
    }

    private static int CountWords(string line, string word)
    {
        int total = 0;
        int index = line.IndexOf(word);
        while (index != -1)
        {
            total++;
            index = line.IndexOf(word, index + word.Length);
        }

        return total;
    }

    private void FillArrays() {
        var half = (diagonalLR.Length / 2) + 1;
        for (int i = 0; i < diagonalLR.Length; i++) {
            for (int j = 0; j < input.Length; j++) {
                var offset = i - half + 1;

                // Fill diagonals Left to Right
                if (i <= diagonalLR.Length / 2 && j < input.Length - i) {
                    diagonalLR[i] = diagonalLR[i] + input[j][j+i];
                } if (i >= half && i - half < input.Length - 1 && j < input.Length - offset) {
                    diagonalLR[i] = diagonalLR[i] + input[offset + j][j];
                }

                // Fill diagonals Right to Left
                if (i <= diagonalRL.Length / 2 && j < input.Length - i) {
                    diagonalRL[i] = diagonalRL[i] + input[j][input.Length -1 -i -j];
                } else if (i >= half && i - half < input.Length - 1 && j < input.Length - offset) {
                    diagonalRL[i] = diagonalRL[i] + input[offset + j][input.Length -1 - j];
                }
            }
        }

        foreach (var line in input)
        {
            // fill vertical array
            for (int i = 0; i < line.Length; i++) {
                vertical[i] = vertical[i] + line[i];
            }
        }
    }
}