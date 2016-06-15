--declare @CompanyId int=1
--declare @BranchId int=1
--declare @FirstDate datetime ='11/23/2014'
--declare @LastDate  datetime = '1/1/2015'
--declare @Scope int      =17   --  0 All accounts
--declare @ShowZeroBalances int = 1  --1 show all accounts 0 show only account have balances 
--declare @TypeOfShowing int =1  --  1 Show all balances  2 show debit balances  3 show credit balances 
--declare @level int=1
--declare @AccCode nvarchar(50)=1




 ---------Scope 0 All Accounts---------------

-- First insert account tree in temp table for performance 
if OBJECT_ID('tempdb..#GL_ChartOfAccounts') is not null drop table #GL_ChartOfAccounts
select * into  #GL_ChartOfAccounts from GLView_ChartOfAccounts

if @Scope = 0
begin


select * from (
select Code as AccountCode,AccSort, NameA,NameE,FirstDebit as FirstDebit,FirstCredit as FirstCredit,PeriodDebit as PeriodDebit,PeriodCredit as PeriodCredit,  
FirstDebit + PeriodDebit as  TotalDebit, FirstCredit + PeriodCredit as TotalCredit,
case when (FirstDebit+PeriodDebit)-(FirstCredit+PeriodCredit) >0 then  (FirstDebit+PeriodDebit)-(FirstCredit+PeriodCredit) else 0 end as LastDebit,
case when (FirstCredit+PeriodCredit)-(FirstDebit+PeriodDebit) >0 then  (FirstCredit+PeriodCredit)-(FirstDebit+PeriodDebit) else 0 end as LastCredit
from(
select Code,AccSort,NameA,NameE,sum(FirstDebit) as FirstDebit,sum(FirstCredit) as FirstCredit,sum(PeriodDebit) as PeriodDebit,sum(PeriodCredit) as PeriodCredit from (

--Select Opening Balance Query
SELECT        GL_ChartOfAccounts.Code,SUBSTRING (GL_ChartOfAccounts.Code, 1, 1) as AccSort, GL_ChartOfAccounts.NameA, GL_ChartOfAccounts.NameE, isnull(DailyVoucher.FirstDebit,0) as FirstDebit, isnull(DailyVoucher.FirstCredit,0) as FirstCredit,0 as PeriodDebit,0 as PeriodCredit
FROM            (SELECT       GL_DailyVoucher_DTL.AccountId, 
case when SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0))- SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0)) > 0 then SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0))- SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))  else 0 end AS FirstDebit,
case when SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))- SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)) > 0 then SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))-SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)) else 0 end AS FirstCredit
 FROM   GL_DailyVoucher INNER JOIN GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
        WHERE  GL_DailyVoucher.CompanyId IN (@CompanyId) AND GL_DailyVoucher.BranchId IN (@BranchId) AND cast (GL_DailyVoucher_DTL.TransactDate_Gregi as date) < @FirstDate
		GROUP BY GL_DailyVoucher_DTL.AccountId) AS DailyVoucher RIGHT OUTER JOIN
        GL_ChartOfAccounts ON DailyVoucher.AccountId = GL_ChartOfAccounts.Id where GL_ChartOfAccounts.CompanyId in(@CompanyId) and GL_ChartOfAccounts.HasChildren=0 

union all

-- Select Transaction Query
SELECT         GL_ChartOfAccounts.Code ,SUBSTRING (GL_ChartOfAccounts.Code, 1, 1) as AccSort, GL_ChartOfAccounts.NameA, GL_ChartOfAccounts.NameE, 0 as FirstDebit,0 as FirstCredit,
              SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)) AS PeriodDebit, SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0)) AS PeriodCredit
FROM            GL_DailyVoucher INNER JOIN
                         GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId  INNER JOIN
                         GL_ChartOfAccounts ON GL_DailyVoucher_DTL.AccountId = GL_ChartOfAccounts.Id
