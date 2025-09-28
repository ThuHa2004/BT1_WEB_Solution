# Trần Thị Thu Hà - K225480106009
# K58KTP - Môn: Phát triển ứng dụng trên nền web 
## BÀI TẬP VỀ NHÀ 01: 
# TẠO SOLUTION GỒM CÁC PROJECT SAU: 
1. DLL đa năng, keyword: c# window library -> **Class Library (.NET Framework)** bắt buộc sử dụng **.NET Framework 2.0**: giải bài toán bất kỳ, độc lạ càng tốt, phải có dấu ấn cá nhân trong kết quả, biên dịch ra DLL. DLL độc lập vì nó ko nhập, ko xuất, nó nhận input truyền vào thuộc tính của nó, và trả về dữ liệu thông qua thuộc tính khác, hoặc thông qua giá trị trả về của hàm. Nó độc lập thì sẽ sử dụng được trên app dạng console (giao diện dòng lệnh - đen sì), cũng sử dụng được trên app desktop (dạng cửa sổ), và cũng sử dụng được trên web form (web chạy qua iis).
2. Console app, bắt buộc sử dụng **.NET Framework 2.0**, sử dụng được DLL trên: nhập được input, gọi DLL, hiển thị kết quả, phải có dấu án cá nhân. keyword: c# window Console => **Console App (.NET Framework)**, biên dịch ra EXE
3. Windows Form Application, bắt buộc sử dụng **.NET Framework 2.0**, sử dụng được DLL đa năng trên, kéo các control vào để có thể lấy đc input, gọi DLL truyền input để lấy đc kq, hiển thị kq ra window form, phải có dấu án cá nhân; keyword: c# window Desktop => **Windows Form Application (.NET Framework)**, biên dịch ra EXE
4. Web đơn giản, bắt buộc sử dụng **.NET Framework 2.0**, sử dụng web server là IIS, dùng file hosts để tự tạo domain, gắn domain này vào iis, file index.html có sử dụng html css js để xây dựng giao diện nhập được các input cho bài toán, dùng mã js để tiền xử lý dữ liệu, js để gửi lên backend. backend là api.aspx, trong code của api.aspx.cs thì lấy được các input mà js gửi lên, rồi sử dụng được DLL đa năng trên. kết quả gửi lại json cho client, js phía client sẽ nhận được json này hậu xử lý để thay đổi giao diện theo dữ liệu nhận dược, phải có dấu án cá nhân. keyword: c# window web => **ASP.NET Web Application (.NET Framework)** + tham khảo link chatgpt thầy gửi. project web này biên dịch ra DLL, phải kết hợp với IIS mới chạy được.

# ĐỀ TÀI: BÀI TOÁN VỀ GAME CỜ CARO
<img width="1909" height="977" alt="image" src="https://github.com/user-attachments/assets/c70c928c-4795-4939-a00a-8daa281c0315" />

# CÁC BƯỚC THỰC HIỆN - TẠO CÁC SOLUTION: 
## 1. Tạo project Class Library (.NET Framework 2.0) 
### **Các bước tạo project**:
- Bước 1: Mở Visual Studio 2022.
- Bước 2: Chọn File -> New - Project. Trên thanh tìm kiếm tìm "Class Library (.NET Framework)" -> chọn và nhấn Next
  
  <img width="1904" height="927" alt="image" src="https://github.com/user-attachments/assets/5684d153-222f-419d-a0e1-caf23df5c06e" />  

- Bước 3: Trong giao diện "Configure your new project", đặt tên so project trong solution -> Chọn vị trí lưu, trong Frammework thì chọn **.NET Framework 2.0** và nhấn Create.  
  <img width="1919" height="1010" alt="image" src="https://github.com/user-attachments/assets/6ea332ab-0093-4216-a067-baf96bfde89f" />  

### **Sau khi tạo xong project**: viết các logic của game cờ Caro vào project này rồi Build và Run.
- Click chuột phải vào **Library_CoCaro** rồi chọn Build hoặc chuột phải vào **Solution 'CoCaro'** rồi chọn Build solution.  
  <img width="1919" height="1012" alt="image" src="https://github.com/user-attachments/assets/5bdf5bc3-6a58-4f09-90a5-4cfb421b91f4" />  

**Mục đích** của project này là tạo một DLL đa năng và độc lập vì nó ko nhập, ko xuất, nó nhận input truyền vào thuộc tính của nó, và trả về dữ liệu thông qua thuộc tính khác, hoặc thông qua giá trị trả về của hàm. DLL này gồm toàn bộ logic của game cờ Caro (quản lý bàn cờ, lượt đi, thắng, thua...) xử lý việc kiểm tra xem các ô X, O do người chơi đi có tạo thành một hàng thẳng, ngang, chéo gồm 5 ô của X hoặc O hay không, nếu có tạo thành thì trò chơi sẽ kết thúc và reset lại để chơi ván mới. Nó chỉ nhận input (tọa độ đánh, lượt người chơi) và trả về kết quả (trạng thái bàn cờ, ai thắng, thông báo).  
**Ứng dụng**: DLL có thể tái sử dụng trong các ứng dụng khác nhau. 
DLL này không chạy trực tiếp mà nó sẽ được tham chiếu (reference) đến các project khác trong solution.

