USE [channel.ports.Temp]
BEGIN
DECLARE @cnt INT = 1;
DECLARE @guid AS uniqueidentifier;

Declare @SystemName AS VARCHAR(200);
Declare @Shipment AS VARCHAR(200);
create table #ShipmentsTemp(storeID INT, ShipmentName VARCHAR(200), ProgressName VARCHAR(200))
insert into #ShipmentsTemp values
(1, 'DRAFT', 'Draft'),
(2, 'READYTOTRANSMIT', 'ReadyToTransmit'),
(3, 'READYTOTRAPI', 'ReadyToTransmitAPI'),
(4, 'ASMACCEPT', 'ASMAccept'),
(5, 'ASMREJECT', 'ASMReject'),
(6, 'AWAITINGARRIVAL', 'AwaitingArrival'),
(7, 'AWAITINGDEPARTUR', 'AwaitingDeparture'),
(8, 'PROCESSINGERRORA', 'ProcessingErrorArrival'),
(9, 'PROCESSINGERRORD', 'ProcessingErrorDeparture'),
(10, 'ARRIVED', 'Arrived'),
(11, 'WITHCUSTOMS', 'WithCustoms'),
(12, 'QUERIEDARRIVED', 'QueriedArrived'),
(13, 'QUERIEDWITHCUSTO', 'QueriedWithCustoms'),
(14, 'CLEARED', 'Cleared'),
(15, 'CANCELLED', 'Cancelled'),
(16, 'MGENEREAL', 'ManualGenereal'),
(17, 'MCPC', 'ManualCPC'),
(18, 'MLICENSE', 'ManualLicense'),
(19, 'MROUTE', 'ManualRoute'),
(20, 'MGENEREALASMACCE', 'ManualGenerealASMAccepted'),
(21, 'MCPCASMACCEPTED', 'ManualCPCASMAccepted'),
(22, 'MLICENSEASMACCEP', 'ManualLicenseASMAccepted'),
(23, 'MROUTEASMACCEPTE', 'ManualRouteASMAccepted'),
(24, 'MGENEREALASMREJE', 'ManualGenerealASMRejected'),
(25, 'MCPCASMREJECTED', 'ManualCPCASMRejected'),
(26, 'MLICENSEASMREJEC', 'ManualLicenseASMRejected'),
(27, 'MROUTEASMREJECTE', 'ManualRouteASMRejected'),
(28, 'INTERNALERROR', 'InternalError'),
(29, 'PARTIAL', 'Partial')

Select * from #ShipmentsTemp

WHILE @cnt < 30

BEGIN
Set @SystemName = (SELECT TOP 1 ProgressName FROM #ShipmentsTemp WHERE storeID = @cnt)
Set @Shipment = (SELECT TOP 1 ShipmentName FROM #ShipmentsTemp WHERE storeID =  @cnt)

Set @guid = (SELECT TOP 1 Id FROM [dbo].[Progress] WHERE [SystemName] like @SystemName)



UPDATE [dbo].[Shipments]
  SET [Progress] = @guid where [MyReferenceForCPInvoice] like @Shipment


   SET @cnt = @cnt + 1;
END;
Drop TABLE #ShipmentsTemp
END;