WHERE        GL_DailyVoucher.CompanyId IN (@CompanyId) AND GL_DailyVoucher.BranchId IN (@BranchId) AND cast(GL_DailyVoucher_DTL.TransactDate_Gregi as date) between @FirstDate and @LastDate
GROUP BY GL_ChartOfAccounts.Id, GL_ChartOfAccounts.Code,SUBSTRING (GL_ChartOfAccounts.Code, 1, 1), GL_ChartOfAccounts.NameA, GL_ChartOfAccounts.NameE



) as TblG1

 GROUP BY Code,AccSort, NameA,NameE) as TblG2
  )as TblG3

 where ABS( LastDebit- LastCredit)> IIF(@ShowZeroBalances=1,-1,0)  and LastDebit > IIF(@TypeOfShowing=2,0,-1) and LastCredit> IIF(@TypeOfShowing=3,0,-1)
 order by AccSort, cast (AccountCode as numeric)
 end 
 ---------End of Scope 0 All Accounts---------------

 ---------Scope Levels---------------
 if @Scope < 0
 begin

select * from (
select Code as AccountCode,AccSort,NameA,NameE,FirstDebit ,FirstCredit,PeriodDebit,PeriodCredit ,FirstDebit+ PeriodDebit as TotalDebit,FirstCredit+PeriodCredit as TotalCredit,
case when (FirstDebit+PeriodDebit)-(FirstCredit+PeriodCredit) >0 then  (FirstDebit+PeriodDebit)-(FirstCredit+PeriodCredit) else 0 end as LastDebit,
case when (FirstCredit+PeriodCredit)-(FirstDebit+PeriodDebit) >0 then  (FirstCredit+PeriodCredit)-(FirstDebit+PeriodDebit) else 0 end as LastCredit
from(
select Code,AccSort,NameA,NameE,sum(FirstDebit) as FirstDebit ,sum(FirstCredit) as FirstCredit,sum(PeriodDebit) as PeriodDebit,sum(PeriodCredit) as PeriodCredit  

from (
select Code,AccSort,NameA,NameE,
case when sum(FirstDebit)-sum(FirstCredit) > 0 then sum(FirstDebit)-sum(FirstCredit) else 0 end  as FirstDebit,
case when sum(FirstCredit)-sum(FirstDebit) > 0 then sum(FirstCredit)-sum(FirstDebit) else 0 end  as FirstCredit,
sum(PeriodDebit) as PeriodDebit,sum(PeriodCredit) as PeriodCredit from (

--Select Opening Balance Query
SELECT   
case @Scope when-1 then IIF(#GL_ChartOfAccounts.Parent1=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent1) when -2 then IIF(#GL_ChartOfAccounts.Parent2=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent2) when -3 then IIF(#GL_ChartOfAccounts.Parent3=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent3) when -4  then IIF(#GL_ChartOfAccounts.Parent4=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent4) when -5  then IIF(#GL_ChartOfAccounts.Parent5=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent5) when -6  then IIF(#GL_ChartOfAccounts.Parent6=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent6) when -7  then IIF(#GL_ChartOfAccounts.Parent7=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent7)  when -8  then IIF(#GL_ChartOfAccounts.Parent8=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent8) when -9  then IIF(#GL_ChartOfAccounts.Parent9=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent9)   else #GL_ChartOfAccounts.Code end as Code ,
case @Scope when-1 then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent1=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent1), 1, 1)  when -2 then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent2=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent2), 1, 1) when -3 then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent3=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent3), 1, 1) when -4  then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent4=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent4), 1, 1) when -5  then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent5=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent5), 1, 1) when -6  then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent6=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent6), 1, 1) when -7  then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent7=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent7), 1, 1)  when -8  then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent8=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent8), 1, 1) when -9  then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent9=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent9), 1, 1)   else SUBSTRING (#GL_ChartOfAccounts.Code, 1, 1)  end as AccSort, 
case @Scope when-1 then IIF(#GL_ChartOfAccounts.Parent1=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent1NameA) when -2 then IIF(#GL_ChartOfAccounts.Parent2=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent2NameA) when -3 then IIF(#GL_ChartOfAccounts.Parent3=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent3NameA) when -4  then IIF(#GL_ChartOfAccounts.Parent4=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent4NameA) when -5  then IIF(#GL_ChartOfAccounts.Parent5=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent5NameA) when -6  then IIF(#GL_ChartOfAccounts.Parent6=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent6NameA) when -7  then IIF(#GL_ChartOfAccounts.Parent7=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent7NameA)  when -8  then IIF(#GL_ChartOfAccounts.Parent8=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent8NameA) when -9  then IIF(#GL_ChartOfAccounts.Parent9=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent9NameA)  else #GL_ChartOfAccounts.NameA end as NameA ,
case @Scope when-1 then IIF(#GL_ChartOfAccounts.Parent1=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent1NameE) when -2 then IIF(#GL_ChartOfAccounts.Parent2=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent2NameE) when -3 then IIF(#GL_ChartOfAccounts.Parent3=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent3NameE) when -4  then IIF(#GL_ChartOfAccounts.Parent4=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent4NameE) when -5  then IIF(#GL_ChartOfAccounts.Parent5=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent5NameE) when -6  then IIF(#GL_ChartOfAccounts.Parent6=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent6NameE) when -7  then IIF(#GL_ChartOfAccounts.Parent7=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent7NameE)  when -8  then IIF(#GL_ChartOfAccounts.Parent8=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent8NameE) when -9  then IIF(#GL_ChartOfAccounts.Parent9=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent9NameE)  else #GL_ChartOfAccounts.NameE end as NameE ,
 isnull(DailyVoucher.FirstDebit,0) as FirstDebit, isnull(DailyVoucher.FirstCredit,0) as FirstCredit,0 as PeriodDebit,0 as PeriodCredit
FROM  (SELECT       GL_DailyVoucher_DTL.AccountId, 
case when SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0))- SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0)) > 0 then SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0))- SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))  else 0 end AS FirstDebit,
case when SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))- SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)) > 0 then SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))-SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)) else 0 end AS FirstCredit
FROM   GL_DailyVoucher INNER JOIN GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
WHERE  GL_DailyVoucher.CompanyId IN (@CompanyId) AND GL_DailyVoucher.BranchId IN (@BranchId) AND cast(GL_DailyVoucher_DTL.TransactDate_Gregi as date) < @FirstDate
GROUP BY GL_DailyVoucher_DTL.AccountId) AS DailyVoucher RIGHT OUTER JOIN
#GL_ChartOfAccounts ON DailyVoucher.AccountId = #GL_ChartOfAccounts.Id where #GL_ChartOfAccounts.CompanyId in(@CompanyId) and (#GL_ChartOfAccounts.Levels=ABS(@Scope) or  #GL_ChartOfAccounts.HasChildren=0)



) as OBG1 
group by Code,AccSort,NameA,NameE

