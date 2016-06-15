--declare @CompanyId int=1
--declare @BranchId int=1
--declare @CurrentYearEndDate datetime ='12/31/2014'
--declare @CompareYearEndDate  datetime = '12/31/2013'
--declare @Kind  int      = 0  --0 General  <0 Levels


------------- General View-----------------------------------
if @Kind=0

begin

select AccountCode,AccSort,NameA,NameE,Parent1,Parent1NameA,Parent1NameE,Parent2,Parent2NameA,Parent2NameE,sum(CurrentYearValue) as CurrentYearValue, sum(PreviousYearValue) as PreviousYearValue from(
-------------Current Year
select  AccountCode,AccSort,NameA,NameE,Parent1,Parent1NameA,Parent1NameE,Parent2,Parent2NameA,Parent2NameE,Value as CurrentYearValue, 0 as PreviousYearValue from (
--Assets
select case when Parent3=0 then Code else parent3 end as AccountCode,SUBSTRING (case when Parent3=0 then Code else parent3 end , 1, 1) as AccSort ,
 case when Parent3=0 then NameA else  Parent3NameA end as NameA, case when Parent3=0 then NameE else  Parent3NameE end as NameE,
  case when Parent1=0 then Code else Parent1 end as Parent1, case when Parent1=0 then NameA else Parent1NameA end  as Parent1NameA,case when Parent1=0 then NameE else Parent1NameE end  as Parent1NameE,
   case when Parent2=0 then Code else Parent2 end as Parent2,case when Parent2=0 then NameA else Parent2NameA end as Parent2NameA,case when Parent2=0 then NameE else Parent2NameE end as Parent2NameE  ,sum(Value) as Value from(

SELECT                         GLView_ChartOfAccounts.Id, GLView_ChartOfAccounts.Code, GLView_ChartOfAccounts.NameA, GLView_ChartOfAccounts.NameE, Value,
                         GLView_ChartOfAccounts.Parent1, GLView_ChartOfAccounts.Parent1NameA, GLView_ChartOfAccounts.Parent1NameE, GLView_ChartOfAccounts.Parent2, 
                         GLView_ChartOfAccounts.Parent2NameA, GLView_ChartOfAccounts.Parent2NameE,GLView_ChartOfAccounts.Parent3, 
                         GLView_ChartOfAccounts.Parent3NameA, GLView_ChartOfAccounts.Parent3NameE
						 
FROM            (SELECT        GL_DailyVoucher_DTL.AccountId,ISNULL( SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0))-SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0)),0) AS Value
                           FROM            GL_DailyVoucher INNER JOIN
                                                    GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
                           WHERE        (GL_DailyVoucher.CompanyId IN (@CompanyId)) AND (GL_DailyVoucher.BranchId IN (@BranchId)) AND (CAST(GL_DailyVoucher_DTL.TransactDate_Gregi AS date) 
                                                    <= @CurrentYearEndDate)
                           GROUP BY GL_DailyVoucher_DTL.AccountId) AS DailyVoucher inner JOIN
                         GLView_ChartOfAccounts ON DailyVoucher.AccountId = GLView_ChartOfAccounts.Id where GLView_ChartOfAccounts.CompanyId in (@CompanyId)  and isnull(GLView_ChartOfAccounts.Deleted,0)=0 and Parent1 in  (SELECT Value FROM fn_Split((select distinct AssetsAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId and isnull(AssetsAccountIds,'')<>''), ','))

) as TBL1 

group by case when Parent3=0 then Code else parent3 end ,case when Parent3=0 then NameA else  Parent3NameA end ,case when Parent3=0 then NameE else  Parent3NameE end ,
case when Parent2=0 then Code else parent2 end ,case when Parent2=0 then NameA else  Parent2NameA end ,case when Parent2=0 then NameE else  Parent2NameE end ,
case when Parent1=0 then Code else parent1 end ,case when Parent1=0 then NameA else  Parent1NameA end ,case when Parent1=0 then NameE else  Parent1NameE end 

union all

--Liabilities

