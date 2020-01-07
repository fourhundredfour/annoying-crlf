FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine as build

WORKDIR /usr/src/app
COPY . /usr/src/app

RUN dotnet restore && \
    dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-alpine

RUN mkdir -p /data /tmp/converted_files

WORKDIR /usr/src/app
COPY --from=build /usr/src/app/out /usr/src/app

ENTRYPOINT [ "dotnet", "AnnoyingCRLF.dll" ]