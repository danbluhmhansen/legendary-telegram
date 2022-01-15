namespace BlazorApp1.Client.Data;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http.Headers;

using BlazorApp1.Client.Configuration;
using BlazorApp1.Shared.Models.v1;

using Microsoft.Extensions.Options;
using Microsoft.OData;
using Microsoft.OData.Client;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

public class ODataServiceContext : DataServiceContext
{
	public ODataServiceContext(
		HttpClient httpClient,
		IOptions<ServerOptions> serverOptions,
		ILogger<ODataServiceContext> logger) : base(serverOptions.Value.Route)
	{
		ODataConventionModelBuilder oDataBuilder = new();
		oDataBuilder.EntitySet<Character>("Characters");
		oDataBuilder.EntitySet<Feature>("Features");
		oDataBuilder.EntitySet<CoreEffect>("CoreEffects");
		oDataBuilder.EntitySet<Effect>("Effects");
		EntitySetConfiguration<CharacterFeature> characterFeaturesConfiguration = oDataBuilder
			.EntitySet<CharacterFeature>("CharacterFeatures");
		characterFeaturesConfiguration.EntityType
			.HasKey((CharacterFeature characterFeature) =>
				new { characterFeature.CharacterId, characterFeature.FeatureId });
		IEdmModel edmModel = oDataBuilder.GetEdmModel();

		this.HttpRequestTransportMode = HttpRequestTransportMode.HttpClient;
		this.Format.UseJson(edmModel);

		this.Configurations.RequestPipeline.OnMessageCreating = (DataServiceClientRequestMessageArgs args) =>
			new HttpClientRequestMessage(httpClient, args, this.Configurations);

		this.Characters = base.CreateQuery<Character>("v1/Characters");
		this.Features = base.CreateQuery<Feature>("v1/Features");
		this.CoreEffects = base.CreateQuery<CoreEffect>("v1/CoreEffects");
		this.Effects = base.CreateQuery<Effect>("v1/Effects");
		this.CharacterFeatures = base.CreateQuery<CharacterFeature>("v1/CharacterFeatures");
		this.logger = logger;
	}

	public DataServiceQuery<Character> Characters { get; }
	public DataServiceQuery<Feature> Features { get; }
	public DataServiceQuery<CoreEffect> CoreEffects { get; }
	public DataServiceQuery<Effect> Effects { get; }

	public DataServiceQuery<CharacterFeature> CharacterFeatures { get; }

	private readonly ILogger<ODataServiceContext> logger;
}

/// <summary>
/// HttpClient based implementation of DataServiceClientRequestMessage.
/// </summary>
internal class HttpClientRequestMessage : DataServiceClientRequestMessage, IDisposable
{
	/// <summary>
	/// HttpClient distinguishes "Content" headers from "Request" headers, so we
	/// need to know which category a header belongs to.
	/// </summary>
	private static readonly IEnumerable<string> contentHeaderNames = new[]
	{
		"Allow",
		"Content-Disposition",
		"Content-Encoding",
		"Content-Language",
		"Content-Length",
		"Content-Location",
		"Content-MD5",
		"Content-Range",
		"Content-Type",
		"Expires",
		"Last-Modified",
	};

	private readonly HttpRequestMessage requestMessage;
	private readonly HttpClient client;
	private readonly MemoryStream messageStream;
	private readonly Dictionary<string, string> contentHeaderValueCache;
	private readonly DataServiceClientConfigurations config;

	/// <summary>
	/// Constructor for HttpClientRequestMessage.
	/// </summary>
	/// <param name="client">The client</param>
	/// <param name="args">The args</param>
	/// <param name="config">The config</param>
	public HttpClientRequestMessage(HttpClient client, DataServiceClientRequestMessageArgs args, DataServiceClientConfigurations config)
		: base(args.ActualMethod)
	{
		this.requestMessage = new HttpRequestMessage();

		// TODO, avoid create Memory Stream each time, consider object pooling
		this.messageStream = new MemoryStream();

		this.contentHeaderValueCache = new Dictionary<string, string>();

		foreach (var header in args.Headers)
		{
			this.SetHeader(header.Key, header.Value);
		}

		this.Url = args.RequestUri;
		this.Method = args.Method;
		this.config = config;

		// link http and odata properties to share state between odata and http handlers
		/*
			* TODO: UNCOMMENT this after properties is supported
		if (config.Properties != null)
		{
			foreach (var item in config.Properties)
			{
				this.requestMessage.Properties[item.Key] = item.Value;
			}
		}
		*/

		this.client = client;
	}

