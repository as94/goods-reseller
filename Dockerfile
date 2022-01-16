FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.10-buster-slim@sha256:6f8a0dbb114961f86e8831105a9b41d31707b6384e630ed69670af5db18e1974
COPY ./artifacts/backend/ .
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000
ENTRYPOINT [ "dotnet", "GoodsReseller.Api.dll" ]