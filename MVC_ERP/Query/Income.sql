declare @CompanyId int=1
declare @BranchId int=1
declare @CurrentYearEndDate datetime ='12/31/2014'
declare @CompareYearEndDate  datetime = '12/31/2013'
declare @Scope int =-9




select AccountCode,AccSort,NameA,NameE,sum(CurrentYearValue) as CurrentYearValue, sum(PreviousYearValue) as PreviousYearValue from(
-------------Current Year
select  AccountCode,AccSort,NameA,NameE,Value as CurrentYearValue, 0 as PreviousYearValue from (
--Revenue
select AccountCode,AccSort,NameE,NameA ,sum(Value) as Value from(

SELECT    case @Scope when-1 then IIF(GLView_ChartOfAccounts.Parent1=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent1) when -2 then IIF(GLView_ChartOfAccounts.Parent2=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent2) when -3 then IIF(GLView_ChartOfAccounts.Parent3=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent3) when -4  then IIF(GLView_ChartOfAccounts.Parent4=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent4) when -5  then IIF(GLView_ChartOfAccounts.Parent5=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent5) when -6  then IIF(GLView_ChartOfAccounts.Parent6=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent6) when -7  then IIF(GLView_ChartOfAccounts.Parent7=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent7)  when -8  then IIF(GLView_ChartOfAccounts.Parent8=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent8) when -9  then IIF(GLView_ChartOfAccounts.Parent9=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent9)   else GLView_ChartOfAccounts.Code end as AccountCode ,
          case @Scope when-1 then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent1=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent1), 1, 1)  when -2 then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent2=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent2), 1, 1) when -3 then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent3=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent3), 1, 1) when -4  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent4=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent4), 1, 1) when -5  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent5=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent5), 1, 1) when -6  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent6=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent6), 1, 1) when -7  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent7=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent7), 1, 1)  when -8  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent8=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent8), 1, 1) when -9  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent9=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent9), 1, 1)   else SUBSTRING (GLView_ChartOfAccounts.Code, 1, 1)  end as AccSort, 
          case @Scope when-1 then IIF(GLView_ChartOfAccounts.Parent1=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent1NameA) when -2 then IIF(GLView_ChartOfAccounts.Parent2=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent2NameA) when -3 then IIF(GLView_ChartOfAccounts.Parent3=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent3NameA) when -4  then IIF(GLView_ChartOfAccounts.Parent4=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent4NameA) when -5  then IIF(GLView_ChartOfAccounts.Parent5=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent5NameA) when -6  then IIF(GLView_ChartOfAccounts.Parent6=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent6NameA) when -7  then IIF(GLView_ChartOfAccounts.Parent7=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent7NameA)  when -8  then IIF(GLView_ChartOfAccounts.Parent8=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent8NameA) when -9  then IIF(GLView_ChartOfAccounts.Parent9=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent9NameA)  else GLView_ChartOfAccounts.NameA end as NameA ,
          case @Scope when-1 then IIF(GLView_ChartOfAccounts.Parent1=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent1NameE) when -2 then IIF(GLView_ChartOfAccounts.Parent2=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent2NameE) when -3 then IIF(GLView_ChartOfAccounts.Parent3=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent3NameE) when -4  then IIF(GLView_ChartOfAccounts.Parent4=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent4NameE) when -5  then IIF(GLView_ChartOfAccounts.Parent5=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent5NameE) when -6  then IIF(GLView_ChartOfAccounts.Parent6=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent6NameE) when -7  then IIF(GLView_ChartOfAccounts.Parent7=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent7NameE)  when -8  then IIF(GLView_ChartOfAccounts.Parent8=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent8NameE) when -9  then IIF(GLView_ChartOfAccounts.Parent9=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent9NameE)  else GLView_ChartOfAccounts.NameE end as NameE,
         Value
						 
