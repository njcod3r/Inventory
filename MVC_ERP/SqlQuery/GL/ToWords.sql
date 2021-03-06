

Create FUNCTION [dbo].[ToWords] (@TheNo  numeric(18,3),@Currency nvarchar(50),@Lang  int=1) 
returns varchar(1000) as 
begin 
declare @TheNoAfterReplicate varchar(15) 
declare @ComWithWord varchar(1000),@TheNoWithDecimal as varchar(400),@ThreeWords as int 
declare   @Tafket TABLE (num int,  NoName varchar(100)) 

if  @Lang=1 
BEGIN
if @TheNo <= 0   return 'صفر ' 

set @TheNoAfterReplicate = right(replicate('0',15)+cast(floor(@TheNo) as varchar(15)),15) 

set @ThreeWords=0 
set @ComWithWord  = 'فقط ' 

INSERT INTO @Tafket VALUES (0,'')  
INSERT INTO @Tafket VALUES (1,'واحد ') 
INSERT INTO @Tafket VALUES (2,'اثنان ') 
INSERT INTO @Tafket VALUES (3,'ثلاثة ') 
INSERT INTO @Tafket VALUES (4,'اربعة ') 
INSERT INTO @Tafket VALUES (5,'خمسة ') 
INSERT INTO @Tafket VALUES (6,'ستة ') 
INSERT INTO @Tafket VALUES (7,'سبعة ') 
INSERT INTO @Tafket VALUES (8,'ثمانية ') 
INSERT INTO @Tafket VALUES (9,'تسعة ') 
INSERT INTO @Tafket VALUES (10,'عشرة ') 
INSERT INTO @Tafket VALUES (11,'احدى عشر ') 
INSERT INTO @Tafket VALUES (12,'اثنى عشر ') 
INSERT INTO @Tafket VALUES (13,'ثلاثة عشر ') 
INSERT INTO @Tafket VALUES (14,'اربعة عشر ') 
INSERT INTO @Tafket VALUES (15,'خمسة عشر ') 
INSERT INTO @Tafket VALUES (16,'ستة عشر ') 
INSERT INTO @Tafket VALUES (17,'سبعة عشر ') 
INSERT INTO @Tafket VALUES (18,'ثمانية عشر ') 
INSERT INTO @Tafket VALUES (19,'تسعة عشر ') 
INSERT INTO @Tafket VALUES (20,'عشرون ') 
INSERT INTO @Tafket VALUES (30,'ثلاثون ') 
INSERT INTO @Tafket VALUES (40,'اربعون ') 
INSERT INTO @Tafket VALUES (50,'خمسون ') 
INSERT INTO @Tafket VALUES (60,'ستون ') 
INSERT INTO @Tafket VALUES (70,'سبعون ') 
INSERT INTO @Tafket VALUES (80,'ثمانون ') 
INSERT INTO @Tafket VALUES (90,'تسعون ') 
INSERT INTO @Tafket VALUES (100,'مائة ') 
INSERT INTO @Tafket VALUES (200,'مائتان ') 
INSERT INTO @Tafket VALUES (300,'ثلاثمائة ') 
INSERT INTO @Tafket VALUES (400,'أربعمائة ') 
INSERT INTO @Tafket VALUES (500,'خمسمائة ') 
INSERT INTO @Tafket VALUES (600,'ستمائة ') 
INSERT INTO @Tafket VALUES (700,'سبعمائة ') 
INSERT INTO @Tafket VALUES (800,'ثمانمائة ') 
INSERT INTO @Tafket VALUES (900,'تسعمائة ') 
INSERT INTO @Tafket  SELECT FirstN.num+LasteN.num,LasteN.NoName+'و '+FirstN.NoName 
FROM (SELECT * FROM @Tafket WHERE num BETWEEN 20 AND 90) FirstN CROSS JOIN (SELECT * FROM @Tafket WHERE num BETWEEN 1 AND 9) LasteN 
INSERT INTO @Tafket  SELECT FirstN.num+LasteN.num,FirstN.NoName+'و '+LasteN.NoName 
FROM (SELECT * FROM @Tafket WHERE num BETWEEN 100 AND 900) FirstN CROSS JOIN (SELECT * FROM @Tafket WHERE num BETWEEN 1 AND 99) LasteN 
if left(@TheNoAfterReplicate,3) > 0 
    set @ComWithWord = @ComWithWord + ISNULL((select NoName  from  @Tafket where num=left(@TheNoAfterReplicate,3)),'')+  'ترليون ' 
if left(right(@TheNoAfterReplicate,12),3) > 0 and  left(@TheNoAfterReplicate,3) > 0 
    set @ComWithWord=@ComWithWord+ 'و ' 
if left(right(@TheNoAfterReplicate,12),3) > 0 
    set @ComWithWord = @ComWithWord +ISNULL((select NoName from @Tafket where num=left(right(@TheNoAfterReplicate,12),3)),'') +  'بليون ' 
if left(right(@TheNoAfterReplicate,9),3) > 0 
begin 
    set @ComWithWord=@ComWithWord + case  when @TheNo>999000000  then 'و '  else '' end 
