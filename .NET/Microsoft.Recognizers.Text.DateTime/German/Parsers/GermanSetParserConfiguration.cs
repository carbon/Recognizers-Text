using System;
using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Microsoft.Recognizers.Text.DateTime.Utilities;

namespace Microsoft.Recognizers.Text.DateTime.German
{
    public class GermanSetParserConfiguration : BaseDateTimeOptionsConfiguration, ISetParserConfiguration
    {
        public GermanSetParserConfiguration(ICommonDateTimeParserConfiguration config)
            : base(config)
        {
            DurationExtractor = config.DurationExtractor;
            TimeExtractor = config.TimeExtractor;
            DateExtractor = config.DateExtractor;
            DateTimeExtractor = config.DateTimeExtractor;
            DatePeriodExtractor = config.DatePeriodExtractor;
            TimePeriodExtractor = config.TimePeriodExtractor;
            DateTimePeriodExtractor = config.DateTimePeriodExtractor;

            DurationParser = config.DurationParser;
            TimeParser = config.TimeParser;
            DateParser = config.DateParser;
            DateTimeParser = config.DateTimeParser;
            DatePeriodParser = config.DatePeriodParser;
            TimePeriodParser = config.TimePeriodParser;
            DateTimePeriodParser = config.DateTimePeriodParser;
            UnitMap = config.UnitMap;

            EachPrefixRegex = GermanSetExtractorConfiguration.EachPrefixRegex;
            PeriodicRegex = GermanSetExtractorConfiguration.PeriodicRegex;
            EachUnitRegex = GermanSetExtractorConfiguration.EachUnitRegex;
            EachDayRegex = GermanSetExtractorConfiguration.EachDayRegex;
            SetWeekDayRegex = GermanSetExtractorConfiguration.SetWeekDayRegex;
            SetEachRegex = GermanSetExtractorConfiguration.SetEachRegex;
        }

        public IDateTimeExtractor DurationExtractor { get; }

        public IDateTimeParser DurationParser { get; }

        public IDateTimeExtractor TimeExtractor { get; }

        public IDateTimeParser TimeParser { get; }

        public IDateExtractor DateExtractor { get; }

        public IDateTimeParser DateParser { get; }

        public IDateTimeExtractor DateTimeExtractor { get; }

        public IDateTimeParser DateTimeParser { get; }

        public IDateTimeExtractor DatePeriodExtractor { get; }

        public IDateTimeParser DatePeriodParser { get; }

        public IDateTimeExtractor TimePeriodExtractor { get; }

        public IDateTimeParser TimePeriodParser { get; }

        public IDateTimeExtractor DateTimePeriodExtractor { get; }

        public IDateTimeParser DateTimePeriodParser { get; }

        public IImmutableDictionary<string, string> UnitMap { get; }

        public Regex EachPrefixRegex { get; }

        public Regex PeriodicRegex { get; }

        public Regex EachUnitRegex { get; }

        public Regex EachDayRegex { get; }

        public Regex SetWeekDayRegex { get; }

        public Regex SetEachRegex { get; }

        public bool GetMatchedDailyTimex(string text, out string timex)
        {
            var trimmedText = text.Trim();

            // @TODO move hardcoded values to resources file

            if (trimmedText is "täglich" ||
                trimmedText is "täglicher" ||
                trimmedText is "tägliches" ||
                trimmedText is "tägliche" ||
                trimmedText is "täglichen" ||
                trimmedText is "alltäglich" ||
                trimmedText is "alltäglicher" ||
                trimmedText is "alltägliches" ||
                trimmedText is "alltägliche" ||
                trimmedText is "alltäglichen" ||
                trimmedText is "jeden tag")
            {
                timex = "P1D";
            }
            else if (trimmedText is "wöchentlich" ||
                     trimmedText is "wöchentlicher" ||
                     trimmedText is "wöchentliches" ||
                     trimmedText is "wöchentliche" ||
                     trimmedText is "wöchentlichen" ||
                     trimmedText is "allwöchentlich" ||
                     trimmedText is "allwöchentlicher" ||
                     trimmedText is "allwöchentliches" ||
                     trimmedText is "allwöchentliche" ||
                     trimmedText is "allwöchentlichen")
            {
                timex = "P1W";
            }
            else if (trimmedText is "monatlich" ||
                     trimmedText is "monatlicher" ||
                     trimmedText is "monatliches" ||
                     trimmedText is "monatliche" ||
                     trimmedText is "monatlichen" ||
                     trimmedText is "allmonatlich" ||
                     trimmedText is "allmonatlicher" ||
                     trimmedText is "allmonatliches" ||
                     trimmedText is "allmonatliche" ||
                     trimmedText is "allmonatlichen")
            {
                timex = "P1M";
            }
            else if (trimmedText is "jährlich" ||
                     trimmedText is "jährlicher" ||
                     trimmedText is "jährliches" ||
                     trimmedText is "jährliche" ||
                     trimmedText is "jährlichen" ||
                     trimmedText is "alljährlich" ||
                     trimmedText is "alljährlicher" ||
                     trimmedText is "alljährliches" ||
                     trimmedText is "alljährliche" ||
                     trimmedText is "alljährlichen")
            {
                timex = "P1Y";
            }
            else
            {
                timex = null;
                return false;
            }

            return true;
        }

        public bool GetMatchedUnitTimex(string text, out string timex)
        {
            var trimmedText = text.Trim();

            // @TODO move hardcoded values to resources file

            if (trimmedText is "tag")
            {
                timex = "P1D";
            }
            else if (trimmedText is "woche")
            {
                timex = "P1W";
            }
            else if (trimmedText is "monat")
            {
                timex = "P1M";
            }
            else if (trimmedText is "jahr")
            {
                timex = "P1Y";
            }
            else
            {
                timex = null;
                return false;
            }

            return true;
        }

        public string WeekDayGroupMatchString(Match match) => SetHandler.WeekDayGroupMatchString(match);
    }
}