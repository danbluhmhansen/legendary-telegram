#!/bin/bash

dotnet ef database update \
  --context ApplicationDbContext \
  --project './Source/Server/LegendaryTelegram.Server.csproj' \
  --startup-project './Source/Server/LegendaryTelegram.Server.csproj'

