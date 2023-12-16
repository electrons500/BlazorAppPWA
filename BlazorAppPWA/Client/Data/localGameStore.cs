using BlazorAppPWA.Shared;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BlazorAppPWA.Client.Data
{
    public class localGameStore
    {
        private HttpClient _client;
        private IJSRuntime js;

        public localGameStore(HttpClient client, IJSRuntime js)
        {
            _client = client;
            this.js = js;
        }


        //Check if server is online
        public async Task<bool> CheckIfServerIsOnline()
        {
            var response = await _client.GetAsync("api/PingDB/PingDatabase");
            return response.IsSuccessStatusCode;
        }
       
        public async Task<Game[]> GetAllGamesFromlocalDb(string storename)
        {
            return await GetAllAsync<Game[]>(storename);
        }

        public async Task InsertServerDataIntoLocal(string storeName, List<Game> games)
        {
           for (int i = 0;i < games.Count ;i++)
           {
                await PutAsync(storeName, games[i].Id, games[i]);
           }
        }

        public async Task<bool> AddNewGameToLocalAsync(string storeName, Game game)
        {
            var gamelist = await GetAllGamesFromlocalDb("serverdata");
            int newId = (gamelist.Last().Id) + 1;
            game.Id = newId;
            await PutAsync(storeName,newId , game);
            
            return true;
        }

        public async Task AddUserOperationsAsync(string storename,string sqlquery) 
        {
            await PutOperationsAsync(storename, sqlquery);
        }

      

        public async Task<Game> GetGameAsync(string storename,object key)
        {
            var gamefromlocal = await GetAsync<Game>(storename,key);
            if (gamefromlocal is null)
            {
                return null;
            }
            return gamefromlocal;
        }


        public async Task<bool> DeleteGameAsync(string storename,object key)
        {
            await DeleteAsync(storename,key);
            
            return true;
        }
        public async Task<bool> UpdateGameToLocalAsync(string storeName, Game game)
        {
            await PutAsync(storeName,game.Id, game);
            
            return true;
        }

        public async Task DeleteAllGameFromlocal(string storeName)
        {
           await DeleteAllAsync(storeName);
        }

        public async Task<int> GetNumberofValuesInObjectStore(string storeName)
        {
            return await CountAsync<int>(storeName); 
        }

        public async Task<bool> SynchronizeAsync() 
        {
            int count = 0;
            foreach (var sql in await GetAllAsync<string[]>("operations"))
            {
                var response = await _client.PostAsJsonAsync("api/server/SendQuery", sql);
                if(response.IsSuccessStatusCode) { count++; }
            }
            return count > 0;
        }

      
        ValueTask<T> GetAsync<T>(string storeName, object key) 
            => js.InvokeAsync<T>("localGameStore.get", storeName, key);

        ValueTask<T> GetAllAsync<T>(string storeName) 
            => js.InvokeAsync<T>("localGameStore.getAll", storeName);

        ValueTask PutAsync(string storeName, object key, object value)   
            => js.InvokeVoidAsync("localGameStore.put", storeName, key, value);

         ValueTask PutOperationsAsync(string storeName, string sqlquery)     
            => js.InvokeVoidAsync("localGameStore.putwithoutKey", storeName, sqlquery);

         ValueTask PutPushSubscriptionAsync(string storeName, object value)     
            => js.InvokeVoidAsync("localGameStore.putwithoutKey", storeName, value);



        ValueTask PutAllFromJsonAsync(string storeName, string json)
            => js.InvokeVoidAsync("localGameStore.putAllFromJson", storeName,json);

        ValueTask DeleteAsync(string storeName, object key)  
           => js.InvokeVoidAsync("localGameStore.delete", storeName, key);
        ValueTask DeleteAllAsync(string storeName)  
           => js.InvokeVoidAsync("localGameStore.clear", storeName);

        ValueTask<T> CountAsync<T>(string storeName)
            => js.InvokeAsync<T>("localGameStore.count", storeName); 

    }
}
