FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY ./artifacts/backend/ .
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000
ENTRYPOINT [ "dotnet", "GoodsReseller.Api.dll" ]