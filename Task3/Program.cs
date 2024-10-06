string path = "/Users/hoshi/Desktop/Проекты";
DirectoryInfo dir = new DirectoryInfo(path);
var folders = dir.GetDirectories();

WriteDirectoryInfo(folders);

Console.WriteLine($"Всего директория занимает: {DirSize(dir)}");

static void WriteDirectoryInfo(DirectoryInfo[] folders)
{
    Console.WriteLine();
    Console.WriteLine("Папки: ");
    Console.WriteLine();

    foreach (var folder in folders)
    {
        try
        {
            Console.WriteLine(folder.Name + $"- {DirSize(folder)} байт");
        }
        catch (Exception e)
        {
            Console.WriteLine(folder.Name + $"Не удалось рассчитать размер: {e.Message}");
        }
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