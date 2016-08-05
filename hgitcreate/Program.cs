using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;


namespace hgitcreate
{
  
    class Program
    {
        static string home_path = Directory.GetCurrentDirectory();

        static string git_hash(string[] head, string path)     
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
                    if (path != "")
                        Directory.SetCurrentDirectory(path);
                    Process git = new Process();         
                    git.StartInfo = new ProcessStartInfo(f[0], " --login -i");//задаем имя исполняемого файла                    
                    git.StartInfo.RedirectStandardInput = true;// перенаправить вход
                    git.StartInfo.RedirectStandardOutput = true;//перенаправить выход
                    git.StartInfo.UseShellExecute = false;//обязательный параметр, для работы предыдущих                
                    git.Start();
                    git.StandardOutput.ReadLine();
                    git.StandardOutput.ReadLine();
                    git.StandardOutput.ReadLine();
                    git.StandardOutput.ReadLine();
                    git.StandardOutput.ReadLine();         
                    git.StandardInput.WriteLine("git log");         
                    git_hash = git.StandardOutput.ReadLine();
                    git_hash = git_hash.Substring(7, 7);          
                    Directory.SetCurrentDirectory(home_path);
                    git.Close();
                }
                else
                {
                    Console.Write("git(sh.exe) not found");
                }

            }
            return git_hash;
        }

      static string git_commit(string[] head, string path)
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
                  if (path != "")
                  Directory.SetCurrentDirectory(path);
                  git.StartInfo = new ProcessStartInfo(f[0], " --login -i");//задаем имя исполняемого файла
                  git.StartInfo.RedirectStandardInput = true;// перенаправить вход
                  git.StartInfo.RedirectStandardOutput = true;//перенаправить выход
                  git.StartInfo.UseShellExecute = false;//обязательный параметр, для работы предыдущих                
                  git.Start();
                  git.StandardOutput.ReadLine();
                  git.StandardOutput.ReadLine();
                  git.StandardOutput.ReadLine();
                  git.StandardOutput.ReadLine();          
                  git.StandardOutput.ReadLine();                
                  git.StandardInput.WriteLine("git status");                
                  git_commit = "/* ";                 
                  StreamReader myStreamReader = git.StandardOutput;
                  git_commit += myStreamReader.ReadLine() + " \n ";        
                  while (myStreamReader.Peek() != -1)
                  git_commit += myStreamReader.ReadLine() + " \n ";
                  git.StandardInput.WriteLine("git status -s | wc -l");
                  git_commit += " */ \n //" + myStreamReader.ReadLine().Trim();               
                  Directory.SetCurrentDirectory(home_path);
                  git.Close();
              }
              else
              {
                  Console.Write("git(sh.exe) not found");
              }

          }
          return git_commit;
      }

      static string git_revlist(string[] head, string path)
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
                  if (path != "")
                      Directory.SetCurrentDirectory(path);
                  Process git = new Process();
                  git.StartInfo = new ProcessStartInfo(f[0], " --login -i");//задаем имя исполняемого файла
                  git.StartInfo.RedirectStandardInput = true;// перенаправить вход
                  git.StartInfo.RedirectStandardOutput = true;//перенаправить выход
                  git.StartInfo.UseShellExecute = false;//обязательный параметр, для работы предыдущих
                  git.Start();
                  git.StandardOutput.ReadLine();
                  git.StandardOutput.ReadLine();
                  git.StandardOutput.ReadLine();
                  git.StandardOutput.ReadLine();
                  git.StandardOutput.ReadLine();
                  git.StandardInput.WriteLine("git rev-list HEAD --count");
                  git_revlist = git.StandardOutput.ReadLine();
                  Directory.SetCurrentDirectory(home_path);
                  git.Close();
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

                        string par_dir = cur_head[1].Substring(1, 1);
                        int j = Convert.ToInt32(par_dir);
                        if (j != 0)
                        {
                            DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory());
                            for (int x = 0; x < j; x++)
                            {
                                info = Directory.GetParent(info.FullName);
                                par_dir = info.FullName;
                            }
                        }
                        else
                            par_dir = "";
                         cur_head[1] = cur_head[1].Remove(0, 2);
                        string path = par_dir + cur_head[1];
                        File.Delete(path + cur_head[0] + ".h");
                        File.Copy(path + cur_head[0] + ".template", path + cur_head[0] + ".h");
                        string cur_hash = git_hash(cur_head, path);
                        string cur_commit = git_commit(cur_head, path);               
                        string str = string.Empty;
                        using (System.IO.StreamReader reader = System.IO.File.OpenText(path + cur_head[0] + ".h"))
                        {
                            str = reader.ReadToEnd();
                        }

                        str = str.Replace("$git_namespace", cur_head[0]);  // Хэш данного коммита
                        str = str.Replace("$git_countFromHead", git_revlist(cur_head, path));
                        str = str.Replace("$git_numUnrevisionedFiles", cur_commit.Substring(cur_commit.Length - 2, 2).TrimStart('/')); 
                        str = str.Replace("$git_hash", cur_hash);  // Хэш данного коммита
                        str = str.Replace("$git_timeNow_second", DateTime.Now.Second.ToString());  // Текущая время-дата
                        str = str.Replace("$git_timeNow_minute", DateTime.Now.Minute.ToString());  // Текущая время-дата
                        str = str.Replace("$git_timeNow_hour", DateTime.Now.Hour.ToString());  // Текущая время-дата
                        str = str.Replace("$git_timeNow_day", DateTime.Now.Day.ToString());  // Текущая время-дата
                        str = str.Replace("$git_timeNow_month", DateTime.Now.Month.ToString());  // Текущая время-дата
                        str = str.Replace("$git_timeNow_year", DateTime.Now.Year.ToString());  // Текущая время-дата
                        str = str.Replace("$git_commit", cur_commit);  // состояние коммита



                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path + cur_head[0] + ".h"))
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
            }
        }


    }
}
