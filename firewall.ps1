# Requires running PowerShell as Administrator
$ruleNamePrefix = "DeJonge Server"
$ports = @(8080, 8081)
foreach ($port in $ports) {
    $ruleName = "$ruleNamePrefix $port"
    $existingRule = Get-NetFirewallRule -DisplayName $ruleName -ErrorAction SilentlyContinue
    if (-not $existingRule) {
        New-NetFirewallRule `
            -DisplayName $ruleName `
            -Direction Inbound `
            -Protocol TCP `
            -LocalPort $port `
            -Action Allow `
            -Profile Any `
            -Description "Allow inbound HTTP traffic on port $port"
        Write-Host "Created firewall rule for port $port"
    }
    else {
        Write-Host "Firewall rule for port $port already exists"
    }
}
