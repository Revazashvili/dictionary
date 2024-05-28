docker build -t revazashvili/dictionary-api:latest -f src/DictionaryApi/Dockerfile .

docker tag revazashvili/dictionary-api:latest revazashvili/dictionary-api:latest

docker push revazashvili/dictionary-api:latest