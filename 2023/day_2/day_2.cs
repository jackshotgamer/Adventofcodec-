using System.Collections.ObjectModel;
using System.Drawing;

part_1();
const int REDVAL = 0;
const int GREVAL = 1;
const int BLUVAL = 2;
void part_1(bool use_input = true)
{
    String[] lines;
    List<int> conditions = [12, 13, 14];
    int totalgames = 0;
    int sum_of_powers = 0;
    if (use_input)
    {
        lines = File.ReadAllLines(@"..\..\2023\input\2023_day_2.txt");
    }
    else
    {
        lines = File.ReadAllLines(@"..\..\2023\samples\2023_day_2.txt");
    }
    foreach (var line in lines)
    {   
        int maxred = 0;
        int maxgreen = 0;
        int maxblue = 0;
        bool possible = true;
        int game = 1;
        //words_in_line is a list of the WORDS in line
        var words_in_line = (line + ";").Split();
        var bag = new CubeBag();
        for (int i = 0; i < words_in_line.Length; i++)
        {
            if (i == 0)
            {
                continue;
            }
            if (i == 1)
            {
                // Gets the game number
                game = int.Parse(words_in_line[i].ToString().Remove(words_in_line[i].Length - 1));
                continue;
            }
            //Checks if word "words_in_line[i]" is not a number
            if (!Char.IsDigit(words_in_line[i][0]))
            {
                int value = int.Parse(words_in_line[i - 1]);
                string tempstring;
                bool final_pick = false;
                // Checks if the end character of the word is a ";"
                if (words_in_line[i][^1].ToString() == ";")
                {
                    final_pick = true;
                }
                // Gets words_in_line[i] (current word we're looking at) but without the last character
                tempstring = words_in_line[i].Remove(words_in_line[i].Length - 1);

                int color = tempstring switch
                {
                    "red" => REDVAL,
                    "green" => GREVAL,
                    "blue" => BLUVAL,
                    _ => throw new FormatException()
                };

                if (color == REDVAL)
                {
                    bag.reds += value;
                    maxred = Math.Max(maxred, value);
                }
                else if (color == GREVAL)
                {
                    bag.greens += value;
                    maxgreen = Math.Max(maxgreen, value);
                }
                else if (color == BLUVAL)
                {
                    bag.blues += value;
                    maxblue = Math.Max(maxblue, value);
                }
                if (final_pick)
                {
                    if (bag.reds > conditions[0] || bag.greens > conditions[1] || bag.blues > conditions[2])
                    {
                        possible = false;
                    }
                    bag = new CubeBag();
                    final_pick = false;
                }
            }
            // 12 red cubes, 13 green cubes, and 14 blue cubes

            // System.Console.WriteLine(words_in_line[i]);
            
        }
        int power = maxred * maxgreen * maxblue;
        if (possible) {
            totalgames += game;
        }
        sum_of_powers += power;


    }
    System.Console.WriteLine(totalgames);
    System.Console.WriteLine(sum_of_powers);
}


class CubeBag
{
    public int reds = 0;
    public int greens = 0;
    public int blues = 0;

    public CubeBag()
    {
        reds = 0;
        greens = 0;
        blues = 0;
    }
}