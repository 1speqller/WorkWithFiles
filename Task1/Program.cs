string path = "/Users/hoshi/Desktop/NewCopy";

DeleteDirectory(path);
static void DeleteDirectory(string path)
{
    DirectoryInfo dir = new DirectoryInfo(path);

    if (dir.Exists && (dir.LastAccessTime < DateTime.Now - TimeSpan.FromMinutes(30)))
    {
        try
        {
            Console.WriteLine("Указанный каталог существует, приступаем к удалению...");
            Directory.Delete(path, true);
            Console.WriteLine("Каталог успешно удалены");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    else if (!dir.Exists)
        Console.WriteLine("Каталог по указанному пути не существует!");

    else if (dir.LastAccessTime > DateTime.Now - TimeSpan.FromMinutes(30))
        Console.WriteLine("Каталог использовался менее 30 минут назад!");
}