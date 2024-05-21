using Godot;

namespace FlappyBirdRemake.Utils
{
    public class ScoreFileManager : IFileManager<int>
    {
        private readonly string _filePath = "user://score.dat";

        public int Load()
        {
            using (var file = FileAccess.Open(_filePath, FileAccess.ModeFlags.ReadWrite))
            {
                return file?.Get16() ?? 0;
            }
        }

        public void Save(int data)
        {
            using var file = FileAccess.Open(_filePath, FileAccess.ModeFlags.WriteRead);
            file?.Store16((ushort)data);
        }
    }
}