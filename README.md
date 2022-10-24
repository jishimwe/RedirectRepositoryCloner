# Remote Development and Compilation
___

**Author** : *Ishimwe Jean-Paul*

**Noma** : *6919-12-00*
___

## Redirect Repository Cloner

This program will clone the necessary repository for a *Redirect Files* Project to work. Namely :

- The *Redirect Files* repository
- The *Real* Repository, but it will not download the files.

To launch the program, use the following arguments:
```
-s <Real repository> : The url for the project repository
-d <Destination path>: The path where to put the cloned project repository
-r <Redir repository>: The url for the redir repository
-p <Redir path>      : The path where to put the cloned redir repository
-t <Path to token>   : The path to a file containing a git token (not needed if public repo)
```