# 📇 Ứng dụng Quản lý Danh bạ (Contact Manager)

[![WPF](https://img.shields.io/badge/UI-WPF-blue.svg)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
[![.NET](https://img.shields.io/badge/Framework-.NET-purple.svg)](https://dotnet.microsoft.com/)
[![EF Core](https://img.shields.io/badge/ORM-Entity_Framework_Core-green.svg)](https://docs.microsoft.com/en-us/ef/core/)
[![Architecture](https://img.shields.io/badge/Architecture-MVVM-orange.svg)](#)

> **Đồ án môn học:** PRN212
> **Mô tả:** Ứng dụng Desktop quản lý danh bạ cá nhân với các thao tác CRUD cơ bản, được xây dựng trên nền tảng C# WPF, áp dụng chặt chẽ kiến trúc MVVM và sử dụng Entity Framework Core để tương tác với SQL Server.

## 👥 Nhóm Thực Hiện & Phân Công Giao Việc

| STT | Họ và Tên | MSSV | Vai trò chính | Nhiệm vụ chi tiết |
| :---: | :--- | :---: | :--- | :--- |
| 1 | Nguyễn Ngọc Tuệ Minh | SE170244 | **Data Layer (Database)** | Thiết kế Models, quản lý chuỗi kết nối SQL Server. |
| 2 | Lê Thị Hồng Loan | SE182208 | **View (UI/UX - XAML)** | Thiết kế giao diện chính. |
| 3 | Lê Anh Khôi | SE193615 | **ViewModel (Logic CRUD)** | Xử lý logic nghiệp vụ MVVM. |
| 4 | Nguyễn Lê Hữu Trí | SE193083 | **Integration, QA & Git** | Khởi tạo cấu trúc dự án, quản lý Git/GitHub. Xử lý Validation. |
## 🎯 Chức năng chính (Features)

Hệ thống cung cấp đầy đủ các thao tác quản lý dữ liệu (CRUD):
- **C**reate: Thêm mới một liên hệ với thông tin chi tiết (Tên, SĐT, Email, Địa chỉ) kèm Validate dữ liệu đầu vào.
- **R**ead: Hiển thị danh sách liên hệ trực quan trên DataGrid. Hỗ trợ tính năng **Tìm kiếm theo thời gian thực (Real-time Search)**.
- **U**pdate: Chọn một liên hệ để chỉnh sửa và cập nhật thông tin trực tiếp vào CSDL.
- **D**elete: Xóa liên hệ khỏi hệ thống (Có cảnh báo xác nhận trước khi xóa).

## 🛠 Công nghệ & Kiến trúc áp dụng

- **Ngôn ngữ:** C# 
- **Giao diện (UI):** Windows Presentation Foundation (WPF) - XAML
- **Cơ sở dữ liệu:** Microsoft SQL Server
- **ORM:** Entity Framework Core (EF Core SQL Server)
- **Kiến trúc phần mềm:** Chuẩn **MVVM** (Model - View - ViewModel), phân tách hoàn toàn giao diện và logic.
