# Define the folder containing the generated files
$folder = "Generated"

# Define replacement patterns and their replacements
$replacements = @(
    @{ Pattern = 'public\s+partial\s+class\s+(\S+)\s+:\s+Parser'; Replacement = 'internal partial class $1 : Parser' },
    @{ Pattern = 'public\s+partial\s+class\s+(\S+)\s+:\s+Lexer'; Replacement = 'internal partial class $1 : Lexer' },
    @{ Pattern = 'public\s+partial\s+class\s+(\S+)\s+:\s+AbstractParseTreeVisitor'; Replacement = 'internal partial class $1 : AbstractParseTreeVisitor' },
    @{ Pattern = 'public\s+partial\s+class\s+(\S+)\s+:\s+(\S+)Listener'; Replacement = 'internal partial class $1 : $2Listener' },
    @{ Pattern = 'public\s+interface'; Replacement = 'internal interface' }
)

# Process each .cs file in the specified folder
Get-ChildItem -Path $folder -Filter *.cs -Recurse | ForEach-Object {
    $filePath = $_.FullName
    $content = Get-Content -Path $filePath
    # Apply each replacement
    foreach ($rule in $replacements) {
        $content = $content -replace $rule.Pattern, $rule.Replacement
    }
    # Save the modified content back to the file
    Set-Content -Path $filePath -Value $content
}

Write-Host "Modifier replacements completed for all files in '$folder'."