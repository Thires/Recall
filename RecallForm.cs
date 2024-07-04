using System.Xml;

namespace Recall
{
    public partial class RecallForm : Form
    {
        private static string xmlPath = Path.Combine(RecallInfo.Info?.get_Variable("PluginPath") ?? "", "Recall.xml");
        private bool isClosingDueToEscKey = false;

        public IEnumerable<RecallItem> GetComboBoxItems()
        {
            foreach (var item in comboBox1.Items)
            {
                if (item is RecallItem recallItem)
                {
                    yield return recallItem;
                }
            }
        }

        public RecallForm()
        {
            InitializeComponent();
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).

            xmlPath = Path.Combine(RecallInfo.Info?.get_Variable("PluginPath") ?? "", "Recall.xml");
            if (File.Exists(xmlPath))
            {
                LoadFromXML();
            }
            else
            {
                var xmlDocument = new XmlDocument();
                XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDocument.AppendChild(xmlDeclaration);

                XmlElement rootNode = xmlDocument.CreateElement("RecallData");
                xmlDocument.AppendChild(rootNode);
                xmlDocument.Save(xmlPath);
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is RecallItem selectedItem)
            {
                textBox2.Text = selectedItem.Info;
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                if (comboBox1.SelectedItem is RecallItem selectedItem)
                {
                    selectedItem.Info = textBox2.Text;
                }
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                var existingItem = FindItemByName(comboBox1.Text);
                if (existingItem != null)
                {
                    existingItem.Info = textBox2.Text;
                }
                else
                {
                    RecallItem item = new(comboBox1.Text, textBox2.Text);
                    comboBox1.Items.Add(item);
                }
                SaveToXML();
                textBox2.Clear();
                comboBox1.Text = string.Empty;
            }
            LoadFromXML();
        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                if (comboBox1.SelectedItem is RecallItem selectedItem)
                {
                    var result = MessageBox.Show($"Are you sure you want to remove '{selectedItem.Name}'?", "Confirm Deletion",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        comboBox1.Items.Remove(selectedItem);
                        textBox2.Clear();
                    }
                }
            }
            SaveToXML();
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            LoadFromXML();
        }

        private void RecallForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                isClosingDueToEscKey = true;
                this.Close();
            }
        }

        private void RecallForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (isClosingDueToEscKey)
            {
                // Unregister the event handlers before closing the form.
                this.KeyDown -= (s, ev) => RecallForm_KeyDown(s, ev);
                this.FormClosing -= (s, ev) => RecallForm_FormClosing(s, ev);
            }
            else
            {
                isClosingDueToEscKey = true;
                this.Close();
            }
        }

        private void RecallForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            this.KeyDown -= (s, ev) => RecallForm_KeyDown(s, ev);
            this.FormClosing -= (s, ev) => RecallForm_FormClosing(s, ev);
            this.FormClosed -= (s, ev) => RecallForm_FormClosed(s, ev);
        }
    
        private RecallItem? FindItemByName(string name)
        {
            foreach (var item in comboBox1.Items)
            {
                if (item is RecallItem recallItem && recallItem.Name == name)
                {
                    return recallItem;
                }
            }
            return null;
        }

        internal void SaveToXML()
        {
            string pluginPath = RecallInfo.Info?.get_Variable("PluginPath") ?? string.Empty;
            if (!string.IsNullOrEmpty(pluginPath))
            {
                string xmlPath = Path.Combine(pluginPath, "Recall.xml");
                XmlDocument xmlDocument = new();

                if (File.Exists(xmlPath))
                {
                    xmlDocument.Load(xmlPath);
                }
                else
                {
                    XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
                    xmlDocument.AppendChild(xmlDeclaration);

                    XmlElement rootNode = xmlDocument.CreateElement("RecallData");
                    xmlDocument.AppendChild(rootNode);
                }

                XmlElement? rootElement = xmlDocument.DocumentElement;

                if (rootElement != null)
                {
                    rootElement.RemoveAll();
                    var sortedItems = comboBox1.Items.Cast<RecallItem>().OrderBy(item => item.Name).ToList();


                    foreach (RecallItem item in sortedItems)
                    {
                        XmlElement newItemNode = xmlDocument.CreateElement("Item");
                        newItemNode.SetAttribute("Name", item.Name);
                        XmlElement infoElement = xmlDocument.CreateElement("Info");
                        infoElement.InnerText = item.Info.Replace(Environment.NewLine, "\n");
                        newItemNode.AppendChild(infoElement);
                        rootElement.AppendChild(newItemNode);
                    }

                    xmlDocument.Save(xmlPath);
                }
                else
                {
                    RecallInfo.Info?.EchoText("Failed to create XML root element.");
                }
            }
            else
            {
                RecallInfo.Info?.EchoText("Plugin path is not set.");
            }
        }

        internal void LoadFromXML()
        {
            string pluginPath = RecallInfo.Info?.get_Variable("PluginPath") ?? string.Empty;
            if (!string.IsNullOrEmpty(pluginPath))
            {
                string xmlPath = Path.Combine(pluginPath, "Recall.xml");
                if (File.Exists(xmlPath))
                {
                    XmlDocument xmlDocument = new();
                    try
                    {
                        xmlDocument.Load(xmlPath);
                        XmlElement? rootElement = xmlDocument.DocumentElement;
                        comboBox1.Items.Clear();

                        if (rootElement != null)
                        {
                            foreach (XmlNode node in rootElement.ChildNodes)
                            {
                                if (node is XmlElement element)
                                {
                                    string name = element.GetAttribute("Name");
                                    string info = element["Info"]?.InnerText.Replace("\n", Environment.NewLine) ?? string.Empty;
                                    RecallItem item = new(name, info);
                                    comboBox1.Items.Add(item);
                                }
                            }
                        }
                        else
                        {
                            RecallInfo.Info?.EchoText("Invalid XML structure.");
                        }
                    }
                    catch (Exception ex)
                    {
                        RecallInfo.Info?.EchoText($"Failed to load XML: {ex.Message}");
                    }
                }
                else
                {
                    RecallInfo.Info?.EchoText("XML file not found.");
                }
            }
            else
            {
                RecallInfo.Info?.EchoText("Plugin path is not set.");
            }
        }
    }

    public class RecallItem
    {
        public string Name { get; set; }
        public string Info { get; set; }

        public RecallItem(string name, string info)
        {
            Name = name;
            Info = info;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
