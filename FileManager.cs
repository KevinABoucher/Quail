using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;

namespace Quail
{
    public class FileManager
    {
        public FileManager()
        {
        }

        // Copy directory structure recursively
        public static void CopyDirectory(string Src, string Dst)
        {
            String[] Files;

            if (Dst[Dst.Length - 1] != Path.DirectorySeparatorChar)
            {
                Dst += Path.DirectorySeparatorChar;
            }

            if (!Directory.Exists(Dst))
            {
                Directory.CreateDirectory(Dst);
            }

            Files = Directory.GetFileSystemEntries(Src);
            foreach (string Element in Files)
            {
                if (Directory.Exists(Element))
                {
                    // Sub directories
                    CopyDirectory(Element, Dst + Path.GetFileName(Element));
                }
                else
                {
                    // Files in directory
                    File.Copy(Element, Dst + Path.GetFileName(Element), true);
                }
            }
        }

        public static string GetFileAndRead(string extension)
        {
            string s = "";
            string filter = extension + " files (*." + extension + ")|*." + extension;
            string fileName = FileManager.SelectFile("Open file *." + extension, filter, Application.ExecutablePath);

            if (fileName.Length > 0)
            {
                s = FileManager.ReadTextFile(fileName);
            }
            return s;
        }

        public static void GetFileAndSave(string extension, string saveString)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = extension;
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                if (saveFile.FileNames.Length > 0)
                {
                    FileManager.WriteTextFile(saveFile.FileName, saveString);
                }
            }
        }

        public static void WriteTextFile(string filename, string writeString)
        {
            if (filename.Length > 0)
            {
                try
                {
                    using (StreamWriter file = new StreamWriter(filename))
                    {
                        file.Write(writeString);
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message.ToString());
                }
            }
        }


        public static void AppendToFile(string filename, string writeString)
        {
            if (filename.Length > 0)
            {
                try
                {
                    using (StreamWriter file = new StreamWriter(File.Open(filename, FileMode.Append, FileAccess.Write, FileShare.ReadWrite)))
                    {
                        file.Write(writeString);
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message.ToString());
                }
            }
        }

        public static string ReadTextFile(string filename)
        {
            string s = "";
            try
            {
                using (StreamReader sr = File.OpenText(filename))
                {
                    s = sr.ReadToEnd();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString());
            }
            return s;
        }

        public static List<string> ReadTextFileIntoList(string filename)
        {
            List<string> list = new List<string>();
            string line = null;

            try
            {
                using (StreamReader sr = File.OpenText(filename))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        list.Add(line);
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString());
            }

            return list;
        }

        public static string SelectFile(string message, string filter, string initialDir)
        {
            string filename = "";
            OpenFileDialog openFile;
            openFile = new OpenFileDialog();
            openFile.Title = message;
            openFile.Multiselect = false;
            openFile.InitialDirectory = initialDir;
            openFile.Filter = filter; // example: "txt files (*.txt)|*.txt|All files (*.*)|*.*"
            openFile.RestoreDirectory = true;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                if (openFile.FileNames.Length > 0)
                {
                    filename = openFile.FileName;
                }
            }
            return filename;
        }

        public static string SelectDirectory(string message)
        {
            string filename = "";
            FolderBrowserDialog openFolder;
            openFolder = new FolderBrowserDialog();
            openFolder.Description = message;

            if (openFolder.ShowDialog() == DialogResult.OK)
            {
                filename = openFolder.SelectedPath;
            }
            return filename;
        }

        public static void EditFile(string aFilename, string editCommand)
        {
            Process logEdit = new Process();
            string cmd = String.Format(editCommand, aFilename);
            int firstSpace = cmd.IndexOf(" ");
            string filename = "";
            string args = "";
            if (firstSpace >= 0)
            {
                filename = cmd.Substring(0, firstSpace);
                args = cmd.Substring(firstSpace + 1, (cmd.Length - 1) - (firstSpace));
            }
            else
            {
                filename = cmd;
            }

            logEdit.StartInfo.FileName = filename;
            logEdit.StartInfo.Arguments = args;

            logEdit.Start();
        }
    }
}