select case when Parent3=0 then Code else parent3 end as AccountCode,SUBSTRING (case when Parent3=0 then Code else parent3 end , 1, 1) as AccSort ,
 case when Parent3=0 then NameA else  Parent3NameA end as NameA, case when Parent3=0 then NameE else  Parent3NameE end as NameE,
  case when Parent1=0 then Code else Parent1 end as Parent1, case when Parent1=0 then NameA else Parent1NameA end  as Parent1NameA,case when Parent1=0 then NameE else Parent1NameE end  as Parent1NameE,
   case when Parent2=0 then Code else Parent2 end as Parent2,case when Parent2=0 then NameA else Parent2NameA end as Parent2NameA,case when Parent2=0 then NameE else Parent2NameE end as Parent2NameE  ,sum(Value) as Value from(

SELECT                         GLView_ChartOfAccounts.Id, GLView_ChartOfAccounts.Code, GLView_ChartOfAccounts.NameA, GLView_ChartOfAccounts.NameE, Value,
                         GLView_ChartOfAccounts.Parent1, GLView_ChartOfAccounts.Parent1NameA, GLView_ChartOfAccounts.Parent1NameE, GLView_ChartOfAccounts.Parent2, 
                         GLView_ChartOfAccounts.Parent2NameA, GLView_ChartOfAccounts.Parent2NameE,GLView_ChartOfAccounts.Parent3, 
                         GLView_ChartOfAccounts.Parent3NameA, GLView_ChartOfAccounts.Parent3NameE
						 
FROM            (SELECT        GL_DailyVoucher_DTL.AccountId,ISNULL( SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))-SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)),0) AS Value
                           FROM            GL_DailyVoucher INNER JOIN
                                                    GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
                           WHERE        (GL_DailyVoucher.CompanyId IN (@CompanyId)) AND (GL_DailyVoucher.BranchId IN (@BranchId)) AND (CAST(GL_DailyVoucher_DTL.TransactDate_Gregi AS date) 
                                                    <= @CurrentYearEndDate)
                           GROUP BY GL_DailyVoucher_DTL.AccountId) AS DailyVoucher inner JOIN
                         GLView_ChartOfAccounts ON DailyVoucher.AccountId = GLView_ChartOfAccounts.Id where GLView_ChartOfAccounts.CompanyId in (@CompanyId)  and isnull(GLView_ChartOfAccounts.Deleted,0)=0 and Parent1 in  (SELECT Value FROM fn_Split((select distinct LiabilitiesAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId and isnull(LiabilitiesAccountIds,'')<>''), ','))

) as TBL1 

group by case when Parent3=0 then Code else parent3 end ,case when Parent3=0 then NameA else  Parent3NameA end ,case when Parent3=0 then NameE else  Parent3NameE end ,
case when Parent2=0 then Code else parent2 end ,case when Parent2=0 then NameA else  Parent2NameA end ,case when Parent2=0 then NameE else  Parent2NameE end ,
case when Parent1=0 then Code else parent1 end ,case when Parent1=0 then NameA else  Parent1NameA end ,case when Parent1=0 then NameE else  Parent1NameE end 

union all 
select '999999999999999' ,9,'’«›Ï —»Õ Œ”«—… «·› —…','Period Net Loss & Profit',
(select max(Value) from(
SELECT Value FROM fn_Split((select distinct LiabilitiesAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId
and isnull(LiabilitiesAccountIds,'')<>''), ',')) as t) 
,'','','','','',sum(Value)

FROM            (SELECT        GL_DailyVoucher_DTL.AccountId,ISNULL( SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0))-SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0)),0) AS Value
                           FROM            GL_DailyVoucher INNER JOIN
                                                    GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
                           WHERE        (GL_DailyVoucher.CompanyId IN (@CompanyId)) AND (GL_DailyVoucher.BranchId IN (@BranchId)) AND (CAST(GL_DailyVoucher_DTL.TransactDate_Gregi AS date) 
                                                    <= @CurrentYearEndDate)
                           GROUP BY GL_DailyVoucher_DTL.AccountId) AS DailyVoucher inner JOIN
                         GLView_ChartOfAccounts ON DailyVoucher.AccountId = GLView_ChartOfAccounts.Id where GLView_ChartOfAccounts.CompanyId in (@CompanyId)  and isnull(GLView_ChartOfAccounts.Deleted,0)=0
						 and  GLView_ChartOfAccounts.Parent1 in  (
                         SELECT Value FROM fn_Split((select distinct AssetsAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId and isnull(AssetsAccountIds,'')<>''), ',')
                         union all
                         SELECT Value FROM fn_Split((select distinct LiabilitiesAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId and isnull(LiabilitiesAccountIds,'')<>''), ',')
                           )


) as CurrentYear

