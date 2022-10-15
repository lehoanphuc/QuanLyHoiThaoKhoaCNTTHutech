USE MASTER
GO

CREATE DATABASE QL_HOITHAO
GO
USE QL_HOITHAO
GO

Create table [GIOITHIEU]
(
	[ID] INT IDENTITY(1,1) NOT NULL,
	[NoiDung] NVARCHAR(MAX) NULL,
	[TrangThai] BIT NULL,
)

Create table [QUYEN]
(
	[MAQUYEN] INT IDENTITY(1,1) NOT NULL,
	[TENQUYEN] NVARCHAR(255) NULL,
	[GHICHU] NVARCHAR(255) NULL,
	[TRANGTHAI] INT NULL,
Primary Key ([MAQUYEN])
) 
GO

Create table [CONGVIEC]
(
	[MaCongViec] INT IDENTITY(1,1) NOT NULL,
	[TenCongViec] NVARCHAR(255) NULL,
	[GhiChu] NVARCHAR(255) NULL,
	[TrangThai] INT NULL,
Primary Key ([MaCongViec])
) 
go

Create table [CHITIETQUYEN]
(
	[MaQuyenCongViec] INT IDENTITY(1,1) NOT NULL,
	[MAQUYEN] INT NULL,
	[MaCongViec] INT NULL,
	CONSTRAINT FK_QUYEN FOREIGN KEY (MAQUYEN) REFERENCES QUYEN(MAQUYEN),
	CONSTRAINT FK_CV FOREIGN KEY (MaCongViec) REFERENCES CONGVIEC(MaCongViec),
) 
go

Create table [LOP]
(
	[MaLop] INT IDENTITY(1,1) NOT NULL,
	[TenLop] Char(10) NULL, 
	[SiSo] INT NULL,
	[NgayTao] DATETIME NULL,
	[NgayChinhSua] DATETIME NULL,
	[TrangThai] INT NULL,
Primary Key ([MaLop])
) 
go


Create table [USER]
(
	[MaSo] VARCHAR(50) NOT NULL,
	[Pass] VARCHAR(200) NULL,
	[Ten] NVARCHAR(255) NULL,
	[Hinh] NVARCHAR(MAX) NULL,
	[NgaySinh] DATETIME NULL,
	[GioiTinh] INT NULL,
	[SDT] VARCHAR(20) NULL,
	[Email] NVARCHAR(255) UNIQUE NULL,
	[TK] NVARCHAR(255),
	[Quyen] INT NULL,
	[TrangThai] INT NULL,
	[MaLop] INT NULL,
	CONSTRAINT FK_LOP FOREIGN KEY (MaLop) REFERENCES LOP(MaLop),
	
Primary Key ([MaSo])
) 
go

Create table [USERADMIN]
(
	[Account] VARCHAR(50) NOT NULL,
	[Pass] NVARCHAR(255) NULL,
	[HoTen] NVARCHAR(255) NULL,
	[SDT] VARCHAR(20) NULL,
	[Mail] VARCHAR(255) NULL,
	[NgayTao] DATETIME NULL,
	[TrangThai] INT NULL,
	[MAQUYEN] INT NULL,
	CONSTRAINT FK_QUYENUSER FOREIGN KEY (MAQUYEN) REFERENCES QUYEN(MAQUYEN),
Primary Key ([Account])
) 
go



Create table [DIEULE]
(
	[MaDieuLe] INT IDENTITY(1,1) NOT NULL,
	[TenDieuLe] NVARCHAR(255) NULL,
	[DuongDan] NVARCHAR(MAX) NULL,
	[TomTat] NVARCHAR(255) NULL,
	[NoiDung] NVARCHAR(MAX) NULL,
	[NgayTao] DATETIME NULL,
	[NgayChinhSua] DATETIME NULL,
	[TrangThai] INT NULL,
Primary Key ([MaDieuLe])
) 
go

Create table [TINTUC]
(
	[MaTinTuc] INT IDENTITY(1,1) NOT NULL,
	[TenTinTuc] NVARCHAR(1000) NULL,
	[DuongDan] NVARCHAR(MAX) NULL,
	[Hinh] NVARCHAR(MAX) NULL,
	[NoiDung] NVARCHAR(MAX) NULL,
	[NgayViet] DATETIME NULL,
	[TrangThai] INT NULL,
Primary Key ([MaTinTuc])
) 
go

Create table [PHANHOI]
(
	[MaPhanHoi] INT IDENTITY(1,1) NOT NULL,
	[HoTen] NVARCHAR(255) NULL,
	[Mail] VARCHAR(MAX) NULL,
	[SDT] VARCHAR(20) NULL,
	[NoiDung] NVARCHAR(MAX) NULL,
	[NgayGui] DATETIME NULL,
	[TrangThai] INT NULL,
Primary Key ([MaPhanHoi])
) 
go


