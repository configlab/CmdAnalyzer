using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConfigLab.Dnc.Comp.DataType
{
    /// <summary>
    /// Description:cmd Analyzer
    /// CreateDate：2018-1-11
    /// Author:config.net.cn
    /// </summary>
    public class CmdAnalyzer
    {
        /// <summary>
        /// Analysys Result
        /// </summary>
        public class CommodStruct
        {
            private string _cmdBaseCommond = "";
            private string _cmdExtendCommond = "";
            private Dictionary<string, string> _cmdParams = new Dictionary<string, string>();
            /// <summary>
            /// Base cmd
            /// </summary>
            public string CmdBaseCommond
            {
                get
                {
                    return _cmdBaseCommond;
                }

                set
                {
                    _cmdBaseCommond = value;
                }
            }
            /// <summary>
            /// extend cmd
            /// </summary>
            public string CmdExtendCommond
            {
                get
                {
                    return _cmdExtendCommond;
                }

                set
                {
                    _cmdExtendCommond = value;
                }
            }
            /// <summary>
            /// cmd parmas
            /// </summary>
            public Dictionary<string, string> CmdParams
            {
                get
                {
                    return _cmdParams;
                }

                set
                {
                    _cmdParams = value;
                }
            }

            public string GetExplain()
            {
                StringBuilder sbstr = new StringBuilder("");
                sbstr.AppendFormat("Base Cmd：{0}\r\n",this._cmdBaseCommond);
                foreach(string sKey in _cmdParams.Keys)
                {
                    sbstr.AppendFormat("{0}:{1}\r\n",sKey,_cmdParams[sKey]);
                }
                return sbstr.ToString();
            }
        }
        private static readonly string cmdParamPattern = @"\s*\-[a-z]+[a-z0-9A-Z]{1,50}\s*([^\s]+)";
        /// <summary>
        /// Analysys Cmd and Return Result
        /// </summary>
        /// <param name="sCmdStr"></param>
        /// <returns></returns>
        public CommodStruct StartAnalzer(string sCmdStr)
        {
            if (string.IsNullOrEmpty(sCmdStr))
                return null;
            string[] cmdItems = sCmdStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (cmdItems == null || cmdItems.Length == 0)
                return null;
            CommodStruct result = new CmdAnalyzer.CommodStruct();
            if (cmdItems.Length == 1)
            {
                result.CmdBaseCommond = cmdItems[0];
            }
            else
            {
                if (cmdItems[0].StartsWith("-") == false)
                {
                    if (cmdItems[1].StartsWith("-") == false)
                    {
                        result.CmdBaseCommond = cmdItems[1];
                    }
                    else
                    {
                        result.CmdBaseCommond = cmdItems[0];
                    }
                }
            }
            MatchCollection cmdParamsMatchs = this.GetAllMatchItem(sCmdStr,cmdParamPattern);
            if (cmdParamsMatchs == null|| cmdParamsMatchs.Count==0)
                return result;
            List<string> listExtendCommond = new List<string>();
            foreach(Match mMatch in cmdParamsMatchs)
            {
                if (string.IsNullOrEmpty(mMatch.Value))
                    continue;
                string[] sParamItems = mMatch.Value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (sParamItems == null || sParamItems.Length == 0)
                    continue;
                if (sParamItems.Length < 2)
                    continue;
                result.CmdParams[sParamItems[0].TrimStart('-').ToLower()] = sParamItems[1];
            }
            return result;
        }
        /// <summary>
        /// Get All Match Item
        /// </summary>
        /// <param name="sText"></param>
        /// <param name="sPattern"></param>
        /// <returns></returns>
        private  MatchCollection GetAllMatchItem(string sText, string sPattern)
        {
            MatchCollection matchs = Regex.Matches(sText, sPattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return matchs;
        }
    }
}
