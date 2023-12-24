
start: 
	dotnet watch --project src/AuctionService/AuctionService.csproj # start the auction service only

run:
	dotnet run --project $(PROJECT)

dcp: 
	docker compose up -d

dcd: 
	docker compose down