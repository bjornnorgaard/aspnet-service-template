#!/bin/bash

read -p "Input migration name: " name
echo "Rolling back to migration: $name"

dotnet ef database update $name --project ../Ant.Todos.Api.csproj
