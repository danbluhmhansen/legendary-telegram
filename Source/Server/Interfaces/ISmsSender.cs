namespace LegendaryTelegram.Server.Interfaces;

public interface ISmsSender
{
    Task SendSmsAsync(string number, string message);
}
