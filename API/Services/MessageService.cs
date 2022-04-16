using Microsoft.Extensions.Options;

namespace API.Services
{
    public class MessageService
    {
        private readonly IOptions<MessageSettings> _config;

        public MessageService(IOptions<MessageSettings> config)
        {
            this._config = config;
        }

        public MessageSettings GetMessageSettings()
        {
            MessageSettings MessageSettings = _config.Value;
            return MessageSettings;

        }
        
    }

    public class MessageSettings
    {
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
    }
}