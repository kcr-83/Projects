dotnet restore
dotnet publish -c Release  -o ./publish/service
sc.exe create MyWorkerService binpath= ./publish/service/WorkerService1.exe