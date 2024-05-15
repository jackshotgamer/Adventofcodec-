using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

part_1();

void part_1(bool use_input = true)
{
    String[] lines;
    if (use_input)
    {
        lines = File.ReadAllLines(@"D:\CsharpLearn\AdventofCode\2023\input\2023_day_4.txt");
    }
    else
    {
        lines = File.ReadAllLines(@"D:\CsharpLearn\AdventofCode\2023\samples\2023_day_4.txt");
    }

    Dictionary<int, long> wins = new();
    foreach (string item in lines)
    {
        long internal_total = 0;
        System.Console.WriteLine(item);
        String[] split_lines = item.Split();
        int card = -1;
        bool before_pipe = true;
        List<int> winning_nums = [];
        List<double> nums_that_won = [];
        int i_offset = 0;
        for (int i = 0; i < split_lines.Length; i++)
        {
            string word = split_lines[i];
            try
            {
                if (split_lines[i] == "") {
                    i_offset++;
                    continue;
                }
                if (i == 0+i_offset)
                {
                    continue;
                }
                else if (i == 1+i_offset)
                {
                    card = int.Parse(word[..^1]);
                }
                else if (word == "|")
                {
                    before_pipe = false;
                }
                else if (before_pipe)
                {
                    winning_nums.Add(int.Parse(word));
                }
                else if (!before_pipe && winning_nums.Contains(int.Parse(word)))
                {
                    nums_that_won.Add(int.Parse(word));
                }
            }
            catch (System.Exception)
            {
                System.Console.WriteLine($"Word: {word}");
                throw;
            }
        }
        if (nums_that_won.Count > 0) {
            internal_total = nums_that_won.Count;
        }
        wins[card] = internal_total;
    }
    int queue_max_length = 0;
    Queue<int> queue = new();
    for (int i = 1; i <= lines.Length; i++)
    {
        System.Console.WriteLine(i);
        queue.Enqueue(i);
        queue_max_length = Math.Max(queue_max_length, queue.Count);        
    }
    int num_of_scratchcards = 0;
    while (queue.Count > 0)
    {
        int game = queue.Dequeue();
        num_of_scratchcards++;
        for (int i = game+1; i <= game+wins[game]; i++)
        {
            queue.Enqueue(i);
            queue_max_length = Math.Max(queue_max_length, queue.Count);  
            // Game 5: Worth 4
        }
    }
    System.Console.WriteLine(queue_max_length);
    System.Console.WriteLine(num_of_scratchcards);
}
