using System;
using System.Collections.Generic;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using StarterProject.Interfaces;

namespace StarterProject.Services
{
    public class InsightService : IInsightService
    {
        public void ReportException(Exception exception)
        {
            Crashes.TrackError(exception);
        }

        public void ReportException(Exception exception, Dictionary<string, string> payload)
        {
            Crashes.TrackError(exception, payload);
        }

        public void TrackAnalyticsEvent(string eventName, IDictionary<string, string> properties =null)
        {
            if (properties != null)
            {
                Analytics.TrackEvent(eventName, properties);
            }
            Analytics.TrackEvent(eventName);

        }
    }
}