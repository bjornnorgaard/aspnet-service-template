#!/bin/bash

read -p "Input migration name: " name
echo "Will create migration: $name"

dotnet ef migrations add $name --project ../Ant.Todo.Api.csproj --output-dir ./Database/Migrations