## 2. Tạo project Console App (.NET Framework 2.0)  
### **Các bước tạo project Console App**:
Cũng giống như các bước tạo project Class Library
- Bước 1: Click chuột phát vào Solution 'CoCaro' -> Add -> New project.
  <img width="1919" height="978" alt="image" src="https://github.com/user-attachments/assets/b4f4e4e7-311c-4b62-848b-58a6e11a7053" />
- Bước 2: Gõ tìm Console App (.Net Framework) -> Next -> Đặt tên cho project ở khung project name (ConsoleCoCaro) -> Chọn .NET Framework 2.0 -> Create.  
  <img width="1919" height="1012" alt="image" src="https://github.com/user-attachments/assets/705db4d0-8641-46ba-84ac-4f5a91130628" />  
- Sau khi đã tạo xong project -> References tới project Library_CoCaro: Click chuột phải vào project ConsoleCoCaro -> Add -> References -> Project -> Tích vào Library_CoCaro -> OK.
  <img width="1029" height="700" alt="image" src="https://github.com/user-attachments/assets/88f2346e-1759-4a1c-b701-075caa023a76" />

### Mục đích của project này:
- Xây dựng trò chơi Cờ Caro chạy trên môi trường Console bằng C# (.NET Framework 2.0).
- Giúp người chơi có thể chơi trực tiếp trên cửa sổ dòng lệnh mà không cần giao diện đồ họa phức tạp.
- Có thể chơi 2 người trên cùng máy

### Thuật toán: 
- Dùng mảng 2 chiều (board[20,20]) để lưu trạng thái của từng ô, đảm bảo mỗi lượt đi sẽ được lưu lại đúng vị trí.
- Thuật toán luân phiên người chơi, dùng biến curentPlayer để đảm bảo người chơi X/O sẽ luân phiên nhau đánh.
- Kiểm tra nước đi hợp lệ: khi người chơi nhập tọa độ (x,y) -> hệ thống kiểm tra điều kiện **0 <= x < Rowa && 0 <= O < Cols** và **board[x,y] ==0** => Nếu sai thì yêu cầu người chơi nhập lại
- Kiểm tra thắng: Với mỗi nước cờ mới đặt, kểm tra theo 4 hướng chính, đếm số quân liên tiếp giống nhau theo 2 chiều, nếu >= 5 thì thắng. 4 hướng chính gồm:
  - *Ngang (trái - phải)*
  - *Dọc (trên - dưới)*
  - *Chéo chính (trái trên - phải dưới)*
  - *Chéo phụ (phải trên - trái dưới)*  
### Input: 
- Một bàn cờ có kích thước rows & cols (ma trận 20x20).
- Người dùng nhập tọa độ đặt quân cờ (i,j) (đánh vào ô vị trí hàng i cột j)
- Lệnh nhập để chọn chế độ:
  - *1: Người chơi với người.*
  - *2: Chơi với máy.*
  - *3. Thoát game.*
### Output: 
- Hiển thị bàn cờ trên console
- Thông báo lượt chơi của X/O.
- Thông báo kết thúc trò chơi khi có một người thắng.

### Sau khi tạo xong project: viết các code xử lý dữ liệu, thuật toán logic game, giao diện console,... -> Build   
**Kết quả**: 
  <img width="1493" height="825" alt="image" src="https://github.com/user-attachments/assets/5d0f937d-d311-4c02-b525-1e267262e58b" />

**Kết quả khi đánh vào ô đã chọn X/O**:  
<img width="741" height="570" alt="image" src="https://github.com/user-attachments/assets/2a0d766f-939d-4eca-bd5f-eb9cec111a4d" />  


## 3. Tạo project Windows Forms Applications (.NET Framework 2.0) 
**Các bước tạo prject này ương tự như tạo project Console App (.NET Framework 2.0): Trong Solution Explorer chuột phải vào Solution CoCaro -> Add -> New Project**
- Tìm Windows Forms App (.NET Framework) trên thanh tìm kiếm
- Chọn Next -> Đặt tên project: WinFormCoCaro
- Phần Framework chọn .NET Framework 2.0 -> Create.
  <img width="1919" height="1023" alt="image" src="https://github.com/user-attachments/assets/b9af829a-253c-4a5e-a339-2f8ac6bcd793" />

**Khi đã tạo xong project -> References tới project Library_CoCaro: Click chuột phải vào project ConsoleCoCaro -> Add -> References -> Project -> Tích vào Library_CoCaro -> OK.**  
<img width="979" height="666" alt="image" src="https://github.com/user-attachments/assets/64e380ba-bebe-474a-803a-a8a055b8f963" />

