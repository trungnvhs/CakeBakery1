USE [master]
GO
/****** Object:  Database [BakeryCake]    Script Date: 7/21/2023 10:45:33 AM ******/
CREATE DATABASE [BakeryCake]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BakeryCake', FILENAME = N'E:\SQL\MSSQL15.MSSQLSERVER\MSSQL\DATA\BakeryCake.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BakeryCake_log', FILENAME = N'E:\SQL\MSSQL15.MSSQLSERVER\MSSQL\DATA\BakeryCake_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BakeryCake] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BakeryCake].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BakeryCake] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BakeryCake] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BakeryCake] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BakeryCake] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BakeryCake] SET ARITHABORT OFF 
GO
ALTER DATABASE [BakeryCake] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BakeryCake] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BakeryCake] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BakeryCake] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BakeryCake] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BakeryCake] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BakeryCake] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BakeryCake] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BakeryCake] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BakeryCake] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BakeryCake] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BakeryCake] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BakeryCake] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BakeryCake] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BakeryCake] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BakeryCake] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BakeryCake] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BakeryCake] SET RECOVERY FULL 
GO
ALTER DATABASE [BakeryCake] SET  MULTI_USER 
GO
ALTER DATABASE [BakeryCake] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BakeryCake] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BakeryCake] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BakeryCake] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BakeryCake] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BakeryCake] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BakeryCake', N'ON'
GO
ALTER DATABASE [BakeryCake] SET QUERY_STORE = OFF
GO
USE [BakeryCake]
GO
/****** Object:  Table [dbo].[Blogs]    Script Date: 7/21/2023 10:45:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blogs](
	[BlogID] [int] IDENTITY(1,1) NOT NULL,
	[BlogTitle] [nvarchar](250) NULL,
	[Description] [text] NULL,
	[Create_by] [int] NULL,
	[Create_date] [datetime] NULL,
	[Image] [nvarchar](250) NULL
PRIMARY KEY CLUSTERED 
(
	[BlogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 7/21/2023 10:45:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 7/21/2023 10:45:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [text] NULL,
	[BlogID] [int] NULL,
	[Owner] [int] NULL,
	[ParentID] [int],
	[Create_date] [datetime] not null,
PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 7/21/2023 10:45:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Address] [nvarchar](250) NULL,
	[Phone] [varchar](12) NULL,
	[Mail] [varchar](100) NULL,
	[WorkTime] [nvarchar](150) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 7/21/2023 10:45:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[FeadbackID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [text] NULL,
	[Owner] [int] NULL,
	[ProductID] [int] NULL,
	[Like] [bit] NULL,
	[Rate] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[FeadbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 7/21/2023 10:45:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[OrderItemID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[ProductID] [int] NULL,
	[Quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 7/21/2023 10:45:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[OrderDate] [datetime] NULL,
	[status] [varchar](20) NULL,
	[Address] [nvarchar](255) NULL,
	[phone] [varchar](12) NULL,
	[note] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 7/21/2023 10:45:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[UnitPrice] [money] NULL,
	[UnitInStock] [int] NULL,
	[Image] [varchar](250) NULL,
	[CategoryID] [int] NULL,
	[Discontinued] [bit] NULL,
	[Describe] [nvarchar](250) NULL,
	[Discount] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 7/21/2023 10:45:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](30) NULL,
	[Password] [varchar](20) NULL,
	[Name] [nvarchar](100) NULL,
	[Phone] [varchar](12) NULL,
	[Mail] [varchar](100) NULL,
	[Sex] [bit] NULL,
	[Dob] [datetime] NULL,
	[Avatar] [varchar](100) NULL,
	[RoleName] [nvarchar](20) NULL,
	[is_active] [bit] NULL,
	[address] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Blogs]  WITH CHECK ADD FOREIGN KEY([Create_by])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([BlogID])
REFERENCES [dbo].[Blogs] ([BlogID])
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([Owner])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([ParentID])
REFERENCES [dbo].[Comments] ([CommentID])
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD FOREIGN KEY([Owner])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO

Insert into Users
values
	('hieptd', '123', 'Tran Dinh Hiep', NULL, 'hieptd@gmail.com', '0', '2002-02-22', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS7vWHjatimRMCENuZGo1-EAb5-Vs8QWHuVgg&usqp=CAU', 'Customer', '1', 'Hoa Lac- Thach That'),
	('quannt', '123', 'Nguyen Trung Quan', NULL, 'quannt@gmail.com', '0', '2002-05-26', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS7vWHjatimRMCENuZGo1-EAb5-Vs8QWHuVgg&usqp=CAU', 'Customer', '1', 'Hoa Lac- Thach That'),
	('khoant', '123', 'Nguyen Tat Khoa', NULL, 'khoant@gmial.com', '0', '2001-07-23', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS7vWHjatimRMCENuZGo1-EAb5-Vs8QWHuVgg&usqp=CAU', 'Admin', '1', 'Hoa Lac- Thach That'),
	('quantn', '123', 'Ta Ngoc Quan', NULL, 'quantn@gmail.com', '0', '2002-06-10', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS7vWHjatimRMCENuZGo1-EAb5-Vs8QWHuVgg&usqp=CAU', 'Customer', '1', 'Hoa Lac- Thach That'),
	('trungnv', '123', 'Nguyen Viet Trung', NULL, 'trungnv@gmail.com', '0', '2002-06-15', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS7vWHjatimRMCENuZGo1-EAb5-Vs8QWHuVgg&usqp=CAU', 'Staff', '1', 'Hoa Lac- Thach That'),
	('thuongvv', '123', 'Vu Van Thuong', NULL, 'thuongvv@gmail.com', '0', '1999-06-15', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS7vWHjatimRMCENuZGo1-EAb5-Vs8QWHuVgg&usqp=CAU', 'Staff', '1', 'Hoa Lac- Thach That')

Insert into Categories(Name)
values
	(N'Cupcake'),
	(N'Chiffon, angel food, devil food'),
	(N'Pound cake'),
	(N'Cheesecake'),
	(N'PIE & TART'),
	(N'COOKIES'),
	(N'Soft drink'),
	(N'Tea')

Insert into Products
values
	(N'Cupcake cam', '20', '100', 'https://www.hoteljob.vn/files/Anh-HTJ-Hong/cupcake-la-gi-1.jpg', '1', '1', N'Bánh cupcake là một loại bánh nhỏ, thường có hình dạng tròn và được nướng trong các chiếc khuôn nhỏ. Nó có nguồn gốc từ Mỹ và trở thành món ăn phổ biến trên toàn thế giới. Dưới đây là mô tả về bánh cupcake:', 0),
	(N'Cupcake oreo', '30', '100', 'https://www.hoteljob.vn/files/Anh-HTJ-Hong/cupcake-la-gi-2.jpg', '1', '1', N'Cupcake Oreo là một phiên bản độc đáo và ngon miệng của bánh cupcake truyền thống, sử dụng bánh Oreo - một loại bánh quy sô cô la nổi tiếng, làm thành phần chính. Dưới đây là mô tả về Cupcake Oreo:', 0),
	(N'Cupcake sữa chua', '30', '100', 'https://www.hoteljob.vn/files/Anh-HTJ-Hong/cupcake-la-gi-3.jpg', '1', '1', N'Cupcake sữa chua là một loại bánh cupcake đặc biệt, được làm từ sữa chua hoặc yogurt, tạo nên vị ngọt mềm mịn và hương thơm đặc trưng. ', 0),
	(N'Cupcake nhân socola', '30', '100', 'https://www.hoteljob.vn/files/Anh-HTJ-Hong/cupcake-la-gi-5.jpg', '1', '1', N'Cupcake nhân sô cô la là một loại bánh cupcake được làm từ bột cacao và có nhân sô cô la bên trong. Đây là một trong những loại bánh cupcake phổ biến và được nhiều người yêu thích.', 0),
	(N'Cupcake chanh', '15', '100', 'https://www.hoteljob.vn/files/Anh-HTJ-Hong/cupcake-la-gi-8.jpg', '1', '1', N'', 0),
	(N'Bánh chiffon sữa chua chanh', '30', '100', 'https://cdn.tgdd.vn/2021/04/content/f16-800x500.jpg', '2', '1', N'', 0),
	(N'Bánh kem chiffon dâu tây', '50', '100', 'https://cdn.tgdd.vn/2021/04/content/f12-800x500.jpg', '2', '1', N'', 0),
	(N'Angel Food Cake', '50', '100', 'https://daylambanh.edu.vn/wp-content/uploads/2021/06/cach-lam-banh-angel-food-cake.jpg', '2', '1', N'', 0),
	(N'Devil`s food cake', '50', '100', 'https://kenh14cdn.com/thumb_w/620/2019/3/27/2-1553669071470297053685.jpg', '2', '1', N'', 0),
	(N'Bánh bông lan chiffon trà xanh', '30', '100', 'https://cdn.tgdd.vn/2021/04/content/f14-800x500.jpg', '2', '1', N'', 0),
	(N'Bánh Pound Socola', '30', '100', 'https://minhhouseware.com.vn/wp-content/uploads/2022/08/banh-tao2-1-600x400-1.jpeg', '3', '1', N'', 0),
	(N'Bánh Pound chanh', '40', '100', 'https://minhhouseware.com.vn/wp-content/uploads/2022/08/image-asset-1-600x394.jpeg', '3', '1', N'', 0),
	(N'Bánh Pound táo vị quế', '50', '100', 'https://minhhouseware.com.vn/wp-content/uploads/2022/08/CAKE-4021-500x500-1.png', '3', '1', N'', 0),
	(N'Butter cake', '20', '100', 'https://media.cooky.vn/images/blog-2016/cach-phan-biet-4-loai-banh-bong-lan-co-ban-nhat-danh-cho-nguoi-moi-bat-dau-lam-banh%202.jpg', '3', '1', N'', 0),
	(N'Pound cake', '15', '100', 'https://media.cooky.vn/recipe/g4/34936/s800x500/cooky-recipe-cover-r34936.png', '3', '1', N'', 0),
	(N'Bánh cheesecake oreo', '50', '100', 'https://cdn.tgdd.vn/2021/02/content/1111-800x450-1.jpg', '4', '1', N'', 0),
	(N'Bánh cheesecake sữa chua', '35', '100', 'https://cdn.tgdd.vn/2021/01/content/2-800x450-15.jpg', '4', '1', N'', 0),
	(N'Bánh cheesecake chanh dây', '30', '100', 'https://cdn.tgdd.vn/2021/01/content/3-800x450-7.jpg', '4', '1', N'', 0),
	(N'Bánh cheesecake xoài', '30', '100', 'https://cdn.tgdd.vn/2021/01/content/4-800x450-5.jpg', '4', '1', N'', 0),
	(N'Bánh cheesecake dâu tây', '30', '100', 'https://cdn.tgdd.vn/2021/01/content/1-800x450-42.jpg', '4', '1', N'', 0),
	(N'Bánh Pie Táo', '30', '100', 'https://nhichan.com/wp-content/uploads/2019/05/apple-pie-01-324x235.jpg', '5', '1', N'', 0),
	(N'Bánh Pie Táo Hoa Hồng', '30', '100', 'https://nhichan.com/wp-content/uploads/2019/06/applerosetart-01-01-324x235.jpg', '5', '1', N'', 0),
	(N'Bánh Tart chanh xanh Meringue', '30', '100', 'https://nhichan.com/wp-content/uploads/2019/03/chanh-01-324x235.jpg', '5', '1', N'', 0),
	(N'Bánh Tart hoa quả mini', '15', '100', 'https://nhichan.com/wp-content/uploads/2019/02/fruit-tart-01-324x235.jpg', '5', '1', N'', 0),
	(N'Bánh Tart', '10', '100', 'https://imonanngon.info/wp-content/uploads/2022/12/banh-tart-1.webp', '5', '1', N'', 0),
	(N'Bánh quy bơ mứt dâu', '20', '100', 'https://dep.com.vn/Uploaded/haly/2014_07_07/banh-quy-bo-mut-dau-deponline.jpg', '6', '1', N'', 0),
	(N'Bánh quy chuối dừa và chocolate chip', '25', '100', 'https://dep.com.vn/Uploaded/haly/2014_07_07/banh-quy-chuoi-socola-deponline.jpg', '6', '1', N'', 0),
	(N'Bánh quy nhân mứt Thumbprint cookies', '30', '100', 'https://dep.com.vn/Uploaded/haly/2014_07_07/cookies-deponline(3).jpg', '6', '1', N'', 0),
	(N'Bánh quy cam', '15', '100', 'https://dep.com.vn/Uploaded/haly/2014_07_07/banh-quy-cam-deponline.jpg', '6', '1', N'', 0),
	(N'Cookies hạt dẻ', '30', '100', 'https://dep.com.vn/Uploaded/haly/2014_07_07/cookies-hat-de-deponline.jpg', '6', '1', N'', 0),
	(N'Coca Cola', '10', '100', 'https://sieuthixanh.com.vn/Upload/products/zoom/Loc-6-lon-nuoc-ngot-Coca-Cola-320ml132690948229883779.jpg', '7', '1', N'', 0),
	(N'Pepsi', '10', '100', 'https://cdn.tgdd.vn/2021/05/content/3.2-800x450-1.jpg', '7', '1', N'', 0),
	(N'7Up', '10', '100', 'https://cdn.tgdd.vn/2021/05/content/3.3-800x450-1.jpg', '7', '1', N'', 0),
	(N'Sprite', '10', '100', 'https://cdn.tgdd.vn/2021/05/content/3.4-800x450-1.jpg', '7', '1', N'', 0),
	(N'Mirinda', '10', '100', 'https://cdn.tgdd.vn/2021/05/content/3.5-800x450.jpg', '7', '1', N'', 0),
	(N'Fanta', '10', '100', 'https://cdn.tgdd.vn/2021/05/content/3.6-800x450.jpg', '7', '1', N'', 0),
	(N'Trà Xanh', '30', '100', 'https://file.hstatic.net/1000135323/file/1_tra_xanh_e4051722ae3e4f3db67e812506997632_1024x1024.jpg', '8', '1', N'', 0),
	(N'Trà Đen (Black Tea)', '30', '100', 'https://file.hstatic.net/1000135323/file/2_tra_den_753be5788b87481b88063d4231b71137_1024x1024.jpg', '8', '1', N'', 0),
	(N'Trà Oolong (Oolong Tea)', '30', '100', 'https://dongsontea.com/wp-content/uploads/2019/12/Ol-L%C3%A0i-250gr.jpg', '8', '1', N'', 0),
	(N'Trà Nhài (Jasmine Tea)', '35', '100', 'https://file.hstatic.net/1000135323/file/5_tra_nhai_7fe0f21c7fcc43c78ff456a2b9660183_1024x1024.jpg', '8', '1', N'', 0),
	(N'Trà Bá Tước (Earl Grey Tea)', '50', '100', 'https://file.hstatic.net/1000135323/file/7_ba_tuoc_27ca49a38daa42deb75f5ce5bd43b6f6_1024x1024.jpg', '8', '1', N'', 0)
	
	Insert into Blogs(BlogTitle,Description,Create_by, Create_date, image)
	values
	('Thuc pham chuc nang khong thay the thuoc chua benh', 'Scrip.......',3,'2023-07-05 10:30:00','https://www.hoteljob.vn/files/Anh-HTJ-Hong/cupcake-la-gi-1.jpg'),
	('Banh Sieu Ngon', 'Scrip.......',3,'2023-07-05 21:30:00','https://www.hoteljob.vn/files/Anh-HTJ-Hong/cupcake-la-gi-2.jpg')

	Insert into Comments(BlogID,Description,Owner, Create_date)
	values
	(2, 'Sieu ngon.......',2, '2023-07-05'),
	(2, 'Vip pro.......',2, '2023-07-06')


	Insert into Orders(CustomerID,OrderDate, status, Address, phone)
	values
	(1, '2023-07-16','Done', 'Hoa Lac-Ha Noi', '0321654953'),
	(2, '2023-07-16','Done', 'Hoa Lac-Ha Noi', '0321654953')

	Insert into OrderItems(OrderID,ProductID, Quantity)
	values
	(1, 5, 1),
	(1, 9, 2),
	(2, 15, 1),
	(2, 23, 1),
	(2, 7, 3)