-------------End Current Year-----------------
union all
-------------Previous Year--------------------
select AccountCode,AccSort,NameA,NameE,Parent1,Parent1NameA,Parent1NameE,Parent2,Parent2NameA,Parent2NameE,0 as CurrentYearValue,Value as PreviousYearValue from (
--Assets
select case when Parent3=0 then Code else parent3 end as AccountCode,SUBSTRING (case when Parent3=0 then Code else parent3 end , 1, 1) as AccSort ,
 case when Parent3=0 then NameA else  Parent3NameA end as NameA, case when Parent3=0 then NameE else  Parent3NameE end as NameE,
  case when Parent1=0 then Code else Parent1 end as Parent1, case when Parent1=0 then NameA else Parent1NameA end  as Parent1NameA,case when Parent1=0 then NameE else Parent1NameE end  as Parent1NameE,
   case when Parent2=0 then Code else Parent2 end as Parent2,case when Parent2=0 then NameA else Parent2NameA end as Parent2NameA,case when Parent2=0 then NameE else Parent2NameE end as Parent2NameE  ,sum(Value) as Value from(

SELECT                         GLView_ChartOfAccounts.Id, GLView_ChartOfAccounts.Code, GLView_ChartOfAccounts.NameA, GLView_ChartOfAccounts.NameE, Value,
                         GLView_ChartOfAccounts.Parent1, GLView_ChartOfAccounts.Parent1NameA, GLView_ChartOfAccounts.Parent1NameE, GLView_ChartOfAccounts.Parent2, 
                         GLView_ChartOfAccounts.Parent2NameA, GLView_ChartOfAccounts.Parent2NameE,GLView_ChartOfAccounts.Parent3, 
                         GLView_ChartOfAccounts.Parent3NameA, GLView_ChartOfAccounts.Parent3NameE
						 
FROM            (SELECT        GL_DailyVoucher_DTL.AccountId,ISNULL( SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0))-SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0)),0) AS Value
                           FROM            GL_DailyVoucher INNER JOIN
                                                    GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
                           WHERE        (GL_DailyVoucher.CompanyId IN (@CompanyId)) AND (GL_DailyVoucher.BranchId IN (@BranchId)) AND (CAST(GL_DailyVoucher_DTL.TransactDate_Gregi AS date) 
                                                    <= @CompareYearEndDate)
                           GROUP BY GL_DailyVoucher_DTL.AccountId) AS DailyVoucher inner JOIN
                         GLView_ChartOfAccounts ON DailyVoucher.AccountId = GLView_ChartOfAccounts.Id where GLView_ChartOfAccounts.CompanyId in (@CompanyId)  and isnull(GLView_ChartOfAccounts.Deleted,0)=0 and Parent1 in  (SELECT Value FROM fn_Split((select distinct AssetsAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId and isnull(AssetsAccountIds,'')<>''), ','))

) as TBL1 

group by case when Parent3=0 then Code else parent3 end ,case when Parent3=0 then NameA else  Parent3NameA end ,case when Parent3=0 then NameE else  Parent3NameE end ,
case when Parent2=0 then Code else parent2 end ,case when Parent2=0 then NameA else  Parent2NameA end ,case when Parent2=0 then NameE else  Parent2NameE end ,
case when Parent1=0 then Code else parent1 end ,case when Parent1=0 then NameA else  Parent1NameA end ,case when Parent1=0 then NameE else  Parent1NameE end 

union all

--Liabilities

