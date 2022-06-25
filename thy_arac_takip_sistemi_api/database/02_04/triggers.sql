CREATE   TRIGGER create_eventlog_buttonentry ON THY_ARAC_TAKIP_SISTEMI.dbo.ButtonEntries 
AFTER INSERT
AS
BEGIN
	
	declare  @plate nvarchar(max) =( select plate from inserted );

	declare @doorNo nvarchar  = ( select CONVERT(nvarchar(10), doorNo) from inserted );
	declare @buttonNoText nvarchar  = ( select CONVERT(nvarchar(10), buttonNo) from inserted );


	if @buttonNoText='0'
		insert into THY_ARAC_TAKIP_SISTEMI.dbo.EventLogs(message,eventLogType,dateCreated) values(@plate+' '+ @doorNo +N' numaralı kapıdan EXPORT butonu ile giriş yaptı.',0,dateadd(hour,3,getdate())) 
    else if @buttonNoText='1' 
    	insert into THY_ARAC_TAKIP_SISTEMI.dbo.EventLogs(message,eventLogType,dateCreated) values(@plate+' '+ @doorNo +N' numaralı kapıdan IMPORT butonu ile giriş yaptı.',0,dateadd(hour,3,getdate()))
	else if @buttonNoText='2' 
    	insert into THY_ARAC_TAKIP_SISTEMI.dbo.EventLogs(message,eventLogType,dateCreated) values(@plate+' '+ @doorNo +N' numaralı kapıdan ICHAT butonu ile giriş yaptı.',0,dateadd(hour,3,getdate()))
		
END;

CREATE TRIGGER create_eventlog_on_doorassign ON THY_ARAC_TAKIP_SISTEMI.dbo.DoorAssigns
AFTER INSERT
AS
BEGIN
	declare @id int = (select reservationId from inserted);
	declare  @plate nvarchar(max) =( select carPlate from THY_ARAC_TAKIP_SISTEMI.dbo.Reservations r where r.id=@id);
	declare @doorNo nvarchar(max)= (select CONVERT (nvarchar(max),doorNo+1) from inserted);
		
	insert into THY_ARAC_TAKIP_SISTEMI.dbo.EventLogs(message,eventLogType,dateCreated) values(@plate+' ' +@doorNo  + N' numaralı kapıya atandı.',0,dateadd(hour,3,getdate())) 
 
END;

CREATE   TRIGGER create_eventlog_on_res_create ON THY_ARAC_TAKIP_SISTEMI.dbo.Reservations
AFTER INSERT
AS
BEGIN
	
	declare  @plate nvarchar(max) =( select carPlate from inserted );
	
	insert into THY_ARAC_TAKIP_SISTEMI.dbo.EventLogs(message,eventLogType,dateCreated) values(@plate+ N' için rezervasyon yapıldı.',0,dateadd(hour,3,getdate())) 
 
END;

CREATE   TRIGGER create_eventlog_on_waiting_entry ON THY_ARAC_TAKIP_SISTEMI.dbo.WaitingQueues
AFTER INSERT
AS
BEGIN
	declare @id int = (select reservationId from inserted);
	declare  @plate nvarchar(max) =( select carPlate from THY_ARAC_TAKIP_SISTEMI.dbo.Reservations r where r.id=@id);
		
	insert into THY_ARAC_TAKIP_SISTEMI.dbo.EventLogs(message,eventLogType,dateCreated) values(@plate+N' bekleme alanına yönlendirildi.',0,dateadd(hour,3,getdate())) 
 
END;

CREATE   TRIGGER create_eventlog_ptsenrty ON THY_ARAC_TAKIP_SISTEMI.dbo.PTSLogs
AFTER INSERT
AS
BEGIN
	
	declare  @plate nvarchar(max) =( select plate from inserted );
	declare  @isButtonEntry bit =( select isButtonEntry from inserted );
	declare @doorNo nvarchar  = ( select CONVERT(nvarchar(10), doorNo) from inserted );
	if @isButtonEntry=0 
    	insert into THY_ARAC_TAKIP_SISTEMI.dbo.EventLogs(message,eventLogType,dateCreated) values(@plate+' '+ @doorNo + N' numaralı kapıya geldi.',0,dateadd(hour,3,getdate())) 

END

/*

if @isButtonEntry=1
		insert into THY_ARAC_TAKIP_SISTEMI.dbo.EventLogs(message,eventLogType,dateCreated) values(@plate+' '+ @doorNo +N' numaralı kapıdan buton ile giriş yaptı.',0,dateadd(hour,3,getdate())) 
    else 
*/;

