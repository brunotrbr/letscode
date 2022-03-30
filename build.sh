#!/bin/env bash

## Build dos Dockerfiles do backend e do frontend
echo "Build dos Dockerfiles do backend e do frontend"

export DOCKER_BUILDKIT=1
cd BACK/kanban-api
docker build -t kanban_api .
CD ../../FRONT
docker build -t kanban_frontend .

echo "Imagens buildadas, executar docker-compose up"
