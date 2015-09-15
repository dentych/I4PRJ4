# Contributing

If you wish to contribute, there are some standards that needs to be followed.

## Git

If you wish to contribute to the repository, you'll have to follow this commit/push order:

* Make sure to pull *master* to get the latest changes.
* Create a new branch for your edits.
* Make changes in the code.
* Commit the changes you've made.
* Push the changes you've made to the remote equivalent of your branch.

### Git help

This is some help for the commands to pull/clone/push/commit to the repository.

#### Setting up git
If you have just installed git, there are some things you need to set up, before you can download the repository. If you have already set up Git, you can skip to the "downloading the repository" part.

You have to set your name and email address. This will be written on all commits you make.

`git config --global user.name "Username"`
`git config --global user.email "Email"`

*Username* is your Github username.  
*Email* is your Github email address.

#### Downloading the repository

When you first start out, you need to get a new copy of the repository. This action is called "cloning" the repository.  
To clone the repository, you need to execute the following command:

`git clone git@github.com:dentych/I4PRJ4.git`

#### Create a new branch for your edits

The repository is now completely fresh and up-to-date. Now it's time to create a new branch, for all your edits. A branch is a place you can edit code, without messing with the *master* branch code. This way, if you fuck up, your edits can just be discarded without ruining the existing code.

To create a new branch and go to it, write:  
`git checkout -b <name>`

Instead of *<name>*, you should write a name for your branch (e.g. newBranch). The name should be something that tells others what is happening in that particular branch. If you are adding a new button in the GUI, you can call it something that will make people know that just by looking at the branch name. Examples: *GUIAddButton, NewGUIButton, Kasper-GUI-AddButton*

If you want to switch between existing branches, use:  
`git checkout <name>`  
Notice the missing *-b* option.

#### Pull changes from others

.If you have cloned the repository a long time ago, others might have pushed changes to the remote repo. This means that you have to "pull" these changes to your local repo.

To pull changes, switch to the *master* branch and execute:  
`git pull`

#### Commit changes you've made

When you made changes to files, you'll have to commit those changes. Committing means writing the changes into the git history. 

There are some states of the files that you have to be aware of.

* Untracked files
 * Files that git does not track and which is only present in your local repo.
* Changes not staged for commit
 * Files that git is tracking that you have made changes to.
* Changes to be committed
 * Changes that have been changed and put up to be committed.

Generally, you'll be working with editing existing files. This means that you will most often see the *"Changes not staged for commit"* message, if you execute `git status`.  
When the files are modified AND tracked, you can set the files up for commit AND do the commit in one take.

To do so, write:  
`git commit -a`  
This will open an editor, where you are asked to write a "commit message". This is the message that will be shown in the git history log, and has to state what changes this particular commit introduced to the source code. Example: *Added handler for Next-button click*.  
The messages can only be of a certain length (around 60 chars), so your messages have to be precise and short.

If you want to commit and write a message in the console line, you can issue the *-m* option:  
`git commit -am "This is a commit message"`

#### Push changes to remote repo

Now it's time to push your changes to the remote repo for everyone else to see.

To do this, write:  
`git push`

If git is complaining about something regarding "push.default", you should issue the command git tells you, that includes *"push.default simple"*. This means you have to only write `git push` instead of `git push origin <branch>`

That's basically it. The more you do it, the better you get at it.

## Git commands

This is a lookup list for the most used commands and their usage.

First Header  | Second Header
------------- | -------------
Content Cell  | Content Cell
Content Cell  | Content Cell