set @ThreeWords=left(right(@TheNoAfterReplicate,9),3) 
       set @ComWithWord = @ComWithWord + ISNULL((select case when   @ThreeWords>2 then NoName end  from @Tafket  where num=left(right(@TheNoAfterReplicate,9),3)),'')  + case when  @ThreeWords=2 then 'مليونان ' when   @ThreeWords between 3 and 10 then 'ملايين ' else 'مليون ' end 
end 
if left(right(@TheNoAfterReplicate,6),3) > 0 
begin 
    set @ComWithWord=@ComWithWord + case  when @TheNo>999000  then 'و '  else '' end 
    set @ThreeWords=left(right(@TheNoAfterReplicate,6),3) 
    set @ComWithWord = @ComWithWord + ISNULL((select case when  @ThreeWords>2 then NoName  end from @Tafket where num=left(right(@TheNoAfterReplicate,6),3)),'')+ case when  @ThreeWords=2 then 'الفان ' when @ThreeWords between 3 and 10 then 'الاف '  else 'الف ' end 
end 

  if right(@TheNoAfterReplicate,3) > 0 
  begin 
    if @TheNo>999 
begin 
    set @ComWithWord=@ComWithWord + 'و ' 
end 

    set @ThreeWords=right(@TheNoAfterReplicate,2) 
if @ThreeWords=0 
begin 
--   set @ComWithWord=@ComWithWord + ' و' 
   set @ComWithWord = @ComWithWord + ISNULL((select NoName  from @Tafket where @ThreeWords=0 AND num=right(@TheNoAfterReplicate,3)),'') 
end 

end 

set @ThreeWords=right(@TheNoAfterReplicate,2) 
set @ComWithWord =  @ComWithWord  +   ISNULL((select  NoName  from @Tafket where @ThreeWords>2 AND num=right(@TheNoAfterReplicate,3)),'') 
set @ComWithWord = @ComWithWord +''+ case when  @ThreeWords=2 then 'اثنين ' when @ThreeWords =1 then 'واحد '  else '' end 
if right(rtrim(@ComWithWord),1)=',' set @ComWithWord = substring(@ComWithWord,1,len(@ComWithWord)-1) 
if  right(@TheNo,len(@TheNo)-charindex('.',@TheNo)) >0 and charindex('.',@TheNo)<>0 
    begin 
        set @ThreeWords=left(right(round(@TheNo,3),3),3) 
        SELECT @TheNoWithDecimal=  'و ' +'0.'+ ISNULL(right(@TheNo,len(@TheNo)-charindex('.',@TheNo)),'') + ' '

set @ComWithWord = @ComWithWord + @TheNoWithDecimal 
END 
set @ComWithWord = @ComWithWord  + @Currency + ' لا غير' 


END

--  ENGLISH lANGAUGE
IF @LANG=0
BEGIN 
if @TheNo <= 0  return  'zero ' 

set @TheNoAfterReplicate = right(replicate('0',15)+cast(floor(@TheNo) as varchar(15)),15) 

set @ThreeWords=0 
set @ComWithWord  = 'Only ' 

INSERT INTO @Tafket VALUES (0,'')  
INSERT INTO @Tafket VALUES (1,'one ') 
INSERT INTO @Tafket VALUES (2,'two ') 
INSERT INTO @Tafket VALUES (3,'three ') 
INSERT INTO @Tafket VALUES (4,'four ') 
INSERT INTO @Tafket VALUES (5,'five ') 
INSERT INTO @Tafket VALUES (6,'six ') 
INSERT INTO @Tafket VALUES (7,'seven ') 
INSERT INTO @Tafket VALUES (8,'eight ') 
INSERT INTO @Tafket VALUES (9,'nine ') 
INSERT INTO @Tafket VALUES (10,'ten ') 
INSERT INTO @Tafket VALUES (11,'eleven ') 
INSERT INTO @Tafket VALUES (12,'twelve ') 
INSERT INTO @Tafket VALUES (13,'thirteen ') 
INSERT INTO @Tafket VALUES (14,'fourteen ') 
INSERT INTO @Tafket VALUES (15,'fifteen ') 
INSERT INTO @Tafket VALUES (16,'sixteen ') 
INSERT INTO @Tafket VALUES (17,'seventeen ') 
INSERT INTO @Tafket VALUES (18,'eighteen ') 
INSERT INTO @Tafket VALUES (19,'nineteen ') 
INSERT INTO @Tafket VALUES (20,'twenty ') 
INSERT INTO @Tafket VALUES (30,'thirty ') 
INSERT INTO @Tafket VALUES (40,'fortieth ') 
INSERT INTO @Tafket VALUES (50,'fifty ') 
INSERT INTO @Tafket VALUES (60,'sixty ') 
INSERT INTO @Tafket VALUES (70,'seventy ') 
INSERT INTO @Tafket VALUES (80,'eighty ') 
INSERT INTO @Tafket VALUES (90,'ninety ') 
INSERT INTO @Tafket VALUES (100,'one hundred ') 
INSERT INTO @Tafket VALUES (200,'two hundred ') 
INSERT INTO @Tafket VALUES (300,'three hundred ') 
INSERT INTO @Tafket VALUES (400,'four hundred ') 
INSERT INTO @Tafket VALUES (500,'five hundred ') 
INSERT INTO @Tafket VALUES (600,'six hundred ') 
INSERT INTO @Tafket VALUES (700,'seven hundred ') 
INSERT INTO @Tafket VALUES (800,'eight hundred ') 
INSERT INTO @Tafket VALUES (900,'nine hundred ') 

