using Newtonsoft.Json;

namespace NIM
{
    public class NIMVedioAttachment : NIMMessageAttachment
    {
        [JsonProperty("dur")]
        public int Duration { get; set; }

        [JsonProperty("w")]
        public int Width { get; set; }

        [JsonProperty("h")]
        public int Height { get; set; }
    }

    public class NIMVedioMessage : NIMIMMessage
    {
        public NIMVedioMessage()
        {
            MessageType = NIMMessageType.kNIMMessageTypeVideo;
        }

        [JsonProperty(AttachmentPath)]
        public NIMVedioAttachment VedioAttachment { get; set; }
    }
}