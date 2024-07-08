# 使用基礎映像
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# 復制證書
COPY ["./aspnetapp.pfx", "/https/"]

# 設置環境變量
ENV ASPNETCORE_URLS="https://+;http://+"
ENV ASPNETCORE_HTTPS_PORT=443
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="Jason_76114"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/https/aspnetapp.pfx"


# 使用微軟官方的SDK映像來構建專案
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FISH.csproj", "./"]
RUN dotnet restore "FISH.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "FISH.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FISH.csproj" -c Release -o /app/publish

# 從基礎映像開始，將發布的應用程序從構建階段複製到運行時容器的工作目錄中
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FISH.dll"]
