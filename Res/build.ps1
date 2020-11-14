function Digit($name, $d, $max) {
	$delete = ""
	For ($i=$max; $i -ge 1; $i--) {
		If ($i -ne $d) {
			$delete = $delete + " -delete $i"
		}
    }
	$cmd = "magick convert -background none $name.xcf$delete -flatten $name-$d.png"
	Write-Host $cmd
	$cmd | Invoke-Expression
}

For ($i=1; $i -le 4; $i++) {
	Digit "D4" $i 4
}

For ($i=1; $i -le 6; $i++) {
	Digit "D6" $i 6
}

For ($i=1; $i -le 8; $i++) {
	Digit "D8" $i 8
}

For ($i=1; $i -le 12; $i++) {
	Digit "D12" $i 12
}

Copy-Item "*.png" -Destination "../Dice/res/dice" -Force