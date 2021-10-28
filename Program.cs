using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Timecodes
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("TimeCode Addition");
            Console.WriteLine("---------------------------- \n");
            AddTimecodes("13:30:00:20", "01:15:00:10");
            Console.WriteLine("\n");
            Console.WriteLine("TimeCode Substraction");
            Console.WriteLine("---------------------------- \n");
            SubtractTimecodes("13:30:00:10", "01:15:00:20");
            Console.WriteLine("\n");
            Console.WriteLine("TimeCode Sorting - Ascending");
            Console.WriteLine("---------------------------- \n");
            SortTimecodes(new List<string> { "13:30:00:10", "01:15:00:20", "08:30:00:10", "00:45:00:25", "14:14:00:10" }, 25, "ASC");
            Console.WriteLine("\n");
            Console.WriteLine("TimeCode Sorting - Descending");
            Console.WriteLine("----------------------------  \n");
            SortTimecodes(new List<string> { "13:30:00:10", "01:15:00:20", "08:30:00:10", "00:45:00:25", "14:14:00:10" }, 25, "DESC");
        }

        // Method to compute frames from the timecode
        public static int GetTotalFrames(string timecode, int frameRate)
        {
            int totalFrames = 0, hours, minutes, seconds, frames;
            string TimeCodePattern = @"^(?<hours>[0-2][0-9]):(?<minutes>[0-5][0-9]):(?<seconds>[0-5][0-9])[:|;|\.](?<frames>[0-9]{2,3})$";
            var tcRegex = new Regex(TimeCodePattern);
            var match = tcRegex.Match(timecode);
            hours = int.Parse(match.Groups["hours"].Value);
            minutes = int.Parse(match.Groups["minutes"].Value);
            seconds = int.Parse(match.Groups["seconds"].Value);
            frames = int.Parse(match.Groups["frames"].Value);

            int totalSeconds = (hours * 3600) + (minutes * 60) + seconds;

            totalFrames = (totalSeconds * frameRate) + frames;

            return totalFrames;
        }

        // Method to compute timecode from totalframes
        public static string GetTimecode(int totalFrames, int frameRate)
        {
            string timecode = "";
            int hours, minutes, seconds, frames;

            hours = totalFrames / (3600 * frameRate);
            if (hours > 23)
            {
                hours %= 24;
                totalFrames -= 23 * 3600 * frameRate;
            }
            minutes = totalFrames % (3600 * frameRate) / (60 * frameRate);
            seconds = totalFrames % (3600 * frameRate) % (60 * frameRate) / frameRate;
            frames = totalFrames % (3600 * frameRate) % (60 * frameRate) % frameRate;

            timecode += hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2") + ":" + frames.ToString("D2");

            return timecode;
        }

        // Add timecodes
        // assuming timecode will be a string of format "hh:mm:ss:ff"
        // idea is to convert timecodes into total frames and performing add/substract/sort operations
        public static string AddTimecodes(string timecode1, string timecode2)
        {
            int t1Frames, t2Frames, sumFrames;
            string result;
            t1Frames = GetTotalFrames(timecode1, 25);
            t2Frames = GetTotalFrames(timecode2, 25);
            sumFrames = t1Frames + t2Frames;
            result = GetTimecode(sumFrames, 25);
            Console.WriteLine("{0} + {1} = {2}", timecode1, timecode2, result);
            return result;
        }


        public static string SubtractTimecodes(string timecode1, string timecode2)
        {
            int t1Frames, t2Frames, substractFrames;
            string result;
            t1Frames = GetTotalFrames(timecode1, 25);
            t2Frames = GetTotalFrames(timecode2, 25);
            substractFrames = Math.Abs(t1Frames - t2Frames);
            result = GetTimecode(substractFrames, 25);
            Console.WriteLine("{0} - {1} = {2}", timecode1, timecode2, result);
            return result;
        }

        public static void SortTimecodes(List<string> sourceTimecodes, int frameRate, string sortingType)
        {
            sourceTimecodes.Sort((t1, t2) =>
            {
                int t1Frames = GetTotalFrames(t1, frameRate);
                int t2Frames = GetTotalFrames(t2, frameRate);

                if (t1Frames == t2Frames) return 0;
                else if (t1Frames > t2Frames) return sortingType == "ASC" ? 1 : -1;
                else if (t1Frames < t2Frames) return sortingType == "ASC" ? -1 : 1;
                else return -1;
            });
            Console.WriteLine(String.Join("\n", sourceTimecodes));
        }
    }
}
