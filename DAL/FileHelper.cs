namespace DAL;

public static class FileHelper 
{
    public static string BasePath = Environment
                                        .GetFolderPath(System.Environment.SpecialFolder.UserProfile)
                                    + Path.DirectorySeparatorChar + "stellas_business_task_2024" + Path.DirectorySeparatorChar;
    
}