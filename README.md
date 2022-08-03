# Dockerized asp.Net 6 REST API with mongoDB

### To Build the REST API in Docker
```bash
docker build --rm -t masum/dotnet_web_api:latest .
```

### To Run the REST API in Docker
```bash
docker image ls | findstr dotnet_web_api

docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 masum/dotnet_web_api
```

### To Stop the REST API in Docker
```bash
docker ps

docker container stop <first 3 digit of container id>
```

## Feedback

If you have any feedback, please reach out to me at abdullahmasum6035@gmail.com

<a href="https://github.com/MasumBhai"><img alt="Abdullah Al Masum's Github Stats" src="https://github-readme-stats.vercel.app/api?username=masumBhai&show_icons=true&count_private=true&theme=great-gatsby" width=400></a>
<a href="https://github.com/MasumBhai"><img alt="Abdullah Al Masum's Github Streak" src="https://github-readme-streak-stats.herokuapp.com?user=MasumBhai&theme=vision-friendly-dark&fire=DD2727&sideNums=CD5CDD" width=400></a>

