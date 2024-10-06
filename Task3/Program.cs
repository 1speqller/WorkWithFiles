string path = "/ProgramData/ВеселаяФерма2/sys";
bool IsDelete = false;


Console.WriteLine($"Исходный размер папки: {UrlDirSize(path)}");
long deleteEqual = UrlDirSize(path);
DeleteDirectory(path, ref IsDelete);
if (IsDelete)
    Console.WriteLine($"Освобождено: {deleteEqual}: ");
else
    Console.WriteLine("Освобождено: 0");
Console.WriteLine($"Текущий размер папки: {UrlDirSize(path)}");

static long UrlDirSize(string path)
{
    try
    {
        DirectoryInfo dir = new DirectoryInfo(path);

        if (dir.Exists)
            return DirSize(dir);

        else
        {
            Console.WriteLine("Каталог по указанному пути не существует!");
            return 0;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return 0;
    }
}
static long DirSize(DirectoryInfo d)
{
    long size = 0;
    FileInfo[] fis = d.GetFiles();
    foreach (FileInfo fi in fis)
        size += fi.Length;

    DirectoryInfo[] dirs = d.GetDirectories();
    foreach (DirectoryInfo dir in dirs)
        size += DirSize(dir);

    return size;
}
static void DeleteDirectory(string path, ref bool IsDelete)
{
    
    DirectoryInfo dir = new DirectoryInfo(path);

    if (dir.Exists && (dir.LastAccessTime < DateTime.Now - TimeSpan.FromMinutes(30)))
    {
        try
        {
            Console.WriteLine("Указанный каталог существует, приступаем к удалению...");
            Directory.Delete(path, true);
            Console.WriteLine("Каталог успешно удалены");
            IsDelete = true;
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