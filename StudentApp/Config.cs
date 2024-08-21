namespace StudentApp_FunctionalProgramming_
{
    public static class Config
    {
        public static string dbFilePath = "StudentApp2.db";
        public static string dbConnection = string.Format("URI=file:{0}", Config.dbFilePath);
    }
}
