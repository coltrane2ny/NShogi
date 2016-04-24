namespace NShogi
{
    public enum Color
    {
        Black,
        White,
    }

    static class ColorExt
    {
        private static string[] recordNames = { "▲", "△" };

        public static string ToRecordName(this Color color)
        {
            return recordNames[(int)color];
        }
    }
}