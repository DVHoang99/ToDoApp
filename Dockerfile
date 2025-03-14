# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy toàn bộ source code vào container
COPY . ./

# Kiểm tra xem file ToDoApp.csproj có nằm đúng thư mục không
RUN ls -la /app

# Chạy restore
RUN dotnet restore ToDoApp/ToDoApp.csproj
RUN dotnet build ToDoApp/ToDoApp.csproj -c Release -o /app/build

# Publish ứng dụng
RUN dotnet publish ToDoApp/ToDoApp.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

CMD ["dotnet", "ToDoApp.dll"]
