DECLARE @OrderId INT = 1
SELECT
	SUM(oil.Quantity) AS [Кол-во продкуции в заказе],
	SUM(oil.Quantity * cost.ProductMaterialCost) AS [Стоимость всех материалов]
FROM Orders o
INNER JOIN  OrderItemList oil ON o.Id = oil.Id
INNER JOIN (
	SELECT 
		ProductId,
		SUM(pil.Quantity * m.Price) AS ProductMaterialCost
	FROM ProductItemList pil
	INNER JOIN Materials m ON pil.MaterialId = m.Id
	GROUP BY ProductId
) cost ON oil.ProductId = cost.ProductId
WHERE o.Id = @OrderId
