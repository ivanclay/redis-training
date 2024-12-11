# Redis Training

![Badge de Licença](https://img.shields.io/badge/.NET-8.0.0-blue.svg?style=flat-square&logo=dotnet)
![Badge de Licença](https://img.shields.io/badge/Redis-0.0.0-orange.svg?style=flat-square&logo=redis)
![Badge de Licença](https://img.shields.io/badge/git-2.42.0-lightgrey.svg?style=flat-square&logo=git)
![Badge de Licença](https://img.shields.io/badge/docker-27.2.0-orange.svg?style=flat-square&logo=docker)

![Badge de Versão](https://img.shields.io/badge/app-v_1.0.0-green.svg?style=flat-square&logo=app)
![Badge de Status do Projeto](https://img.shields.io/badge/status-training-blue.svg?style=flat-square)

It is only a Redis training project.

### Contextualization

We can execute Redis Database on local Docker containers to features tests.

### Docker RUN

```sh
docker run -d --name redis-container -p 6380:6379 redis
````

### Dependencies

- [StackExchange.Redis 2.8.22]()