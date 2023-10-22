# Orders Management System

## Setup del progetto
Di seguito vengono riportate le istruzione per l'esecuzione del progetto.

### Creare container docker per database

Navigare nella cartella `assets` del progetto e lanciare
```
docker-compose up -d
```

Il file `docker-compose.yaml` si occuperà di creare il container con il DB Postgres utilizzato dai servizi.

### Applicare le migration al database

La connection string è già impostata nel file `appSettings.json`, è sufficiente lanciare le migration nel modo che si preferisce; personalmente preferisco usare il comando 
```
dotnet ef migrations script
```
per farmi generare lo script SQL e lo eseguo manualmente tramite DBeaver.  


## TODO
 - Localize error messages
 - Tests
 - Verify emails via regex
 - Remove nullability on Products from Order entity
 - Paginate results of GetOrders