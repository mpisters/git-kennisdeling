# git-kennisdeling

### basic commands

```bash
git status
```
```bash
git add .
git add -p
```
```bash
git commit
git commit -m "some commit message"
git commit --amend
```

```bash
git checkout <branch_name>
git checkout -
git checkout -p
git checkout -b <new_branch_name>
```

```bash
git stash
git stash pop
git stash list
git stash apply <number_stash>
```

### updating branch
```bash
git rebase <branch_name>
git rebase -i <branch_name>
git rebase --continue
git rebase --skip
git rebase --abort
```

```bash
git merge <branch_name>
git merge --abort
```

```bash
git cherry-pick <commit-message-id>
git cherry-pick --continue
git cherry-pick --abort
```

```bash
git push origin HEAD
git push origin HEAD -f
```

### showing git history and searching files
```bash
git log
git reflog
```

```bash
git grep
```

