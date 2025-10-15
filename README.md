# EF9 Database First Lab

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

3. Database first 第一道指令。在目標專案目錄執行。
```bash
dotnet ef dbcontext scaffold Name=DefaultConnection ` 指定 appsettings.json 設定的連線字串
  Microsoft.EntityFrameworkCore.SqlServer           ` 指定 database provider
  --context-dir Data                           ` 生成 DbContext 目錄
  --output-dir Schema                               ` 生成 entitys 目錄
  --startup-project ../FirstDatabaseLab             ` 指定到起始專案目錄不然找不到 appsettings.json
  --data-annotations --force                        ` 生成 annotation 且強制覆寫。
```