	/// <summary>
	/// Returns the collection of request headers.
	/// </summary>
	public override IEnumerable<KeyValuePair<string, string>> Headers
	{
		get
		{
			if (this.requestMessage.Content != null)
			{
				return this.requestMessage.Headers.ToStringDictionary().Concat(this.requestMessage.Content.Headers.ToStringDictionary());
			}

			return this.requestMessage.Headers.ToStringDictionary().Concat(this.contentHeaderValueCache);
		}
	}

	/// <summary>
	/// Gets or sets the request url.
	/// </summary>
	public override Uri Url
	{
		get
		{
			return requestMessage.RequestUri;
		}

		set
		{
			requestMessage.RequestUri = value;
		}
	}

	/// <summary>
	/// Gets or sets the method for this request.
	/// </summary>
	public override string Method
	{
		get
		{
			return this.requestMessage.Method.ToString();
		}

		set
		{
			this.requestMessage.Method = new HttpMethod(value);
		}
	}

	/// <summary>
	///  Gets or set the credentials for this request.
	/// </summary>
	public override ICredentials Credentials
	{
		get
		{
			return this.requestMessage.Properties[typeof(ICredentials).FullName] as ICredentials;
		}

		set
		{
			this.requestMessage.Properties[typeof(ICredentials).FullName] = value;
		}
	}

	/// <summary>
	/// Gets or sets the timeout (in seconds) for this request.
	/// </summary>
	public override int Timeout
	{
		get
		{
			return (int)this.client.Timeout.TotalSeconds;
		}
		set
		{
			this.client.Timeout = new TimeSpan(0, 0, value);
		}
	}

	public override int ReadWriteTimeout
	{
		get
		{
			return (int)this.client.Timeout.TotalSeconds;
		}
		set
		{
			this.client.Timeout = new TimeSpan(0, 0, value);
		}
	}

#if !(NETCOREAPP1_0 || NETCOREAPP2_0)
	/// <summary>
	/// Gets or sets a value that indicates whether to send data in segments to the Internet resource.
	/// </summary>
	public override bool SendChunked
	{
		get
		{
			bool? transferEncodingChunked = this.requestMessage.Headers.TransferEncodingChunked;
			return transferEncodingChunked.HasValue && transferEncodingChunked.Value;
		}
		set
		{
			this.requestMessage.Headers.TransferEncodingChunked = value;
		}
	}
#endif

	/// <summary>
	/// Returns the value of the header with the given name.
	/// </summary>
	/// <param name="headerName">Name of the header.</param>
	/// <returns>Returns the value of the header with the given name.</returns>
	public override string GetHeader(string headerName)
	{
		if (contentHeaderNames.Contains(headerName))
		{
			// If this is a "Content" header then we retrieve the value either
			// from the message content (if available) or the cache (otherwise)
			if (this.requestMessage.Content != null)
			{
				return string.Join(",", this.requestMessage.Content.Headers.GetValues(headerName));
			}

			return this.contentHeaderValueCache.TryGetValue(headerName, out string headerValue) ? headerValue : string.Empty;
		}

		return this.requestMessage.Headers.Contains(headerName) ? string.Join(",", this.requestMessage.Headers.GetValues(headerName)) : string.Empty;
	}

	/// <summary>
	/// Sets the value of the header with the given name.
	/// </summary>
	/// <param name="headerName">Name of the header.</param>
	/// <param name="headerValue">Value of the header.</param>
	public override void SetHeader(string headerName, string headerValue)
	{
		if (contentHeaderNames.Contains(headerName))
		{
			// If this is a "Content" header then we cache the value (due
			// to the message content not being set yet) and set it
			// upon sending the message (when the content will be available)
			this.contentHeaderValueCache[headerName] = headerValue;
			return;
		}

		this.requestMessage.Headers.Remove(headerName);
		this.requestMessage.Headers.Add(headerName, headerValue);
	}

	/// <summary>
	/// Gets the stream to be used to write the request payload.
	/// </summary>
	/// <returns>Stream to which the request payload needs to be written.</returns>
	public override Stream GetStream()
	{
		return this.messageStream;
	}

	/// <summary>
	/// Abort the current request.
	/// </summary>
	public override void Abort()
	{
		this.client.CancelPendingRequests();
	}

	/// <summary>
	/// Begins an asynchronous request for a System.IO.Stream object to use to write data.
	/// </summary>
	/// <param name="callback">The System.AsyncCallback delegate.</param>
	/// <param name="state">The state object for this request.</param>
	/// <returns>An System.IAsyncResult that references the asynchronous request.</returns>
	public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
	{
		var taskCompletionSource = new TaskCompletionSource<Stream>();
		taskCompletionSource.TrySetResult(this.messageStream);
		return taskCompletionSource.Task.ToApm(callback, state);
	}

