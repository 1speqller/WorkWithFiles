string path = "/Users/hoshi/Desktop/Проекты";


Console.WriteLine($"Всего директория занимает: {UrlDirSize(path)}");

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
    catch(Exception ex)
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