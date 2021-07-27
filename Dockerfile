FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

EXPOSE 80

WORKDIR /

# copy and publish app and libraries
COPY . .

RUN dotnet restore "Sources/Org.VSATemplate.WebApi/Org.VSATemplate.WebApi.csproj"

RUN dotnet publish -c Release -o /app


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
#Unblock comment if can't connect db have security lower version
#RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf && sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /usr/lib/ssl/openssl.cnf
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Org.VSATemplate.WebApi.dll"]