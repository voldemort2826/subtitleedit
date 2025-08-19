using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using WeCantSpell.Hunspell;

namespace Nikse.SubtitleEdit.Logic.SpellCheck
{
    public class SpellCheckBase : Hunspell
    {
        private WordList _wordList;

        public SpellCheckBase(string affDictionary, string dicDictionary)
        {
            try
            {
                if (File.Exists(dicDictionary) && File.Exists(affDictionary))
                {
                    // WeCantSpell.Hunspell automatically finds the .aff file when given the .dic file
                    _wordList = WordList.CreateFromFiles(dicDictionary);
                }
                else
                {
                    throw new FileNotFoundException($"Dictionary files not found: {dicDictionary} or {affDictionary}");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to load dictionary: {ex.Message}", ex);
            }
        }

        public override bool Spell(string word)
        {
            return _wordList?.Check(word) ?? false;
        }

        public override List<string> Suggest(string word)
        {
            if (_wordList == null)
            {
                return new List<string>();
            }

            try
            {
                string filtered = Regex.Replace(word, @"\p{Cs}", "");
                var suggestions = _wordList.Suggest(filtered);
                var list = new List<string>(suggestions);
                AddIShouldBeLowercaseLSuggestion(list, filtered);
                return list;
            }
            catch
            {
                return new List<string>();
            }
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // WeCantSpell.Hunspell doesn't require explicit disposal
                // WordList is automatically garbage collected
                _wordList = null;
            }
        }
    }
}