### Mục đích:
**Mục đích** của project này là tạo dòng lệnh Console để chơi game cờ Caro, chương trình sẽ gọi DLL Library_CoCaro để kiểm tra xem DLL có hoạt động tốt trong giao diện dòng lệnh không. Người chơi sẽ nhập tọa độ X, O → Console gọi DLL xử lý → Console hiển thị kết quả (in bàn cờ bằng text).  

### Thiết kế giao diện: 
- Thiết kế giao diện cho game, ta sẽ mở file form1.cs ở chế độ thiết kế Design, thêm các box control, bàn cờ, panel để thiết kế giao diện cho game.
  <img width="1919" height="1014" alt="image" src="https://github.com/user-attachments/assets/d6704718-46ad-4115-896e-69293655bf1b" />

### Luồng chương trình
- Khi khởi động chương trình sẽ hiển thị form chính
- Người dùng nhập dữ liệu vào textbox
- Nhấn Button xử lý chương trình đọc dữ liệu từ textbox và gọi đến hàm trong DLL để xử lý và trả về kết quả.
- Kết quả sẽ hiển thị trên Label/MessageBox

### Build & Run có kết quả: 
  <img width="1356" height="820" alt="image" src="https://github.com/user-attachments/assets/7eb2d8b7-742d-41e9-8afb-a4ddd741b47e" />  

## 4. Tạo project - ASP.NET Web Application (.NET Framework 2.0)
**Trong Solution Explorer chuột phải vào Solution CoCaro -> Add -> New Project -> Tìm ASP.NET Web Application (.NET Framework 2.0) -> Đặt tên project: WebAppCoCaro -> Ở Framework chọn .NET Framework 2.0 -> Create**  
  <img width="1914" height="1015" alt="image" src="https://github.com/user-attachments/assets/ab3c959a-d30a-406e-ac5e-3fd1528135a6" />  
**Sau khi tạo xong project -> references đến DLL**
  <img width="985" height="673" alt="image" src="https://github.com/user-attachments/assets/56738eb4-23c5-4efd-874f-f30d2fe36104" />  

**Tạo file index.html để hiển thị giao diện, css và javarscript để xử lý sự kiện**: Chuột phải project -> Add -> New Item -> HTML Page -> đặt tên index.html  
**Tạo file backend api.aspx.cx, file này sẽ nhận input và gọi đến DLL để kiểm tra đường đi, lượt đi, tạo bàn cờ,...**: Chuột phải vào project -> Add -> New Item -> WebForm -> đặt tên file api.aspx.  
**Kết quả sau khi Build và Run solution**:  
  <img width="1914" height="1018" alt="image" src="https://github.com/user-attachments/assets/3135d14c-d642-4992-b2dc-77deae32ccb4" />  

## 5. Cấu hình IIS
### Các bước cài đặt và cấu hình IIS
- Mở control Panel -> Programes -> Turn Windows features on or off -> Tick Internet Information Services -> OK
- Mở IIS Manager -> Click chuột phải vào Sites -> Add website...
  <img width="1410" height="422" alt="Screenshot 2025-09-28 230655" src="https://github.com/user-attachments/assets/3d73ad50-2059-4dcc-be06-7349b0ca3d3c" />
#### Đặt:
  - Sitename:GameCoCaro
  - Physiscal path: D:\BAITAP\BT_LTWEB\CoCaro\WebAppCoCaro
  - Binding path: Type: http; IP Address: ALL Unassigned; Port: 80 
  - Hostname:gamecocaro.com
  <img width="834" height="889" alt="image" src="https://github.com/user-attachments/assets/6b659c27-765a-4761-a3f9-3658ab78a318" />
####Thêm Domain vào file hosts:
  - Mở nodepad và chọn Run as Administrator
  - Openfile: C:\Windows\System32\drivers\etc
  - Tạo domain local: 127.0.0.1 gamecocaro.com
  <img width="1171" height="759" alt="image" src="https://github.com/user-attachments/assets/54652255-df3a-4ac5-b24d-f4fd4e340f0b" />
#### Cấu hình Application Pool
- Mở IIS -> Application Pool
- Tìm đến Application Pool vừa tạo WebAppCoCaro
- Chuột phải -> Basic Settings, .NET chọn v2.0 
  <img width="1524" height="793" alt="image" src="https://github.com/user-attachments/assets/206facfa-49ab-4fb7-8a64-928540eafee1" />
- Phải đảm bảo trong thư mục WepAppCoCaro\bin có file DLL của 2 project 1 và 4 để chạy chương trình.
- Mở trình duyệt và tìm http://gamecocaro.com để chạy lên web.
#### Kết quả 
  <img width="1917" height="1011" alt="image" src="https://github.com/user-attachments/assets/072f1aa6-33a0-49fe-92f8-4ca574f825c5" />

# HẾT!
