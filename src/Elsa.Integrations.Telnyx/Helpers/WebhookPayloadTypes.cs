using System.Reflection;
using Elsa.Integrations.Telnyx.Attributes;
using Elsa.Integrations.Telnyx.Payloads.Abstractions;

namespace Elsa.Integrations.Telnyx.Helpers;

/// <summary>
/// A helper class related to webhook event payload discovery. 
/// </summary>
public static class WebhookPayloadTypes
{
    /// <summary>
    /// A list of <see cref="Payload"/> types.
    /// </summary>
    public static readonly ICollection<Type> PayloadTypes;

    /// <summary>
    /// A dictionary of event types mapped to payload types.
    /// </summary>
    public static readonly IDictionary<string, Type> PayloadTypeDictionary;

    static WebhookPayloadTypes()
    {
        PayloadTypes = typeof(WebhookPayloadTypes).Assembly.GetTypes().Where(x => typeof(Payload).IsAssignableFrom(x) && x.GetCustomAttribute<WebhookAttribute>(true) != null).ToList();
        
        var query =
            from payloadType in PayloadTypes
            let payloadAttribute = payloadType.GetCustomAttribute<WebhookAttribute>()
            where payloadAttribute != null
            select (payloadType, payloadAttribute);

        PayloadTypeDictionary = query.ToDictionary(x => x.payloadAttribute!.EventType, x => x.payloadType);
    }
}