## 为解决问题

- 日志中文乱码？
- ip:port 可以访问，但 localhost:port 不行？

## 打包

### vs 打包

```powershell
docker build -f "D:\dotnet-test\DotnetBenchmarks\HttpTest\WebApi\Dockerfile" --force-rm -t webapi  --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=WebApi" "D:\dotnet-test\DotnetBenchmarks"
```

### 手动打包

```powershell
# 需要修改内容
docker build -t web-api -f .\LocalDockerfile ./
```

```csproj
	<PropertyGroup>
		...
		<OutputType>library</OutputType>
		...
	</PropertyGroup>
```

## 运行

```powershell
docker run --name web-api -p 5186:80 --network mhw-dev-network -e TZ=Asia/Shanghai -e LANG=zh_CN.UTF-8 -v /d/logs:/app/logs --restart=always -d webapi
```

## Dockerfile 添加下列环境变量，这样启动是就不用指定了

```
ENV TZ="Asia/Shanghai"
ENV LANG="en_US.UTF-8"
ENV LANGUAGE="en_US:en"
ENV LC_ALL="en_US.UTF-8"
```
