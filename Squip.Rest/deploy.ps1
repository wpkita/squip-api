docker build -t squip-api .
docker tag squip-api us-central1-docker.pkg.dev/squip-project/squip-repository/squip-api
docker push us-central1-docker.pkg.dev/squip-project/squip-repository/squip-api