select case when Parent3=0 then Code else parent3 end as AccountCode,SUBSTRING (case when Parent3=0 then Code else parent3 end , 1, 1) as AccSort ,
 case when Parent3=0 then NameA else  Parent3NameA end as NameA, case when Parent3=0 then NameE else  Parent3NameE end as NameE,
  case when Parent1=0 then Code else Parent1 end as Parent1, case when Parent1=0 then NameA else Parent1NameA end  as Parent1NameA,case when Parent1=0 then NameE else Parent1NameE end  as Parent1NameE,
   case when Parent2=0 then Code else Parent2 end as Parent2,case when Parent2=0 then NameA else Parent2NameA end as Parent2NameA,case when Parent2=0 then NameE else Parent2NameE end as Parent2NameE  ,sum(Value) as Value from(

SELECT                         GLView_ChartOfAccounts.Id, GLView_ChartOfAccounts.Code, GLView_ChartOfAccounts.NameA, GLView_ChartOfAccounts.NameE, Value,
                         GLView_ChartOfAccounts.Parent1, GLView_ChartOfAccounts.Parent1NameA, GLView_ChartOfAccounts.Parent1NameE, GLView_ChartOfAccounts.Parent2, 
                         GLView_ChartOfAccounts.Parent2NameA, GLView_ChartOfAccounts.Parent2NameE,GLView_ChartOfAccounts.Parent3, 
                         GLView_ChartOfAccounts.Parent3NameA, GLView_ChartOfAccounts.Parent3NameE
						 
FROM            (SELECT        GL_DailyVoucher_DTL.AccountId,ISNULL( SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0))-SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0)),0) AS Value
                           FROM            GL_DailyVoucher INNER JOIN
                                                    GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
                           WHERE        (GL_DailyVoucher.CompanyId IN (@CompanyId)) AND (GL_DailyVoucher.BranchId IN (@BranchId)) AND (CAST(GL_DailyVoucher_DTL.TransactDate_Gregi AS date) 
                                                    <= @CompareYearEndDate)
                           GROUP BY GL_DailyVoucher_DTL.AccountId) AS DailyVoucher inner JOIN
                         GLView_ChartOfAccounts ON DailyVoucher.AccountId = GLView_ChartOfAccounts.Id where GLView_ChartOfAccounts.CompanyId in (@CompanyId)  and isnull(GLView_ChartOfAccounts.Deleted,0)=0 and Parent1 in  (SELECT Value FROM fn_Split((select distinct LiabilitiesAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId and isnull(LiabilitiesAccountIds,'')<>''), ','))

) as TBL1 

group by case when Parent3=0 then Code else parent3 end ,case when Parent3=0 then NameA else  Parent3NameA end ,case when Parent3=0 then NameE else  Parent3NameE end ,
case when Parent2=0 then Code else parent2 end ,case when Parent2=0 then NameA else  Parent2NameA end ,case when Parent2=0 then NameE else  Parent2NameE end ,
case when Parent1=0 then Code else parent1 end ,case when Parent1=0 then NameA else  Parent1NameA end ,case when Parent1=0 then NameE else  Parent1NameE end 

union all 
select '999999999999999' ,9,'’«›Ï —»Õ Œ”«—… «·› —…','Period Net Loss & Profit',
(select max(Value) from(
SELECT Value FROM fn_Split((select distinct LiabilitiesAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId
and isnull(LiabilitiesAccountIds,'')<>''), ',')) as t) 
,'','','','','',sum(Value)

FROM            (SELECT        GL_DailyVoucher_DTL.AccountId,ISNULL( SUM(ISNULL(GL_DailyVoucher_DTL.DebitLocal, 0))-SUM(ISNULL(GL_DailyVoucher_DTL.CreditLocal, 0)),0) AS Value
                           FROM            GL_DailyVoucher INNER JOIN
                                                    GL_DailyVoucher_DTL ON GL_DailyVoucher.Id = GL_DailyVoucher_DTL.DailyVoucherId
                           WHERE        (GL_DailyVoucher.CompanyId IN (@CompanyId)) AND (GL_DailyVoucher.BranchId IN (@BranchId)) AND (CAST(GL_DailyVoucher_DTL.TransactDate_Gregi AS date) 
                                                    <= @CompareYearEndDate)
                           GROUP BY GL_DailyVoucher_DTL.AccountId) AS DailyVoucher inner JOIN
                         GLView_ChartOfAccounts ON DailyVoucher.AccountId = GLView_ChartOfAccounts.Id where GLView_ChartOfAccounts.CompanyId in (@CompanyId)  and isnull(GLView_ChartOfAccounts.Deleted,0)=0
						 and  GLView_ChartOfAccounts.Parent1 in  (
                         SELECT Value FROM fn_Split((select distinct AssetsAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId and isnull(AssetsAccountIds,'')<>''), ',')
                         union all
                         SELECT Value FROM fn_Split((select distinct LiabilitiesAccountIds from GL_Setting_Defaults where CompanyId=@CompanyId and isnull(LiabilitiesAccountIds,'')<>''), ',')
                           )


) as PreviousYear
------------- End of Previous Year-------------
) as finalTbl
group by  AccountCode,AccSort,NameA,NameE,Parent1,Parent1NameA,Parent1NameE,Parent2,Parent2NameA,Parent2NameE
order by AccSort,AccountCode
end
------------- End of General View-------------