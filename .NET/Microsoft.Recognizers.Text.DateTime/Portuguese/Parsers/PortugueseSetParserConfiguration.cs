using System;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Portuguese;
using Microsoft.Recognizers.Text.DateTime.Utilities;

namespace Microsoft.Recognizers.Text.DateTime.Portuguese
{
    public class PortugueseSetParserConfiguration : BaseDateTimeOptionsConfiguration, ISetParserConfiguration
    {
        public PortugueseSetParserConfiguration(ICommonDateTimeParserConfiguration config)
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

            EachPrefixRegex = PortugueseSetExtractorConfiguration.EachPrefixRegex;
            PeriodicRegex = PortugueseSetExtractorConfiguration.PeriodicRegex;
            EachUnitRegex = PortugueseSetExtractorConfiguration.EachUnitRegex;
            EachDayRegex = PortugueseSetExtractorConfiguration.EachDayRegex;
            SetWeekDayRegex = PortugueseSetExtractorConfiguration.SetWeekDayRegex;
            SetEachRegex = PortugueseSetExtractorConfiguration.SetEachRegex;
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
            var trimmedText = text.Trim().Normalized(DateTimeDefinitions.SpecialCharactersEquivalent);

            // @TODO move hardcoded values to resources file
            if (trimmedText.EndsWith("diario", StringComparison.Ordinal) || trimmedText.EndsWith("diaria", StringComparison.Ordinal) ||
                trimmedText.EndsWith("diariamente", StringComparison.Ordinal))
            {
                timex = "P1D";
            }
            else if (trimmedText is "semanalmente")
            {
                timex = "P1W";
            }
            else if (trimmedText is "quinzenalmente")
            {
                timex = "P2W";
            }
            else if (trimmedText is "mensalmente")
            {
                timex = "P1M";
            }
            else if (trimmedText is "anualmente")
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
            var trimmedText = text.Trim().Normalized(DateTimeDefinitions.SpecialCharactersEquivalent);

            // @TODO move hardcoded values to resources file
            if (trimmedText is "dia" or "dias")
            {
                timex = "P1D";
            }
            else if (trimmedText is "semana" or "semanas")
            {
                timex = "P1W";
            }
            else if (trimmedText is "mes" or "meses")
            {
                timex = "P1M";
            }
            else if (trimmedText is "ano" or "anos")
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