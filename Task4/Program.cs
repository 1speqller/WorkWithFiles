[Serializable]
public class Student
{
    public string Name { get; set; }
    public string Group { get; set; }
    public DateTime DateOfBirth { get; set; }
    public decimal AvgScore { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        string pathFile = "/Users/hoshi/Desktop/Students.dat";
        List<Student> list = ReadStudentsFromBinFile(pathFile);
        string pathDirectory = "/Users/hoshi/Desktop/Students";

        Directory.CreateDirectory(pathDirectory);
        foreach (Student student in list)
        {
            if (File.Exists(pathDirectory + $"/{student.Group}.txt"))
            {
                using (StreamWriter sw = File.CreateText(pathDirectory + $"/{student.Group}.txt"))
                {
                    sw.WriteLine("");
                }
            }
        }

        foreach (Student student in list)
        {
            if (!File.Exists(pathDirectory + $"/{student.Group}.txt"))
                File.Create(pathDirectory + $"/{student.Group}.txt");

            else if (File.Exists(pathDirectory + $"/{student.Group}.txt"))
            {
                using (StreamWriter sw = File.AppendText(pathDirectory + $"/{student.Group}.txt"))
                {
                    sw.WriteLine("{0}, {1}, {2}", student.Name, student.DateOfBirth, student.AvgScore);
                }
            }
        }
    }

    static List<Student> ReadStudentsFromBinFile(string fileName)
    {
        List<Student> result = new();
        using FileStream fs = new FileStream(fileName, FileMode.Open);
        using StreamReader sr = new StreamReader(fs);

        Console.WriteLine(sr.ReadToEnd());

        fs.Position = 0;

        BinaryReader br = new BinaryReader(fs);

        while (fs.Position < fs.Length)
        {
            Student student = new Student();
            student.Name = br.ReadString();
            student.Group = br.ReadString();
            long dt = br.ReadInt64();
            student.DateOfBirth = DateTime.FromBinary(dt);
            student.AvgScore = br.ReadDecimal();

            result.Add(student);
        }

        fs.Close();
        return result;
    }
}