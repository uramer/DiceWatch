function ToPng($name) {
	$from = "$name.xcf"
	$to = "$name.png"
	magick convert -background none $from -flatten $to
}

Get-ChildItem "." -Filter *.xcf | 
Foreach-Object {
    $name = $_.Basename
	ToPng $name
}

Copy-Item "*.png" -Destination "../Dice/res" -Force

cd ./Dice
./build.ps1
cd ..