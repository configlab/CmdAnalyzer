# CmdManager
Cmd Demo:  
ConfigLab.exe  Valide_JwtToken_Ecdsa  -jwttoken "eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE2MjQwMjg5NzIsIm5iZiI6MTYyNDAyODk3MiwiZXhwIjoxNjI4MzQ4OTcyLCJhdWQiOiJodHRwOi8vY29uZmlnLm5ldC5jbiIsImlzcyI6Imh0dHA6Ly9jb25maWcubmV0LmNuIiwic3ViIjoiMyIsImp0aSI6Ik5qWXpNMlJpT1RBdE9ERXdNaTAwWVdJM0xXSTJPVEF0WVRNek9EQmtObVE1WkRFeCJ9.v2RKARfRJ61fa-rx1lGOUGPJeLbvNDX80Fqd8c1LKLG-deFN_7idYpZj9OEvdcWInKEubf73pzABS1TGZTpQvA" -pubkey "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEmIiDFSHJ2PLw/bEWFUiQfVmA1XFPkYFXm2ahfDUWFwK4ny7yy4FokDkNGnuXjWbEiQC+2W8IelIFLI2oHt20qg==" -issuer "http://config.net.cn" -audience "http://config.net.cn"  


Implements for ConfigLab: 
        /// <summary>  
        /// ConfigLab  Create_EccKeyPair  
        /// </summary>  
        /// <param name="args"></param>  
        static void Main(string[] args)  
        {  
            if (args == null || args.Length < 1)
            {
                Console.WriteLine("invalide params");  
                return; 
            }  
            CommodStruct cmdArgs = CmdManager.AnalysysCmd(args); 
            switch (cmdArgs.CmdBaseCommond) 
            { 
                case "Create_EccKeyPair": 
                    EcdsaKeyPairResult resultKey = EcdsaUtils.GenerateKeyPairForCrossPlatform();
                    Console.Write(JsonConvert.SerializeObject(resultKey)); 
                    break;
                case "Valide_JwtToken_Ecdsa": 
                    string jwtToken = ParamsDictionary.GetParamsByKey(cmdArgs.CmdParams, "jwttoken");
                    string pubKey = ParamsDictionary.GetParamsByKey(cmdArgs.CmdParams, "pubkey");
                    string validIssuer = ParamsDictionary.GetParamsByKey(cmdArgs.CmdParams, "issuer");
                    string validAudience = ParamsDictionary.GetParamsByKey(cmdArgs.CmdParams, "audience");
                    var jwtTokenValide= EcdsaUtils.JwtTokenValide(jwtToken, pubKey, validIssuer, validAudience);
                    Console.Write(jwtTokenValide);  
                    break;  
                default:  
                    Console.WriteLine("unknow action");  
                    break;  
            }  
        }  
        
        
        
