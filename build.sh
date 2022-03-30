#!/bin/env bash

## Build dos Dockerfiles do backend e do frontend
echo "Build dos Dockerfiles do backend e do frontend"

export DOCKER_BUILDKIT=1
docker build --target backend -t kanban_api .
docker build --target frontend -t kanban_frontend .

echo "Imagens buildadas, executar docker-compose up"
