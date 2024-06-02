using OnaxTools.Dto.Http;
using PrimusCommon.MessageQueue.Services;
using System.Runtime.CompilerServices;

namespace PrimusCommon.MessageQueue.Interfaces
{
    public interface IMessageQueueService
    {
        Task<GenResponse<string>> FetchAndProcessMsgFromQueue(FetchQueueProps props, CancellationToken ct = default, [CallerMemberName] string caller = "");
        Task<GenResponse<bool>> RabbitMqPublish<T>(T data, MqPublisherProps props, CancellationToken ct = default, [CallerMemberName] string caller = "");
    }
}