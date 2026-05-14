mergeInto(LibraryManager.library, {
    GetPlayerData: function () {
        ygGameInstance.SendMessage('Progress', 'SetPhoto', player.getPhoto("small"));
    },

    SaveExtern: function(data) {
        var dataString = UTF8ToString(data);
        var myobj = JSON.parse(dataString);
        player.setData(myobj);
    },

    LoadExtern: function() {
        player.getData().then(_data => {
            const myJSON = JSON.stringify(_data);
            ygGameInstance.SendMessage('Progress', 'SetPlayerInfo', myJSON);
        });
    },
});