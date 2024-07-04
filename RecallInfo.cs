using GeniePlugin.Interfaces;
using System.Text.RegularExpressions;

namespace Recall
{
    public class RecallInfo : IPlugin
    {
        public string Name => "Recall";

        public string Version => "1.0.0";

        public string Description => "Recall info";

        public string Author => "Thires";

        public bool Enabled { get; set; } = true;

        private RecallForm? Form;
        //private RecallActions? Action;
        private static IHost? info;
        public static IHost? Info { get => info; set => info = value; }

        public void Initialize(IHost host)
        {
            Info = host;
            //Action = new RecallActions();
        }

        public void ParentClosing()
        {
        }

        public string ParseInput(string Text)
        {
            if (Regex.IsMatch(Text, @"^/recall\shelp$", RegexOptions.IgnoreCase))
            {
                Info?.EchoText("\n/recall will open the GUI\n/recall <name> to display the note\n/recall add <name> <info> to add a note\nUse /n or \\\\n to add a new line\nExample /recall add test this is/na test\nDon't click the clickable link, it just throws an error\n/recall remove <name> to remove a note");
                
            }
            else if (Regex.IsMatch(Text, @"^/recall\sadd[\s|$]", RegexOptions.IgnoreCase))
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
            }
            else if (Regex.IsMatch(Text, @"^/recall\sremove\s", RegexOptions.IgnoreCase))
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
                    Info?.EchoText($"No information found for '{name}'");
                }
            }
            else if (Regex.IsMatch(Text, @"^/recall$", RegexOptions.IgnoreCase))
            {
                Show();
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
