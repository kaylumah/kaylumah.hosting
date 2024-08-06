#Requires -Version 7.4

# See: https://git-scm.com/docs/git-diff
# $DiffOutput = git diff --name-only HEAD^ HEAD
$DiffOutput = git diff --name-status HEAD^ HEAD
# git diff --stat HEAD^ HEAD

$AssetFolderDiff = $diff | Where-Object { $_ -match '^assets/' }
$AssestFolderChanged = $AssetFolderDiff.Length -gt 0

[PSCustomObject]@{
    ChangedFiles = $DiffOutput
    Assets = $AssestFolderChanged
}