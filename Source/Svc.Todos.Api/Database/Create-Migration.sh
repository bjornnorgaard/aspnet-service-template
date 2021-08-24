#!/bin/bash

read -p "Input migration name: " name
echo "Will create migration: $name"

dotnet ef migrations add $name --project ../Ant.Todos.Api.csproj --output-dir ./Database/Migrations
