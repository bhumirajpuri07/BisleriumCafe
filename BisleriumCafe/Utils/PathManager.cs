namespace BisleriumCafe.Utils
{
    public static class PathManager
    {
        public static string GetJSONFilePath(string fileName)
        {
            string directoryPath = @"C:\Users\kafka\OneDrive\Desktop\BisleriumCafe\BisleriumCafe\Datas\";

            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, fileName);

                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }

                return filePath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




    }

}