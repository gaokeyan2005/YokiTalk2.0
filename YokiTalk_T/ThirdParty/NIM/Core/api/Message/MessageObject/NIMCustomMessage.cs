using Newtonsoft.Json;

namespace NIM
{
    /// <summary>
    /// 自定义消息基类
    /// </summary>
    /// <typeparam name="T">自定义消息的实际类型</typeparam>
    public class NIMCustomMessage<T> : NIMIMMessage
    {
        public NIMCustomMessage()
        {
            MessageType = NIMMessageType.kNIMMessageTypeCustom;
        }

        [JsonProperty(AttachmentPath)]
        public virtual T CustomContent { get; set; }

        [JsonProperty(MessageBodyPath)]
        public string Extention { get; set; }

        /// <summary>
        /// 上传附件(只支持在发送包含本地资源的自定义消息)
        /// 成功上传资源服务器获取到的url信息将存放在kNIMMsgKeyAttach字段, 内容为{"url" : "资源url", ...}
        /// </summary>
        [JsonProperty("need_upload_res")]
        public bool NeedUploadResource { get; set; }

    }
}