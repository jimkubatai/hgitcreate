/*

2 июля 2015 г. 16:44:06


On branch master
Your branch is up-to-date with 'origin/master'.

Changes not staged for commit:
  (use "git add <file>..." to update what will be committed)
  (use "git checkout -- <file>..." to discard changes in working directory)

	modified:   code/exchange.cpp
	modified:   code/mto_slave.c
	modified:   code/mto_slave.h
	modified:   code/mto_slave.od

no changes added to commit (use "git add" and/or "git commit -a")
*/

#ifndef _GIT_VERSION_lpc2000_Bumerang_Mto_H_
#define _GIT_VERSION_lpc2000_Bumerang_Mto_H_

#include <ctime>

namespace lpc2000_Bumerang_Mto
{
  const unsigned int countFromHead = 7;
  const unsigned int hash = 0x70ed2e1;
  const unsigned int numUnrevisionedFiles = 4;
  const std::tm timeNow =
  {
    6,
    44,
    16,
    2,
    7,
    2015 - 1900
  };
}
#endif
