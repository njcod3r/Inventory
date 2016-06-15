SELECT        dbo.GL_ChartOfCostCenter.Id, dbo.GL_ChartOfCostCenter.MemberShipId, dbo.GL_ChartOfCostCenter.CompanyId, dbo.GL_ChartOfCostCenter.MemberShipId, dbo.GL_ChartOfCostCenter.NameA, dbo.GL_ChartOfCostCenter.NameE, dbo.GL_ChartOfCostCenter.ParentId, 
                         dbo.GL_ChartOfCostCenter.HasChildren, dbo.GL_ChartOfCostCenter.Levels, dbo.GL_ChartOfCostCenter.Notes, dbo.GL_ChartOfCostCenter.Create_Uid, dbo.GL_ChartOfCostCenter.Create_Date,dbo.GL_ChartOfCostCenter.Write_Uid, dbo.GL_ChartOfCostCenter.Write_Date,
                         dbo.GL_ChartOfCostCenter.Post,   dbo.GL_ChartOfCostCenter.Post_Uid,dbo.GL_ChartOfCostCenter.Post_Date,
						 dbo.GL_ChartOfCostCenter.Deleted,   dbo.GL_ChartOfCostCenter.Delete_Uid,dbo.GL_ChartOfCostCenter.Delete_Date,
						 CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_1.Code WHEN 9 THEN GL_ChartOfCostCenter_2.Code WHEN 8 THEN GL_ChartOfCostCenter_3.Code
                          WHEN 7 THEN GL_ChartOfCostCenter_4.Code WHEN 6 THEN GL_ChartOfCostCenter_5.Code WHEN 5 THEN GL_ChartOfCostCenter_6.Code WHEN 4 THEN GL_ChartOfCostCenter_7.Code
                          WHEN 3 THEN GL_ChartOfCostCenter_8.Code WHEN 2 THEN GL_ChartOfCostCenter_9.Code WHEN 1 THEN 0 END AS Parent1, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_1.NameA WHEN 9 THEN GL_ChartOfCostCenter_2.NameA WHEN 8 THEN GL_ChartOfCostCenter_3.NameA WHEN 7 THEN
                          GL_ChartOfCostCenter_4.NameA WHEN 6 THEN GL_ChartOfCostCenter_5.NameA WHEN 5 THEN GL_ChartOfCostCenter_6.NameA WHEN 4 THEN GL_ChartOfCostCenter_7.NameA WHEN 3 THEN GL_ChartOfCostCenter_8.NameA
                          WHEN 2 THEN GL_ChartOfCostCenter_9.NameA WHEN 1 THEN '' END AS Parent1NameA, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_1.NameE WHEN 9 THEN GL_ChartOfCostCenter_2.NameE WHEN 8 THEN GL_ChartOfCostCenter_3.NameE WHEN 7 THEN
                          GL_ChartOfCostCenter_4.NameE WHEN 6 THEN GL_ChartOfCostCenter_5.NameE WHEN 5 THEN GL_ChartOfCostCenter_6.NameE WHEN 4 THEN GL_ChartOfCostCenter_7.NameE WHEN 3 THEN
                          GL_ChartOfCostCenter_8.NameE WHEN 2 THEN GL_ChartOfCostCenter_9.NameE WHEN 1 THEN '' END AS Parent1NameE, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_2.Code WHEN 9 THEN GL_ChartOfCostCenter_3.Code WHEN 8 THEN GL_ChartOfCostCenter_4.Code
                          WHEN 7 THEN GL_ChartOfCostCenter_5.Code WHEN 6 THEN GL_ChartOfCostCenter_6.Code WHEN 5 THEN GL_ChartOfCostCenter_7.Code WHEN 4 THEN GL_ChartOfCostCenter_8.Code
                          WHEN 3 THEN GL_ChartOfCostCenter_9.Code WHEN 2 THEN 0 WHEN 1 THEN 0 END AS Parent2, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_2.NameA WHEN 9 THEN GL_ChartOfCostCenter_3.NameA WHEN 8 THEN GL_ChartOfCostCenter_4.NameA WHEN 7 THEN
                          GL_ChartOfCostCenter_5.NameA WHEN 6 THEN GL_ChartOfCostCenter_6.NameA WHEN 5 THEN GL_ChartOfCostCenter_7.NameA WHEN 4 THEN GL_ChartOfCostCenter_8.NameA WHEN 3 THEN GL_ChartOfCostCenter_9.NameA
                          WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent2NameA, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_2.NameE WHEN 9 THEN GL_ChartOfCostCenter_3.NameE WHEN 8 THEN GL_ChartOfCostCenter_4.NameE WHEN 7 THEN
                          GL_ChartOfCostCenter_5.NameE WHEN 6 THEN GL_ChartOfCostCenter_6.NameE WHEN 5 THEN GL_ChartOfCostCenter_7.NameE WHEN 4 THEN GL_ChartOfCostCenter_8.NameE WHEN 3 THEN
                          GL_ChartOfCostCenter_9.NameE WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent2NameE, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_3.Code WHEN 9 THEN GL_ChartOfCostCenter_4.Code WHEN 8 THEN GL_ChartOfCostCenter_5.Code
                          WHEN 7 THEN GL_ChartOfCostCenter_6.Code WHEN 6 THEN GL_ChartOfCostCenter_7.Code WHEN 5 THEN GL_ChartOfCostCenter_8.Code WHEN 4 THEN GL_ChartOfCostCenter_9.Code
                          WHEN 3 THEN 0 WHEN 2 THEN 0 WHEN 1 THEN 0 END AS Parent3, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_3.NameA WHEN 9 THEN GL_ChartOfCostCenter_4.NameA WHEN 8 THEN GL_ChartOfCostCenter_5.NameA WHEN 7 THEN
                          GL_ChartOfCostCenter_6.NameA WHEN 6 THEN GL_ChartOfCostCenter_7.NameA WHEN 5 THEN GL_ChartOfCostCenter_8.NameA WHEN 4 THEN GL_ChartOfCostCenter_9.NameA WHEN 3 THEN ''
                          WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent3NameA, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_3.NameE WHEN 9 THEN GL_ChartOfCostCenter_4.NameE WHEN 8 THEN GL_ChartOfCostCenter_5.NameE WHEN 7 THEN
                          GL_ChartOfCostCenter_6.NameE WHEN 6 THEN GL_ChartOfCostCenter_7.NameE WHEN 5 THEN GL_ChartOfCostCenter_8.NameE WHEN 4 THEN GL_ChartOfCostCenter_9.NameE WHEN 3 THEN
                          '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent3NameE, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_4.Code WHEN 9 THEN GL_ChartOfCostCenter_5.Code WHEN 8 THEN GL_ChartOfCostCenter_6.Code
                          WHEN 7 THEN GL_ChartOfCostCenter_7.Code WHEN 6 THEN GL_ChartOfCostCenter_8.Code WHEN 5 THEN GL_ChartOfCostCenter_9.Code WHEN 4 THEN 0 WHEN 3
                          THEN 0 WHEN 2 THEN 0 WHEN 1 THEN 0 END AS Parent4, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_4.NameA WHEN 9 THEN GL_ChartOfCostCenter_5.NameA WHEN 8 THEN GL_ChartOfCostCenter_6.NameA WHEN 7 THEN
                          GL_ChartOfCostCenter_7.NameA WHEN 6 THEN GL_ChartOfCostCenter_8.NameA WHEN 5 THEN GL_ChartOfCostCenter_9.NameA WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN
                          1 THEN '' END AS Parent4NameA, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_4.NameE WHEN 9 THEN GL_ChartOfCostCenter_5.NameE WHEN 8 THEN GL_ChartOfCostCenter_6.NameE WHEN 7 THEN
                          GL_ChartOfCostCenter_7.NameE WHEN 6 THEN GL_ChartOfCostCenter_8.NameE WHEN 5 THEN GL_ChartOfCostCenter_9.NameE WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN
                          1 THEN '' END AS Parent4NameE, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_5.Code WHEN 9 THEN GL_ChartOfCostCenter_6.Code WHEN 8 THEN GL_ChartOfCostCenter_7.Code
                          WHEN 7 THEN GL_ChartOfCostCenter_8.Code WHEN 6 THEN GL_ChartOfCostCenter_9.Code WHEN 5 THEN 0 WHEN 4 THEN 0 WHEN 3 THEN 0 WHEN 2 THEN 0 WHEN
                          1 THEN 0 END AS Parent5, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_5.NameA WHEN 9 THEN GL_ChartOfCostCenter_6.NameA WHEN 8 THEN GL_ChartOfCostCenter_7.NameA WHEN 7 THEN
                          GL_ChartOfCostCenter_8.NameA WHEN 6 THEN GL_ChartOfCostCenter_9.NameA WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS
                          Parent5NameA, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_5.NameE WHEN 9 THEN GL_ChartOfCostCenter_6.NameE WHEN 8 THEN GL_ChartOfCostCenter_7.NameE WHEN 7 THEN
                          GL_ChartOfCostCenter_8.NameE WHEN 6 THEN GL_ChartOfCostCenter_9.NameE WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS
                          Parent5NameE, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_6.Code WHEN 9 THEN GL_ChartOfCostCenter_7.Code WHEN 8 THEN GL_ChartOfCostCenter_8.Code
                          WHEN 7 THEN GL_ChartOfCostCenter_9.Code WHEN 6 THEN 0 WHEN 5 THEN 0 WHEN 4 THEN 0 WHEN 3 THEN 0 WHEN 2 THEN 0 WHEN 1 THEN 0 END AS Parent6,
                          CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_6.NameA WHEN 9 THEN GL_ChartOfCostCenter_7.NameA WHEN 8 THEN GL_ChartOfCostCenter_8.NameA WHEN 7 THEN
                          GL_ChartOfCostCenter_9.NameA WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent6NameA, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_6.NameE WHEN 9 THEN GL_ChartOfCostCenter_7.NameE WHEN 8 THEN GL_ChartOfCostCenter_8.NameE WHEN 7 THEN
                          GL_ChartOfCostCenter_9.NameE WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent6NameE, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_7.Code WHEN 9 THEN GL_ChartOfCostCenter_8.Code WHEN 8 THEN GL_ChartOfCostCenter_9.Code
                          WHEN 7 THEN 0 WHEN 6 THEN 0 WHEN 5 THEN 0 WHEN 4 THEN 0 WHEN 3 THEN 0 WHEN 2 THEN 0 WHEN 1 THEN 0 END AS Parent7, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_7.NameA WHEN 9 THEN GL_ChartOfCostCenter_8.NameA WHEN 8 THEN GL_ChartOfCostCenter_9.NameA WHEN 7 THEN
                          '' WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent7NameA, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_7.NameE WHEN 9 THEN GL_ChartOfCostCenter_8.NameE WHEN 8 THEN GL_ChartOfCostCenter_9.NameE WHEN 7 THEN
                          '' WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent7NameE, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_8.Code WHEN 9 THEN GL_ChartOfCostCenter_9.Code WHEN 8 THEN 0 WHEN 7 THEN 0 WHEN 6
                          THEN 0 WHEN 5 THEN 0 WHEN 4 THEN 0 WHEN 3 THEN 0 WHEN 2 THEN 0 WHEN 1 THEN 0 END AS Parent8, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_8.NameA WHEN 9 THEN GL_ChartOfCostCenter_9.NameA WHEN 8 THEN '' WHEN 7 THEN '' WHEN 6 THEN '' WHEN
                          5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent8NameA, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_8.NameE WHEN 9 THEN GL_ChartOfCostCenter_9.NameE WHEN 8 THEN '' WHEN 7 THEN '' WHEN 6 THEN '' WHEN
                          5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent8NameE, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_9.Code WHEN 9 THEN 0 WHEN 8 THEN 0 WHEN 7 THEN 0 WHEN 6 THEN 0 WHEN 5 THEN 0 WHEN
                          4 THEN 0 WHEN 3 THEN 0 WHEN 2 THEN 0 WHEN 1 THEN 0 END AS Parent9, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_9.NameA WHEN 9 THEN '' WHEN 8 THEN '' WHEN 7 THEN '' WHEN 6 THEN '' WHEN 5 THEN '' WHEN
                          4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent9NameA, 
                         CASE GL_ChartOfCostCenter.Levels WHEN 10 THEN GL_ChartOfCostCenter_9.NameE WHEN 9 THEN '' WHEN 8 THEN '' WHEN 7 THEN '' WHEN 6 THEN '' WHEN 5 THEN '' WHEN
                          4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent9NameE
