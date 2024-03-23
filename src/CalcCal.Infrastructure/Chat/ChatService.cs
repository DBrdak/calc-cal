using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CalcCal.Application.Abstractions.Chat;
using CalcCal.Infrastructure.Chat.Models.ChatCompletionResponse;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Responses.DB;

namespace CalcCal.Infrastructure.Chat
{
    public sealed class ChatService : IChatService
    {
        private readonly HttpClient _httpClient;
        private readonly ChatOptions _chatOptions;

        public ChatService(HttpClient httpClient, IOptions<ChatOptions> chatOptions)
        {
            _httpClient = httpClient;
            _chatOptions = chatOptions.Value;
        }

        public async Task<Result<string>> SendChatCompletionAsync(string message)
        {
            _httpClient.BaseAddress = new Uri(_httpClient.BaseAddress!, _chatOptions.ChatUrl);

            var response = await _httpClient.SendAsync(GetChatCompletionRequest(message));

            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure<string>(Error.TaskFailed($"Error occured while sending request to chat provider. Status code: {response.StatusCode}"));
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var chatCompletionResponse = JsonConvert.DeserializeObject<ChatCompletionResponse>(responseContent);

            if (chatCompletionResponse is null)
            {
                return Result.Failure<string>(Error.TaskFailed($"Problem while deserializing {nameof(response)} to {nameof(ChatCompletionResponse)}"));
            }

            var content = chatCompletionResponse.Choices[0].Message.Content;

            return Result.Success(content);
        }

        private HttpRequestMessage GetChatCompletionRequest(string message)
        {
            var requestMessage = GetRequestCompletionBody(message);

            return new HttpRequestMessage(HttpMethod.Post, requestMessage.ToString());
        }

        private StringContent GetRequestCompletionBody(string message) => new ($$"""
              {
                "model": "{{_chatOptions.ChatModel}}",
                "messages": [
                    {
                        "role": "user",
                        "content": "{{message}}",
                        "temperature": 0.7
                    }
                ]
              }
              """, 
            Encoding.UTF8, 
            "application/json"
            );
    }
}
