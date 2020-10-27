SELECT TOP 1
        PostalCode
    ,   CalculationType
    
    FROM dbo.CalculationTypes
    WHERE 
        PostalCode = @PostalCode