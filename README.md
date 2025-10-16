# EF9 Database First Lab

# 操作行為練習目標
database first → 調整 schema → migration → update → 調整 schema → migration → update → ...

# § 先總結整理 database first 使用指令
再重新整理 EF Tools 於 database first 程序使用指令。經拆解化整為零只有三道步驟，四個指令如下：

```bash
-- 指令順序(省略細部參數)
-- step 1: 自 DB 同步 scheam。
dotnet ef dbcontext scaffold <connection> <provider> 
  
-- step 2: 自動製作 migration 『schema 差異指令』。
dotnet ef migrations add <name>

-- step 2a: [option] 移除最近一次的 migration，可能不滿意想重作。
dotnet ef migrations remove 

-- step 3: 把最近一次的 migration 送到目標 DB 並執行『schema 差異指令』。
dotnet ef database update
```
之後再用此四個指令為基礎化零為整。

# § 練習步驟
## 1. 安裝套件
```
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.SqlServer
```
主專案一定要安裝的套件
```
Microsoft.EntityFrameworkCore.Design
```

## 2. 安裝EF工具程式
有兩個版本，二擇一。 
 - [Entity Framework Core 工具參考 - 封裝管理員 Visual Studio 中的控制台](https://learn.microsoft.com/zh-tw/ef/core/cli/powershell)   
   （失敗Orz。此指令模式只能在標準的 Powershell CLI 視窗執行的樣子？）
 - [Entity Framework Core 工具參考 - .NET CLI](https://learn.microsoft.com/zh-tw/ef/core/cli/dotnet)

```bash
dotnet tool install --global dotnet-ef
```

## 3. Database first 第一道指令，

在方案目錄執行
```bash
dotnet ef dbcontext scaffold Name=DefaultConnection ` 指定 appsettings.json 設定的連線字串。
  Microsoft.EntityFrameworkCore.SqlServer           ` 指定 database provider。
  --output-dir Schema                               ` 生成 entitys 放置目錄。
  --project ./FirstDatabaseLab.DB                   ` 指定目標專案目錄。
  --startup-project ./FirstDatabaseLab              ` 指定到起始專案目錄不然找不到 appsettings.json。
  --data-annotations --force                        ` 生成 annotation 且強制覆寫。
  --no-pluralize                                    ` 禁用複數化。
```

## 4. 組織 Database Context 服務。
※設定好 DbContext 服務，後序 migration 才能成功。
```C#
// program.cs
builder.Services.AddDbContext<MyTestDbContext>();
// 或
builder.Services.AddDbContext<MyTestDbContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
```
   
## 5.Add-Migration <name> (第一次 Add-Migration)
> ※若欲關聯的 DbContext 服務未組織會無法執行。 

```bash
dotnet ef migrations add InitialScaffoldDatabase ` 建立第一個 migration 並指定名稱。
 --project ./FirstDatabaseLab.DB                 ` 指定目標專案目錄。
 --startup-project ./FirstDatabaseLab            ` 指定到起始專案目錄不然找不到 appsettings.json。
```  

## 5a.Remove-Migrtion
 移除最近一次 Migrtion。若前步驟生成的 migration 不滿意，移除再重作。
```bash
dotnet ef migrations remove            ` 移除最近一次的 migration。
 --project ./FirstDatabaseLab.DB       ` 指定目標專案目錄。
 --startup-project ./FirstDatabaseLab  ` 指定到起始專案目錄不然找不到 appsettings.json。
```
試完 Remove-Migrtion 再重新 Add-Migration。不然無法繼續練習。

## 6.第一次 Database Update (第一次更新 DB Schema)
> ※第一次 Database Update 應該會失敗，因為 table 是從已存在的 DB Schema 跑 scaffold 生成的。再重新建立一次當然會失敗。

```bash
dotnet ef database update              ` 上傳最近一次 migration 到 DB 並執行。
 --project ./FirstDatabaseLab.DB       ` 指定目標專案目錄。
 --startup-project ./FirstDatabaseLab  ` 指定到起始專案目錄不然找不到 appsettings.json。
```

## 6a.清空第一個 Migration Up/Down 再重新 Database Update
先手動暫時清空 InitialScaffoldDatabase Migration Up/Down method 再 update 一次。    
`database update` 完成後還原剛清空的 Up/Down method。

```C#
public partial class InitialScaffoldDatabase : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    // 先手動暫時清空，於`database update`完成後還原。
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    // 先手動暫時清空，於`database update`完成後還原。
  }
}
```
## 7.Add-Migration AddDeveloper
 變更 shcema 宣告，例：加入新欄位 Developer。
 ```C#
 [StringLength(50)]
 public string Developer { get; set; } = null!; 
 ```
 產生差異的 migration 並命名為 `AddDeveloper`。

```bash
dotnet ef migrations add AddDeveloper  ` 產生差異的 migration 並命名為 AddDeveloper。
 --project ./FirstDatabaseLab.DB       ` 指定目標專案目錄。
 --startup-project ./FirstDatabaseLab  ` 指定到起始專案目錄不然找不到 appsettings.json。
```

## 8.Update-Database
```bash
dotnet ef database update             ` 上傳最近一次 migration 到 DB 並執行。
 --project ./FirstDatabaseLab.DB      ` 指定目標專案目錄。
 --startup-project ./FirstDatabaseLab ` 指定到起始專案目錄不然找不到 appsettings.json。
```
## 9.覆反精進
再來就是重覆調整 schema 產生 migration 再 update 到資料庫。整理後表述成
```
datagase first → first migration → first update
 → migration → update
 → migration → update
 → ... 
```
若想保證 scheam 的一致性就重新跑一輪 scaffold 流程。

# § 沒圖沒真象

## 執行紀錄(省略雜訊)
```bash
**********************************************************************
** Visual Studio 2022 Developer PowerShell v17.14.16
** Copyright (c) 2025 Microsoft Corporation
**********************************************************************
PS> dotnet ef dbcontext scaffold Name=DefaultConnection Microsoft.EntityFrameworkCore.SqlServer --output-dir Schema --project ./FirstDatabaseLab.DB --startup-project ./FirstDatabaseLab --data-annotations --force --no-pluralize
Build started...
Build succeeded.

PS> dotnet ef migrations add InitialScaffoldDatabase --project ./FirstDatabaseLab.DB --startup-project ./FirstDatabaseLab
Build started...
Build succeeded.
Done. To undo this action, use 'ef migrations remove'

PS> dotnet ef database update --project ./FirstDatabaseLab.DB --startup-project ./FirstDatabaseLab
Build started...
Build succeeded.
info: ...略...
      Applying migration '20251016014322_InitialScaffoldDatabase'.
Applying migration '20251016014322_InitialScaffoldDatabase'.
fail: ...略...
Error Number:2714,State:6,Class:16
資料庫中已經有一個名為 'Customer' 的物件。

-- `database update`失敗是因為 'Customer' 本來就存在是經由 scaffold 拿到，再新建一次當然會失敗。
-- 手動操作：把 Migration Up/Down 暫時清除。再動新 `database update`。

PS> dotnet ef database update --project ./FirstDatabaseLab.DB --startup-project ./FirstDatabaseLab
Build started...
Build succeeded.
info: ...略...
Done.

-- 手動操作：調整 schema，此例：加入新欄位 Developer。
-- 下一步：自動生成差異 scheam migration 指令。並取名為 AddDeveloper。

PS> dotnet ef migrations add AddDeveloper --project ./FirstDatabaseLab.DB --startup-project ./FirstDatabaseLab
Build started...
Build succeeded.
Done. To undo this action, use 'ef migrations remove'

PS> dotnet ef database update --project ./FirstDatabaseLab.DB --startup-project ./FirstDatabaseLab
Build started...
Build succeeded.
info: ...略...
Done.
```

## 參考文件
[EF Core 9 🚀 Database First / DB First (Entity Framework Core 9 / .NET 9) & Code-First Migration](https://www.youtube.com/watch?v=NoDk6JVVLkw)

(EOF)