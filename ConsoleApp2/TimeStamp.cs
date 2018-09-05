namespace ConsoleApp2
{
    class TimeStamp
    {
        public string First = "00:00:00.000";
        public string Last = "0";



        public static int ID { get; set; } = 1;

        public string TimeSpanSplict()
        {
            return First = First + "-->" + Last;

        }
        public void ChangeStingFormat(ref string changst)
        {
            changst = changst + ".000";
        }
    }
}