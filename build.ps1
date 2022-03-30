## Build dos Dockerfiles do backend e do frontend
write-host("Build dos Dockerfiles do backend e do frontend")

$Env:DOCKER_BUILDKIT = 1
Set-Location -Path "BACK/kanban-api"
docker build -t kanban_api .
Set-Location -Path "..\..\FRONT"
docker build -t kanban_frontend .

write-host("Imagens buildadas, executar docker-compose up")