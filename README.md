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
per far generare lo script SQL ed eseguirlo manualmente tramite DBeaver.  


## Wishlist
Per preparare questo POC per la produzione sarebbe necessario eseguire le seguenti operazioni:
 - Localizzare i messaggi di errore ed utilizzare uno standard per gli errori delle operazioni CRUD su Database
 - Scrivere ulteriori test
 - Verificare email tramite regex
 - Impaginare i risultati dell'API GetOrders
 - Log di ogni operazione su livelli differenti in base al tipo di log.
 - Creare un progetto per ogni entity presente nel Context, nello specifico:
   - OrdersManagementSystem.Address
   - OrdersManagementSystem.Category
   - OrdersManagementSystem.Product
   - OrdersManagementSystem.Order
 - (OPZIONALE) Creare un DB per progetto, in alternativa creare un ruolo nel DB per ogni progetto ed assegnare ad ogni progetto una connection string che preveda l'uso dell'utente specifico invece del ruolo di default