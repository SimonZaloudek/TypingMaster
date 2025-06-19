using System.IO;
using System.Windows.Shapes;

namespace TypingMasterCore.Core
{
    class TxtHandler(string path)
    {

        public List<string>? Dictionary { get; private set; } = [];
        private readonly string _path = path;
        internal static readonly string[] separator = [" ", "\n", "\r", "\t"];

        internal bool Handle()
        {
            try
            {
                string fileText = File.ReadAllText(_path);

                string[] tempDictionary;
                tempDictionary = fileText.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in tempDictionary)
                {
                    if (word.Length > 12 || word.Length < 0 || word.Contains(' ') || !word.All(Char.IsLetter))
                    {
                        return false;
                    }
                    
                    Dictionary?.Add(word.ToUpper());
                }
                return true;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            } 
        }
    }
}
