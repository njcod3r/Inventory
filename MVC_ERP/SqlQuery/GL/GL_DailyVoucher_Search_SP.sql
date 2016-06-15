

 CREATE PROCEDURE [dbo].[GL_DailyVoucher_Search_SP] @SourceTable CodeName_Type READONLY,@Lang nvarchar(50), @CompanyId int,@BranchId int,@FinancialYearId int,@SearchType int,@TxtSearchVal nvarchar(100)  AS  

 if @SearchType=1
 begin

 SELECT top(100) GL_DailyVoucher.Id, GL_DailyVoucher.Code, GL_JournalTypes.NameE as JournalName, GL_DailyVoucher.TransactDate_Gregi, GL_DailyVoucher.Notes, GL_DailyVoucher.SourceNo, GL_DailyVoucher.TotalDebit,Source.NameE as SourceName 
 FROM   GL_DailyVoucher INNER JOIN GL_JournalTypes ON GL_DailyVoucher.JournalId = GL_JournalTypes.Id  INNER JOIN @SourceTable AS Source ON GL_DailyVoucher.Source =  Source.Id 
 where GL_DailyVoucher.FinancialYearId=@FinancialYearId and  GL_DailyVoucher.CompanyId=@CompanyId and GL_DailyVoucher.BranchId=@BranchId 
 and isnull(GL_DailyVoucher.Deleted,0)=0  and 
 (GL_DailyVoucher.Code=@TxtSearchVal or CONVERT(varchar(10), TransactDate_Gregi, 103) 
 like '%'+ @TxtSearchVal + '%'   or  GL_JournalTypes.NameE like '%'+ @TxtSearchVal + '%' 
 or  Source.NameE like '%'+ @TxtSearchVal + '%'   or GL_DailyVoucher.SourceNo =@TxtSearchVal or GL_DailyVoucher.Notes like '%'+ @TxtSearchVal + '%'  or  CONVERT(nvarchar(50),cast(GL_DailyVoucher.TotalDebit as float))= @TxtSearchVal)
 order by GL_DailyVoucher.Id desc ,GL_DailyVoucher.TransactDate_Gregi desc

 end

  if @SearchType=2
 begin

 SELECT top(100) GL_DailyVoucher.Id, GL_DailyVoucher.Code, GL_JournalTypes.NameE as JournalName, GL_DailyVoucher.TransactDate_Gregi, GL_DailyVoucher.Notes, GL_DailyVoucher.SourceNo, GL_DailyVoucher.TotalDebit,Source.NameE as SourceName 
 FROM   GL_DailyVoucher INNER JOIN GL_JournalTypes ON GL_DailyVoucher.JournalId = GL_JournalTypes.Id  INNER JOIN @SourceTable AS Source ON GL_DailyVoucher.Source =  Source.Id 
 where GL_DailyVoucher.FinancialYearId=@FinancialYearId and  GL_DailyVoucher.CompanyId=@CompanyId and GL_DailyVoucher.BranchId=@BranchId 
 and isnull(GL_DailyVoucher.Deleted,0)=0  and 
 (GL_DailyVoucher.Code=@TxtSearchVal or CONVERT(varchar(10), TransactDate_Gregi, 103) 
 = @TxtSearchVal    or  GL_JournalTypes.NameE = @TxtSearchVal 
 or  Source.NameE = @TxtSearchVal    or GL_DailyVoucher.SourceNo =@TxtSearchVal or GL_DailyVoucher.Notes = @TxtSearchVal   or  CONVERT(nvarchar(50),cast(GL_DailyVoucher.TotalDebit as float))= @TxtSearchVal)
 order by GL_DailyVoucher.Id desc ,GL_DailyVoucher.TransactDate_Gregi desc

 end

 if @SearchType=3
 begin

 SELECT top(100) GL_DailyVoucher.Id, GL_DailyVoucher.Code, GL_JournalTypes.NameE as JournalName, GL_DailyVoucher.TransactDate_Gregi, GL_DailyVoucher.Notes, GL_DailyVoucher.SourceNo, GL_DailyVoucher.TotalDebit,Source.NameE as SourceName 
 FROM   GL_DailyVoucher INNER JOIN GL_JournalTypes ON GL_DailyVoucher.JournalId = GL_JournalTypes.Id  INNER JOIN @SourceTable AS Source ON GL_DailyVoucher.Source =  Source.Id 
 where GL_DailyVoucher.FinancialYearId=@FinancialYearId and  GL_DailyVoucher.CompanyId=@CompanyId and GL_DailyVoucher.BranchId=@BranchId 
 and isnull(GL_DailyVoucher.Deleted,0)=0  and 
 (GL_DailyVoucher.Code  like  @TxtSearchVal + '%' or CONVERT(varchar(10), TransactDate_Gregi, 103) 
 like  @TxtSearchVal + '%'   or  GL_JournalTypes.NameE like  @TxtSearchVal + '%' 
 or  Source.NameE like  @TxtSearchVal + '%'   or GL_DailyVoucher.SourceNo  like  @TxtSearchVal + '%' or GL_DailyVoucher.Notes like  @TxtSearchVal + '%'  or  CONVERT(nvarchar(50),cast(GL_DailyVoucher.TotalDebit as float))  like  @TxtSearchVal + '%')
 order by GL_DailyVoucher.Id desc ,GL_DailyVoucher.TransactDate_Gregi desc

 end








