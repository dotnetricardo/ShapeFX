$path =  [IO.Directory]::GetParent((Get-Item -Path ".\" -Verbose).FullName).Parent.FullName
$regex = "(?<=class\s)(.*?)(?=\s)"
#$date = $(get-date -f MM-dd-yyyy_HH_mm_ss) 

Remove-Item -Recurse -Force ".\.shape"
New-Item ..\.shape -ItemType Directory -Force | Out-Null
New-Item ..\.shape\.snapshot -ItemType Directory -Force | Out-Null
New-Item ..\.shape\.bin -ItemType Directory -Force | Out-Null

[IO.Directory]::GetFiles($path, "*.cs", "AllDirectories") | % {
	$text = [IO.File]::ReadAllText($_)
	
	if ($text.Contains("[Shape(")) {
	
		$file = "..\.shape\" + [regex]::Match($text, $regex) + ".shape"
		
		[IO.File]::WriteAllText($file, $text)
		
		copy-item $file ..\.shape\.snapshot -Recurse -Force
		
	} 
 }

