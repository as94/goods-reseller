FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY ./artifacts/backend/ .
EXPOSE 80
ENTRYPOINT [ "dotnet", "GoodsReseller.Api.dll" ]