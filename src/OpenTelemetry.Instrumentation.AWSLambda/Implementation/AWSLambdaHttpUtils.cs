// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics;
using System.Text;
using System.Web;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.ApplicationLoadBalancerEvents;
using OpenTelemetry.AWS;

namespace OpenTelemetry.Instrumentation.AWSLambda.Implementation;

internal class AWSLambdaHttpUtils
{
    // x-forwarded-... headers are described here https://docs.aws.amazon.com/elasticloadbalancing/latest/classic/x-forwarded-headers.html
    private const string HeaderXForwardedProto = "x-forwarded-proto";
    private const string HeaderHost = "host";

    internal static IEnumerable<KeyValuePair<string, object>> GetHttpTags<TInput>(TInput input)
    {
        var tags = new List<KeyValuePair<string, object>>();

        string? httpScheme;
        string? httpTarget;
        string? urlPath;
        string? urlQuery;
        string? httpMethod;
        string? hostName;
        int? hostPort;

        switch (input)
        {
            case APIGatewayProxyRequest request:
                httpScheme = AWSLambdaUtils.GetHeaderValues(request, HeaderXForwardedProto)?.LastOrDefault();
                urlPath = request.RequestContext?.Path ?? request.Path ?? string.Empty;
                urlQuery = GetQueryString(request);
                httpTarget = string.Concat(urlPath, urlQuery);
                httpMethod = request.RequestContext?.HttpMethod ?? request.HttpMethod;
                var hostHeader = AWSLambdaUtils.GetHeaderValues(request, HeaderHost)?.LastOrDefault();
                (hostName, hostPort) = GetHostAndPort(httpScheme, hostHeader);
                break;
            case APIGatewayHttpApiV2ProxyRequest requestV2:
                httpScheme = AWSLambdaUtils.GetHeaderValues(requestV2, HeaderXForwardedProto)?.LastOrDefault();
                urlPath = requestV2.RawPath ?? string.Empty;
                urlQuery = GetQueryString(requestV2);
                httpTarget = string.Concat(urlPath, urlQuery);
                httpMethod = requestV2.RequestContext?.Http?.Method;
                var hostHeaderV2 = AWSLambdaUtils.GetHeaderValues(requestV2, HeaderHost)?.LastOrDefault();
                (hostName, hostPort) = GetHostAndPort(httpScheme, hostHeaderV2);
                break;
            case ApplicationLoadBalancerRequest albRequest:
                httpScheme = AWSLambdaUtils.GetHeaderValues(albRequest, HeaderXForwardedProto)?.LastOrDefault();
                urlPath = albRequest.Path ?? string.Empty;
                urlQuery = GetQueryString(albRequest);
                httpTarget = string.Concat(urlPath, urlQuery);
                httpMethod = albRequest.HttpMethod;
                var albHostHeader = AWSLambdaUtils.GetHeaderValues(albRequest, HeaderHost)?.LastOrDefault();
                (hostName, hostPort) = GetHostAndPort(httpScheme, albHostHeader);
                break;
            default:
                return tags;
        }

        if (httpScheme != null)
        {
            tags.AddAttributeHttpScheme(httpScheme, addIfEmpty: true);
            tags.AddAttributeUrlScheme(httpScheme, addIfEmpty: true);
        }

        if (httpTarget != null)
        {
            tags.AddAttributeHttpTarget(httpTarget, addIfEmpty: true);
        }

        if (urlPath != null)
        {
            tags.AddAttributeUrlPath(urlPath, addIfEmpty: true);
        }

        if (urlQuery != null)
        {
            tags.AddAttributeUrlQuery(urlQuery, addIfEmpty: true);
        }

        if (httpMethod != null)
        {
            tags.AddAttributeHttpMethod(httpMethod, addIfEmpty: true);
        }

        if (hostName != null)
        {
            tags.AddAttributeNetHostName(hostName, addIfEmpty: true);
        }

        tags.AddAttributeNetHostPort(hostPort);

        return tags;
    }