FROM            (SELECT        GL_DailyVoucher_DTL.AccountId,ISNULL( SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))-SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)),0) AS Value
                           FROM            GL_DailyVoucher INNER JOIN
                                                    GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
                           WHERE        (GL_DailyVoucher.CompanyId IN (@CompanyId)) AND (GL_DailyVoucher.BranchId IN (@BranchId)) AND (CAST(GL_DailyVoucher_DTL.TransactDate_Gregi AS date) 
                                                    <= @CurrentYearEndDate)
                           GROUP BY GL_DailyVoucher_DTL.AccountId) AS DailyVoucher inner JOIN
                         GLView_ChartOfAccounts ON DailyVoucher.AccountId = GLView_ChartOfAccounts.Id where GLView_ChartOfAccounts.CompanyId in (@CompanyId)  and isnull(GLView_ChartOfAccounts.Deleted,0)=0 and Parent1 in  (SELECT Value FROM fn_Split((select distinct RevenueAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId and isnull(RevenueAccountIds,'')<>''), ','))

) as TBL1 

group by  AccountCode,AccSort,NameE,NameA




union all

--Expenses

select Code,AccSort,NameE,NameA ,sum(Value) as Value from(

SELECT    case @Scope when-1 then IIF(GLView_ChartOfAccounts.Parent1=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent1) when -2 then IIF(GLView_ChartOfAccounts.Parent2=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent2) when -3 then IIF(GLView_ChartOfAccounts.Parent3=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent3) when -4  then IIF(GLView_ChartOfAccounts.Parent4=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent4) when -5  then IIF(GLView_ChartOfAccounts.Parent5=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent5) when -6  then IIF(GLView_ChartOfAccounts.Parent6=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent6) when -7  then IIF(GLView_ChartOfAccounts.Parent7=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent7)  when -8  then IIF(GLView_ChartOfAccounts.Parent8=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent8) when -9  then IIF(GLView_ChartOfAccounts.Parent9=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent9)   else GLView_ChartOfAccounts.Code end as Code ,
          case @Scope when-1 then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent1=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent1), 1, 1)  when -2 then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent2=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent2), 1, 1) when -3 then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent3=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent3), 1, 1) when -4  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent4=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent4), 1, 1) when -5  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent5=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent5), 1, 1) when -6  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent6=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent6), 1, 1) when -7  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent7=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent7), 1, 1)  when -8  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent8=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent8), 1, 1) when -9  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent9=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent9), 1, 1)   else SUBSTRING (GLView_ChartOfAccounts.Code, 1, 1)  end as AccSort, 
          case @Scope when-1 then IIF(GLView_ChartOfAccounts.Parent1=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent1NameA) when -2 then IIF(GLView_ChartOfAccounts.Parent2=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent2NameA) when -3 then IIF(GLView_ChartOfAccounts.Parent3=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent3NameA) when -4  then IIF(GLView_ChartOfAccounts.Parent4=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent4NameA) when -5  then IIF(GLView_ChartOfAccounts.Parent5=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent5NameA) when -6  then IIF(GLView_ChartOfAccounts.Parent6=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent6NameA) when -7  then IIF(GLView_ChartOfAccounts.Parent7=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent7NameA)  when -8  then IIF(GLView_ChartOfAccounts.Parent8=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent8NameA) when -9  then IIF(GLView_ChartOfAccounts.Parent9=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent9NameA)  else GLView_ChartOfAccounts.NameA end as NameA ,
          case @Scope when-1 then IIF(GLView_ChartOfAccounts.Parent1=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent1NameE) when -2 then IIF(GLView_ChartOfAccounts.Parent2=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent2NameE) when -3 then IIF(GLView_ChartOfAccounts.Parent3=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent3NameE) when -4  then IIF(GLView_ChartOfAccounts.Parent4=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent4NameE) when -5  then IIF(GLView_ChartOfAccounts.Parent5=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent5NameE) when -6  then IIF(GLView_ChartOfAccounts.Parent6=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent6NameE) when -7  then IIF(GLView_ChartOfAccounts.Parent7=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent7NameE)  when -8  then IIF(GLView_ChartOfAccounts.Parent8=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent8NameE) when -9  then IIF(GLView_ChartOfAccounts.Parent9=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent9NameE)  else GLView_ChartOfAccounts.NameE end as NameE,
         Value
		 FROM            (SELECT        GL_DailyVoucher_DTL.AccountId,ISNULL( SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))-SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)),0) AS Value
                           FROM            GL_DailyVoucher INNER JOIN
                                                    GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
                           WHERE        (GL_DailyVoucher.CompanyId IN (@CompanyId)) AND (GL_DailyVoucher.BranchId IN (@BranchId)) AND (CAST(GL_DailyVoucher_DTL.TransactDate_Gregi AS date) 
                                                    <= @CurrentYearEndDate)
                           GROUP BY GL_DailyVoucher_DTL.AccountId) AS DailyVoucher inner JOIN
                         GLView_ChartOfAccounts ON DailyVoucher.AccountId = GLView_ChartOfAccounts.Id where GLView_ChartOfAccounts.CompanyId in (@CompanyId)  and isnull(GLView_ChartOfAccounts.Deleted,0)=0 and Parent1 in  (SELECT Value FROM fn_Split((select distinct ExpensesAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId and isnull(ExpensesAccountIds,'')<>''), ','))

) as TBL1 