union all



select Code,AccSort,NameA,NameE,sum(FirstDebit) as FirstDebit,sum(FirstCredit) as FirstCredit,sum(PeriodDebit) as PeriodDebit,sum(PeriodCredit) as PeriodCredit  from (
-- Select Transaction Query

SELECT   
case @Scope when-1 then IIF(#GL_ChartOfAccounts.Parent1=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent1) when -2 then IIF(#GL_ChartOfAccounts.Parent2=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent2) when -3 then IIF(#GL_ChartOfAccounts.Parent3=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent3) when -4  then IIF(#GL_ChartOfAccounts.Parent4=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent4) when -5  then IIF(#GL_ChartOfAccounts.Parent5=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent5) when -6  then IIF(#GL_ChartOfAccounts.Parent6=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent6) when -7  then IIF(#GL_ChartOfAccounts.Parent7=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent7)  when -8  then IIF(#GL_ChartOfAccounts.Parent8=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent8) when -9  then IIF(#GL_ChartOfAccounts.Parent9=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent9)   else #GL_ChartOfAccounts.Code end as Code ,
case @Scope when-1 then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent1=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent1), 1, 1)  when -2 then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent2=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent2), 1, 1) when -3 then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent3=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent3), 1, 1) when -4  then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent4=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent4), 1, 1) when -5  then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent5=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent5), 1, 1) when -6  then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent6=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent6), 1, 1) when -7  then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent7=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent7), 1, 1)  when -8  then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent8=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent8), 1, 1) when -9  then SUBSTRING (IIF(#GL_ChartOfAccounts.Parent9=0,#GL_ChartOfAccounts.Code,#GL_ChartOfAccounts.Parent9), 1, 1)   else SUBSTRING (#GL_ChartOfAccounts.Code, 1, 1)  end as AccSort, 
case @Scope when-1 then IIF(#GL_ChartOfAccounts.Parent1=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent1NameA) when -2 then IIF(#GL_ChartOfAccounts.Parent2=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent2NameA) when -3 then IIF(#GL_ChartOfAccounts.Parent3=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent3NameA) when -4  then IIF(#GL_ChartOfAccounts.Parent4=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent4NameA) when -5  then IIF(#GL_ChartOfAccounts.Parent5=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent5NameA) when -6  then IIF(#GL_ChartOfAccounts.Parent6=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent6NameA) when -7  then IIF(#GL_ChartOfAccounts.Parent7=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent7NameA)  when -8  then IIF(#GL_ChartOfAccounts.Parent8=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent8NameA) when -9  then IIF(#GL_ChartOfAccounts.Parent9=0,#GL_ChartOfAccounts.NameA,#GL_ChartOfAccounts.Parent9NameA)  else #GL_ChartOfAccounts.NameA end as NameA ,
case @Scope when-1 then IIF(#GL_ChartOfAccounts.Parent1=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent1NameE) when -2 then IIF(#GL_ChartOfAccounts.Parent2=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent2NameE) when -3 then IIF(#GL_ChartOfAccounts.Parent3=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent3NameE) when -4  then IIF(#GL_ChartOfAccounts.Parent4=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent4NameE) when -5  then IIF(#GL_ChartOfAccounts.Parent5=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent5NameE) when -6  then IIF(#GL_ChartOfAccounts.Parent6=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent6NameE) when -7  then IIF(#GL_ChartOfAccounts.Parent7=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent7NameE)  when -8  then IIF(#GL_ChartOfAccounts.Parent8=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent8NameE) when -9  then IIF(#GL_ChartOfAccounts.Parent9=0,#GL_ChartOfAccounts.NameE,#GL_ChartOfAccounts.Parent9NameE)  else #GL_ChartOfAccounts.NameE end as NameE ,
0 as FirstDebit,0 as FirstCredit,
ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0) AS PeriodDebit, ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0) AS PeriodCredit
FROM            GL_DailyVoucher INNER JOIN
                         GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId  INNER JOIN
                         #GL_ChartOfAccounts ON GL_DailyVoucher_DTL.AccountId = #GL_ChartOfAccounts.Id
WHERE        GL_DailyVoucher.CompanyId IN (@CompanyId) AND GL_DailyVoucher.BranchId IN (@BranchId) AND cast(GL_DailyVoucher_DTL.TransactDate_Gregi as date) between @FirstDate and @LastDate


) as PG1
 group by Code,AccSort,NameA,NameE) as TBLG1
 group by Code,AccSort,NameA,NameE) as TBLG2
 )as TBLG3
 where ABS( LastDebit- LastCredit)> IIF(@ShowZeroBalances=1,-1,0)  and LastDebit > IIF(@TypeOfShowing=2,0,-1) and LastCredit> IIF(@TypeOfShowing=3,0,-1)
 order by AccSort , CAST(AccountCode as numeric)