	/// <summary>
	/// Ends an asynchronous request for a System.IO.Stream object to use to write data.
	/// </summary>
	/// <param name="asyncResult">The pending request for a stream.</param>
	/// <returns>A System.IO.Stream to use to write request data.</returns>
	public override Stream EndGetRequestStream(IAsyncResult asyncResult)
	{
		return ((Task<Stream>)asyncResult).Result;
	}

	/// <summary>
	/// Begins an asynchronous request to an Internet resource.
	/// </summary>
	/// <param name="callback">The System.AsyncCallback delegate.</param>
	/// <param name="state">The state object for this request</param>
	/// <returns>An System.IAsyncResult that references the asynchronous request for a response.</returns>
	public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
	{
		var send = CreateSendTask();
		return send.ToApm(callback, state);
	}

	/// <summary>
	/// Ends an asynchronous request to an Internet resource.
	/// </summary>
	/// <param name="asyncResult">The pending request for a response.</param>
	/// <returns> A System.Net.WebResponse that contains the response from the Internet resource.</returns>
	public override IODataResponseMessage EndGetResponse(IAsyncResult asyncResult)
	{
		return UnwrapAggregateException(() => new HttpClientResponseMessage(((Task<HttpResponseMessage>)asyncResult).Result, this.config));
	}

#if !(NETCOREAPP1_0 || NETCOREAPP2_0)
	public override IODataResponseMessage GetResponse()
	{
		return UnwrapAggregateException(() =>
			{
				var send = CreateSendTask();
				send.Wait();
				return new HttpClientResponseMessage(send.Result, this.config);
			});
	}
#endif

	/// <summary>
	/// Dispose the object.
	/// </summary>
	public void Dispose()
	{
	}

	private Task<HttpResponseMessage> CreateSendTask()
	{
		// Only set the message content if the stream has been written to, otherwise
		// HttpClient will complain if it's a GET request.
		var messageContent = this.messageStream.ToArray();
		if (messageContent.Length > 0)
		{
			this.requestMessage.Content = new ByteArrayContent(messageContent);

			// Apply cached "Content" header values
			foreach (var contentHeader in this.contentHeaderValueCache)
			{
				this.requestMessage.Content.Headers.Add(contentHeader.Key, contentHeader.Value);
			}
		}

		this.requestMessage.Method = new HttpMethod(this.ActualMethod);

		var send = this.client.SendAsync(this.requestMessage);
		return send;
	}

	private static T UnwrapAggregateException<T>(Func<T> action)
	{
		try
		{
			return action();
		}
		catch (AggregateException aggregateException)
		{
			if (aggregateException.InnerExceptions.Count == 1 && aggregateException.InnerExceptions.Single() is WebException webException)
			{
				throw ConvertToDataServiceWebException(webException);
			}

			throw;
		}
	}

	private static DataServiceTransportException ConvertToDataServiceWebException(WebException webException)
	{
		HttpWebResponseMessage errorResponseMessage = null;
		if (webException.Response != null)
		{
			var httpResponse = (HttpWebResponse)webException.Response;
			errorResponseMessage = new HttpWebResponseMessage(httpResponse);
		}

		return new DataServiceTransportException(errorResponseMessage, webException);
	}
}

internal class HttpClientResponseMessage : HttpWebResponseMessage, IODataResponseMessage
{
	public HttpClientResponseMessage(HttpResponseMessage httpResponse, DataServiceClientConfigurations config)
		: base(httpResponse.ToStringDictionary(),
				(int)httpResponse.StatusCode,
				() => { var task = httpResponse.Content.ReadAsStreamAsync(); task.Wait(); return task.Result; })
	{
	}
}

internal static class HttpHeadersExtensions
{
	public static IDictionary<string, string> ToStringDictionary(this HttpHeaders headers)
	{
		return headers.ToDictionary((h1) => h1.Key, (h2) => string.Join(",", h2.Value));
	}

	public static IDictionary<string, string> ToStringDictionary(this HttpResponseMessage message)
	{
		if (message.Content != null)
		{
			var dic = message.Headers.ToStringDictionary();
			foreach (var item in message.Content.Headers.ToStringDictionary())
			{
				dic[item.Key] = item.Value;
			}

			return dic;
		}

		return message.Headers.ToStringDictionary();
	}
}
