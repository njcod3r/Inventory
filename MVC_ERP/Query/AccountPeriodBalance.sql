declare @CompanyId int=1
declare @BranchId int=1
declare @AccountId int =34
declare @FinancialYearId int=2
declare @PeriodTbl table ( idx int identity(1,1), Id int,Code nvarchar(50) ,NameE nvarchar(50),NameA nvarchar(50), FinancialYearId int,PeriodStartDate_Gregi date,PeriodEndDate_Gregi date)
declare @ResulTbl table ( AccountId int , AccountCode nvarchar(50),NameE nvarchar(50),NameA nvarchar(50),PeriodNameE nvarchar(50),PeriodNameA nvarchar(50),Debit float,Credit float,Balance float )

DECLARE @PeriodRowsCount  int
DECLARE @CurrentRow     int
DECLARE @PeriodFirstDate     date
DECLARE @PeriodLastDate     date
DECLARE @NameE nvarchar(50)
DECLARE @NameA nvarchar(50)


--First Balance
insert into @ResulTbl ( AccountId  , AccountCode,NameE ,NameA ,PeriodNameE ,PeriodNameA ,Debit ,Credit ,Balance  )
select AccountId,AccountCode,NameE,NameA,PeriodNameE,PeriodNameA,sum(Debit) as Debit,sum(Credit) as Credit,sum(Debit)-sum(Credit) as Balance
          from(
SELECT        GL_DailyVoucher_DTL.AccountId, GL_ChartOfAccounts.Code as AccountCode, GL_ChartOfAccounts.NameA, GL_ChartOfAccounts.NameE,'First Balance' as PeriodNameE,'—’Ìœ ”«»ﬁ' as PeriodNameA, GL_DailyVoucher_DTL.DebitLocal as Debit, 
                         GL_DailyVoucher_DTL.CreditLocal as Credit
FROM            GL_DailyVoucher INNER JOIN
                         GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId INNER JOIN
                         GL_ChartOfAccounts ON GL_DailyVoucher_DTL.AccountId = GL_ChartOfAccounts.Id

						 where GL_DailyVoucher_DTL.AccountId=@AccountId and GL_DailyVoucher.CompanyId in (@CompanyId) and GL_DailyVoucher.BranchId in (@BranchId) and GL_DailyVoucher_DTL.TransactDate_Gregi<(select StartDate_Gregi from GL_FinancialYears where Id= @FinancialYearId)
	) as Tbl
	 group by  AccountId,AccountCode,NameE,NameA,PeriodNameE,PeriodNameA

--Period Balance
INSERT INTO @PeriodTbl (Id ,Code  ,NameE ,NameA , FinancialYearId ,PeriodStartDate_Gregi ,PeriodEndDate_Gregi )
SELECT Id ,Code  ,NameE ,NameA , FinancialYearId ,PeriodStartDate_Gregi ,PeriodEndDate_Gregi from GL_FinancialYears_Periods where FinancialYearId=@FinancialYearId
--Get Period Count
SET @PeriodRowsCount=@@ROWCOUNT

 
SET @CurrentRow=0
WHILE @CurrentRow<@PeriodRowsCount
BEGIN
    SET @CurrentRow=@CurrentRow+1
    SELECT 
	    @NameE=NameE,
    	@NameA=NameA,
        @PeriodFirstDate=PeriodStartDate_Gregi,
	    @PeriodLastDate=PeriodEndDate_Gregi
        FROM @PeriodTbl
        WHERE idx=@CurrentRow

    --do your thing here--
--select @NameE,@NameA,@PeriodFirstDate,@PeriodLastDate
insert into @ResulTbl ( AccountId  , AccountCode,NameE ,NameA ,PeriodNameE ,PeriodNameA ,Debit ,Credit ,Balance  )
select AccountId,AccountCode,NameE,NameA,PeriodNameE,PeriodNameA,sum(isnull(Debit,0)) as Debit,sum(isnull(Credit,0)) as Credit,sum(isnull(Debit,0))-sum(isnull(Credit,0)) as Balance
          from(


SELECT        GL_ChartOfAccounts.Id as AccountId, GL_ChartOfAccounts.Code as AccountCode, GL_ChartOfAccounts.NameA, GL_ChartOfAccounts.NameE,@NameE as PeriodNameE,@NameA as PeriodNameA, DailyVoucher.DebitLocal as Debit, 
                         DailyVoucher.CreditLocal as Credit, DailyVoucher.CompanyId, DailyVoucher.BranchId
FROM            (SELECT        GL_DailyVoucher_DTL.AccountId, GL_DailyVoucher_DTL.DebitLocal, GL_DailyVoucher_DTL.CreditLocal, GL_DailyVoucher.CompanyId, 
                         GL_DailyVoucher.BranchId, GL_DailyVoucher_DTL.TransactDate_Gregi
FROM            GL_DailyVoucher INNER JOIN
                         GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
						 	WHERE GL_DailyVoucher.CompanyId IN (@CompanyId) AND GL_DailyVoucher.BranchId IN (@BranchId) AND 
                         GL_DailyVoucher_DTL.TransactDate_Gregi BETWEEN @PeriodFirstDate AND @PeriodLastDate	
						 ) AS DailyVoucher RIGHT OUTER JOIN
                    
						 GL_ChartOfAccounts ON DailyVoucher.AccountId = GL_ChartOfAccounts.Id
						 where GL_ChartOfAccounts.Id = @AccountId



	) as Tbl
	 group by  AccountId,AccountCode,NameE,NameA,PeriodNameE,PeriodNameA


END

select * from  @ResulTbl