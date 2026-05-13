mergeInto(LibraryManager.library, {
    GetPlayerData: function () {
        ygGameInstance.SendMessage('Progress', 'SetPhoto', player.getPhoto("small"));
    },
});