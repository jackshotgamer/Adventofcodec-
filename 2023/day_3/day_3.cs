using System.Collections.ObjectModel;
using System.Drawing;

part_1();
void part_1(bool use_input = true)
{
    String[] lines;
    if (use_input)
    {
        lines = File.ReadAllLines(@"D:\CsharpLearn\AdventofCode\2023\input\2023_day_3_alt.txt");
    }
    else
    {
        lines = File.ReadAllLines(@"D:\CsharpLearn\AdventofCode\2023\samples\2023_day_3.txt");
    }
    int height = lines.Length;
    int width = lines[0].Length;
    List<int>[,] schematic = new List<int>[height, width];
    for (int h = 0; h < schematic.GetLength(0); h++)
    {
        for (int w = 0; w < schematic.GetLength(1); w++)
        {
            schematic[h, w] = [-4, -1];
        }
    }
    System.Console.WriteLine("Log");
    int SYMBOL = -2;
    int GEAR = -3;
    int DOT = 0;
    int id_count = 0;
    for (int h = 0; h < height; h++)
    {
        string int_val = "";
        List<(int, int)> int_val_indexes = [];

        for (int w = 0; w < width; w++)
        {
            if (Char.IsDigit(lines[h][w]))
            {
                int_val += lines[h][w];
                int_val_indexes.Add((h, w));
            }
            if (!Char.IsDigit(lines[h][w]))
            {
                if (int_val_indexes.Count > 0)
                {
                    foreach (var item in int_val_indexes)
                    {
                        schematic[item.Item1, item.Item2] = [int.Parse(int_val), id_count];
                    }
                    id_count++;

                }
                int_val = "";
                int_val_indexes.Clear();
            }
            if (!Char.IsDigit(lines[h][w]) && lines[h][w].ToString() != ".")
            {
                if (lines[h][w].ToString() == "*")
                {
                    schematic[h, w][0] = GEAR;
                }
                else
                {
                    schematic[h, w][0] = SYMBOL;
                }
            }
            if (lines[h][w].ToString() == ".")
            {
                schematic[h, w][0] = DOT;
            }
        }
        if (int_val != "")
        {
            if (int_val_indexes.Count > 0)
            {
                foreach (var item in int_val_indexes)
                {
                    schematic[item.Item1, item.Item2] = [int.Parse(int_val), id_count];
                }
                id_count++;
            }
        }

    }
    List<int> part_ids = [];
    List<(int, int)> part_nos = [];
    List<(int, int, int)> indexes_to_check = check_indexes(schematic);
    foreach (var index in indexes_to_check)
    {
        if (schematic[index.Item1, index.Item2][0] > 0)
        {
            if (!part_ids.Contains(schematic[index.Item1, index.Item2][1]))
            {
                part_nos.Add((schematic[index.Item1, index.Item2][0], index.Item3));
                part_ids.Add(schematic[index.Item1, index.Item2][1]);
            }

            schematic[index.Item1, index.Item2][0] = -1;
        }
    }
    foreach (var item in part_nos)
    {
        System.Console.WriteLine(item);
    }
    
    Dictionary<int, (int, int)> symbol_parts_dict = new();
    foreach (var item in part_nos)
    {
        if (symbol_parts_dict.ContainsKey(item.Item2))
        {
            var tup = symbol_parts_dict[item.Item2];
            tup.Item1 *= item.Item1;
            tup.Item2++;
            symbol_parts_dict[item.Item2] = tup;
        }
        else
        {
            symbol_parts_dict[item.Item2] = (item.Item1, 1);
        }
    }
    long sum_of_gear_ratios = 0;
    foreach (var s in symbol_parts_dict)
    {
        int s_id = s.Key;
        if (symbol_parts_dict[s_id].Item2 == 2)
        {
            sum_of_gear_ratios += symbol_parts_dict[s_id].Item1;
        }
    }

    System.Console.WriteLine($"SUM: {sum_of_gear_ratios}");
    // PrintArray(schematic);
}

List<(int, int, int)> check_indexes(List<int>[,] arr)
{
    List<(int, int, int)> indexes_to_return = [];
    int symbol_id = 0;
    for (int h = 0; h < arr.GetLength(0); h++)
    {
        for (int w = 0; w < arr.GetLength(1); w++)
        {
            if (arr[h, w][0] == -3)
            {

                for (int index_h = -1; index_h < 2; index_h++)
                {
                    for (int index_w = -1; index_w < 2; index_w++)
                    {
                        if (index_h + h >= 0 && index_h + h < arr.GetLength(0) && index_w + w >= 0 && index_w + w < arr.GetLength(1))
                        {
                            indexes_to_return.Add((index_h + h, index_w + w, symbol_id));
                        }
                    }
                }
                symbol_id++;
            }

        }
    }
    return indexes_to_return;
}


void PrintArray(List<int>[,] arr)
{
    for (int i = 0; i < arr.GetLength(0); i++)
    {
        for (int o = 0; o < arr.GetLength(1); o++)
        {

            System.Console.Write($"{(arr[i, o][0]).ToString().PadLeft(4)}");
        }
        System.Console.WriteLine();
    }
}
/* 
467..114.. 
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..
 */
