# Guncharted.PackageUpdater

### First - pack
    dotnet pack
### Then install:
    dotnet tool install --global --add-source ./nupkg Guncharted.PackageUpdater
### Go ahead and use
    pkgupd -p MyProject.Abstractions -v 1.1