FROM            dbo.GL_ChartOfCostCenter LEFT OUTER JOIN
                         dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_9 ON dbo.GL_ChartOfCostCenter.ParentId = GL_ChartOfCostCenter_9.Id LEFT OUTER JOIN
                         dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_8 ON GL_ChartOfCostCenter_9.ParentId = GL_ChartOfCostCenter_8.Id LEFT OUTER JOIN
                         dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_7 ON GL_ChartOfCostCenter_8.ParentId = GL_ChartOfCostCenter_7.Id LEFT OUTER JOIN
                         dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_6 ON GL_ChartOfCostCenter_7.ParentId = GL_ChartOfCostCenter_6.Id LEFT OUTER JOIN
                         dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_5 ON GL_ChartOfCostCenter_6.ParentId = GL_ChartOfCostCenter_5.Id LEFT OUTER JOIN
                         dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_4 ON GL_ChartOfCostCenter_5.ParentId = GL_ChartOfCostCenter_4.Id LEFT OUTER JOIN
                         dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_3 ON GL_ChartOfCostCenter_4.ParentId = GL_ChartOfCostCenter_3.Id LEFT OUTER JOIN
                         dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_2 ON GL_ChartOfCostCenter_3.ParentId = GL_ChartOfCostCenter_2.Id LEFT OUTER JOIN
                         dbo.GL_ChartOfCostCenter AS GL_ChartOfCostCenter_1 ON GL_ChartOfCostCenter_2.ParentId = GL_ChartOfCostCenter_1.Id