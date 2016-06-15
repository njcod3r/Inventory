declare @CompanyId int=1
declare @BranchId int=1
declare @FirstDate datetime ='11/23/2014'
declare @LastDate  datetime = '1/1/2015'
declare @AccountId  int      = 0
declare @CostCenterId  int   = 1

select CostCenterId,CostCenterCode,CostCenterNameE,CostCenterNameA,AccountId,AccountCode,AccountNameE,AccountNameA,sum(isnull(Debit,0)) as Debit,sum(isnull(Credit,0)) as Credit from(
SELECT        GL_DailyVoucher_CostCenter.CostCenterId, GL_ChartOfCostCenter.Code AS CostCenterCode, GL_ChartOfCostCenter.NameA AS CostCenterNameA, 
                         GL_ChartOfCostCenter.NameE AS CostCenterNameE, GL_DailyVoucher_DTL.TransactDate_Gregi, GL_DailyVoucher_DTL.TransactDate_Hijri, 
                         GL_DailyVoucher_DTL.AccountId, GL_ChartOfAccounts.Code AS AccountCode, GL_ChartOfAccounts.NameA AS AccountNameA, 
                         GL_ChartOfAccounts.NameE AS AccountNameE, GL_DailyVoucher_CostCenter.Notes, Sys_Branches.NameA AS BranchNameA, Sys_Branches.NameE AS BranchNameE, 
                         GL_DailyVoucher.Source, GL_DailyVoucher.SourceNo, GL_DailyVoucher_CostCenter.DebitLocal AS Debit, 
                         GL_DailyVoucher_CostCenter.CreditLocal AS Credit
FROM            GL_DailyVoucher_CostCenter INNER JOIN
                         GL_DailyVoucher_DTL ON GL_DailyVoucher_CostCenter.DailyVoucherRowId = GL_DailyVoucher_DTL.Id INNER JOIN
                         GL_DailyVoucher ON GL_DailyVoucher_CostCenter.DailyVoucherId = GL_DailyVoucher.Id AND 
                         GL_DailyVoucher_DTL.DailyVoucherId = GL_DailyVoucher.Id INNER JOIN
                         GL_ChartOfAccounts ON GL_DailyVoucher_DTL.AccountId = GL_ChartOfAccounts.Id INNER JOIN
                         GL_ChartOfCostCenter ON GL_DailyVoucher_CostCenter.CostCenterId = GL_ChartOfCostCenter.Id INNER JOIN
                         Sys_Branches ON GL_DailyVoucher.BranchId = Sys_Branches.Id
						 where GL_DailyVoucher.CompanyId in (@CompanyId) and GL_DailyVoucher.BranchId in (@BranchId)
						 and cast(GL_DailyVoucher_DTL.TransactDate_Gregi as date) between @FirstDate and @LastDate and 
						 GL_DailyVoucher_CostCenter.CostCenterId between IIf(@CostCenterId=0,-88888888888888888888888888,@CostCenterId) and  IIf(@CostCenterId=0,88888888888888888888888888,@CostCenterId)
						 and GL_DailyVoucher_DTL.AccountId between IIf(@AccountId=0,-88888888888888888888888888,@AccountId) and  IIf(@AccountId=0,88888888888888888888888888,@AccountId)
) as Tbl
group by CostCenterId,CostCenterCode,CostCenterNameE,CostCenterNameA,AccountId,AccountCode,AccountNameE,AccountNameA
