/* On branch master 
 Changes not staged for commit: 
   (use "git add/rm <file>..." to update what will be committed) 
   (use "git checkout -- <file>..." to discard changes in working directory) 
  
 	deleted:    ../../../config.csv 
 	deleted:    ../../../git_header_maker/git_test1.template 
 	deleted:    ../../../git_header_maker/git_test2.h 
 	deleted:    ../../../git_header_maker/git_test2.template 
 	deleted:    ../../../git_header_maker/git_version_lpc2000_Bumerang_Mto.h 
 	deleted:    ../../../git_header_maker/git_version_lpc2000_Bumerang_Mto.template 
 	deleted:    ../../../hgitcreate.exe 
 	modified:   ../../Program.cs 
 	deleted:    ../../git_header_maker/git_test1.h 
 	deleted:    ../../git_header_maker/git_test1.template 
 	deleted:    ../../git_header_maker/git_test2.h 
 	deleted:    ../../git_header_maker/git_test2.template 
 	deleted:    ../../git_header_maker/git_version_lpc2000_Bumerang_Mto.h 
 	deleted:    ../../git_header_maker/git_version_lpc2000_Bumerang_Mto.template 
 	modified:   ../../../main exe/config.csv 
 	deleted:    ../../../main exe/hello.txt 
 	modified:   ../../../main exe/hgitcreate.exe 
  
 Untracked files: 
   (use "git add <file>..." to include in what will be committed) 
  
 	../../../../ConsoleApplication1/ 
 	../../gitgit/ 
  
 no changes added to commit (use "git add" and/or "git commit -a") 
  */ 
 //19

#ifndef git_test1
#define git_test1

#include <ctime>

namespace git_test1
{
  const unsigned int countFromHead = 6;
  const unsigned int hash = 0xc8d89f2;
  const unsigned int numUnrevisionedFiles = 19;
  const std::tm timeNow =
  {
    53,
    14,
    11,
    8,
    7,
    2015 - 1900
  };
}
#endif
