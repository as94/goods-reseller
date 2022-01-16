FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY ./artifacts/backend/ .

ENTRYPOINT [ "dotnet", "GoodsReseller.Api.dll", "--updateDatabase" ]