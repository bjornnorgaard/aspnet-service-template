  
#!bin/bash
docker-compose build todos-api
docker-compose rm --stop --force todos-api
docker-compose up -d
read -n 1 -s -r -p "Press any key to continue"