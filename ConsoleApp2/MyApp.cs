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

        const string Format = @"^\d\d[:]?\d\d[:]?\d\d";
        const string Format2 = @"^\d\d[:]?\d\d[:]?\d\d-->\d\d[:]?\d\d[:]?\d\d";
        const string Format3 = @"^\d\d[:]?\d\d[:]?\d\d[.]\d{3}-->\d\d[:]?\d\d[:]?\d\d[.]\d{3}";
        const string Format4 = @"^\D.+";
        //const string Format5 = @"^[0-9]*$";


        const string path = @"C:\Users\Administrator\source\repos\ConsoleApp2\ConsoleApp2\";

        TimeStamp timeStamp = new TimeStamp();

        bool invalid = false;
        public int counter { get; set; } = 0;
        public string Line { get; set; }

        public void run()
        {
            TimeStamp timeStamp = new TimeStamp();
            string Subtitle = "";
            string text = "";
            string line;
            try
            {
                StreamReader sr = new StreamReader(path);

                while ((line = sr.ReadLine()) != null)
                {
                    if (Line == Environment.NewLine)
                    {

                    }
                    else if (IsValidTimeFormat(line, Format))
                    {
                        timeStamp.First = line;
                        timeStamp.ChangeStingFormat(ref timeStamp.First);
                        
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
                        timeStamp.TimeSpanSplict();
                    }

                    else if (IsValidTimeFormat(Line, Format3))
                    {

                    }
                    else if (IsValidTimeFormat(Line, Format4))
                    {
                        Subtitle = line;
                    }

                    text = text + line;
                }

                sr.Close();
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
                                      RegexOptions.IgnoreCase,
                                      TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

    }
}
