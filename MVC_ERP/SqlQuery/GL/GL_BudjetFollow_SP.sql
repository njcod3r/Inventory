CREATE PROCEDURE [dbo].[GL_BudjetFollow_SP]
	-- Add the parameters for the stored procedure here
        @CompanyId int,
		@BranchId nvarchar(max),
		@FinancialYearId int
AS
BEGIN


declare @PeriodTbl table ( idx int identity(1,1), Id int,Code nvarchar(50) ,NameE nvarchar(50),NameA nvarchar(50), FinancialYearId int,PeriodStartDate_Gregi date,PeriodEndDate_Gregi date)
declare @ResulTbl table ( AccountId int , AccountCode nvarchar(50),NameE nvarchar(50),NameA nvarchar(50),PeriodId int,PeriodNameE nvarchar(50),PeriodNameA nvarchar(50),ActualBalance float,EstimateBalance float,CompleteRate float )
declare @PeriodRowsCount  int
declare @CurrentRow     int
declare @PeriodFirstDate     date
declare @PeriodLastDate     date
declare @NameE as nvarchar(50)
declare @NameA as nvarchar(50)
declare @PeriodId     int



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
	    @PeriodId=Id,
	    @NameE=NameE,
    	@NameA=NameA,
        @PeriodFirstDate=PeriodStartDate_Gregi,
	    @PeriodLastDate=PeriodEndDate_Gregi
        FROM @PeriodTbl
        WHERE idx=@CurrentRow

    --do your thing here--

insert into @ResulTbl ( AccountId  , AccountCode,NameE ,NameA ,PeriodId,PeriodNameE ,PeriodNameA ,ActualBalance ,EstimateBalance,CompleteRate )
select AccountIds,AccountCode,NameE,NameA,PeriodId,PeriodNameE,PeriodNameA,isnull(ActualBalance,0),isnull(EstimateBalance,0),case when isnull(EstimateBalance,0)=0 then 0 else isnull(ActualBalance,0)/isnull(EstimateBalance,0) end
 from(
select AccountIds,AccountCode,NameE,NameA,PeriodId,PeriodNameE,PeriodNameA,sum(isnull(Debit,0))-sum(isnull(Credit,0)) as ActualBalance,

(SELECT        SUM(isnull(GL_EstimateBudget_Periods.DebitLocal,0)) - sum(isnull(GL_EstimateBudget_Periods.CreditLocal,0)) as EstimateBalance
FROM            GL_EstimateBudget INNER JOIN
                         GL_EstimateBudget_DTL ON GL_EstimateBudget.Id = GL_EstimateBudget_DTL.EstimateBudgetId INNER JOIN
                         GL_EstimateBudget_Periods ON GL_EstimateBudget.Id = GL_EstimateBudget_Periods.EstimateBudgetId AND 
                         GL_EstimateBudget_DTL.Id = GL_EstimateBudget_Periods.EstimateBudgetRowId

						 where CompanyId in(@CompanyId) and BranchId in (SELECT Value FROM dbo.fn_Split(@BranchId,',')) and AccountId =AccountIds and PeriodId=@PeriodId) as EstimateBalance
          from(


SELECT        GL_ChartOfAccounts.Id as AccountIds, GL_ChartOfAccounts.Code as AccountCode, GL_ChartOfAccounts.NameA, GL_ChartOfAccounts.NameE,@PeriodId as PeriodId ,@NameE as PeriodNameE,@NameA as PeriodNameA, DailyVoucher.DebitLocal as Debit, 
                         DailyVoucher.CreditLocal as Credit, DailyVoucher.CompanyId, DailyVoucher.BranchId
FROM            (SELECT        GL_DailyVoucher_DTL.AccountId, GL_DailyVoucher_DTL.DebitLocal, GL_DailyVoucher_DTL.CreditLocal, GL_DailyVoucher.CompanyId, 
                         GL_DailyVoucher.BranchId, GL_DailyVoucher_DTL.TransactDate_Gregi
FROM            GL_DailyVoucher INNER JOIN
                         GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
						 	WHERE GL_DailyVoucher.CompanyId IN (@CompanyId) AND GL_DailyVoucher.BranchId IN (SELECT Value FROM dbo.fn_Split(@BranchId,',')) AND 
                         cast(GL_DailyVoucher_DTL.TransactDate_Gregi as date) BETWEEN @PeriodFirstDate AND @PeriodLastDate	
						 ) AS DailyVoucher RIGHT OUTER JOIN
                    
						 GL_ChartOfAccounts  ON DailyVoucher.AccountId = GL_ChartOfAccounts.Id
						where GL_ChartOfAccounts.Id in(select distinct GL_EstimateBudget_DTL.AccountId from  GL_EstimateBudget_DTL inner join GL_EstimateBudget on GL_EstimateBudget_DTL.EstimateBudgetId =GL_EstimateBudget.Id
						 where GL_EstimateBudget.CompanyId in(@CompanyId) and GL_EstimateBudget.BranchId in (@BranchId) and GL_EstimateBudget.FinancialYearId=@FinancialYearId)



	) as Tbl
	 group by  AccountIds,AccountCode,NameE,NameA,PeriodId,PeriodNameE,PeriodNameA
	 ) as TBL2

END

select * from  @ResulTbl
order by AccountCode,PeriodId



END