end
 ---------End of Scope Levels---------------

  ---------Specific Account Scope-------------
 if @Scope > 0
 begin

 -- First insert account tree in temp table for performance 
--if OBJECT_ID('tempdb..#GL_ChartOfAccounts') is not null drop table #GL_ChartOfAccounts
--select * into #GL_ChartOfAccounts  from GLView_ChartOfAccounts



select * from (
select Code as AccountCode,AccSort,NameA,NameE,FirstDebit ,FirstCredit,PeriodDebit,PeriodCredit ,FirstDebit+ PeriodDebit as TotalDebit,FirstCredit+PeriodCredit as TotalCredit,
case when (FirstDebit+PeriodDebit)-(FirstCredit+PeriodCredit) >0 then  (FirstDebit+PeriodDebit)-(FirstCredit+PeriodCredit) else 0 end as LastDebit,
case when (FirstCredit+PeriodCredit)-(FirstDebit+PeriodDebit) >0 then  (FirstCredit+PeriodCredit)-(FirstDebit+PeriodDebit) else 0 end as LastCredit
from(
select Code,AccSort,NameA,NameE,sum(FirstDebit) as FirstDebit ,sum(FirstCredit) as FirstCredit,sum(PeriodDebit) as PeriodDebit,sum(PeriodCredit) as PeriodCredit  

from (
select Code,AccSort,NameA,NameE,
case when sum(FirstDebit)-sum(FirstCredit) > 0 then sum(FirstDebit)-sum(FirstCredit) else 0 end  as FirstDebit,
case when sum(FirstCredit)-sum(FirstDebit) > 0 then sum(FirstCredit)-sum(FirstDebit) else 0 end  as FirstCredit,
sum(PeriodDebit) as PeriodDebit,sum(PeriodCredit) as PeriodCredit from (

--Select Opening Balance Query
SELECT   
 #GL_ChartOfAccounts.Code  as Code ,
SUBSTRING (#GL_ChartOfAccounts.Code, 1, 1)   AccSort, #GL_ChartOfAccounts.NameA  as NameA ,#GL_ChartOfAccounts.NameE  as NameE ,
 isnull(DailyVoucher.FirstDebit,0) as FirstDebit, isnull(DailyVoucher.FirstCredit,0) as FirstCredit,0 as PeriodDebit,0 as PeriodCredit
FROM  (SELECT       GL_DailyVoucher_DTL.AccountId, 
case when SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0))- SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0)) > 0 then SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0))- SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))  else 0 end AS FirstDebit,
case when SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))- SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)) > 0 then SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))-SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)) else 0 end AS FirstCredit
FROM   GL_DailyVoucher INNER JOIN GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
WHERE  GL_DailyVoucher.CompanyId IN (@CompanyId) AND GL_DailyVoucher.BranchId IN (@BranchId) AND cast(GL_DailyVoucher_DTL.TransactDate_Gregi as date) < @FirstDate
GROUP BY GL_DailyVoucher_DTL.AccountId) AS DailyVoucher RIGHT OUTER JOIN
#GL_ChartOfAccounts ON DailyVoucher.AccountId = #GL_ChartOfAccounts.Id where #GL_ChartOfAccounts.CompanyId in(@CompanyId) and  #GL_ChartOfAccounts.HasChildren=0 
and cast(#GL_ChartOfAccounts.Parent1 as numeric) between IIf(@level=1, @AccCode,-88888888888888888888888888) and IIf(@level=1, @AccCode,88888888888888888888888888) 
and cast(#GL_ChartOfAccounts.Parent2 as numeric) between IIf(@level=2, @AccCode,-88888888888888888888888888) and IIf(@level=2, @AccCode,88888888888888888888888888) 
and cast(#GL_ChartOfAccounts.Parent3 as numeric) between IIf(@level=3, @AccCode,-88888888888888888888888888) and IIf(@level=3, @AccCode,88888888888888888888888888) 
and cast(#GL_ChartOfAccounts.Parent4 as numeric) between IIf(@level=4, @AccCode,-88888888888888888888888888) and IIf(@level=4, @AccCode,88888888888888888888888888) 
and cast(#GL_ChartOfAccounts.Parent5 as numeric) between IIf(@level=5, @AccCode,-88888888888888888888888888) and IIf(@level=5, @AccCode,88888888888888888888888888) 
and cast(#GL_ChartOfAccounts.Parent6 as numeric) between IIf(@level=6, @AccCode,-88888888888888888888888888) and IIf(@level=6, @AccCode,88888888888888888888888888) 
and cast(#GL_ChartOfAccounts.Parent7 as numeric) between IIf(@level=7, @AccCode,-88888888888888888888888888) and IIf(@level=7, @AccCode,88888888888888888888888888) 
and cast(#GL_ChartOfAccounts.Parent8 as numeric) between IIf(@level=8, @AccCode,-88888888888888888888888888) and IIf(@level=8, @AccCode,88888888888888888888888888) 
and cast(#GL_ChartOfAccounts.Parent9 as numeric) between IIf(@level=9, @AccCode,-88888888888888888888888888) and IIf(@level=9, @AccCode,88888888888888888888888888) 



union all


-- Select Transaction Query
SELECT   
 #GL_ChartOfAccounts.Code  as Code ,
SUBSTRING (#GL_ChartOfAccounts.Code, 1, 1)   AccSort, #GL_ChartOfAccounts.NameA  as NameA ,#GL_ChartOfAccounts.NameE  as NameE ,
0 as FirstDebit,0 as FirstCredit,
ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0) AS PeriodDebit, ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0) AS PeriodCredit
FROM            GL_DailyVoucher INNER JOIN
                         GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId  INNER JOIN
                         #GL_ChartOfAccounts ON GL_DailyVoucher_DTL.AccountId = #GL_ChartOfAccounts.Id
WHERE        GL_DailyVoucher.CompanyId IN (@CompanyId) AND GL_DailyVoucher.BranchId IN (@BranchId) AND cast(GL_DailyVoucher_DTL.TransactDate_Gregi as date) between @FirstDate and @LastDate   
and cast(#GL_ChartOfAccounts.Parent1 as numeric) between IIf(@level=1, @AccCode,-88888888888888888888888888) and IIf(@level=1, @AccCode,88888888888888888888888888) 
and cast(#GL_ChartOfAccounts.Parent2 as numeric) between IIf(@level=2, @AccCode,-88888888888888888888888888) and IIf(@level=2, @AccCode,88888888888888888888888888) 
and cast(#GL_ChartOfAccounts.Parent3 as numeric) between IIf(@level=3, @AccCode,-88888888888888888888888888) and IIf(@level=3, @AccCode,88888888888888888888888888) 
and cast(#GL_ChartOfAccounts.Parent4 as numeric) between IIf(@level=4, @AccCode,-88888888888888888888888888) and IIf(@level=4, @AccCode,88888888888888888888888888) 
and cast(#GL_ChartOfAccounts.Parent5 as numeric) between IIf(@level=5, @AccCode,-88888888888888888888888888) and IIf(@level=5, @AccCode,88888888888888888888888888) 
and cast(#GL_ChartOfAccounts.Parent6 as numeric) between IIf(@level=6, @AccCode,-88888888888888888888888888) and IIf(@level=6, @AccCode,88888888888888888888888888) 
and cast(#GL_ChartOfAccounts.Parent7 as numeric) between IIf(@level=7, @AccCode,-88888888888888888888888888) and IIf(@level=7, @AccCode,88888888888888888888888888) 
and cast(#GL_ChartOfAccounts.Parent8 as numeric) between IIf(@level=8, @AccCode,-88888888888888888888888888) and IIf(@level=8, @AccCode,88888888888888888888888888) 
and cast(#GL_ChartOfAccounts.Parent9 as numeric) between IIf(@level=9, @AccCode,-88888888888888888888888888) and IIf(@level=9, @AccCode,88888888888888888888888888) 


) as PG1
 group by Code,AccSort,NameA,NameE) as TBLG1
 group by Code,AccSort,NameA,NameE) as TBLG2
 )as TBLG3
 where ABS( LastDebit- LastCredit)> IIF(@ShowZeroBalances=1,-1,0)  and LastDebit > IIF(@TypeOfShowing=2,0,-1) and LastCredit> IIF(@TypeOfShowing=3,0,-1)
 order by AccSort , CAST(AccountCode as numeric)



 end

  ---------End Specific Account Scope-------------








