using System;
using System.Reflection;
using MethodDecorator.Fody.Interfaces;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using StarterProject;

[module: Insights]

namespace StarterProject
{
    [AttributeUsage(
        AttributeTargets.Method
        | AttributeTargets.Constructor
        | AttributeTargets.Assembly
        | AttributeTargets.Module)]
    public class InsightsAttribute : Attribute, IMethodDecorator
    {
        private string _methodName;

        public void Init(object instance, MethodBase method, object[] args)
        {
            if (method.DeclaringType != null) _methodName = method.DeclaringType.FullName + "." + method.Name;
        }

        public void OnEntry()
        {
            var message = $"OnEntry: {_methodName}";
            Analytics.TrackEvent(message);
        }

        public void OnExit()
        {
            var message = $"OnExit: {_methodName}";
            Analytics.TrackEvent(message);
        }

        public void OnException(Exception exception)
        {
            Crashes.TrackError(exception);
        }
    }
}