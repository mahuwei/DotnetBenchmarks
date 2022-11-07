docker build -f "D:\dotnet-test\DotnetBenchmarks\Docker\ApiDockerDemo\Dockerfile" --force-rm -t apidockerdemo  --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=ApiDockerDemo" "D:\dotnet-test\DotnetBenchmarks"

docker run -dt -v "C:\Users\mahuw\vsdbg\vs2017u5:/remote_debugger:rw" -v "C:\Users\mahuw\AppData\Roaming\Microsoft\UserSecrets:/root/.microsoft/usersecrets:ro" -v "C:\Users\mahuw\AppData\Roaming\ASP.NET\Https:/root/.aspnet/https:ro" -e "ASPNETCORE_LOGGING__CONSOLE__DISABLECOLORS=true" -e "ASPNETCORE_ENVIRONMENT=Development" -e "ASPNETCORE_URLS=https://+:443;http://+:80" -P --name ApiDockerDemo --entrypoint tail apidockerdemo -f /dev/null


docker pull mcr.microsoft.com/dotnet/samples:aspnetapp
docker run --rm -it -p 8000:80 -p 8001:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_Kestrel__Certificates__Default__Password="<CREDENTIAL_PLACEHOLDER>" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v ${HOME}/.aspnet/https:/https/ mcr.microsoft.com/dotnet/samples:aspnetapp

