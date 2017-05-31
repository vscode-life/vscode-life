﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace WinAssemblyToTypeScriptDeclare
{
    partial class WinAssemblyToTypeScriptDeclare
    {
        static void AnalyzePropertyInfoList(Type t, int nestLevel)
        {
            // プロパティの一覧を取得する
            PropertyInfo[] props = t.GetProperties(GetBindingFlags());

            foreach (PropertyInfo p in props)
            {
                try
                {
                    AnalyzePropertyInfo(p, nestLevel);
                }
                catch (Exception)
                {
                    // SW.WriteLine(e.Message);
                }
            }
        }
        static void AnalyzePropertyInfo(PropertyInfo p, int nestLevel)
        {
 
            // TypeScript向けに変換
            var ts = TypeToString(p.PropertyType);

            // 引数一覧
            var genepara = p.PropertyType.GetGenericArguments();

            // 「.」があったら、複雑すぎると判断する。
            bool isComplex = IsGenericAnyCondtion(genepara, (g) => { return g.ToString().Contains("."); });

            ts = ModifyType(ts, isComplex);

            SWTabSpace(nestLevel + 1);

            SW.WriteLine(p.Name + " :" + ts + ";");
        }

    }
}
