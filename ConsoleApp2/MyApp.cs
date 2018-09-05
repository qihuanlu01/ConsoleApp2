using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class MyApp
    {

        const string Format = @"^\d\d[:]?\d\d[:]?\d\d[-->]{3}$";
        const string Format2 = @"^\d\d[:]?\d\d[:]?\d\d-->\d\d[:]?\d\d[:]?\d\d$";
        const string Format3 = @"^\d\d[:]?\d\d[:]?\d\d[.]\d{3}-->\d\d[:]?\d\d[:]?\d\d[.]\d{3}$";
        const string Format4 = @"^\D.+";
        //const string Format5 = @"^[0-9]*$";
        const string path = @"C:\Users\Administrator\source\repos\ConsoleApp2\ConsoleApp2\ad.srt";
        const string wrpath = @"C:\Users\Administrator\Source\Repos\Subtitle_Edit\ConsoleApp2\after.srt";

       

        TimeStamp timeStamp = new TimeStamp();

        bool invalid = false;
        public int counter { get; set; } = 0;

        public void run()
        {
            string lineTemp = "";
            TimeStamp timeStamp = new TimeStamp();
            string Subtitle = "";
            string text = "";
            string line;
            try
            {
                StreamReader sr = new StreamReader(path);
                StreamWriter wr = new StreamWriter(wrpath);

                while ((line = sr.ReadLine()) != null)
                {

                    // Console.WriteLine(line);
                    //Console.ReadKey();

                    if (line =="")
                    {
                        continue;
                    }
                    else if (IsValidTimeFormat(line, Format))
                    {

                        line = Regex.Replace(line, @"[-][-][>]", string.Empty);
                        timeStamp.First = line;
                        timeStamp.ChangeStingFormat(ref timeStamp.First);

                        timeStamp.Last = timeStamp.First;

                        timeStamp.First = lineTemp;
                        lineTemp = timeStamp.Last;

                        text = timeStamp.TimeSpanSplict();

                    }
                    else if (IsValidTimeFormat(line, Format2))
                    {
                        var matches = Regex.Matches(line, @"\d\d[:]?\d\d[:]?\d\d");
                        string[] vs = new string[2];
                        int i = 0;
                        foreach (Match match in matches)
                        {

                            vs[i] = match.Groups[0].Value;
                            i++;
                        }
                        timeStamp.First = vs[0];
                        timeStamp.ChangeStingFormat(ref timeStamp.First);
                        timeStamp.Last = vs[1];
                        timeStamp.ChangeStingFormat(ref timeStamp.Last);
                        text = timeStamp.TimeSpanSplict();
                        // Console.WriteLine(text);

                    }
                    else if (IsValidTimeFormat(line, Format3))
                    {
                        continue;
                    }

                    else if (IsValidTimeFormat(line, @"\d{1,3}"))
                    {
                        continue;
                    }
                    else if (IsValidTimeFormat(line, Format4))
                    {
                        Subtitle = line;
                       // Console.WriteLine(Subtitle);
                        text = Subtitle;
                    }

                    //
                    if (IsValidTimeFormat(text, Format3))
                    {
                        wr.WriteLine(TimeStamp.ID);
                        TimeStamp.ID++;
                    }
                    Console.WriteLine(text);
                    wr.WriteLine(text);
                    text = "";
              //  Console.ReadKey();
                }

                wr.Close();
                sr.Close();
                TimeStamp.ID = 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("文件读取失败");
                Console.WriteLine(e.Message);

            }
        }

        public bool IsValidTimeFormat(string strIn, string Format)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;
            if (invalid)
                return false;

            // Return true if strIn is in valid email format.
            try
            {
                return Regex.IsMatch(strIn,
                                      Format,
                                      RegexOptions.None,
                                      TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return false;
            }
        }

    }
}