INSERT INTO @Tafket  SELECT FirstN.num+LasteN.num,FirstN.NoName + LasteN.NoName  
FROM (SELECT * FROM @Tafket WHERE num BETWEEN 20 AND 90) FirstN CROSS JOIN (SELECT * FROM @Tafket WHERE num BETWEEN 1 AND 9) LasteN 
INSERT INTO @Tafket  SELECT FirstN.num+LasteN.num,FirstN.NoName+'and '+LasteN.NoName 
FROM (SELECT * FROM @Tafket WHERE num BETWEEN 100 AND 900) FirstN CROSS JOIN (SELECT * FROM @Tafket WHERE num BETWEEN 1 AND 99) LasteN

if left(@TheNoAfterReplicate,3) > 0 
    set @ComWithWord = @ComWithWord + ISNULL((select NoName  from  @Tafket where num=left(@TheNoAfterReplicate,3)),'')+  'trillion ' 
if left(right(@TheNoAfterReplicate,12),3) > 0 and  left(@TheNoAfterReplicate,3) > 0 
    set @ComWithWord=@ComWithWord+ 'and ' 
if left(right(@TheNoAfterReplicate,12),3) > 0 
    set @ComWithWord = @ComWithWord +ISNULL((select NoName from @Tafket where num=left(right(@TheNoAfterReplicate,12),3)),'') +  'billion ' 
if left(right(@TheNoAfterReplicate,9),3) > 0 
begin 
    set @ComWithWord=@ComWithWord + case  when @TheNo>999000000  then 'and '  else '' end 
set @ThreeWords=left(right(@TheNoAfterReplicate,9),3) 
       set @ComWithWord = @ComWithWord + ISNULL((select case when   @ThreeWords>2 then NoName end  from @Tafket  where num=left(right(@TheNoAfterReplicate,9),3)),'')  + case when  @ThreeWords=1 then 'one million '  when  @ThreeWords=2 then 'two million '  when   @ThreeWords between 3 and 10 then 'million ' else 'million ' end 
end 
if left(right(@TheNoAfterReplicate,6),3) > 0 
begin 
    set @ComWithWord=@ComWithWord + case  when @TheNo>999000  then 'and '  else '' end 
    set @ThreeWords=left(right(@TheNoAfterReplicate,6),3) 
    set @ComWithWord = @ComWithWord + ISNULL((select case when  @ThreeWords>2 then NoName  end from @Tafket where num=left(right(@TheNoAfterReplicate,6),3)),'')+ case when  @ThreeWords=1 then 'one thousand ' when  @ThreeWords=2 then 'two thousands ' when @ThreeWords between 3 and 10 then 'thousands '  else ' thousands ' end 
end 

  if right(@TheNoAfterReplicate,3) > 0 
  begin 
    if @TheNo>999 
begin 
    set @ComWithWord=@ComWithWord + 'and ' 
end 

    set @ThreeWords=right(@TheNoAfterReplicate,2) 
if @ThreeWords=0 
begin 
--   set @ComWithWord=@ComWithWord + ' و' 
   set @ComWithWord = @ComWithWord + ISNULL((select NoName  from @Tafket where @ThreeWords=0 AND num=right(@TheNoAfterReplicate,3)),'') 
end 

end 

set @ThreeWords=right(@TheNoAfterReplicate,2) 
set @ComWithWord =  @ComWithWord  +   ISNULL((select  NoName  from @Tafket where @ThreeWords>2 AND num=right(@TheNoAfterReplicate,3)),'') 
set @ComWithWord = @ComWithWord +''+ case when  @ThreeWords=2 then 'two ' when @ThreeWords =1 then 'one '  else '' end 
if right(rtrim(@ComWithWord),1)=',' set @ComWithWord = substring(@ComWithWord,1,len(@ComWithWord)-1) 
if  right(@TheNo,len(@TheNo)-charindex('.',@TheNo)) >0 and charindex('.',@TheNo)<>0 
    begin 
        set @ThreeWords=left(right(round(@TheNo,3),3),3) 
        SELECT @TheNoWithDecimal=  'and ' +'0.'+ ISNULL(right(@TheNo,len(@TheNo)-charindex('.',@TheNo)),'') + ' '

set @ComWithWord = @ComWithWord + @TheNoWithDecimal 
END 
set @ComWithWord = @ComWithWord   + @Currency + ''


END
return  rtrim(@ComWithWord) 
end 

