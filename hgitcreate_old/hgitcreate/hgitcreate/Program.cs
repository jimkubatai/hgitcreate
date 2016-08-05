using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;


namespace hgitcreate
{
  
    class Program
    {
      static string git_hash(string[] head)
        {
            string git_hash = "0";

            string[] f = Directory.GetFiles(@"C:\Program Files (x86)\Git\bin", "sh.exe"); //ищем git
            if (f.Length == 0)
            {
                f = Directory.GetFiles(@"C:\Program Files\Git\bin", "sh.exe");
            }
            else
            {
                if (f.Length != 0)
                {
                    Process git = new Process();
                    git.StartInfo = new ProcessStartInfo(f[0], " --login -i");//задаем имя исполняемого файла
                    //git.StartInfo.CreateNoWindow = true;//не создавать окно
                    //git.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    git.StartInfo.RedirectStandardInput = true;// перенаправить вход
                    git.StartInfo.RedirectStandardOutput = true;//перенаправить выход
                    git.StartInfo.UseShellExecute = false;//обязательный параметр, для работы предыдущих
                   // Process.Start(f[0], " --login -i");
                    git.Start();
                    git.StandardOutput.ReadLine();
                    git.StandardOutput.ReadLine();
                    git.StandardOutput.ReadLine();
                    git.StandardOutput.ReadLine();
                    git.StandardOutput.ReadLine();
                    git.StandardInput.WriteLine("git log");
                    git_hash = git.StandardOutput.ReadLine();
                    git_hash = git_hash.Substring(7, 7);
                    
                    
                   

                   
                
                }
                else
                {
                    Console.Write("git(sh.exe) not found");
                }

            }
            return git_hash;
        }

      static string git_commit(string[] head)
      {
          string git_commit = "0";

          string[] f = Directory.GetFiles(@"C:\Program Files (x86)\Git\bin", "sh.exe"); //ищем git
          if (f.Length == 0)
          {
              f = Directory.GetFiles(@"C:\Program Files\Git\bin", "sh.exe");
          }
          else
          {
              if (f.Length != 0)
              {
                  Process git = new Process();
                  git.StartInfo = new ProcessStartInfo(f[0], " --login -i");//задаем имя исполняемого файла
                  //git.StartInfo.CreateNoWindow = true;//не создавать окно
                  //git.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                  git.StartInfo.RedirectStandardInput = true;// перенаправить вход
                  git.StartInfo.RedirectStandardOutput = true;//перенаправить выход
                  git.StartInfo.UseShellExecute = false;//обязательный параметр, для работы предыдущих
                  // Process.Start(f[0], " --login -i");
                  git.Start();
                  git.StandardOutput.ReadLine();
                  git.StandardOutput.ReadLine();
                  git.StandardOutput.ReadLine();
                  git.StandardOutput.ReadLine();
                  git.StandardOutput.ReadLine();
                  git.StandardInput.WriteLine("git status -s");
                  git_commit = "/* ";
                  int i = 1;
                  git_commit += git.StandardOutput.ReadLine() + " \n ";
                  while (git.StandardOutput.Peek() != -1)
                  {
                      i++;
                      git_commit += git.StandardOutput.ReadLine() + " \n ";
                      
                  }

                  git_commit += " */ \n //" + i.ToString();






              }
              else
              {
                  Console.Write("git(sh.exe) not found");
              }

          }
          return git_commit;
      }

      static string git_revlist(string[] head)
      {
          string git_revlist = "0";

          string[] f = Directory.GetFiles(@"C:\Program Files (x86)\Git\bin", "sh.exe"); //ищем git
          if (f.Length == 0)
          {
              f = Directory.GetFiles(@"C:\Program Files\Git\bin", "sh.exe");
          }
          else
          {
              if (f.Length != 0)
              {
                  Process git = new Process();
                  git.StartInfo = new ProcessStartInfo(f[0], " --login -i");//задаем имя исполняемого файла
                  //git.StartInfo.CreateNoWindow = true;//не создавать окно
                  //git.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                  git.StartInfo.RedirectStandardInput = true;// перенаправить вход
                  git.StartInfo.RedirectStandardOutput = true;//перенаправить выход
                  git.StartInfo.UseShellExecute = false;//обязательный параметр, для работы предыдущих
                  // Process.Start(f[0], " --login -i");
                  git.Start();
                  git.StandardOutput.ReadLine();
                  git.StandardOutput.ReadLine();
                  git.StandardOutput.ReadLine();
                  git.StandardOutput.ReadLine();
                  git.StandardOutput.ReadLine();
                  git.StandardInput.WriteLine("git rev-list HEAD --count");
                  git_revlist = git.StandardOutput.ReadLine();
              }
              else
              {
                  Console.Write("git(sh.exe) not found");
              }

          }
          return git_revlist;
      }


        static void Main(string[] args)
        {
            try
            {
                string[] heads = File.ReadAllLines("config.csv");
                if (heads.Length > 0)
                {
                    for (int i = 0; i < heads.Length; i++)
                    {
                        string[] cur_head = heads[i].Split(';');
                        File.Delete(cur_head[1] + cur_head[0] + ".h");
                        File.Copy(cur_head[1] + cur_head[0] + ".template", cur_head[1] + cur_head[0] + ".h");

                        string cur_hash = git_hash(cur_head);
                        string cur_commit = git_commit(cur_head);
                        //Console.ReadKey();


                        string str = string.Empty;
                        using (System.IO.StreamReader reader = System.IO.File.OpenText(cur_head[1] + cur_head[0] + ".h"))
                        {
                            str = reader.ReadToEnd();
                        }

                        str = str.Replace("$git_namespace", cur_head[0]);  // Хэш данного коммита

                        str = str.Replace("$git_countFromHead", git_revlist(cur_head));
                        str = str.Replace("$git_numUnrevisionedFiles", cur_commit.Substring(cur_commit.Length - 1, 1)); 

                        str = str.Replace("$git_hash", cur_hash);  // Хэш данного коммита
                        str = str.Replace("$git_timeNow_second", DateTime.Now.Second.ToString());  // Текущая время-дата
                        str = str.Replace("$git_timeNow_minute", DateTime.Now.Minute.ToString());  // Текущая время-дата
                        str = str.Replace("$git_timeNow_hour", DateTime.Now.Hour.ToString());  // Текущая время-дата
                        str = str.Replace("$git_timeNow_day", DateTime.Now.Day.ToString());  // Текущая время-дата
                        str = str.Replace("$git_timeNow_month", DateTime.Now.Month.ToString());  // Текущая время-дата
                        str = str.Replace("$git_timeNow_year", DateTime.Now.Year.ToString());  // Текущая время-дата
                        str = str.Replace("$git_commit", cur_commit);  // состояние коммита



                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(cur_head[1] + cur_head[0] + ".h"))
                        {
                            file.Write(str);
                        }


                    }
                }
                else
                {
                    Console.WriteLine("Конфигурационный файл пусть");
                }
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
             //   Console.ReadKey();
            }
        }


    }
}
