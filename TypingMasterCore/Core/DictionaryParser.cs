using System.IO;
using System.Windows.Controls;

namespace TypingMasterCore.Core
{
    class DictionaryParser
    {
        public List<string>? Dictionary { get; private set; }

        public DictionaryParser(ComboBoxItem difficulty, ItemCollection items)
        {
            string path = "";
            path += Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName + "\\Assets\\Words\\";

            for (int i = 0; i < 4; i++)
            {
                ComboBoxItem cbItem = (ComboBoxItem)items[i];
                if (difficulty.Name == cbItem.Name)
                {
                    path += cbItem.Name + ".txt";
                }
            }

            try
            {
                Dictionary = [.. File.ReadAllLines(path)];
            }
            catch (FileNotFoundException e)
            {
                throw new FileNotFoundException(e.Message);
            }
        }
    }
}
