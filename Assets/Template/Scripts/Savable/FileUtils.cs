namespace Template.Scripts.Savable
{
    public class FileUtils 
    {

    }
    
    public interface IFileInterface
    {
        void WriteBytes(string filePath, byte[] bytes);
        byte[] ReadBytes(string filePath);
    }
    
    public class DotNetReadFileInterface : IFileInterface
    {
        public void WriteBytes(string filePath, byte[] bytes)
        {
            if (System.IO.File.Exists(filePath) == false)
            {
                System.IO.File.Create(filePath);
            }

            System.IO.File.WriteAllBytes(filePath, bytes);
        }

        public byte[] ReadBytes(string filePath)
        {
            if (System.IO.File.Exists(filePath) == false)
            {
                return new byte[0];
            }
            return System.IO.File.ReadAllBytes(filePath);
        }
    }
    public struct SavableContext
    {
        public string SourceFilePath;
    }
}