    internal static void SetHttpTagsFromResult(Activity? activity, object? result)
    {
        if (activity == null || result == null)
        {
            return;
        }

        switch (result)
        {
            case APIGatewayProxyResponse response:
                activity.SetTagAttributeHttpStatusCode(response.StatusCode);
                activity.SetTagAttributeHttpResponseStatusCode(response.StatusCode);
                break;
            case APIGatewayHttpApiV2ProxyResponse responseV2:
                activity.SetTagAttributeHttpStatusCode(responseV2.StatusCode);
                activity.SetTagAttributeHttpResponseStatusCode(responseV2.StatusCode);
                break;
            case ApplicationLoadBalancerResponse albResponse:
                activity.SetTagAttributeHttpStatusCode(albResponse.StatusCode);
                activity.SetTagAttributeHttpResponseStatusCode(albResponse.StatusCode);
                break;
            default:
                break;
        }
    }

    internal static string? GetQueryString(APIGatewayProxyRequest request)
    {
        if (request.MultiValueQueryStringParameters == null)
        {
            return string.Empty;
        }

        var queryString = new StringBuilder();
        var separator = '?';
        foreach (var parameterKvp in request.MultiValueQueryStringParameters)
        {
            // Multiple values for the same parameter will be added to query
            // as ampersand separated: name=value1&name=value2
            foreach (var value in parameterKvp.Value)
            {
                queryString.Append(separator)
                    .Append(HttpUtility.UrlEncode(parameterKvp.Key))
                    .Append('=')
                    .Append(HttpUtility.UrlEncode(value));
                separator = '&';
            }
        }

        return queryString.ToString();
    }

    internal static string? GetQueryString(APIGatewayHttpApiV2ProxyRequest request) =>
        string.IsNullOrEmpty(request.RawQueryString) ? string.Empty : "?" + request.RawQueryString;

    internal static string? GetQueryString(ApplicationLoadBalancerRequest request)
    {
        // If the request has a query string value, one of the following properties will be set, depending on if
        // "Multi value headers" is enabled on ELB Target Group.
        if (request.MultiValueQueryStringParameters != null)
        {
            var queryString = new StringBuilder();
            var separator = '?';
            foreach (var parameterKvp in request.MultiValueQueryStringParameters)
            {
                // Multiple values for the same parameter will be added to query
                // as ampersand separated: name=value1&name=value2
                foreach (var value in parameterKvp.Value)
                {
                    queryString.Append(separator)
                        .Append(HttpUtility.UrlEncode(parameterKvp.Key))
                        .Append('=')
                        .Append(HttpUtility.UrlEncode(value));
                    separator = '&';
                }
            }

            return queryString.ToString();
        }

        if (request.QueryStringParameters != null)
        {
            var queryString = new StringBuilder();
            var separator = '?';
            foreach (var parameterKvp in request.QueryStringParameters)
            {
                queryString.Append(separator)
                    .Append(HttpUtility.UrlEncode(parameterKvp.Key))
                    .Append('=')
                    .Append(HttpUtility.UrlEncode(parameterKvp.Value));
                separator = '&';
            }

            return queryString.ToString();
        }

        return string.Empty;
    }

    internal static (string? Host, int? Port) GetHostAndPort(string? httpScheme, string? hostHeader)
    {
        if (hostHeader == null)
        {
            return (null, null);
        }

#pragma warning disable CA1861 // Prefer 'static readonly' fields over constant array arguments if the called method is called repeatedly and is not mutating the passed array
        var hostAndPort = hostHeader.Split([':'], 2);
#pragma warning restore CA1861 // Prefer 'static readonly' fields over constant array arguments if the called method is called repeatedly and is not mutating the passed array
        if (hostAndPort.Length > 1)
        {
            var host = hostAndPort[0];
            return int.TryParse(hostAndPort[1], out var port)
                ? (host, port)
                : (host, null);
        }
        else
        {
            return (hostAndPort[0], GetDefaultPort(httpScheme));
        }
    }

    private static int? GetDefaultPort(string? httpScheme) =>
        httpScheme == "https" ? 443 : httpScheme == "http" ? 80 : null;
}
