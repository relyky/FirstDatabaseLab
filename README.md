# EF9 Database First Lab

# 操作行為目標
database first → 調整 schema → migration → update → 調整 schema → migration → update → ...

# 主要步驟
1. 安裝套件
```
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.SqlServer
```
主專案一定要安裝的套件
```
Microsoft.EntityFrameworkCore.Design
```

2. 安裝EF工具程式
有兩個版本，二擇一。 
 - [Entity Framework Core 工具參考 - 封裝管理員 Visual Studio 中的控制台](https://learn.microsoft.com/zh-tw/ef/core/cli/powershell) --- 失敗(Orz)
 - [Entity Framework Core 工具參考 - .NET CLI](https://learn.microsoft.com/zh-tw/ef/core/cli/dotnet)

```bash
dotnet tool install --global dotnet-ef
```

3. Database first 第一道指令，

在方案目錄執行
```bash
dotnet ef dbcontext scaffold Name=DefaultConnection ` 指定 appsettings.json 設定的連線字串。
  Microsoft.EntityFrameworkCore.SqlServer           ` 指定 database provider。
  --context-dir Data                                ` 生成 Data 目錄。
  --output-dir Schema                               ` 生成 entitys 目錄。
  --project ./FirstDatabaseLab.DB                   ` 指定目標專案目錄。
  --startup-project ./FirstDatabaseLab              ` 指定到起始專案目錄不然找不到 appsettings.json。
  --data-annotations --force                        ` 生成 annotation 且強制覆寫。
```

或在目標專案目錄執行。
```bash
dotnet ef dbcontext scaffold Name=DefaultConnection ` 指定 appsettings.json 設定的連線字串
  Microsoft.EntityFrameworkCore.SqlServer           ` 指定 database provider
  --context-dir Data                                ` 生成 DbContext 目錄
  --output-dir Schema                               ` 生成 entitys 目錄
  --startup-project ../FirstDatabaseLab             ` 指定到起始專案目錄不然找不到 appsettings.json
  --data-annotations --force                        ` 生成 annotation 且強制覆寫。
```

4. 組織 Database Context 服務。
※設定好 DbContext 服務，後序 migration 才能成功。
```program.cs
builder.Services.AddDbContext<MyTestDbContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("MyTestDB_SqlServer"));
});
```

@15:58   
5.Add-Migration <name>
Remove-Migrtion
※若欲關聯的 DbContext 服務未組織會無法執行。 

```bash
dotnet ef migrations add InitialScaffoldDatabase ` 建立第一個 migration 並指定名稱。
 --project ./FirstDatabaseLab.DB                 ` 指定目標專案目錄。
 --startup-project ./FirstDatabaseLab            ` 指定到起始專案目錄不然找不到 appsettings.json。
```  

5A.Remove-Migrtion - 也試一下移除 Migrtion
```base
dotnet ef migrations remove
 --project ./FirstDatabaseLab.DB       ` 指定目標專案目錄。
 --startup-project ./FirstDatabaseLab  ` 指定到起始專案目錄不然找不到 appsettings.json。
```
試完 Remove-Migrtion 再重新 Add-Migration。不然無法繼續練習。

6.加入新欄位



--------
dotnet ef migrations add InitialScaffoldDatabase --project ./FirstDatabaseLab.DB --startup-project ./FirstDatabaseLab            

dotnet ef migrations add AddDeveoper --project ./FirstDatabaseLab.DB --startup-project ./FirstDatabaseLab

