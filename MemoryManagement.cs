using System;
using System.Runtime.InteropServices;

namespace Quail
{
    public class MemoryManagement
    {

        [DllImportAttribute("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        public static void FlushMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                // Minimize Working Set Size:  The pages are removed from the caller's working set. If 
                // there are no other processes that are mapping these pages, then they become 
                // candidates to be written to backing store.  Doesn't really buy us much, but if 
                // someone is looking at Mem Usage when we have loaded a huge file, it looks more 
                // reasonable.  Of course our VM Size is still up there.  So who are we fooling?
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
    }
}
