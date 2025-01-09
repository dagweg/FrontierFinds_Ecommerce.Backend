# cbf (Clean Build Files) is a script that removes all bin and obj folders from the solution.
# Get-ChildItem -Path ".\Ecommerce.Application" -Recurse -Directory -Filter "bin" | Remove-Item -Recurse -Force
# Get-ChildItem -Path ".\Ecommerce.Application" -Recurse -Directory -Filter "obj" | Remove-Item -Recurse -Force
# Get-ChildItem -Path ".\Ecommerce.Domain" -Recurse -Directory -Filter "bin" | Remove-Item -Recurse -Force
# Get-ChildItem -Path ".\Ecommerce.Domain" -Recurse -Directory -Filter "obj" | Remove-Item -Recurse -Force
# Get-ChildItem -Path ".\Ecommerce.Infrastructure" -Recurse -Directory -Filter "bin" | Remove-Item -Recurse -Force
# Get-ChildItem -Path ".\Ecommerce.Infrastructure" -Recurse -Directory -Filter "obj" | Remove-Item -Recurse -Force
# Get-ChildItem -Path ".\Ecommerce.Presentation\Api" -Recurse -Directory -Filter "bin" | Remove-Item -Recurse -Force
# Get-ChildItem -Path ".\Ecommerce.Presentation\Api" -Recurse -Directory -Filter "obj" | Remove-Item -Recurse -Force
# Get-ChildItem -Path ".\Ecommerce.Presentation\Contracts" -Recurse -Directory -Filter "bin" | Remove-Item -Recurse -Force
# Get-ChildItem -Path ".\Ecommerce.Presentation\Contracts" -Recurse -Directory -Filter "obj" | Remove-Item -Recurse -Force
Get-ChildItem -Path . -Recurse -Directory -Filter "obj" | Remove-Item -Recurse -Force
Get-ChildItem -Path . -Recurse -Directory -Filter "bin" | Remove-Item -Recurse -Force
