using System;
using System.Collections.Generic;
using System.IO;

namespace Lec5G2
{
    class Program
    {
        static void Main(string[] args)
        {
            VisualUtils vutils = new VisualUtils();
            vutils.VisualMode = VisualMode.List;
            
            Stack<StackItem2> history = new Stack<StackItem2>();
            StackItem2 item = new StackItem2 { DirInfo = new DirectoryInfo(@"C:\Users\Asus\Desktop"), Index = 0 };

            history.Push(item);

            bool isOK = true;

            while (isOK)
            {
                if (vutils.VisualMode ==  VisualMode.List)
                {
                    VisualUtils.ShowFileSystemInfo(history.Peek());
                }
                ConsoleKeyInfo pressedButton = Console.ReadKey();

                switch (pressedButton.Key)
                {
                    case ConsoleKey.UpArrow:
                        history.Peek().Index = history.Peek().Index - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        history.Peek().Index = history.Peek().Index + 1;
                        break;
                    case ConsoleKey.Enter:
                        StackItem2 item2 = new StackItem2();

                        int index = history.Peek().Index;
                        FileSystemInfo[] objs = history.Peek().DirInfo.GetFileSystemInfos();
                        string path = objs[index].FullName;

                        if (objs[index] is DirectoryInfo)
                        {
                            item2.DirInfo = new DirectoryInfo(path);
                            item2.Index = 0;
                            history.Push(item2);
                        }else if(objs[index]  is FileInfo)
                        {
                            vutils.VisualMode = VisualMode.Content;
                            VisualUtils.ShowFileContent(path);
                        }

                        break;
                    case ConsoleKey.Backspace:

                        if (vutils.VisualMode == VisualMode.Content)
                        {
                            vutils.VisualMode = VisualMode.List;
                        }
                        else
                        {
                            history.Pop();
                        }
                        break;
                    case ConsoleKey.Escape:
                        isOK = false;
                        break;
                }
            }
        }
    }
}