group by  Code,AccSort,NameE,NameA


union all 
select '999999999999999' ,-9,'’«›Ï —»Õ Œ”«—… «·› —…','Period Net Loss & Profit',sum(Value)

FROM            (SELECT        GL_DailyVoucher_DTL.AccountId,ISNULL( SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))-SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)),0) AS Value
                           FROM            GL_DailyVoucher INNER JOIN
                                                    GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
                           WHERE        (GL_DailyVoucher.CompanyId IN (@CompanyId)) AND (GL_DailyVoucher.BranchId IN (@BranchId)) AND (CAST(GL_DailyVoucher_DTL.TransactDate_Gregi AS date) 
                                                    <= @CurrentYearEndDate)
                           GROUP BY GL_DailyVoucher_DTL.AccountId) AS DailyVoucher inner JOIN
                         GLView_ChartOfAccounts ON DailyVoucher.AccountId = GLView_ChartOfAccounts.Id where GLView_ChartOfAccounts.CompanyId in (@CompanyId)  and isnull(GLView_ChartOfAccounts.Deleted,0)=0
						 and  GLView_ChartOfAccounts.Parent1 in  (
                         SELECT Value FROM fn_Split((select distinct RevenueAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId and isnull(RevenueAccountIds,'')<>''), ',')
                         union all
                         SELECT Value FROM fn_Split((select distinct ExpensesAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId and isnull(ExpensesAccountIds,'')<>''), ',')
                           )



) as CurrentYear

-------------End Current Year-----------------
union all
-------------Previous Year--------------------
select AccountCode,AccSort,NameA,NameE,0 as CurrentYearValue,Value as PreviousYearValue from (
--Assets
select AccountCode,AccSort,NameE,NameA ,sum(Value) as Value from(

SELECT    case @Scope when-1 then IIF(GLView_ChartOfAccounts.Parent1=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent1) when -2 then IIF(GLView_ChartOfAccounts.Parent2=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent2) when -3 then IIF(GLView_ChartOfAccounts.Parent3=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent3) when -4  then IIF(GLView_ChartOfAccounts.Parent4=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent4) when -5  then IIF(GLView_ChartOfAccounts.Parent5=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent5) when -6  then IIF(GLView_ChartOfAccounts.Parent6=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent6) when -7  then IIF(GLView_ChartOfAccounts.Parent7=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent7)  when -8  then IIF(GLView_ChartOfAccounts.Parent8=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent8) when -9  then IIF(GLView_ChartOfAccounts.Parent9=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent9)   else GLView_ChartOfAccounts.Code end as AccountCode ,
          case @Scope when-1 then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent1=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent1), 1, 1)  when -2 then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent2=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent2), 1, 1) when -3 then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent3=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent3), 1, 1) when -4  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent4=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent4), 1, 1) when -5  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent5=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent5), 1, 1) when -6  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent6=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent6), 1, 1) when -7  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent7=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent7), 1, 1)  when -8  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent8=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent8), 1, 1) when -9  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent9=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent9), 1, 1)   else SUBSTRING (GLView_ChartOfAccounts.Code, 1, 1)  end as AccSort, 
          case @Scope when-1 then IIF(GLView_ChartOfAccounts.Parent1=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent1NameA) when -2 then IIF(GLView_ChartOfAccounts.Parent2=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent2NameA) when -3 then IIF(GLView_ChartOfAccounts.Parent3=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent3NameA) when -4  then IIF(GLView_ChartOfAccounts.Parent4=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent4NameA) when -5  then IIF(GLView_ChartOfAccounts.Parent5=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent5NameA) when -6  then IIF(GLView_ChartOfAccounts.Parent6=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent6NameA) when -7  then IIF(GLView_ChartOfAccounts.Parent7=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent7NameA)  when -8  then IIF(GLView_ChartOfAccounts.Parent8=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent8NameA) when -9  then IIF(GLView_ChartOfAccounts.Parent9=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent9NameA)  else GLView_ChartOfAccounts.NameA end as NameA ,
          case @Scope when-1 then IIF(GLView_ChartOfAccounts.Parent1=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent1NameE) when -2 then IIF(GLView_ChartOfAccounts.Parent2=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent2NameE) when -3 then IIF(GLView_ChartOfAccounts.Parent3=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent3NameE) when -4  then IIF(GLView_ChartOfAccounts.Parent4=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent4NameE) when -5  then IIF(GLView_ChartOfAccounts.Parent5=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent5NameE) when -6  then IIF(GLView_ChartOfAccounts.Parent6=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent6NameE) when -7  then IIF(GLView_ChartOfAccounts.Parent7=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent7NameE)  when -8  then IIF(GLView_ChartOfAccounts.Parent8=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent8NameE) when -9  then IIF(GLView_ChartOfAccounts.Parent9=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent9NameE)  else GLView_ChartOfAccounts.NameE end as NameE,
         Value
						 
				 
