using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ConfigLab.Dnc.Comp.DataType;
using System.Text.RegularExpressions;
using static ConfigLab.Dnc.Comp.DataType.CmdAnalyzer;

namespace ConfigLab.Dnc.Comp.CmdUtils
{
    /// <summary>
    /// description:Cmd Manager (AnalysysCmd  for Base Cmd ,Extend Cmd ,Args)
    /// author: config.net.cn 
    /// createtime:2021-8-5
    /// </summary>
    public static class CmdManager
    {
        private static readonly string cmdParamPattern = @"\s*\-[a-z]+[a-z0-9A-Z]{1,20}\s*[^\-]+";
        public static CommodStruct AnalysysCmd(string[] args)
        {
            if (args == null || args.Length == 0)
                return null;
            Dictionary<string, string> result = new Dictionary<string, string>();
            CmdAnalyzer cmdAnalyzer = new CmdAnalyzer();
            CommodStruct cmdResult=cmdAnalyzer.StartAnalzer(string.Join(" ", args));
            return cmdResult;
        }
        /// <summary>
        /// Get Match Item
        /// </summary>
        /// <param name="sText"></param>
        /// <param name="sPattern"></param>
        /// <returns></returns>
        private static MatchCollection GetAllMatchItem(string sText, string sPattern)
        {
            MatchCollection matchs = Regex.Matches(sText, sPattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return matchs;
        }
    }
}
