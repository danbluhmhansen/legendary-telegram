#!/bin/bash

dotnet ef database drop \
  --context ApplicationDbContext \
  --project './Source/Server/LegendaryTelegram.Server.csproj' \
  --startup-project './Source/Server/LegendaryTelegram.Server.csproj' \
  --force


