using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Quail
{
    /// <summary>
    /// This is just a list of words found in a bunch of text, along with counts of how often those words appear.
    /// (Corpus is Paul Graham's scary word meaning "bunch of text", and "tokens" are the geeky correct way of saying "words").
    /// From: http://www.paulgraham.com/spam.html
    /// </summary>
    public class Corpus
    {
        /// <summary>
        /// Regex pattern for words that don't start with a number
        /// </summary>
        public const string TokenPattern = @"([a-zA-Z]\w+)\W*";

        private SortedDictionary<string, int> _tokens = new SortedDictionary<string, int>();


        /// <summary>
        /// A sorted list of all the words that show up in the text, along with counts of how many times they appear.
        /// </summary>
        public SortedDictionary<string, int> Tokens
        {
            get { return _tokens; }
        }

        /// <summary>
        /// Public constructor.  Fires up a new Corpus object for you to play with.  (included for Serialization)
        /// </summary>
        public Corpus()
        {
        }

        /// <summary>
        /// Fire up a new Corpus and populate it with text from the supplied reader
        /// </summary>
        /// <param name="reader"></param>
        public Corpus(TextReader reader)
        {
            LoadFromReader(reader);
        }

        /// <summary>
        /// Fire up a new Corpus and populate it with the contents of the supplied file
        /// </summary>
        /// <param name="filepath"></param>
        public Corpus(string filepath)
        {
            if (filepath != null) LoadFromFile(filepath);
        }

        /// <summary>
        /// Populate the Corpus with text from a file.
        /// </summary>
        /// <param name="filepath"></param>
        public void LoadFromFile(string filepath)
        {
            if ((filepath != null) && (File.Exists(filepath)))
            {
                StreamReader sr = new StreamReader(filepath);
                LoadFromReader(sr);
                sr.Close();
            }
        }

        /// <summary>
        /// Loads tokens from the specified TextReader into the Corpus.
        /// Doesn't initialize the collection, so it can be called from
        /// a loop if needed.
        /// </summary>
        /// <param name="reader"></param>
        public void LoadFromReader(TextReader reader)
        {
            Regex re = new Regex(TokenPattern, RegexOptions.Compiled);
            string line;
            while (null != (line = reader.ReadLine()))
            {
                Match m = re.Match(line);
                while (m.Success)
                {
                    string token = m.Groups[1].Value;
                    AddToken(token);
                    m = m.NextMatch();
                }
            }
        }

        /// <summary>
        /// Stick a word into the list, incrementing its count if it's already there.
        /// </summary>
        /// <param name="rawPhrase"></param>
        public void AddToken(string rawPhrase)
        {
            if (!_tokens.ContainsKey(rawPhrase))
            {
                _tokens.Add(rawPhrase, 1);
            }
            else
            {
                _tokens[rawPhrase]++;
            }
        }
    }
}
