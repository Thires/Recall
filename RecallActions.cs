using System.Xml;

namespace Recall
{
    internal class RecallActions
    {
        private static RecallForm? Form;

        public static void Initialize()
        {
            Form ??= new RecallForm();
        }

        public static void Display(string name, string info)
        {
            RecallInfo.Info?.EchoText($"\n--- {name} ---\n{info}\n");
        }

        public static string GetInfoByName(string name)
        {
            if (Form != null)
            {
                foreach (var item in Form.GetComboBoxItems())
                {
                    if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        return item.Info.Replace("&#xA;", "\n");
                    }
                }
            }

            string pluginPath = RecallInfo.Info?.get_Variable("PluginPath") ?? string.Empty;
            if (!string.IsNullOrEmpty(pluginPath))
            {
                string xmlPath = Path.Combine(pluginPath, "Recall.xml");
                if (File.Exists(xmlPath))
                {
                    XmlDocument xmlDocument = new();
                    xmlDocument.Load(xmlPath);
                    XmlElement? rootElement = xmlDocument.DocumentElement;

                    if (rootElement != null)
                    {
                        foreach (XmlNode node in rootElement.ChildNodes)
                        {
                            if (node is XmlElement element && element.GetAttribute("Name").Equals(name, StringComparison.OrdinalIgnoreCase))
                            {
                                return element["Info"]?.InnerText.Replace("&#xA;", "\n") ?? string.Empty;
                            }
                        }
                    }
                }
            }

            return string.Empty;
        }

        public static void AddNote(string name, string info)
        {
            string encodedInfo = info.Replace("\\n", "\n");

            if (Form != null)
            {
                var existingItem = Form.GetComboBoxItems().FirstOrDefault(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (existingItem != null)
                {
                    existingItem.Info = encodedInfo;
                }
                else
                {
                    RecallItem newItem = new(name, encodedInfo);
                    Form.Invoke((Action)(() => Form.comboBox1.Items.Add(newItem)));
                }

                // Sort items alphabetically
                Form.Invoke((Action)(() => SortComboBoxItems()));

                Form.Invoke((Action)(() => Form.SaveToXML()));
            }

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
                    var nodes = rootElement.GetElementsByTagName("Item");
                    XmlElement? existingElement = null;
                    foreach (XmlElement element in nodes)
                    {
                        if (element.GetAttribute("Name").Equals(name, StringComparison.OrdinalIgnoreCase))
                        {
                            existingElement = element;
                            break;
                        }
                    }

                    if (existingElement != null)
                    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        existingElement["Info"].InnerXml = encodedInfo;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    }
                    else
                    {
                        XmlElement newItemNode = xmlDocument.CreateElement("Item");
                        newItemNode.SetAttribute("Name", name);
                        XmlElement infoElement = xmlDocument.CreateElement("Info");
                        infoElement.InnerXml = encodedInfo; // Use InnerXml to handle encoding
                        newItemNode.AppendChild(infoElement);
                        rootElement.AppendChild(newItemNode);
                    }

                    // Sort XML elements alphabetically before saving
                    var sortedNodes = rootElement.ChildNodes.Cast<XmlElement>().OrderBy(e => e.GetAttribute("Name")).ToList();
                    rootElement.RemoveAll();
                    foreach (var sortedNode in sortedNodes)
                    {
                        rootElement.AppendChild(sortedNode);
                    }

                    xmlDocument.Save(xmlPath);
                    RecallInfo.Info?.EchoText($"Recall note for '{name}' has been added/updated.");
                }
                else
                {
                    RecallInfo.Info?.EchoText("Failed to access XML root element.");
                }
            }
        }

        private static void SortComboBoxItems()
        {
            if (Form != null)
            {
                var sortedItems = Form.comboBox1.Items.Cast<RecallItem>().OrderBy(item => item.Name).ToList();
                Form.comboBox1.Items.Clear();
                foreach (var item in sortedItems)
                {
                    Form.comboBox1.Items.Add(item);
                }
            }
        }

        public static void RemoveNote(string name)
        {
            if (Form != null)
            {
                var existingItem = Form.GetComboBoxItems().FirstOrDefault(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (existingItem != null)
                {
                    Form.Invoke((Action)(() => Form.comboBox1.Items.Remove(existingItem)));
                }
                Form.Invoke((Action)(() => Form.SaveToXML()));
            }

            string pluginPath = RecallInfo.Info?.get_Variable("PluginPath") ?? string.Empty;
            if (!string.IsNullOrEmpty(pluginPath))
            {
                string xmlPath = Path.Combine(pluginPath, "Recall.xml");
                if (File.Exists(xmlPath))
                {
                    XmlDocument xmlDocument = new();
                    xmlDocument.Load(xmlPath);
                    XmlElement? rootElement = xmlDocument.DocumentElement;

                    if (rootElement != null)
                    {
                        var nodes = rootElement.GetElementsByTagName("Item");
                        XmlElement? existingElement = null;
                        foreach (XmlElement element in nodes)
                        {
                            if (element.GetAttribute("Name").Equals(name, StringComparison.OrdinalIgnoreCase))
                            {
                                existingElement = element;
                                break;
                            }
                        }

                        if (existingElement != null)
                        {
                            rootElement.RemoveChild(existingElement);
                            xmlDocument.Save(xmlPath);
                            RecallInfo.Info?.EchoText($"Recall note for '{name}' has been removed.");
                        }
                        else
                        {
                            RecallInfo.Info?.EchoText($"No recall note found for '{name}' to remove.");
                        }
                    }
                    else
                    {
                        RecallInfo.Info?.EchoText("Failed to access XML root element.");
                    }
                }
                else
                {
                    RecallInfo.Info?.EchoText("XML file not found.");
                }
            }
        }
    }
}