namespace HomeworkAsyncAndFileSystem.Constants
{
    public static class Path
    {
        public const string directoryName = "JSONs";
        public const string addressesFileName = "addresses.json";
        public const string usersFileName = "users.json";

        public static string GetUsersJSONFullPath()
        {
            return System.IO.Path.Combine(directoryName, usersFileName);
        }

        public static string GetAddressesJSONFullPath()
        {
            return System.IO.Path.Combine(directoryName, addressesFileName);
        }
    }
}
