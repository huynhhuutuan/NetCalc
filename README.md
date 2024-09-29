**Required:**
- .NET 8.x

```bash
# Publish
rm -rf .publish/ && dotnet publish NetCalc.csproj -c Release -o .publish/

# Install on MacOS
rm -rf /Applications/QuickTools/netcalc/
cp -r .publish /Applications/QuickTools/netcalc

# Add this line into `%user%/.zshrc`
export PATH=$PATH:/Applications/QuickTools/netcalc

# Verify app in terminal like: `% which calc`
# How to use
% calc 1+2
# Or:
% calc "1 + 2"

# Result:
1+2 = 3

# Show history
% calc history
```
