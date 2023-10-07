using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WeatherApi.Core;
using WeatherApi.Core.Enum;

namespace WeatherApi.ServiceContract.Response
{
    public class BaseResponse
    {
        #region Fields

        private Collection<Message> messages = new Collection<Message>();

        #endregion

        #region Properties

        public Collection<Message> Messages => messages ??= new Collection<Message>();

        #endregion

        #region (public) Methods

        public bool IsError()
        {
            return Messages.Count(item => item.Type == MessageType.Error) > 0;
        }

        public string[] GetMessageErrorTextArray()
        {
            return Messages.Where(item => item.Type == MessageType.Error)
                .Select(item => item.MessageText)
                .ToArray();
        }

        public string GetErrorMessage()
        {
            var messageBuilder = new StringBuilder();
            foreach (var message in Messages)
            {
                messageBuilder.AppendLine(message.MessageText);
            }

            return messageBuilder.ToString().Trim();
        }

        public void AddErrorMessage(string errorMessage)
        {
            Messages.Add(new Message
            {
                MessageText = errorMessage,
                Type = MessageType.Error,
            });
        }

        #endregion
    }
}
