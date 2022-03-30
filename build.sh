#!/bin/env bash

## Build dos Dockerfiles do backend e do frontend
echo "Build dos Dockerfiles do backend e do frontend"

export DOCKER_BUILDKIT=1
cd BACK/kanban-api
docker build --target backend -t kanban_api .
CD ../../FRONT
docker build --target frontend -t kanban_frontend .

echo "Imagens buildadas, executar docker-compose up"
