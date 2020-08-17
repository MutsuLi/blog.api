#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Blog.Api/Blog.Api.csproj", "Blog.Api/"]
COPY ["Blog.IRepository/Blog.IRepository.csproj", "Blog.IRepository/"]
COPY ["Blog.Model/Blog.Model.csproj", "Blog.Model/"]
COPY ["Blog.Common/Blog.Common.csproj", "Blog.Common/"]
COPY ["Blog.Extensions/Blog.Extensions.csproj", "Blog.Extensions/"]
COPY ["Blog.IServices/Blog.IServices.csproj", "Blog.IServices/"]
COPY ["Blog.Repository/Blog.Repository.csproj", "Blog.Repository/"]
COPY ["Blog.Services/Blog.Services.csproj", "Blog.Services/"]
RUN dotnet restore "Blog.Api/Blog.Api.csproj"
COPY . .
WORKDIR "/src/Blog.Api"
RUN dotnet build "Blog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.Api.dll"]