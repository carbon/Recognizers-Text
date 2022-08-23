using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Recognizers.Text.Utilities
{
    public static class QueryProcessor
    {
        // Must be in sync with Base-Numbers YAML due to inter-dependency issue with different .NET targets
        private const string CaseSensitiveTerms = @"(?<=(\s|\d))(kB|K[Bb]?|M[BbM]?|G[Bb]?|B)\b";

        private static readonly Regex SpecialTokensRegex = new Regex(CaseSensitiveTerms, RegexOptions.Compiled);

        public static string Preprocess(string query, bool caseSensitive = false, bool recode = true)
        {
            if (recode)
            {
                Span<char> newQuery = query.Length <= 128
                    ? stackalloc char[128].Slice(0, query.Length)
                    : new char[query.Length];

                int position = 0;

                foreach (var c in query)
                {
                    newQuery[position++] = c switch
                    {
                        '０' => '0',
                        '１' => '1',
                        '２' => '2',
                        '３' => '3',
                        '４' => '4',
                        '５' => '5',
                        '６' => '6',
                        '７' => '7',
                        '８' => '8',
                        '９' => '9',
                        '：' => ':',
                        '－' => '-',
                        '−' => '-',
                        '，' => ',',
                        '／' => '/',
                        'Ｇ' => 'G',
                        'Ｍ' => 'M',
                        'Ｔ' => 'T',
                        'Ｋ' => 'K',
                        'ｋ' => 'k',
                        '．' => '.',
                        '（' => '(',
                        '）' => ')',
                        '％' => '%',
                        '、' => ',',
                        _ => c
                    };
                }

                query = newQuery.ToString();
            }

            query = caseSensitive ?
                ToLowerTermSensitive(query) :
                query.ToLowerInvariant();

            return query;
        }

        public static string ToLowerTermSensitive(string input)
        {
            var lowercase = input.ToLowerInvariant();
            var buffer = new StringBuilder(lowercase);

            var replaced = false;

            var matches = SpecialTokensRegex.Matches(input);
            foreach (Match m in matches)
            {
                ReApplyValue(m.Index, ref buffer, m.Value);
                replaced = true;
            }

            return replaced ? buffer.ToString() : lowercase;
        }

        public static string RemoveDiacritics(string query)
        {
            if (query == null)
            {
                return null;
            }

            // FormD indicates that a Unicode string is normalized using full canonical decomposition.
            var chars =
                from c in query.Normalize(NormalizationForm.FormD).ToCharArray()
                let uc = CharUnicodeInfo.GetUnicodeCategory(c)
                where uc != UnicodeCategory.NonSpacingMark
                select c;

            // FormC indicates that a Unicode string is normalized using full canonical decomposition,
            // followed by the replacement of sequences with their primary composites, if possible.
            var normalizedQuery = new string(chars.ToArray()).Normalize(NormalizationForm.FormC);

            return normalizedQuery;
        }

        private static void ReApplyValue(int idx, ref StringBuilder outString, string value)
        {
            for (int i = 0; i < value.Length; ++i)
            {
                outString[idx + i] = value[i];
            }
        }
    }
}
