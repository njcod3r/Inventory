declare @CompanyId int=1
declare @BranchId int=1
declare @FirstDate datetime ='11/23/2014'
declare @LastDate  datetime = '1/1/2015'
declare @ShowZeroBalances int = 1 --1 show all accounts 0 show only account have balances 
declare @level int=-1

select Code,NameE,NameA, Debit, Credit, Balance from(
select Code,NameE,NameA,sum(Debit) as Debit,sum(Credit) as Credit,sum(Debit)-sum(Credit) as Balance  from (
SELECT    
case @level when-1 then IIF(GLView_ChartOfCostCenter.Parent1=0,GLView_ChartOfCostCenter.Code,GLView_ChartOfCostCenter.Parent1) when -2 then IIF(GLView_ChartOfCostCenter.Parent2=0,GLView_ChartOfCostCenter.Code,GLView_ChartOfCostCenter.Parent2) when -3 then IIF(GLView_ChartOfCostCenter.Parent3=0,GLView_ChartOfCostCenter.Code,GLView_ChartOfCostCenter.Parent3) when -4  then IIF(GLView_ChartOfCostCenter.Parent4=0,GLView_ChartOfCostCenter.Code,GLView_ChartOfCostCenter.Parent4) when -5  then IIF(GLView_ChartOfCostCenter.Parent5=0,GLView_ChartOfCostCenter.Code,GLView_ChartOfCostCenter.Parent5) when -6  then IIF(GLView_ChartOfCostCenter.Parent6=0,GLView_ChartOfCostCenter.Code,GLView_ChartOfCostCenter.Parent6) when -7  then IIF(GLView_ChartOfCostCenter.Parent7=0,GLView_ChartOfCostCenter.Code,GLView_ChartOfCostCenter.Parent7)  when -8  then IIF(GLView_ChartOfCostCenter.Parent8=0,GLView_ChartOfCostCenter.Code,GLView_ChartOfCostCenter.Parent8) when -9  then IIF(GLView_ChartOfCostCenter.Parent9=0,GLView_ChartOfCostCenter.Code,GLView_ChartOfCostCenter.Parent9)   else GLView_ChartOfCostCenter.Code end as Code ,
case @level when-1 then IIF(GLView_ChartOfCostCenter.Parent1=0,GLView_ChartOfCostCenter.NameA,GLView_ChartOfCostCenter.Parent1NameA) when -2 then IIF(GLView_ChartOfCostCenter.Parent2=0,GLView_ChartOfCostCenter.NameA,GLView_ChartOfCostCenter.Parent2NameA) when -3 then IIF(GLView_ChartOfCostCenter.Parent3=0,GLView_ChartOfCostCenter.NameA,GLView_ChartOfCostCenter.Parent3NameA) when -4  then IIF(GLView_ChartOfCostCenter.Parent4=0,GLView_ChartOfCostCenter.NameA,GLView_ChartOfCostCenter.Parent4NameA) when -5  then IIF(GLView_ChartOfCostCenter.Parent5=0,GLView_ChartOfCostCenter.NameA,GLView_ChartOfCostCenter.Parent5NameA) when -6  then IIF(GLView_ChartOfCostCenter.Parent6=0,GLView_ChartOfCostCenter.NameA,GLView_ChartOfCostCenter.Parent6NameA) when -7  then IIF(GLView_ChartOfCostCenter.Parent7=0,GLView_ChartOfCostCenter.NameA,GLView_ChartOfCostCenter.Parent7NameA)  when -8  then IIF(GLView_ChartOfCostCenter.Parent8=0,GLView_ChartOfCostCenter.NameA,GLView_ChartOfCostCenter.Parent8NameA) when -9  then IIF(GLView_ChartOfCostCenter.Parent9=0,GLView_ChartOfCostCenter.NameA,GLView_ChartOfCostCenter.Parent9NameA)  else GLView_ChartOfCostCenter.NameA end as NameA ,
case @level when-1 then IIF(GLView_ChartOfCostCenter.Parent1=0,GLView_ChartOfCostCenter.NameE,GLView_ChartOfCostCenter.Parent1NameE) when -2 then IIF(GLView_ChartOfCostCenter.Parent2=0,GLView_ChartOfCostCenter.NameE,GLView_ChartOfCostCenter.Parent2NameE) when -3 then IIF(GLView_ChartOfCostCenter.Parent3=0,GLView_ChartOfCostCenter.NameE,GLView_ChartOfCostCenter.Parent3NameE) when -4  then IIF(GLView_ChartOfCostCenter.Parent4=0,GLView_ChartOfCostCenter.NameE,GLView_ChartOfCostCenter.Parent4NameE) when -5  then IIF(GLView_ChartOfCostCenter.Parent5=0,GLView_ChartOfCostCenter.NameE,GLView_ChartOfCostCenter.Parent5NameE) when -6  then IIF(GLView_ChartOfCostCenter.Parent6=0,GLView_ChartOfCostCenter.NameE,GLView_ChartOfCostCenter.Parent6NameE) when -7  then IIF(GLView_ChartOfCostCenter.Parent7=0,GLView_ChartOfCostCenter.NameE,GLView_ChartOfCostCenter.Parent7NameE)  when -8  then IIF(GLView_ChartOfCostCenter.Parent8=0,GLView_ChartOfCostCenter.NameE,GLView_ChartOfCostCenter.Parent8NameE) when -9  then IIF(GLView_ChartOfCostCenter.Parent9=0,GLView_ChartOfCostCenter.NameE,GLView_ChartOfCostCenter.Parent9NameE)  else GLView_ChartOfCostCenter.NameE end as NameE ,
isnull( GL_DailyVoucher_CostCenter.DebitLocal,0) AS Debit, isnull(GL_DailyVoucher_CostCenter.CreditLocal,0) AS Credit

FROM            (SELECT        GL_DailyVoucher_CostCenter_1.CostCenterId, GL_DailyVoucher_DTL.TransactDate_Gregi, GL_DailyVoucher_DTL.TransactDate_Hijri, 
                                                    GL_DailyVoucher.MemberShipId, GL_DailyVoucher.CompanyId, GL_DailyVoucher.BranchId,  GL_DailyVoucher_CostCenter_1.DebitLocal, 
                                                   GL_DailyVoucher_CostCenter_1.CreditLocal
                           FROM            GL_DailyVoucher_CostCenter AS GL_DailyVoucher_CostCenter_1 INNER JOIN
                                                    GL_DailyVoucher_DTL ON GL_DailyVoucher_CostCenter_1.DailyVoucherRowId = GL_DailyVoucher_DTL.Id INNER JOIN
                                                    GL_DailyVoucher ON GL_DailyVoucher_CostCenter_1.DailyVoucherId = GL_DailyVoucher.Id AND 
                                                    GL_DailyVoucher_DTL.DailyVoucherId = GL_DailyVoucher.Id
													
WHERE  GL_DailyVoucher.CompanyId in(@CompanyId) AND GL_DailyVoucher.BranchId in(@BranchId)
             AND cast(GL_DailyVoucher.TransactDate_Gregi as date) between @FirstDate and @LastDate
													) AS GL_DailyVoucher_CostCenter RIGHT OUTER JOIN
                         GLView_ChartOfCostCenter ON GL_DailyVoucher_CostCenter.CostCenterId = GLView_ChartOfCostCenter.Id where GLView_ChartOfCostCenter.Levels=ABS(@level) or  GLView_ChartOfCostCenter.HasChildren=0



) as Tbl

group by Code,NameE,NameA) as Tbl2
where ABS( Debit- Credit)> IIF(@ShowZeroBalances=1,-1,0)
