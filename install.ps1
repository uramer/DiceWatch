$sdb = "D:\programming\Watch\tools\sdb"
$count = ("$sdb devices | measure-object -line" | Invoke-Expression).count
"$count devices" | Write-Output
if($count -eq 1)
{
  "$sdb connect 192.168.64.105:26101" | Invoke-Expression
}
"$sdb install Dice\bin\Release\tizen40\urm.dice-1.0.1.tpk" | Invoke-Expression