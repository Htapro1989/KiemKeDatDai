FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/KiemKeDatDai.Web.Host/KiemKeDatDai.Web.Host.csproj", "src/KiemKeDatDai.Web.Host/"]
COPY ["src/KiemKeDatDai.Web.Core/KiemKeDatDai.Web.Core.csproj", "src/KiemKeDatDai.Web.Core/"]
COPY ["src/KiemKeDatDai.Application/KiemKeDatDai.Application.csproj", "src/KiemKeDatDai.Application/"]
COPY ["src/KiemKeDatDai.Core/KiemKeDatDai.Core.csproj", "src/KiemKeDatDai.Core/"]
COPY ["src/KiemKeDatDai.EntityFrameworkCore/KiemKeDatDai.EntityFrameworkCore.csproj", "src/KiemKeDatDai.EntityFrameworkCore/"]
WORKDIR "/src/src/KiemKeDatDai.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/KiemKeDatDai.Web.Host", "src/KiemKeDatDai.Web.Host"]
COPY ["src/KiemKeDatDai.Web.Core", "src/KiemKeDatDai.Web.Core"]
COPY ["src/KiemKeDatDai.Application", "src/KiemKeDatDai.Application"]
COPY ["src/KiemKeDatDai.Core", "src/KiemKeDatDai.Core"]
COPY ["src/KiemKeDatDai.EntityFrameworkCore", "src/KiemKeDatDai.EntityFrameworkCore"]
WORKDIR "/src/src/KiemKeDatDai.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "KiemKeDatDai.Web.Host.dll"]