FROM            (SELECT        GL_DailyVoucher_DTL.AccountId,ISNULL( SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))-SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)),0) AS Value
                           FROM            GL_DailyVoucher INNER JOIN
                                                    GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
                           WHERE        (GL_DailyVoucher.CompanyId IN (@CompanyId)) AND (GL_DailyVoucher.BranchId IN (@BranchId)) AND (CAST(GL_DailyVoucher_DTL.TransactDate_Gregi AS date) 
                                                    <= @CompareYearEndDate)
                           GROUP BY GL_DailyVoucher_DTL.AccountId) AS DailyVoucher inner JOIN
                         GLView_ChartOfAccounts ON DailyVoucher.AccountId = GLView_ChartOfAccounts.Id where GLView_ChartOfAccounts.CompanyId in (@CompanyId)  and isnull(GLView_ChartOfAccounts.Deleted,0)=0 and Parent1 in  (SELECT Value FROM fn_Split((select distinct RevenueAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId and isnull(RevenueAccountIds,'')<>''), ','))

) as TBL1 

group by AccountCode,AccSort,NameE,NameA

union all

--Liabilities

select AccountCode,AccSort,NameE,NameA ,sum(Value) as Value from(

SELECT    case @Scope when-1 then IIF(GLView_ChartOfAccounts.Parent1=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent1) when -2 then IIF(GLView_ChartOfAccounts.Parent2=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent2) when -3 then IIF(GLView_ChartOfAccounts.Parent3=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent3) when -4  then IIF(GLView_ChartOfAccounts.Parent4=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent4) when -5  then IIF(GLView_ChartOfAccounts.Parent5=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent5) when -6  then IIF(GLView_ChartOfAccounts.Parent6=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent6) when -7  then IIF(GLView_ChartOfAccounts.Parent7=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent7)  when -8  then IIF(GLView_ChartOfAccounts.Parent8=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent8) when -9  then IIF(GLView_ChartOfAccounts.Parent9=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent9)   else GLView_ChartOfAccounts.Code end as AccountCode ,
          case @Scope when-1 then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent1=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent1), 1, 1)  when -2 then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent2=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent2), 1, 1) when -3 then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent3=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent3), 1, 1) when -4  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent4=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent4), 1, 1) when -5  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent5=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent5), 1, 1) when -6  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent6=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent6), 1, 1) when -7  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent7=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent7), 1, 1)  when -8  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent8=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent8), 1, 1) when -9  then SUBSTRING (IIF(GLView_ChartOfAccounts.Parent9=0,GLView_ChartOfAccounts.Code,GLView_ChartOfAccounts.Parent9), 1, 1)   else SUBSTRING (GLView_ChartOfAccounts.Code, 1, 1)  end as AccSort, 
          case @Scope when-1 then IIF(GLView_ChartOfAccounts.Parent1=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent1NameA) when -2 then IIF(GLView_ChartOfAccounts.Parent2=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent2NameA) when -3 then IIF(GLView_ChartOfAccounts.Parent3=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent3NameA) when -4  then IIF(GLView_ChartOfAccounts.Parent4=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent4NameA) when -5  then IIF(GLView_ChartOfAccounts.Parent5=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent5NameA) when -6  then IIF(GLView_ChartOfAccounts.Parent6=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent6NameA) when -7  then IIF(GLView_ChartOfAccounts.Parent7=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent7NameA)  when -8  then IIF(GLView_ChartOfAccounts.Parent8=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent8NameA) when -9  then IIF(GLView_ChartOfAccounts.Parent9=0,GLView_ChartOfAccounts.NameA,GLView_ChartOfAccounts.Parent9NameA)  else GLView_ChartOfAccounts.NameA end as NameA ,
          case @Scope when-1 then IIF(GLView_ChartOfAccounts.Parent1=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent1NameE) when -2 then IIF(GLView_ChartOfAccounts.Parent2=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent2NameE) when -3 then IIF(GLView_ChartOfAccounts.Parent3=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent3NameE) when -4  then IIF(GLView_ChartOfAccounts.Parent4=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent4NameE) when -5  then IIF(GLView_ChartOfAccounts.Parent5=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent5NameE) when -6  then IIF(GLView_ChartOfAccounts.Parent6=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent6NameE) when -7  then IIF(GLView_ChartOfAccounts.Parent7=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent7NameE)  when -8  then IIF(GLView_ChartOfAccounts.Parent8=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent8NameE) when -9  then IIF(GLView_ChartOfAccounts.Parent9=0,GLView_ChartOfAccounts.NameE,GLView_ChartOfAccounts.Parent9NameE)  else GLView_ChartOfAccounts.NameE end as NameE,
         Value
		 						 
