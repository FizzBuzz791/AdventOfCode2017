using System;
using System.Collections.Generic;
using System.Linq;

namespace Day2
{
    class Program
    {
        private static string[] boxIds = new string[] 
        {
            "jplenqtlagxhivmwmscfukzodp","jbrehqtlagxhivmeyscfuvzodp","jbreaqtlagxzivmwysofukzodp","jxrgnqtlagxhivmwyscfukwodp","jbrenqtwagjhivmwysxfukzodp","jbrenqplagxhivmwyscfuazoip","jbrenqtlagxhivzwyscfldzodp","jbrefqtlagxhizmwyfcfukzodp","jbrenqtlagxhevmwfsafukzodp","jbrunqtlagxrivmsyscfukzodp","jbrenqtlaguhivmwyhlfukzodp","jbrcnstsagxhivmwyscfukzodp","jbrenqtlagozivmwyscbukzodp","jbrenqwlagxhivswysrfukzodp","jbrenstlagxhuvmiyscfukzodp","jbranqtlhgxhivmwysnfukzodp","jbrenqtvagxhinmxyscfukzodp","jbrenqtlagdhivmwyscfukxody","jbrenqtlagelavmwyscfukzodp","jbrenqtlagxhtvmwyhcfukzbdp","jbrenqwlagxhivmwyscfutzopp","jbrenqtlavxhibmtyscfukzodp","jbronqtlagxnivmwyscfuzzodp","jbredqtlagxhppmwyscfukzodp","jbrcnqtlogxhivmwysxfukzodp","jbremqtlagehivswyscfukzodp","jbrenqolagxhivmcyscfukzokp","jbrehqtlacxhgvmwyscfukzodp","fbrlnqtlagxhivmwyscbukzodp","zbrfnqtlagxhivrwyscfukzodp","jbregqtlagxnivmwyscfhkzodp","jbrenqtllgxnivmwystfukzodp","jurenqtlagxhivmwyscfulzoup","jbrenitdagxhivmwyxcfukzodp","jbrenqtlagxqivmwyscvuszodp","jbqenqwlagxhivmwyscfckzodp","jbrenqtlagxhivmwxscqupzodp","jbrenqtlagxhivmwysciuqiodp","jbrjnjtlagxhivmwyscfukzode","jbrenqtlagxhuvmwqscfukzods","jbrenqtlagxhuvmuyscfukzudp","ibrenqtlagxhivmwyscfuktokp","jbsenqtlagxhivcwyscfuksodp","jbrfnntlagxhivmwnscfukzodp","jzrenqulagxhivmwyscfukzodx","jbrenqtqygxhivmwyscfukzwdp","jbrenqtlagxfixmwyscfukzcdp","jbrenqaoagxhivmwyshfukzodp","jbrenqtlazxhivmworcfukzodp","jbrenqtlagxhicowyscfukrodp","jbrqnqtlagxhivzwyzcfukzodp","jbrenqtlalxhuvxwyscfukzodp","jbrenqtlqgxhhviwyscfukzodp","jbrenqtuggxhivmoyscfukzodp","jbrenqtlagxpivmwyscfukkodw","zbrenqhlagxhivmwyscdukzodp","jbrenutlagxxivmwyscfukzoqp","obrenqtlagxhivmwxscfuszodp","jbrenqtlagxhlvmwyscfuixodp","rbrenqtlagdhixmwyscfukzodp","jbrenqtlagxhivmwescfyszodp","jbrfnqtlagxhivmwyscaukzhdp","jbrenqtiagxhivmbyscfuxzodp","cbrrnqtuagxhivmwyscfukzodp","jbkenqtlagxhigmwysufukzodp","jbjewqtlagxhivmwyscfuqzodp","jbrznqtlagxvivmwyscfukzovp","jbrenttlacxhivmwyscfhkzodp","jblenqtlagxhivmwylcfukaodp","jbrenqtlagxhivmqysiftkzodp","jbrenqtlagwhikmwyscfukqodp","jbrenqtlegxhivmwvsckukzodp","jbrenqwzagxhiamwyscfukzodp","jbrenqtlagxhivcwyscfgkzodc","jbrenqtlagxxhvmwxscfukzodp","jbrenqtlngxhivmwyscfukoowp","jbreomtlagxhivmwpscfukzodp","jfrenqtlagxhivmwyscnumzodp","jbrenqtlagphipmwyscfukfodp","jvrenqtlagxhivmwyscfmkzodw","jbrenqtlaxxoiemwyscfukzodp","jbrenqtlagxhivmwyscemkzpdp","jbrenyjldgxhivmwyscfukzodp","jbrenqtlagxhivfvyspfukzodp","kbrenctlakxhivmwyscfukzodp","jbrmhqtlagxhivmwyscfuizodp","jbjenqtlagxlivmbyscfukzodp","jbrenqtlaaxhivmmyshfukzodp","jbronqtlagxhirmvyscfukzodp","jbcrnqtlagxwivmwyscfukzodp","jxrenszlagxhivmwyscfukzodp","jbpenqtlagxhivmwyscfukkody","jbrewqtlawxhivmwyscfukzhdp","jbrenqylagxhivmwlxcfukzodp","jbrenqtlagxxivtwywcfukzodp","jbrenqtlagxhcvgayscfukzodp","jbrenitlagxhivmwiscfukzohp","jbrenutlagxhivmwyscbukvodp","nbrenqtlagxhivmwysnfujzodp","jbrenqtlagxhivmwyqcfupzoop","jbrenqtrarxhijmwyscfukzodp","jbrenqtlagxhivmwysdfukzovy","jbrrnqtlagxhivmwyvcfukzocp","jbrenqtlagxhivmwyscfuvzzhp","jbhenitlagxhivmwysufukzodp","jbrenqtlagxhivmwyscfuwzoqx","kbrenqtlagxhivmwysqfdkzodp","jbrenqtlagxhivmwyxmfukzodx","jbcenatlagxxivmwyscfukzodp","jbrenhtlagvhdvmwyscfukzodp","jxrenqtlafxhivfwyscfukzodp","jbreaztlpgxhivmwyscfukzodp","tqrenqtlagxfivmwyscfukzodp","jbrenqgllgxhwvmwyscfukzodp","jbrejjtlagxhivmgyscfukzodp","jbrenqtlagxhivmwyscvukzoqv","jbrvnqtlagxsibmwyscfukzodp","jbrenqttagxhuvmwyscfukvodp","jbrenqteagxhivmwcscfukqodp","jbrenqtsabxhivmwyspfukzodp","jbbenqtlagxhivmwyscjukztdp","jnrenqtlagxhiimwydcfukzodp","jbrenqtlagxhfvmwyscxukzodu","jbrenqtluyxhiomwyscfukzodp","jbrenqvlagxhivmwyscuukzolp","ebrenqtlagxnivmwysrfukzodp","jbreeqtlatxhigmwyscfukzodp","jbrenqtvxgxhivmwyscfukzedp","jbrmnqtlagxhivmwywcfuklodp","jbreeqtvagxhivmwyscfukzody","jbrenptlagxhivmxyscfumzodp","jbrenqtlagxhivmwysgfukzfsp","jurenqtlagjhivmwkscfukzodp","jbrenntlagxhivmwtscffkzodp","jbrenqglagxhivmwyocfokzodp","wbrenqtlagxhivmwhscfukiodp","jbrenqtligxhivmqascfukzodp","jbrenqtlagxhivmwxscfukpojp","jurenqtlagxhivmmyscfbkzodp","jbrenqtmagxhivmwyscfrbzodp","jcrenqtlagxhivmwysefukzodm","jbrenqtlagxhicmwywcfukzodl","jbvanqtlagfhivmwyscfukzodp","jbmenqjlagxhivmwyscfdkzodp","jbrenqtlagohivvwysctukzodp","jbrenqtdagxdivmwyscfckzodp","kbrefqtlagxhivmwyscfuazodp","jbrwnqtoagxhivmwyswfukzodp","jjhenqtlagxhivmwyscfukzorp","jbgsnqtlagxhivkwyscfukzodp","jbrynqtlagxhivmsyspfukzodp","jbrenftlmkxhivmwyscfukzodp","nbrenqtxagxhmvmwyscfukzodp","jbrunqtlagxhijmwysmfukzodp","jbrenqtlagmaivmwyscfukzowp","jbrerqtlagxhihmwyscfukzudp","jbrenqtlagahivmwysckukzokp","kbrenqtlagxhirmwyscfupzodp","jbrrnqtlagxhivmwyecfukzodz","jbrenqtlavxhivmwyscfusiodp","jnrenqtlagxhivmwyhcfukzodw","jbretqtlagfhivmwyscfukzrdp","jbreoqtnagxhivmwyscfukzopp","jbrenbtllgxhivmwsscfukzodp","jbrenqtlmgxhivmwyscfuwzedp","jbnenqtlagxhivbwyscfukzokp","jbrenqslagxhivmvyscfukzndp","jbrenqtlagxzivmwuscfukztdp","gbrenqtlagxhyvmwyscfukjodp","jbrenqteagxhivmwyscfuszedp","jbrenqtlaglhivmwysafukkodp","jbrenqtlagxhcvmwascfukzogp","jbrenqtlagxhsvmkcscfukzodp","jbrenqslbgxhivmwyscfufzodp","cbrenqtlagxhivkwtscfukzodp","jbrenqtltgxhivmzyscfukzodj","jbrgnqtlagihivmwyycfukzodp","vbrenqauagxhivmwyscfukzodp","jbrqnqtlagjhivmwyscfqkzodp","jbrenqtlakxhivmwyscfukvobp","jcrenqelagxhivmwtscfukzodp","jbrrnqtlagxhlvmwyscfukzodw","jbrenqtlagxhivmwkscaumzodp","jbrenqdlagxhivmiescfukzodp","jhrenqtlagxhqvmwyscmukzodp","jbrenqtlaghhivmwyscfukkoyp","jowenqtlagxyivmwyscfukzodp","jbrenitaagxhivmwyscfqkzodp","jbrenqtlagxhivmwyscfnkbudp","jbyenqtlagxhivmiyscfukzhdp","jbrenitlagxhibjwyscfukzodp","jbrenqtlavxhjvmwpscfukzodp","jbrenqyaagxhivmwyscflkzodp","jbrenqylagxhivmwyicfupzodp","jbrenqtlagxmevmwylcfukzodp","lbrenqtlagxhiqmwyscfikzodp","jbrenqtnarxhivmwyscfmkzodp","jbrenqtlamxhivmwyscfnkzorp","jbbenqtlavxhivmwyscjukztdp","jbrenqtlagxhivmwyscfnliodp","jbwenetlagxhivmwyscfukzodt","jbrenatlagxhivmwysmfujzodp","jbrsnstlagxhivmwyscfukgodp","jbwvnitlagxhivmwyscfukzodp","jbrenqtbagxhkvmwypcfukzodp","jbrlnqtlafxhivmwyscfukdodp","jbrenztlanxhivmwyscjukzodp","jbrepqtlagxhivmwudcfukzodp","jbrenqtlagxiivmwdscfskzodp","jbrdjqtlagxhivmwyschukzodp","jbrenqtoaguhivmwyccfukzodp","jdrexqjlagxhivmwyscfukzodp","jbrenqtlagxhovmwysckukaodp","pbrfnqblagxhivmwyscfukzodp","jbrenqtlagxrivgiyscfukzodp","jbrelqtgagxhivmryscfukzodp","jbrenqtlagxhicmwjscfikzodp","jbrenqjlagxhivmwyscfmkjodp","jbrenqtlagxpivmwzscgukzodp","jbienqzlagxpivmwyscfukzodp","jbrenqvlagxhivmwdscfukzodx","owrenqtlagxhivmwyicfukzodp","gbrenqtlaathivmwyscfukzodp","jbgenqtlafxhivmwysqfukzodp","jbrenqtlagxhivtwsscfukzokp","jbrnnqylanxhivmwyscfukzodp","ebrenqolagxhivmcyscfukzodp","jbrenqtlarnhivgwyscfukzodp","jbmenqtlagxhivmvyscfukzgdp","jbrevqtlaglhivmwystfukzodp","jbrenqplanthivmwyscfukzodp","jbrenqtlagxhivmkyscbukzosp","jbrenqtlagxaivmwyscfugzodo","jbrenqplagxhnvmwyscfjkzodp","jbrenqtlagxhivgwyscfusrodp","cbrenqtlagxhivmwysmfukzody","jbrenquwaexhivmwyscfukzodp","jbredqtlagxhdvmwyscfukzoup","jbrxnqtlagxhivmwysczukaodp","jbrenqtlafnhivmwyscfuczodp","jbbdkqtlagxhivmwyscfukzodp","ubrenqtlagxhivkwyucfukzodp","ebjenqtlagxhivmwyscfukzodj","jbgenqtlugxxivmwyscfukzodp","jbrenqtoagxqivmwdscfukzodp","bbeenqtlagxhivmwyscfumzodp","jbfeeqtlagxhivmwmscfukzodp","jbrlnqtlagxhiimwescfukzodp","jbrenqtlagwoivmwyscfukhodp","jbrenqtlagnhivmwyscfuszovp"
        };

