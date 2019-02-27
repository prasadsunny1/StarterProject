using System;
using System.Collections.Generic;

namespace StarterProject.Interfaces
{
    public interface IInsightService
    {
        void ReportException(Exception exception);
        void ReportException(Exception exception, Dictionary<string, string> payload);

        void TrackAnalyticsEvent(string eventName, IDictionary<string, string> properties);
    }
}