docker build -t squip-api .
docker tag squip-api us-central1-docker.pkg.dev/squip-project/squip-repository/squip-api
docker push us-central1-docker.pkg.dev/squip-project/squip-repository/squip-api
gcloud config set run/region us-central1
gcloud run deploy squip-api --image=us-central1-docker.pkg.dev/squip-project/squip-repository/squip-api
gcloud run services update-traffic squip-api --to-latest
