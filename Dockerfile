# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App
ENV PORT=80
EXPOSE $PORT

# Copy the project file and restore any dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the application source code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App

# Copy the published output from the build stage
COPY --from=build-env /App/out .

# Specify the entry point
ENTRYPOINT ["dotnet", "WorkoutBuilderAPI.dll"]