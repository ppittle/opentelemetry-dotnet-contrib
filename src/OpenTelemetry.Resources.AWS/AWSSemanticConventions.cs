// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using OpenTelemetry.SemanticConventions;

namespace OpenTelemetry.Resources.AWS;

[Obsolete("", error: true)]
internal static class AWSSemanticConventions
{
    public const string AttributeCloudAccountID = CloudAttributes.AttributeCloudAccountId;
    public const string AttributeCloudAvailabilityZone = CloudAttributes.AttributeCloudAvailabilityZone;
    public const string AttributeCloudPlatform = CloudAttributes.AttributeCloudPlatform;
    public const string AttributeCloudProvider = CloudAttributes.AttributeCloudProvider;
    public const string AttributeCloudRegion = CloudAttributes.AttributeCloudRegion;
    public const string AttributeCloudResourceId = CloudAttributes.AttributeCloudResourceId;

    public const string AttributeContainerID = ContainerAttributes.AttributeContainerId;

    public const string AttributeEcsContainerArn = AwsAttributes.AttributeAwsEcsContainerArn;
    public const string AttributeEcsClusterArn = AwsAttributes.AttributeAwsEcsClusterArn;
    public const string AttributeEcsLaunchtype = AwsAttributes.AttributeAwsEcsLaunchtype;
    public const string ValueEcsLaunchTypeEc2 = AwsAttributes.AwsEcsLaunchtypeValues.Ec2;
    public const string ValueEcsLaunchTypeFargate = AwsAttributes.AwsEcsLaunchtypeValues.Fargate;
    public const string AttributeEcsTaskArn = AwsAttributes.AttributeAwsEcsTaskArn;
    public const string AttributeEcsTaskFamily = AwsAttributes.AttributeAwsEcsTaskFamily;
    public const string AttributeEcsTaskRevision = AwsAttributes.AttributeAwsEcsTaskRevision;

    public const string AttributeHostID = HostAttributes.AttributeHostId;
    public const string AttributeHostType = HostAttributes.AttributeHostType;
    public const string AttributeHostName = HostAttributes.AttributeHostName;

    public const string AttributeK8SClusterName = K8sAttributes.AttributeK8sClusterName;

    public const string AttributeLogGroupNames = AwsAttributes.AttributeAwsLogGroupNames;
    public const string AttributeLogGroupArns = AwsAttributes.AttributeAwsLogGroupArns;
    public const string AttributeLogStreamNames = AwsAttributes.AttributeAwsLogStreamArns;
    public const string AttributeLogStreamArns = AwsAttributes.AttributeAwsLogStreamNames;

    public const string AttributeServiceName = ServiceAttributes.AttributeServiceName;
    public const string AttributeServiceNamespace = ServiceAttributes.AttributeServiceNamespace;
    public const string AttributeServiceInstanceID = ServiceAttributes.AttributeServiceInstanceId;
    public const string AttributeServiceVersion = ServiceAttributes.AttributeServiceVersion;
}

internal static class AWSSemanticConventionsFluent
{
    #region Cloud Attributes
    public static T AddAttributeCloudAccountID<T>(this T attributes, string value)
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeCloudAccountID, value));

        return attributes;
    }
    public static T AddAttributeCloudAvailabilityZone<T>(this T attributes, string value)
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeCloudAvailabilityZone, value));

        return attributes;
    }
    public static T AddAttributeCloudPlatform<T>(this T attributes, string value)
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeCloudProvider, value));

        return attributes;
    }

    public static T AddAttributeCloudProvider<T>(this T attributes, string value)
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeCloudProvider, value));

        return attributes;
    }

    public static T AddAttributeCloudRegion<T>(this T attributes, string value)
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeCloudRegion, value));

        return attributes;
    }

    public static T AddAttributeCloudResourceId<T>(this T attributes, string value)
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeCloudResourceId, value));

        return attributes;
    }
    #endregion

    #region Container
    public static T AddAttributeContainerId<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeContainerID, value));

        return attributes;
    }
    #endregion

    #region AWS
    public static T AddAttributeEcsContainerArn<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeEcsContainerArn, value));

        return attributes;
    }
    public static T AddAttributeEcsClusterArn<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeEcsClusterArn, value));

        return attributes;
    }
    public static T AddAttributeEcsLaunchtype<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeEcsLaunchtype, value));

        return attributes;
    }
    public static T AddValueEcsLaunchTypeEc2<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.ValueEcsLaunchTypeEc2, value)); //TODO --- value

        return attributes;
    }
    public static T AddValueEcsLaunchTypeFargate<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.ValueEcsLaunchTypeFargate, value)); // TODO --- value

        return attributes;
    }
    public static T AddAttributeEcsTaskArn<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeEcsTaskArn, value));

        return attributes;
    }
    public static T AddAttributeEcsTaskFamily<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeEcsTaskFamily, value));

        return attributes;
    }
    public static T AddAttributeEcsTaskRevision<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeEcsTaskRevision, value));

        return attributes;
    }
    public static T AddAttributeLogGroupNames<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeLogGroupNames, value));

        return attributes;
    }
    public static T AddAttributeLogGroupArns<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>

    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeLogGroupArns, value));

        return attributes;
    }
    public static T AddAttributeLogStreamNames<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeLogStreamNames, value));

        return attributes;
    }
    public static T AddAttributeLogStreamArns<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeLogStreamArns, value));

        return attributes;
    }
    #endregion

    #region Host
    public static T AddAttributeHostID<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeHostID, value));

        return attributes;
    }
    public static T AddAttributeHostType<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeHostType, value));

        return attributes;
    }
    public static T AddAttributeHostName<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeHostName, value));

        return attributes;
    }
    #endregion

    #region K8s
    public static T AddAttributeK8SClusterName<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeK8SClusterName, value));

        return attributes;
    }
    #endregion

    #region Service    
    public static T AddAttributeServiceName<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeServiceName, value));

        return attributes;
    }
    public static T AddAttributeServiceNamespace<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeServiceNamespace, value));

        return attributes;
    }
    public static T AddAttributeServiceInstanceID<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeServiceInstanceID, value));

        return attributes;
    }
    public static T AddAttributeServiceVersion<T>(this T attributes, string value)
        where T : IList<KeyValuePair<string, object>>
    {
        // V1
        attributes.Add(new(AWSSemanticConventions.AttributeServiceVersion, value));

        return attributes;
    }
    #endregion
}
