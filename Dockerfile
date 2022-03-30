# Backend
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/BACK/BACK.csproj", "."]
RUN dotnet restore "./BACK.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BACK.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BACK.csproj" -c Release -o /app/publish

FROM base AS backend
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BACK.dll"]

# Frontend
FROM node:16-alpine as frontend

COPY ["/FRONT/package.json", "."]
COPY ["FRONT/yarn.lock", "."]
RUN yarn install

COPY ["FRONT/public/", "./public/"]
COPY ["FRONT/src/", "./src/"]

EXPOSE 3000
CMD ["yarn", "start"]