FROM            (SELECT        GL_DailyVoucher_DTL.AccountId,ISNULL( SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))-SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)),0) AS Value
                           FROM            GL_DailyVoucher INNER JOIN
                                                    GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
                           WHERE        (GL_DailyVoucher.CompanyId IN (@CompanyId)) AND (GL_DailyVoucher.BranchId IN (@BranchId)) AND (CAST(GL_DailyVoucher_DTL.TransactDate_Gregi AS date) 
                                                    <= @CompareYearEndDate)
                           GROUP BY GL_DailyVoucher_DTL.AccountId) AS DailyVoucher inner JOIN
                         GLView_ChartOfAccounts ON DailyVoucher.AccountId = GLView_ChartOfAccounts.Id where GLView_ChartOfAccounts.CompanyId in (@CompanyId)  and isnull(GLView_ChartOfAccounts.Deleted,0)=0 and Parent1 in  (SELECT Value FROM fn_Split((select distinct ExpensesAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId and isnull(ExpensesAccountIds,'')<>''), ','))

) as TBL1 

group by AccountCode,AccSort,NameE,NameA

union all 
select '999999999999999' ,-9,'’«›Ï —»Õ Œ”«—… «·› —…','Period Net Loss & Profit',sum(Value)

FROM            (SELECT        GL_DailyVoucher_DTL.AccountId,ISNULL( SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))-SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)),0) AS Value
                           FROM            GL_DailyVoucher INNER JOIN
                                                    GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
                           WHERE        (GL_DailyVoucher.CompanyId IN (@CompanyId)) AND (GL_DailyVoucher.BranchId IN (@BranchId)) AND (CAST(GL_DailyVoucher_DTL.TransactDate_Gregi AS date) 
                                                    <= @CompareYearEndDate)
                           GROUP BY GL_DailyVoucher_DTL.AccountId) AS DailyVoucher inner JOIN
                         GLView_ChartOfAccounts ON DailyVoucher.AccountId = GLView_ChartOfAccounts.Id where GLView_ChartOfAccounts.CompanyId in (@CompanyId)  and isnull(GLView_ChartOfAccounts.Deleted,0)=0
						 and  GLView_ChartOfAccounts.Parent1 in  (
                         SELECT Value FROM fn_Split((select distinct RevenueAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId and isnull(RevenueAccountIds,'')<>''), ',')
                         union all
                         SELECT Value FROM fn_Split((select distinct ExpensesAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId and isnull(ExpensesAccountIds,'')<>''), ',')
                           )


) as PreviousYear
------------- End of Previous Year-------------
) as finalTbl1
group by  AccountCode,AccSort,NameA,NameE
order by AccSort desc,AccountCode