Create table [MONTHIDAU]
(
	[MaMon] INT IDENTITY(1,1) NOT NULL,
	[TenMon] NVARCHAR(255) NULL,
	[TheLoai] INT NULL,
	[SoLuongDoi] INT NULL,
	[SoLuongThanhVien] INT NULL,
	[LePhi] DECIMAL(18,0) NULL,
	[KyQuy] DECIMAL(18,0) NULL,
	[NgayTao] DATETIME NULL,
	[NgayBatDau] DATETIME NULL,	
	[NgayKetThuc] DATETIME NULL,
	[NgayChinhSua] DATETIME NULL,
	[NoiDung] NVARCHAR(MAX) NULL,
	[GhiChu] NVARCHAR(255) NULL,
	[HuongDanDK] NVARCHAR(MAX) NULL,
	[TrangThai] INT NULL,
	[TrangThaiMenu] INT NULL,
	[MaDieuLe] INT NULL,
	CONSTRAINT fk_dieule FOREIGN KEY (MaDieuLe) REFERENCES DIEULE (MaDieuLe),
Primary Key ([MaMon])
) 
go

Create table [DOI]
(
	[MaDoi] INT IDENTITY(1,1) NOT NULL,
	[MaThamGia] VARCHAR(10),	
	[TenDoi] NVARCHAR(255) NULL,
	[SoLuong] INT NULL,
	[NgayTao] DATETIME NULL,
	[MaMon] INT NOT NULL,
	[MaSo] VARCHAR(50) NOT NULL,
	[TrangThai] INT NULL,
	[MaBangDau] INT NULL,
	CONSTRAINT FK_DMAMON FOREIGN KEY (MaMon) REFERENCES [MONTHIDAU](MaMon),
	CONSTRAINT FK_DMASO FOREIGN KEY (MaSo) REFERENCES [USER](MaSo),
	CONSTRAINT FK_D_MB FOREIGN KEY (MaBangDau) REFERENCES [BANGDAU](MaBangDau),
Primary Key ([MaDoi])
) 
go

Create table [CHITIETDOI]
(
	[ID] INT IDENTITY(1,1) NOT NULL,
	[MaDoi] INT NOT NULL,
	[MaSo] VARCHAR(50) NOT NULL,
	[NgayThamGia] DATETIME NULL,
	[TrangThai] INT NULL,
	CONSTRAINT FK_CTDMADOI FOREIGN KEY (MaDoi) REFERENCES [DOI](MaDoi),
	CONSTRAINT FK_CTDMaSV FOREIGN KEY (MaSo) REFERENCES [USER] (MaSo),
Primary Key ([ID])
) 
go


Create table [PHIEUDANGKY]
(
	[MaPhieuDK] INT IDENTITY(1,1) NOT NULL,
	[NgayDangKy] DATETIME NULL,
	[TrangThai] INT NULL,
	[MaDoi] INT NOT NULL,
	CONSTRAINT FK_PDKMADOI FOREIGN KEY (MaDoi) REFERENCES [DOI](MaDoi),
Primary Key ([MaPhieuDK])
) 
go

/*
ALTER TABLE MONTHIDAU
ADD [KyQuy] DECIMAL(18,0) NULL;

ALTER TABLE CTPHIEUDK
DROP COLUMN HinhThucThanhToan;

*/

Create table [CTPHIEUDK]
(
	[ID] INT IDENTITY(1,1) NOT NULL,
	[MaPhieuDK] INT NOT NULL,
	[Account] VARCHAR(50) NOT NULL,
	[TongTien] DECIMAL(18,0) NULL,
	[HinhThucThanhToan] INT NULL,
	[NgayThanhToan] DATETIME NULL,
	[AccountHoanTra] NVARCHAR(50) NULL,
	[TongTienHoanTra] DECIMAL(18,0) NULL,
	[HinhThucHoanTra] INT NULL,
	[NgayHoanTra] DATETIME NULL,
	[TrangThai] INT NULL,
	CONSTRAINT FK_CTPDK_MPDK FOREIGN KEY (MaPhieuDK) REFERENCES [PHIEUDANGKY](MaPhieuDK),
	CONSTRAINT FK_CTPDK_ACC FOREIGN KEY (Account) REFERENCES [USERADMIN](Account),
Primary Key ([ID])
) 
go


