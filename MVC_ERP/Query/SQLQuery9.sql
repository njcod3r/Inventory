declare @Scope int=-1
if OBJECT_ID('tempdb..#GL_ChartOfAccounts') is not null drop table #GL_ChartOfAccounts
select * into #GL_ChartOfAccounts  from GLView_ChartOfAccounts

select Code,AccSort,NameA,NameE,FirstDebit ,FirstCredit,PeriodDebit,PeriodCredit ,FirstDebit+ PeriodDebit as TotalDebit,FirstCredit+PeriodCredit as TotalCredit,
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
case @Scope when-1 then #GL_ChartOfAccounts.Parent1 when -2 then #GL_ChartOfAccounts.Parent2 when -3 then #GL_ChartOfAccounts.Parent3 when -4  then #GL_ChartOfAccounts.Parent4 when -5  then #GL_ChartOfAccounts.Parent5 when -6  then #GL_ChartOfAccounts.Parent6 when -7  then #GL_ChartOfAccounts.Parent7  when -8  then #GL_ChartOfAccounts.Parent8 when -9  then #GL_ChartOfAccounts.Parent9   else 0 end as Code ,
case @Scope when-1 then SUBSTRING (#GL_ChartOfAccounts.Parent1, 1, 1)  when -2 then SUBSTRING (#GL_ChartOfAccounts.Parent2, 1, 1) when -3 then SUBSTRING (#GL_ChartOfAccounts.Parent3, 1, 1) when -4  then SUBSTRING (#GL_ChartOfAccounts.Parent4, 1, 1) when -5  then SUBSTRING (#GL_ChartOfAccounts.Parent5, 1, 1) when -6  then SUBSTRING (#GL_ChartOfAccounts.Parent6, 1, 1) when -7  then SUBSTRING (#GL_ChartOfAccounts.Parent7, 1, 1)  when -8  then SUBSTRING (#GL_ChartOfAccounts.Parent8, 1, 1) when -9  then SUBSTRING (#GL_ChartOfAccounts.Parent9, 1, 1)   else 0 end as AccSort, 
case @Scope when-1 then #GL_ChartOfAccounts.Parent1NameA when -2 then #GL_ChartOfAccounts.Parent2NameA when -3 then #GL_ChartOfAccounts.Parent3NameA when -4  then #GL_ChartOfAccounts.Parent4NameA when -5  then #GL_ChartOfAccounts.Parent5NameA when -6  then #GL_ChartOfAccounts.Parent6NameA when -7  then #GL_ChartOfAccounts.Parent7NameA  when -8  then #GL_ChartOfAccounts.Parent8NameA when -9  then #GL_ChartOfAccounts.Parent9NameA   else '' end as NameA ,
case @Scope when-1 then #GL_ChartOfAccounts.Parent1NameE when -2 then #GL_ChartOfAccounts.Parent2NameE when -3 then #GL_ChartOfAccounts.Parent3NameE when -4  then #GL_ChartOfAccounts.Parent4NameE when -5  then #GL_ChartOfAccounts.Parent5NameE when -6  then #GL_ChartOfAccounts.Parent6NameE when -7  then #GL_ChartOfAccounts.Parent7NameE  when -8  then #GL_ChartOfAccounts.Parent8NameE when -9  then #GL_ChartOfAccounts.Parent9NameE   else '' end as NameE ,
 isnull(DailyVoucher.FirstDebit,0) as FirstDebit, isnull(DailyVoucher.FirstCredit,0) as FirstCredit,0 as PeriodDebit,0 as PeriodCredit
FROM  (SELECT       GL_DailyVoucher_DTL.AccountId, 
case when SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0))- SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0)) > 0 then SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0))- SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))  else 0 end AS FirstDebit,
case when SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))- SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)) > 0 then SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))-SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)) else 0 end AS FirstCredit
 FROM   GL_DailyVoucher INNER JOIN GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
 WHERE  GL_DailyVoucher.CompanyId IN (1) AND GL_DailyVoucher.BranchId IN (1) AND GL_DailyVoucher_DTL.TransactDate_Gregi < '11/25/2014'
 GROUP BY GL_DailyVoucher_DTL.AccountId) AS DailyVoucher RIGHT OUTER JOIN
  #GL_ChartOfAccounts ON DailyVoucher.AccountId = #GL_ChartOfAccounts.Id where #GL_ChartOfAccounts.CompanyId in(1)
  ) as OBG1 
  group by Code,AccSort,NameA,NameE

  union all


