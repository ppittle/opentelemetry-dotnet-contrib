// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using OpenTelemetry.SemanticConventions;

namespace OpenTelemetry.Resources.AWS;

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
