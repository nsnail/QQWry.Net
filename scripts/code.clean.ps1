jb cleanupcode --no-build --include = $( $( git status --porcelain | Where-Object { $_ -match "^\s*[MA]" } | ForEach-Object { $_.TrimStart(" M").TrimStart(" A") } ) -join ";" ) ../QQWry.Net.sln
dot rbom -w -e refs -e .git -e node_modules ../
dot trim -w -e refs -e .git -e node_modules ../
dot tolf -w -e refs -e .git -e node_modules ../