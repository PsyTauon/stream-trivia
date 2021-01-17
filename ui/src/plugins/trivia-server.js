import { HubConnectionBuilder, LogLevel } from "@aspnet/signalr";

export default {
  install(Vue) {
    const connection = new HubConnectionBuilder()
      .withUrl("/hub")
      .configureLogging(LogLevel.Information)
      .build();

    Vue.config.globalProperties.$GameServer = {
      connection,
      JoinGame(gameId, players) {
        return connection.invoke("JoinGame", gameId, players);
      },
      CreateGame(gameId, players) {
        return connection.invoke("CreateGame", gameId, players);
      },
      EnterRoom(gameId) {
        return connection.invoke("EnterRoom", gameId);
      },
      AddQuestion(gameId, questionText, Answer) {
        return connection.invoke("AddQuestion", gameId, questionText, Answer);
      },
    };

    connection.start();

    Vue.mixin({
      created() {
        if (this.$options.gameServer) {
          const methods = Object.getOwnPropertyNames(this.$options.gameServer);

          methods.forEach((method) => {
            connection.on(method, (data) =>
              this.$options.gameServer[method].call(this, data)
            );
          });
        }
      },
    });
  },
};
