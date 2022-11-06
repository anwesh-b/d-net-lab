FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /tempapp

EXPOSE 5000

ENV ASPNETCORE_URLS http://+:5000

COPY . /tempapp

RUN dotnet publish -c Release -o /app

WORKDIR /app

RUN rm -rf /tempapp

ENTRYPOINT ["dotnet", "MvcMoviw.dll"]
