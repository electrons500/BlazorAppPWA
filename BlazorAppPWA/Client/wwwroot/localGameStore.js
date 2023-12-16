(function () {
    const db = idb.openDB('Games', 1, {
        upgrade(db) {
            db.createObjectStore('serverdata');
            db.createObjectStore('operations', {autoIncrement: true});
        },
    });

    window.localGameStore = {
        get: async (storeName, key) => (await db).transaction(storeName).store.get(key),
        getAll: async (storeName) => (await db).transaction(storeName).store.getAll(),
        put: async (storeName, key, value) => (await db).transaction(storeName, 'readwrite').store.put(value, key === null ? undefined : key),
        putwithoutKey: async (storename,value) => (await db).transaction(storename,'readwrite').store.put(value),
        putAllFromJson: async (storeName, json) => {
            const store = (await db).transaction(storeName, 'readwrite').store;
            JSON.parse(json).forEach(item => store.put(item));
        },
        delete: async (storeName, key) => (await db).transaction(storeName, 'readwrite').store.delete(key),
        clear: async (storeName) => (await db).transaction(storeName, 'readwrite').store.clear(),
        count: async (storeName) => (await db).transaction(storeName, 'readonly').store.count()
    };

})();