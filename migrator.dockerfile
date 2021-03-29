FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY ./artifacts/backend/ .

ENTRYPOINT [ "dotnet", "GoodsReseller.Api.dll", "--updateDatabase" ]