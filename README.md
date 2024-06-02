# Dictionary

Api for electronic dictionary containing api for adding topics, sub topics and words.
identity api for login and adding and editing users for admins.

# Use

use [docker compose](docker-compose.yml) file to start postgres database and api:

```
version: '3.5'

services:
  dictionary-db:
    container_name: dictionary-db
    image: postgres
    restart: always
    ports:
      - "5432:5432"
    volumes:
      - ~/temp/postgres:/var/lib/postgresql/data
    environment:
      - POSTGRES_PASSWORD=mysecretpassword
      - POSTGRES_USER=postgres
    networks:
      - dictionary
  
  dictionary-api:
    container_name: dictionary-api
    image: revazashvili/dictionary-api:latest
    restart: always
    depends_on: [dictionary-db]
    ports:
      - "80:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings:DictionaryDb=Host=dictionary-db;Port=5432;Database=Dictionary;User Id=postgres;Password=mysecretpassword;
    networks: 
      - dictionary
        
networks:
  dictionary:
    name: dictionary_custom
```