        static void Main(string[] args)
        {
            //ChecksumBoxIDs();
            FindCommonCharactersForPrototypeBoxIds();
        }

        private static void FindCommonCharactersForPrototypeBoxIds()
        {
            var shortestPairA = string.Empty;
            var shortestPairB = string.Empty;
            var shortestDist = 99999999999;

            for (int i = 0; i < boxIds.Length; i++)
            {
                var stringA = boxIds[i];
                for (int j = i + 1; j < boxIds.Length; j++)
                {
                    var stringB = boxIds[j];
                    var dist = LevenshteinDistance(stringA, stringB);
                    if (dist < shortestDist)
                    {
                        shortestDist = dist;
                        shortestPairA = stringA;
                        shortestPairB = stringB;
                    }
                }
            }

            var common = "";
            foreach (var character in shortestPairA)
            {
                if (shortestPairB.Contains(character))
                    common += character;
            }
            Console.WriteLine($"Common: {common}");
        }

        private static int LevenshteinDistance(string a, string b)
        {
            int aLength = a.Length;
            int bLength = b.Length;
            int[,] d = new int[aLength + 1, bLength + 1];

            if (aLength == 0)
                return bLength;

            if (bLength == 0)
                return aLength;

            for (int i = 0; i <= aLength; d[i,0] = i++){}
            for (int j = 0; j <= bLength; d[0,j] = j++){}

            for (int i = 1; i <= aLength; i++)
            {
                for (int j = 1; j <= bLength; j++)
                {
                    int cost = b[j-1] == a[i-1] ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(
                            d[i - 1, j] + 1,
                            d[i, j - 1] + 1
                        ),
                        d[i - 1, j - 1] + cost
                    );
                }
            }

            return d[aLength, bLength];
        }

        private static void ChecksumBoxIDs()
        {
            int doubleCount = 0;
            int tripleCount = 0;

            foreach (var boxId in boxIds)
            {
                var dict = new Dictionary<char, int>();
                foreach (var a in boxId)
                {
                    if (dict.ContainsKey(a))
                    {
                        dict[a]++;
                    }
                    else
                    {
                        dict.Add(a, 1);
                    }
                }

                if (dict.Any(kvp => kvp.Value == 2))
                {
                    doubleCount++;
                }

                if (dict.Any(kvp => kvp.Value == 3))
                {
                    tripleCount++;
                }
            }

            Console.WriteLine($"Checksum: {doubleCount * tripleCount}");
        }
    }
}
