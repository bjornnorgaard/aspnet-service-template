FROM mcr.microsoft.com/dotnet/sdk:5.0.301-alpine3.13 AS build
WORKDIR /app

COPY *.sln ./
COPY */*.csproj ./

RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done
RUN dotnet restore

COPY . .
RUN dotnet build
RUN dotnet publish Svc.Todos.Api/Svc.Todos.Api.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0.8-buster-slim-amd64 AS runtime
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Svc.Todos.Api.dll" ]
