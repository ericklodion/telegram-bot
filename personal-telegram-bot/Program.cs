

using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var botClient = new TelegramBotClient("5990201904:AAHj6RYLu29L5-MMrsHy3NORvaTNrypauTU");

var cancellantionToken = new CancellationTokenSource();

botClient.StartReceiving(
    updateHandler: UpdateHandlerAsync,
    pollingErrorHandler: PollingErrorHandlerAsync,
    receiverOptions: new ReceiverOptions
    {
        AllowedUpdates = Array.Empty<UpdateType>()
    },
    cancellationToken: cancellantionToken.Token
);

Task PollingErrorHandlerAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cts)
{
    Console.WriteLine(exception.Message);
    return Task.CompletedTask;
}

async Task UpdateHandlerAsync(ITelegramBotClient botClient, Update update, CancellationToken cts)
{
    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Mensagem recebida: {messageText}");

    var sendMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Mensagem de resposta",
            cancellationToken: cts
        );
}

var me = await botClient.GetMeAsync(cancellantionToken.Token);

Console.WriteLine($"Escutando {me.Username}");
Console.ReadLine();

cancellantionToken.Cancel();
