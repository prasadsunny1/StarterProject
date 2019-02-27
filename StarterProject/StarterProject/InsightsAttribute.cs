using System;
using System.Diagnostics;
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
        private Stopwatch _stopwatch;

        public void Init(object instance, MethodBase method, object[] args)
        {
            if (method.DeclaringType == null) return;
            _methodName = method.DeclaringType.FullName + "." + method.Name;
            _stopwatch = Stopwatch.StartNew();
        }

        public void OnEntry()
        {
            var message = $"OnEntry: {_methodName}";
            Analytics.TrackEvent(message);
        }

        public void OnExit()
        {
            _stopwatch.Stop();
            var message = $"OnExit: {_methodName} {_stopwatch.ElapsedMilliseconds} ms";
            Analytics.TrackEvent(message);
            _stopwatch.Reset();
        }

        public void OnException(Exception exception)
        {
            Crashes.TrackError(exception);
        }
    }
}