-- Select Transaction Query
select Code,AccSort,NameA,NameE,sum(FirstDebit) as FirstDebit,sum(FirstCredit) as FirstCredit,sum(PeriodDebit) as PeriodDebit,sum(PeriodCredit) as PeriodCredit  from (
SELECT   
case @Scope when-1 then #GL_ChartOfAccounts.Parent1 when -2 then #GL_ChartOfAccounts.Parent2 when -3 then #GL_ChartOfAccounts.Parent3 when -4  then #GL_ChartOfAccounts.Parent4 when -5  then #GL_ChartOfAccounts.Parent5 when -6  then #GL_ChartOfAccounts.Parent6 when -7  then #GL_ChartOfAccounts.Parent7  when -8  then #GL_ChartOfAccounts.Parent8 when -9  then #GL_ChartOfAccounts.Parent9   else '0' end as Code ,
case @Scope when-1 then SUBSTRING (#GL_ChartOfAccounts.Parent1, 1, 1)  when -2 then SUBSTRING (#GL_ChartOfAccounts.Parent2, 1, 1) when -3 then SUBSTRING (#GL_ChartOfAccounts.Parent3, 1, 1) when -4  then SUBSTRING (#GL_ChartOfAccounts.Parent4, 1, 1) when -5  then SUBSTRING (#GL_ChartOfAccounts.Parent5, 1, 1) when -6  then SUBSTRING (#GL_ChartOfAccounts.Parent6, 1, 1) when -7  then SUBSTRING (#GL_ChartOfAccounts.Parent7, 1, 1)  when -8  then SUBSTRING (#GL_ChartOfAccounts.Parent8, 1, 1) when -9  then SUBSTRING (#GL_ChartOfAccounts.Parent9, 1, 1)   else 0 end as AccSort, 
case @Scope when-1 then #GL_ChartOfAccounts.Parent1NameA when -2 then #GL_ChartOfAccounts.Parent2NameA when -3 then #GL_ChartOfAccounts.Parent3NameA when -4  then #GL_ChartOfAccounts.Parent4NameA when -5  then #GL_ChartOfAccounts.Parent5NameA when -6  then #GL_ChartOfAccounts.Parent6NameA when -7  then #GL_ChartOfAccounts.Parent7NameA  when -8  then #GL_ChartOfAccounts.Parent8NameA when -9  then #GL_ChartOfAccounts.Parent9NameA   else '' end as NameA ,
case @Scope when-1 then #GL_ChartOfAccounts.Parent1NameE when -2 then #GL_ChartOfAccounts.Parent2NameE when -3 then #GL_ChartOfAccounts.Parent3NameE when -4  then #GL_ChartOfAccounts.Parent4NameE when -5  then #GL_ChartOfAccounts.Parent5NameE when -6  then #GL_ChartOfAccounts.Parent6NameE when -7  then #GL_ChartOfAccounts.Parent7NameE  when -8  then #GL_ChartOfAccounts.Parent8NameE when -9  then #GL_ChartOfAccounts.Parent9NameE   else '' end as NameE ,
0 as FirstDebit,0 as FirstCredit,
              ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0) AS PeriodDebit, ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0) AS PeriodCredit
FROM            GL_DailyVoucher INNER JOIN
                         GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId  INNER JOIN
                         #GL_ChartOfAccounts ON GL_DailyVoucher_DTL.AccountId = #GL_ChartOfAccounts.Id
WHERE        GL_DailyVoucher.CompanyId IN (1) AND GL_DailyVoucher.BranchId IN (1) AND GL_DailyVoucher_DTL.TransactDate_Gregi between '11/25/2014' and '1/1/2015'
) as PG1
 group by Code,AccSort,NameA,NameE) as TBLG1
  group by Code,AccSort,NameA,NameE) as TBLG2

  



