FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY ./artifacts/backend/ .
EXPOSE 80
ENTRYPOINT [ "dotnet", "GoodsReseller.Api.dll" ]