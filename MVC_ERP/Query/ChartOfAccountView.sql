SELECT                  Id, MemberShipId,CompanyId,Code, NameA,NameE, ParentId,  HasChildren,AccType, Levels, AccNature, 
                        CurrencyId, CostCenterId, IsSecret, Notes, Create_Uid, Create_Date, 
                        Write_Uid, Write_Date, Post, Post_Uid, Post_Date, Deleted, Delete_Uid, Delete_Date,                       
                        Parent1, Parent1NameA, Parent1NameE, Parent2, Parent2NameA, Parent2NameE, Parent3, Parent3NameA, Parent3NameE, Parent4, Parent4NameA, 
                        Parent4NameE, Parent5, Parent5NameA, Parent5NameE, Parent6, Parent6NameA, Parent6NameE, Parent7, Parent7NameA, Parent7NameE, Parent8, 
                        Parent8NameA, Parent8NameE, Parent9, Parent9NameA, Parent9NameE
FROM            (SELECT dbo.GL_ChartOfAccounts.Id, dbo.GL_ChartOfAccounts.MemberShipId,dbo.GL_ChartOfAccounts.CompanyId,dbo.GL_ChartOfAccounts.Code, dbo.GL_ChartOfAccounts.NameA, dbo.GL_ChartOfAccounts.NameE, dbo.GL_ChartOfAccounts.ParentId, 
                        dbo.GL_ChartOfAccounts.HasChildren, dbo.GL_ChartOfAccounts.AccType, dbo.GL_ChartOfAccounts.Levels, dbo.GL_ChartOfAccounts.AccNature, 
                        dbo.GL_ChartOfAccounts.CurrencyId, dbo.GL_ChartOfAccounts.CostCenterId, 
                        dbo.GL_ChartOfAccounts.IsSecret, dbo.GL_ChartOfAccounts.Notes, dbo.GL_ChartOfAccounts.Create_Uid, dbo.GL_ChartOfAccounts.Create_Date, 
                        dbo.GL_ChartOfAccounts.Write_Uid, dbo.GL_ChartOfAccounts.Write_Date, dbo.GL_ChartOfAccounts.Post, 
                        dbo.GL_ChartOfAccounts.Post_Uid, dbo.GL_ChartOfAccounts.Post_Date, dbo.GL_ChartOfAccounts.Deleted, dbo.GL_ChartOfAccounts.Delete_Uid, dbo.GL_ChartOfAccounts.Delete_Date,
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_1.Code WHEN 9 THEN GL_ChartOfAccounts_2.Code WHEN 8 THEN GL_ChartOfAccounts_3.Code
                        WHEN 7 THEN GL_ChartOfAccounts_4.Code WHEN 6 THEN GL_ChartOfAccounts_5.Code WHEN 5 THEN GL_ChartOfAccounts_6.Code WHEN 4 THEN
                        GL_ChartOfAccounts_7.Code WHEN 3 THEN GL_ChartOfAccounts_8.Code WHEN 2 THEN GL_ChartOfAccounts_9.Code WHEN 1 THEN '0' END AS Parent1,
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_1.NameA WHEN 9 THEN GL_ChartOfAccounts_2.NameA WHEN 8 THEN GL_ChartOfAccounts_3.NameA
                        WHEN 7 THEN GL_ChartOfAccounts_4.NameA WHEN 6 THEN GL_ChartOfAccounts_5.NameA WHEN 5 THEN GL_ChartOfAccounts_6.NameA WHEN 4 THEN GL_ChartOfAccounts_7.NameA
                        WHEN 3 THEN GL_ChartOfAccounts_8.NameA WHEN 2 THEN GL_ChartOfAccounts_9.NameA WHEN 1 THEN '' END AS Parent1NameA, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_1.NameE WHEN 9 THEN GL_ChartOfAccounts_2.NameE WHEN 8 THEN GL_ChartOfAccounts_3.NameE
                        WHEN 7 THEN GL_ChartOfAccounts_4.NameE WHEN 6 THEN GL_ChartOfAccounts_5.NameE WHEN 5 THEN GL_ChartOfAccounts_6.NameE WHEN 4 THEN GL_ChartOfAccounts_7.NameE
                        WHEN 3 THEN GL_ChartOfAccounts_8.NameE WHEN 2 THEN GL_ChartOfAccounts_9.NameE WHEN 1 THEN '' END AS Parent1NameE, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_2.Code WHEN 9 THEN GL_ChartOfAccounts_3.Code WHEN 8 THEN GL_ChartOfAccounts_4.Code
                        WHEN 7 THEN GL_ChartOfAccounts_5.Code WHEN 6 THEN GL_ChartOfAccounts_6.Code WHEN 5 THEN GL_ChartOfAccounts_7.Code WHEN 4 THEN
                        GL_ChartOfAccounts_8.Code WHEN 3 THEN GL_ChartOfAccounts_9.Code WHEN 2 THEN '0' WHEN 1 THEN '0' END AS Parent2, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_2.NameA WHEN 9 THEN GL_ChartOfAccounts_3.NameA WHEN 8 THEN GL_ChartOfAccounts_4.NameA
                        WHEN 7 THEN GL_ChartOfAccounts_5.NameA WHEN 6 THEN GL_ChartOfAccounts_6.NameA WHEN 5 THEN GL_ChartOfAccounts_7.NameA WHEN 4 THEN GL_ChartOfAccounts_8.NameA
                        WHEN 3 THEN GL_ChartOfAccounts_9.NameA WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent2NameA, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_2.NameE WHEN 9 THEN GL_ChartOfAccounts_3.NameE WHEN 8 THEN GL_ChartOfAccounts_4.NameE
                        WHEN 7 THEN GL_ChartOfAccounts_5.NameE WHEN 6 THEN GL_ChartOfAccounts_6.NameE WHEN 5 THEN GL_ChartOfAccounts_7.NameE WHEN 4 THEN GL_ChartOfAccounts_8.NameE
                        WHEN 3 THEN GL_ChartOfAccounts_9.NameE WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent2NameE, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_3.Code WHEN 9 THEN GL_ChartOfAccounts_4.Code WHEN 8 THEN GL_ChartOfAccounts_5.Code
                        WHEN 7 THEN GL_ChartOfAccounts_6.Code WHEN 6 THEN GL_ChartOfAccounts_7.Code WHEN 5 THEN GL_ChartOfAccounts_8.Code WHEN 4 THEN
                        GL_ChartOfAccounts_9.Code WHEN 3 THEN '0' WHEN 2 THEN '0' WHEN 1 THEN '0' END AS Parent3, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_3.NameA WHEN 9 THEN GL_ChartOfAccounts_4.NameA WHEN 8 THEN GL_ChartOfAccounts_5.NameA
                        WHEN 7 THEN GL_ChartOfAccounts_6.NameA WHEN 6 THEN GL_ChartOfAccounts_7.NameA WHEN 5 THEN GL_ChartOfAccounts_8.NameA WHEN 4 THEN GL_ChartOfAccounts_9.NameA
                        WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent3NameA, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_3.NameE WHEN 9 THEN GL_ChartOfAccounts_4.NameE WHEN 8 THEN GL_ChartOfAccounts_5.NameE
                        WHEN 7 THEN GL_ChartOfAccounts_6.NameE WHEN 6 THEN GL_ChartOfAccounts_7.NameE WHEN 5 THEN GL_ChartOfAccounts_8.NameE WHEN 4 THEN GL_ChartOfAccounts_9.NameE
                        WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent3NameE, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_4.Code WHEN 9 THEN GL_ChartOfAccounts_5.Code WHEN 8 THEN GL_ChartOfAccounts_6.Code
                        WHEN 7 THEN GL_ChartOfAccounts_7.Code WHEN 6 THEN GL_ChartOfAccounts_8.Code WHEN 5 THEN GL_ChartOfAccounts_9.Code WHEN 4 THEN
                        '0' WHEN 3 THEN '0' WHEN 2 THEN '0' WHEN 1 THEN '0' END AS Parent4, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_4.NameA WHEN 9 THEN GL_ChartOfAccounts_5.NameA WHEN 8 THEN GL_ChartOfAccounts_6.NameA
                        WHEN 7 THEN GL_ChartOfAccounts_7.NameA WHEN 6 THEN GL_ChartOfAccounts_8.NameA WHEN 5 THEN GL_ChartOfAccounts_9.NameA WHEN 4 THEN '' WHEN
                        3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent4NameA, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_4.NameE WHEN 9 THEN GL_ChartOfAccounts_5.NameE WHEN 8 THEN GL_ChartOfAccounts_6.NameE
                        WHEN 7 THEN GL_ChartOfAccounts_7.NameE WHEN 6 THEN GL_ChartOfAccounts_8.NameE WHEN 5 THEN GL_ChartOfAccounts_9.NameE WHEN 4 THEN '' WHEN
                        3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent4NameE, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_5.Code WHEN 9 THEN GL_ChartOfAccounts_6.Code WHEN 8 THEN GL_ChartOfAccounts_7.Code
                        WHEN 7 THEN GL_ChartOfAccounts_8.Code WHEN 6 THEN GL_ChartOfAccounts_9.Code WHEN 5 THEN '0' WHEN 4 THEN '0' WHEN 3 THEN '0' WHEN
                        2 THEN '0' WHEN 1 THEN '0' END AS Parent5, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_5.NameA WHEN 9 THEN GL_ChartOfAccounts_6.NameA WHEN 8 THEN GL_ChartOfAccounts_7.NameA
                        WHEN 7 THEN GL_ChartOfAccounts_8.NameA WHEN 6 THEN GL_ChartOfAccounts_9.NameA WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN
                        '' WHEN 1 THEN '' END AS Parent5NameA, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_5.NameE WHEN 9 THEN GL_ChartOfAccounts_6.NameE WHEN 8 THEN GL_ChartOfAccounts_7.NameE
                        WHEN 7 THEN GL_ChartOfAccounts_8.NameE WHEN 6 THEN GL_ChartOfAccounts_9.NameE WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN
                        '' WHEN 1 THEN '' END AS Parent5NameE, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_6.Code WHEN 9 THEN GL_ChartOfAccounts_7.Code WHEN 8 THEN GL_ChartOfAccounts_8.Code
                        WHEN 7 THEN GL_ChartOfAccounts_9.Code WHEN 6 THEN '0' WHEN 5 THEN '0' WHEN 4 THEN '0' WHEN 3 THEN '0' WHEN 2 THEN '0' WHEN 1 THEN
                        '0' END AS Parent6, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_6.NameA WHEN 9 THEN GL_ChartOfAccounts_7.NameA WHEN 8 THEN GL_ChartOfAccounts_8.NameA
                        WHEN 7 THEN GL_ChartOfAccounts_9.NameA WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END
                        AS Parent6NameA, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_6.NameE WHEN 9 THEN GL_ChartOfAccounts_7.NameE WHEN 8 THEN GL_ChartOfAccounts_8.NameE
                        WHEN 7 THEN GL_ChartOfAccounts_9.NameE WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END
                        AS Parent6NameE, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_7.Code WHEN 9 THEN GL_ChartOfAccounts_8.Code WHEN 8 THEN GL_ChartOfAccounts_9.Code
                        WHEN 7 THEN '0' WHEN 6 THEN '0' WHEN 5 THEN '0' WHEN 4 THEN '0' WHEN 3 THEN '0' WHEN 2 THEN '0' WHEN 1 THEN '0' END AS Parent7,
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_7.NameA WHEN 9 THEN GL_ChartOfAccounts_8.NameA WHEN 8 THEN GL_ChartOfAccounts_9.NameA
                        WHEN 7 THEN '' WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent7NameA,
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_7.NameE WHEN 9 THEN GL_ChartOfAccounts_8.NameE WHEN 8 THEN GL_ChartOfAccounts_9.NameE
                        WHEN 7 THEN '' WHEN 6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent7NameE,
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_8.Code WHEN 9 THEN GL_ChartOfAccounts_9.Code WHEN 8 THEN '0' WHEN 7 THEN
                        '0' WHEN 6 THEN '0' WHEN 5 THEN '0' WHEN 4 THEN '0' WHEN 3 THEN '0' WHEN 2 THEN '0' WHEN 1 THEN '0' END AS Parent8, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_8.NameA WHEN 9 THEN GL_ChartOfAccounts_9.NameA WHEN 8 THEN '' WHEN 7 THEN '' WHEN
                        6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent8NameA, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_8.NameE WHEN 9 THEN GL_ChartOfAccounts_9.NameE WHEN 8 THEN '' WHEN 7 THEN '' WHEN
                        6 THEN '' WHEN 5 THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent8NameE, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_9.Code WHEN 9 THEN '0' WHEN 8 THEN '0' WHEN 7 THEN '0' WHEN 6 THEN '0'
                        WHEN 5 THEN '0' WHEN 4 THEN '0' WHEN 3 THEN '0' WHEN 2 THEN '0' WHEN 1 THEN '0' END AS Parent9, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_9.NameA WHEN 9 THEN '' WHEN 8 THEN '' WHEN 7 THEN '' WHEN 6 THEN '' WHEN 5
                        THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent9NameA, 
                        CASE GL_ChartOfAccounts.Levels WHEN 10 THEN GL_ChartOfAccounts_9.NameE WHEN 9 THEN '' WHEN 8 THEN '' WHEN 7 THEN '' WHEN 6 THEN '' WHEN 5
                        THEN '' WHEN 4 THEN '' WHEN 3 THEN '' WHEN 2 THEN '' WHEN 1 THEN '' END AS Parent9NameE
        FROM            dbo.GL_ChartOfAccounts LEFT OUTER JOIN
                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_9 ON dbo.GL_ChartOfAccounts.ParentId = GL_ChartOfAccounts_9.Id LEFT OUTER JOIN
                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_8 ON GL_ChartOfAccounts_9.ParentId = GL_ChartOfAccounts_8.Id LEFT OUTER JOIN
                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_7 ON GL_ChartOfAccounts_8.ParentId = GL_ChartOfAccounts_7.Id LEFT OUTER JOIN
                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_6 ON GL_ChartOfAccounts_7.ParentId = GL_ChartOfAccounts_6.Id LEFT OUTER JOIN
                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_5 ON GL_ChartOfAccounts_6.ParentId = GL_ChartOfAccounts_5.Id LEFT OUTER JOIN
                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_4 ON GL_ChartOfAccounts_5.ParentId = GL_ChartOfAccounts_4.Id LEFT OUTER JOIN
                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_3 ON GL_ChartOfAccounts_4.ParentId = GL_ChartOfAccounts_3.Id LEFT OUTER JOIN
                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_2 ON GL_ChartOfAccounts_3.ParentId = GL_ChartOfAccounts_2.Id LEFT OUTER JOIN
                        dbo.GL_ChartOfAccounts AS GL_ChartOfAccounts_1 ON GL_ChartOfAccounts_2.ParentId = GL_ChartOfAccounts_1.Id) AS Gl_Accounttree