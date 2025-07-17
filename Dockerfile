# Đây là Dockerfile được tối ưu cho MÔI TRƯỜNG PHÁT TRIỂN với tính năng hot-reload.
# Đối với môi trường SẢN XUẤT, bạn nên sử dụng một Dockerfile đa tầng khác,
# có bước `dotnet publish` và chỉ sao chép bản đã publish vào image cuối cùng nhỏ gọn hơn.

# Sử dụng SDK image vì nó chứa các công cụ cần thiết để biên dịch và chạy `dotnet watch`
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev

WORKDIR /app

# Sao chép file project (.csproj) trước để Docker có thể tận dụng cache cho các dependency.
# Đảm bảo đường dẫn này khớp với vị trí file .csproj của bạn trong thư mục gốc của dự án Docker.
COPY UserManagerDemo.csproj ./

# Chạy dotnet restore để tải về các package cần thiết
RUN dotnet restore

# Sao chép toàn bộ mã nguồn còn lại vào thư mục làm việc trong container.
# Lưu ý: Khi sử dụng Docker Volume (-v) trong docker-compose.yml,
# các thay đổi trên host sẽ ghi đè lên nội dung này trong container.
COPY . .

# Mở cổng mà ứng dụng ASP.NET của bạn sẽ lắng nghe (mặc định thường là 80 trong container)
EXPOSE 80
RUN dotnet tool install -g dotnet-ef \
    && echo 'export PATH="$PATH:/root/.dotnet/tools"' >> /root/.bashrc
ENV PATH="${PATH}:/root/.dotnet/tools"

# Đặt lệnh Entrypoint để chạy ứng dụng với `dotnet watch`.
# `dotnet watch run` sẽ tự động phát hiện thay đổi và khởi động lại ứng dụng.
# `--project UserManagerDemo.csproj` chỉ định file project chính của bạn.
# `--urls http://0.0.0.0:80` đảm bảo ứng dụng lắng nghe trên tất cả các địa chỉ IP trên cổng 80 trong container.
ENTRYPOINT ["dotnet", "watch", "run", "--project", "UserManagerDemo.csproj", "--urls", "http://0.0.0.0:80"]


# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# WORKDIR /app
# EXPOSE 80

# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /src
# COPY . .
# RUN dotnet restore
# RUN dotnet publish -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=build /app/publish .

# ENTRYPOINT ["dotnet", "UserManagerDemo.dll"]
