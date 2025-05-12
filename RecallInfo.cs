using GeniePlugin.Interfaces;
using System.Text.RegularExpressions;

namespace Recall
{
    public class RecallInfo : IPlugin
    {
        public string Name => "Recall";

        public string Version => "1.3";

        public string Description => "Recall info about names or whatever";

        public string Author => "Thires";

        public bool Enabled { get; set; } = true;

        private RecallForm? Form;
        private static IHost? info;
        public static IHost? Info { get => info; set => info = value; }

        public void Initialize(IHost host)
        {
            Info = host;
        }

        public void ParentClosing()
        {
        }

        public string ParseInput(string Text)
        {
            if (Regex.IsMatch(Text, @"^/recall\shelp$", RegexOptions.IgnoreCase))
            {
                Info?.EchoText("\n/recall will open the GUI\n/recall <name> to display info\n/recall add <name> <info> to add\nUse /n or \\\\n to add a new line\nExample /recall add test this is/na test\nDon't click the clickable link, it just throws an error\n/recall remove <name> to remove a note");
                return "";
            }
            else if (Regex.IsMatch(Text, @"^/recall\sadd\s+\S+\s+.+", RegexOptions.IgnoreCase))
            {
                string[] parts = Text[12..].Split(new[] { ' ' }, 2);
                if (parts.Length == 2)
                {
                    string name = parts[0].Trim();
                    string info = parts[1].Trim().Replace("/n|\\n", "\n");
                    RecallActions.AddNote(name, info);
                }
                else
                {
                    Info?.EchoText("Usage: /recall add <name> <info>");
                }
                return "";
            }
            else if (Regex.IsMatch(Text, @"^/recall\sremove\s+\S+", RegexOptions.IgnoreCase))
            {
                string name = Text[15..].Trim();
                if (!string.IsNullOrEmpty(name))
                {
                    RecallActions.RemoveNote(name);
                }
                else
                {
                    Info?.EchoText("Usage: /recall remove <name>");
                }
                return "";
            }
            else if (Regex.IsMatch(Text, @"^/recall\s", RegexOptions.IgnoreCase))
            {
                string name = Text[8..].Trim();
                string info = RecallActions.GetInfoByName(name);

                if (!string.IsNullOrEmpty(info))
                {
                    RecallActions.Display(name, info);
                }
                else
                {
                    if (name.Equals("add", StringComparison.OrdinalIgnoreCase))
                        Info?.EchoText("Usage: /recall add <name> <info>");
                    else if (name.Equals("remove", StringComparison.OrdinalIgnoreCase))
                        Info?.EchoText("Usage: /recall remove <name>");
                    else
                    Info?.EchoText($"No information found for '{name}'");
                }
                return "";
            }
            else if (Regex.IsMatch(Text, @"^/recall$", RegexOptions.IgnoreCase))
            {
                Show();
                return "";
            }
            return Text;
        }

        public string ParseText(string Text, string Window)
        {
            return Text;
        }

        public void ParseXML(string XML)
        {
        }

        public void Show()
        {
            if (Form == null || Form.IsDisposed)
            {
                Form = new RecallForm();
            }
            Form?.Show();
        }

        public void VariableChanged(string Variable)
        {
        }
    }
}
