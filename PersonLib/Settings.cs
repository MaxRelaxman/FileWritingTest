namespace PersonLib
{
    public static class Settings
    {
        public static int MinAge { get; set; } = 16;
        public static int MaxAgeOverrideReq { get; set; } = 18;
        public static int MaxFirstNameLen { get; set; } = 30;
        public static int MaxSurNameLen { get; set; } = 30;
        public static string FilePath { get; set; } = Directory.GetCurrentDirectory();
        public static string PersonFilename { get; set; } = "PersonFile.txt";
        public static string SpouseFilePath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "Spouses");
    }
}
