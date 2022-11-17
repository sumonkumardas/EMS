using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SinePulse.EMS.Utility.SmsSending.Utility;

namespace SinePulse.EMS.Utility.SmsSending
{
  public class RobiSmsSender : ISmsSender
  {
    public async Task<bool> SendSms(Sms sms)
    {
      if (sms.PhoneNumbers.Count == 0) return false;

      return await SendRobiSms(sms.PhoneNumbers, sms.Text);
    }

    private async Task<bool> SendRobiSms(ICollection<string> phoneNumbers, string message)
    {
      return await SendSmsByTwoRequest(FormatPhoneNumbers(phoneNumbers), message);
    }

    private static string[] FormatPhoneNumbers(ICollection<string> phoneNumbers)
    {
      return phoneNumbers.Select(FormatPhoneNumber).ToArray();
    }

    private static string FormatPhoneNumber(string phoneNumber)
    {
      return "tel:+88" + phoneNumber.Right(11);
    }

    private async Task<bool> SendSmsByTwoRequest(string[] phoneNumbers, string message)
    {
      var tokenResponse = await PostTokenRequest();
      var token = await ParseTokenResponse(tokenResponse);
      if (token == null) throw new Exception("Token not found");

      var sendSmsResponse = await PostSendSmsRequest(phoneNumbers, message, token);
      return ParseSendSmsResponse(sendSmsResponse);
    }

    private async Task<HttpResponseMessage> PostTokenRequest()
    {
      var url = GetTokenRequestUrl();
      var authHeader = GetTokenRequestAuthHeader();
      var body = GetTokenRequestBody();
      return await PostTokenRequest(url, authHeader, body);
    }

    private Uri GetTokenRequestUrl() => new Uri(BaseUrl, "token");

    private string GetTokenRequestAuthHeader()
    {
      var escapedKey = Uri.EscapeDataString(ConsumerKey);
      var escapedSecret = Uri.EscapeDataString(ConsumerSecret);
      var keySecret = $"{escapedKey}:{escapedSecret}";
      var base64KeySecret = ToBase64String(keySecret);
      return $"Basic {base64KeySecret}";
    }

    private static string ToBase64String(string plainText)
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
    }

    private string GetTokenRequestBody()
    {
      return $"grant_type=password&username={UserName}&password={Password}&scope=PRODUCTION";
    }

    private async Task<HttpResponseMessage> PostTokenRequest(Uri url, string authHeader, string body)
    {
      using (var httpClient = new HttpClient())
      {
        HttpContent httpContent = new StringContent(body);
        if (httpContent.Headers.Any(r => r.Key == "Content-Type"))
          httpContent.Headers.Remove("Content-Type");
        httpContent.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
        var httpRequest = new HttpRequestMessage
        {
          RequestUri = url,
          Method = HttpMethod.Post,
          Content = httpContent
        };
        httpRequest.Headers.Add("Authorization", authHeader);
        return await httpClient.SendAsync(httpRequest);
      }
    }

    private async Task<string> ParseTokenResponse(HttpResponseMessage httpResult)
    {
      var response = await httpResult.Content.ReadAsStringAsync();
      return JObject.Parse(response)["access_token"].ToString();
    }

    private async Task<HttpResponseMessage> PostSendSmsRequest(string[] phoneNumbers, string message, string token)
    {
      var url = GetSendSmsRequestUrl();
      var authHeader = GetSendSmsRequestAuthHeader(token);
      var body = GetSendSmsRequestBody(phoneNumbers, message);
      return await PostSendSmsRequest(url, authHeader, body);
    }

    private Uri GetSendSmsRequestUrl() => new Uri(BaseUrl, $"/smsmessaging/v1/outbound/{SenderAddress}/requests");

    private string GetSendSmsRequestAuthHeader(string token)
    {
      return $"Bearer {token}";
    }

    private string GetSendSmsRequestBody(string[] phoneNumbers, string message)
    {
      var outboundSmsTextMessageRequestPost = new
      {
        outboundSMSMessageRequest = new
        {
          address = phoneNumbers,
          senderAddress = SenderAddress,
          outboundSMSTextMessage = new {message = message},
          clientCorrelator = ClientCorrelator,
          senderName = ""
        }
      };
      return JsonConvert.SerializeObject(outboundSmsTextMessageRequestPost);
    }

    private async Task<HttpResponseMessage> PostSendSmsRequest(Uri url, string authHeader, string body)
    {
      using (var httpClient = new HttpClient())
      {
        HttpContent httpContent = new StringContent(body);
        if (httpContent.Headers.Any(r => r.Key == "Content-Type"))
          httpContent.Headers.Remove("Content-Type");
        httpContent.Headers.Add("Content-Type", "application/json");
        var httpRequest = new HttpRequestMessage
        {
          RequestUri = url,
          Method = HttpMethod.Post,
          Content = httpContent
        };
        httpRequest.Headers.Add("Authorization", authHeader);
        return await httpClient.SendAsync(httpRequest);
      }
    }

    private static bool ParseSendSmsResponse(HttpResponseMessage result)
    {
      return result?.StatusCode == HttpStatusCode.Created;
    }

    private string ConsumerSecret => Configuration.RobiMIFEConsumerSecret;
    private string ConsumerKey => Configuration.RobiMIFEConsumerKey;
    private string UserName => Configuration.RobiMIFEUserName;
    private string Password => Configuration.RobiMIFEPassword;
    private Uri BaseUrl => new Uri(Configuration.RobiBaseUrl);
    private string SenderAddress => Configuration.SenderAddress;
    private string ClientCorrelator => Configuration.ClientCorrelator;
  }
}