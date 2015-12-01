using System;
using System.IO;

namespace Quail
{
    public delegate void DelegateAddString(String s);

    class TextFileMonitor
    {
        DelegateAddString mAddStringFunc = null;
        Form1 mMainForm = null;
        string mFilename = null;
        int mSleepTimeout = 1000;
        long mMaxBytesToLoad = 100000000;

        public TextFileMonitor(Form1 mainForm, string filename, DelegateAddString addStringFunc, int timeout, long maxBytes)
        {
            mMainForm = mainForm;  // reference to main form so we can invoke our add string delegate
            mAddStringFunc = addStringFunc;
            mFilename = filename;
            mSleepTimeout = timeout;
            mMaxBytesToLoad = maxBytes;
        }

        public void DoTail()
        {
            if (!File.Exists(mFilename))
            {
                mMainForm.tbSelected.Text = "I couldn't find the file: " + mFilename;
                return;
            }

            using (StreamReader reader = new StreamReader(new FileStream(mFilename,
                      FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                object[] args = new object[1];
                string title = mFilename;

                DateTime dt = System.DateTime.Now;
                // mMainForm.Text = title + " (Loading file...)";

                mMainForm.lbLog.BeginUpdate();
                int i = 0;
                bool isCompleteFile = true;

                long fileLength = reader.BaseStream.Length;
                if (fileLength > mMaxBytesToLoad)
                {
                    reader.BaseStream.Seek(fileLength - mMaxBytesToLoad, SeekOrigin.Begin);
                    isCompleteFile = false;
                }

                // Quickly do our initial file load, without calling delegate, and without drawing...
                // Thread safe?  No, but fast
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();

                    // Apply Filter
                    if (mMainForm.cbFilterOn.Checked)
                    {
                        if (!line.ToLower().Contains(mMainForm.tbFilterString.Text.ToLower()))
                            continue;
                    }

                    mMainForm.mItems.Add(line);
                    i++;
                }
                mMainForm.lbLog.Count = i;
                mMainForm.lbLog.EndUpdate();
                TimeSpan dt2 = System.DateTime.Now.Subtract(dt);

                mMainForm.lbLog.SelectedIndex = mMainForm.mItems.Count - 1;

                if (isCompleteFile)
                {
                    mMainForm.Text = title + " (Complete file loaded in " + dt2.TotalSeconds.ToString() + " seconds)";
                }
                else
                {
                    mMainForm.Text = title + " (Partial file loaded in " + dt2.TotalSeconds.ToString() + " seconds)";
                }

                // Make memory footprint small
                MemoryManagement.FlushMemory();

                //start at the end of the file
                long lastMaxOffset = reader.BaseStream.Length;

                // Tail Loop
                while (true)
                {
                    System.Threading.Thread.Sleep(mSleepTimeout);

                    // Has file size changed?
                    if (reader.BaseStream.Length == lastMaxOffset)
                        continue;

                    if (reader.BaseStream.Length < lastMaxOffset)
                    {
                        mMainForm.ClearLog();
                        DoTail();
                    }

                    // Seek to the last max offset
                    reader.BaseStream.Seek(lastMaxOffset, SeekOrigin.Begin);

                    // Read to end of file
                    args[0] = reader.ReadToEnd();

                    // Invoke add string delegate
                    mMainForm.Invoke(mAddStringFunc, args);

                    // Update the last max offset
                    lastMaxOffset = reader.BaseStream.Position;

                }
            }
        }

    }
}
