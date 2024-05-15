using System.IO.Compression;
using System.Numerics;
using System.Text.RegularExpressions;

part_2();
void part_2(bool use_input = true)
{
    String[] lines;
    if (use_input)
    {
        lines = File.ReadAllLines(@"..\..\2023\input\2023_day_1_2.txt");
    }
    else
    {
        lines = File.ReadAllLines(@"..\..\2023\samples\2023_day_1_2.txt");
    }
    long total = 0;
    foreach (var line in lines)
    {
        total += scanNumber(line);
    }

    // "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" -> otfsen

    // var matches = Regex.Matches(item, @"\d"); // 123asdf4asdfd5asd -> 45
    // total += long.Parse(matches[0].Value + matches[^1].Value);
    System.Console.WriteLine(total);
}

/* 
two1nine
eightwothree  -> 
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen
*/

long scanNumber(string line)
{
    List<long> vars = [];
    for (int i = 0; i < line.Length; i++)
    {
        if (Char.IsDigit(line[i]))
        {
            vars.Add(long.Parse(line[i].ToString()));
        }
        if ("otfsen".Contains(line[i]))
        {
            string thrice = "aaa";
            string fouce = "aaaa";
            string fice = "aaaaa";
            if (i + 2 < line.Length)
            {
                thrice = line[i].ToString() + line[i + 1].ToString() + line[i + 2].ToString();
            }
            if (new[] { "one", "two", "six" }.Contains(thrice))
            {
                vars.Add(thrice switch
                {
                    "one" => 1,
                    "two" => 2,
                    "six" => 6,
                    _ => -1
                });
            }
            if (i + 3 < line.Length)
            {
                fouce = line[i].ToString() + line[i + 1].ToString() + line[i + 2].ToString() + line[i + 3].ToString();
            }
            if (new[] { "four", "five", "nine" }.Contains(fouce))
            {
                vars.Add(fouce switch
                {
                    "four" => 4,
                    "five" => 5,
                    "nine" => 9,
                    _ => -1
                });
            }
            if (i + 4 < line.Length)
            {
                fice = line[i].ToString() + line[i + 1].ToString() + line[i + 2].ToString() + line[i + 3].ToString() + line[i + 4].ToString();
            }
            if (new[] { "three", "seven", "eight" }.Contains(fice))
            {
                vars.Add(fice switch
                {
                    "three" => 3,
                    "seven" => 7,
                    "eight" => 8,
                    _ => -1
                });
            }

        }
    }
    return long.Parse(vars[0].ToString() + vars[^1].ToString());
}

void part_1(bool use_input = false)
{
    String[] lines;
    if (use_input)
    {
        lines = File.ReadAllLines(@"..\..\2023\input\2023_day_1.txt");
    }
    else
    {
        lines = File.ReadAllLines(@"..\..\2023\samples\2023_day_1.txt");
    }

    long total = 0;
    foreach (var item in lines)
    {
        var matches = Regex.Matches(item, @"\d"); // 123asdf4asdfd5asd -> 45
        total += long.Parse(matches[0].Value + matches[^1].Value);
    }
    System.Console.WriteLine(total);
}
/* 

1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet

 */
