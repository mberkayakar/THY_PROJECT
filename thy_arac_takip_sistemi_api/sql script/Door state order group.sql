SELECT TOP (1000) 
      [id],
      [doorId]
      ,[state]
      ,[order]
  FROM [THY_ARAC_TAKIP_SISTEMI].[dbo].[Doors] group by id,doorId,state,[order] order by doorId 

