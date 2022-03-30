write-host("Build dos Dockerfiles do backend e do frontend")

$Env:DOCKER_BUILDKIT = 1
docker build --target backend -t kanban_api .
docker build --target frontend -t kanban_frontend .

write-host("Imagens buildadas, executar docker-compose up")