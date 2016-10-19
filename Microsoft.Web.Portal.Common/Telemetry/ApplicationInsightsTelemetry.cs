using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Microsoft.Web.Portal.Common.Telemetry
{
    public class ApplicationInsightsTelemetry : ITelemetry
    {
        /// <summary>
        /// Azure Application insight telemetry client
        /// </summary>
        private TelemetryClient _telemetryClient = null;

        /// <summary>
        /// constructor to initialize the telemetry
        /// </summary>
        public ApplicationInsightsTelemetry()
        {
            _telemetryClient =new TelemetryClient();
        }

        /// <summary>
        /// Send an EventTelemetry for display in Diagnostic Search and aggregation in Metrics Explorer.
        /// </summary>
        /// <param name="eventName">A name for the event.</param>
        /// <param name="properties">Named string values you can use to search and classify events.</param>
        /// <param name="metrics">Measurements associated with this event.</param>
        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            Contract.Requires(!string.IsNullOrEmpty(eventName));
            _telemetryClient.TrackEvent(eventName, properties, metrics);
        }

        /// <summary>
        /// Send a MetricTelemetry for aggregation in Metric Explorer.
        /// </summary>
        /// <param name="name">Metric name.</param>
        /// <param name="value">Metric value.</param>
        /// <param name="properties">Named string values you can use to classify and filter metrics.</param>
        public void TrackMetric(string name, double value, IDictionary<string, string> properties = null)
        {
            Contract.Requires(!string.IsNullOrEmpty(name));
            _telemetryClient.TrackMetric(name, value, properties);
        }

        /// <summary>
        /// Send a trace message for display in Diagnostic Search.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="properties">Named string values you can use to search and classify events.</param>
        public void TrackTrace(string message, IDictionary<string, string> properties)
        {
            Contract.Requires(!string.IsNullOrEmpty(message));
            Contract.Requires(properties != null);
            _telemetryClient.TrackTrace(message, properties);
        }


        /// <summary>
        /// Send a trace message for display in Diagnostic Search.
        /// </summary>
        /// <param name="message">Message to display.</param>
        public void TrackTrace(string message)
        {
            Contract.Requires(!string.IsNullOrEmpty(message));
            _telemetryClient.TrackTrace(message);
        }

        /// <summary>
        /// Send an Microsoft.ApplicationInsights.DataContracts.ExceptionTelemetry for display in Diagnostic Search.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="properties">Named string values you can use to classify and search for this exception.</param>
        /// <param name="metrics">Additional values associated with this exception.</param>
        public void TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            _telemetryClient.TrackException(exception, properties, metrics);
        }

        /// <summary>
        /// Send information about the page viewed in the application.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        public void TrackPageView(string pageName)
        {
            Contract.Requires(!string.IsNullOrEmpty(pageName));
            _telemetryClient.TrackPageView(pageName);
        }
    }
}