Create table [BANGDAU]
(
	[MaBangDau] INT IDENTITY(1,1) NOT NULL,
	[TenBangDau] NVARCHAR(255) NULL,
	[Diem] INT NULL,
	[TongBanThang] INT NULL,
	[TongSoBanThua] INT NULL,
	[SoTranThang] INT NULL,
	[SoTranThua] INT NULL,
	[SoTranHoa] INT NULL,
	[HieuSo] INT NULL,
	[Loi] INT  NULL,
	[TrangThai] Char(1) NULL,
Primary Key ([MaBangDau])
) 
go


Create table [LICHTHIDAU]
(
	[MaLichThiDau] INT IDENTITY(1,1) NOT NULL,
	[DoiA] INT NULL,
	[DoiB] INT NULL,
	[NgayThiDau] DATETIME NULL,
	[DiaDiem] NVARCHAR(255) NULL,
	[TrangThai] INT NULL,
Primary Key ([MaLichThiDau])
) 
go


Create table [CHITIETTRANDAU]
(
	[ID] INT IDENTITY(1,1) NOT NULL,
	[NguoiGhiDiem] VARCHAR(50) NULL,
	[DoiGhiDiem] INT NULL,
	[DiemGhiDuoc] INT NULL,
	[Phut] INT NULL,
	[Loi] INT NULL,
	[TrangThai] INT NULL,
	[NguoiCapNhap] VARCHAR(50) NULL,
	[MaBangDau] INT NOT NULL,
	[MaLichThiDau] INT NOT NULL,
	CONSTRAINT FK_CTTD_MBD FOREIGN KEY (MaBangDau) REFERENCES [BANGDAU](MaBangDau),
	CONSTRAINT FK_CTTD_MLTD FOREIGN KEY (MaLichThiDau) REFERENCES [LICHTHIDAU](MaLichThiDau),
Primary Key ([ID])
) 
go
SET IDENTITY_INSERT [dbo].[QUYEN] ON 

INSERT [dbo].[QUYEN] ([MAQUYEN], [TENQUYEN], [GHICHU], [TRANGTHAI]) VALUES (1, N'Quản trị viên', NULL, 1)
INSERT [dbo].[QUYEN] ([MAQUYEN], [TENQUYEN], [GHICHU], [TRANGTHAI]) VALUES (2, N'Ban chấp hành', NULL, 1)
INSERT [dbo].[QUYEN] ([MAQUYEN], [TENQUYEN], [GHICHU], [TRANGTHAI]) VALUES (3, N'Cộng tác viên bóng đá', NULL, 1)
INSERT [dbo].[QUYEN] ([MAQUYEN], [TENQUYEN], [GHICHU], [TRANGTHAI]) VALUES (4, N'Cộng tác viên bóng chuyền', NULL, 1)
INSERT [dbo].[QUYEN] ([MAQUYEN], [TENQUYEN], [GHICHU], [TRANGTHAI]) VALUES (5, N'Cộng tác viên cầu lông', NULL, 1)
INSERT [dbo].[QUYEN] ([MAQUYEN], [TENQUYEN], [GHICHU], [TRANGTHAI]) VALUES (6, N'Cộng tác viên kéo co', NULL, 1)
INSERT [dbo].[QUYEN] ([MAQUYEN], [TENQUYEN], [GHICHU], [TRANGTHAI]) VALUES (7, N'Cộng tác viên cờ vua cờ tướng', NULL, 1)
INSERT [dbo].[QUYEN] ([MAQUYEN], [TENQUYEN], [GHICHU], [TRANGTHAI]) VALUES (8, N'Cộng tác viên điền kinh', NULL, 1)
SET IDENTITY_INSERT [dbo].[QUYEN] OFF
GO
INSERT [dbo].[USERADMIN] ([Account], [Pass], [HoTen], [SDT], [Mail], [NgayTao], [TrangThai], [MAQUYEN]) VALUES (N'admin', N'123', N'Lê Hoàn Phúc', N'123123', N'phuc@gmail.com', CAST(N'2001-05-05T00:00:00.000' AS DateTime), 1, 1)
INSERT [dbo].[USERADMIN] ([Account], [Pass], [HoTen], [SDT], [Mail], [NgayTao], [TrangThai], [MAQUYEN]) VALUES (N'bch', N'123', N'Lê Hoàn Phúc', N'123123', N'hoanpuhcle2001@gmail.com', CAST(N'2012-05-05T00:00:00.000' AS DateTime), 1, 2)
INSERT [dbo].[USERADMIN] ([Account], [Pass], [HoTen], [SDT], [Mail], [NgayTao], [TrangThai], [MAQUYEN]) VALUES (N'ctvbongda', N'123', N'Lê Hoàn Phúc', N'123123', N'hoanpuhcle22001@gmail.com', CAST(N'2012-05-05T00:00:00.000' AS DateTime), 1, 3)
GO
