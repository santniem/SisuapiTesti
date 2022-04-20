using System;
using System.Collections.Generic;
using Paska.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Paska
{
    public class Program
    {
        public static async Task Main()
        {
            var json =  File.ReadAllText("C:/temp/data.json");//await SisuApi.GetDegreeProgrammeJson("otm-1d25ee85-df98-4c03-b4ff-6cad7b09618b");

            var module = Module.FromJson(json);

            var temp = new Module();

            Console.WriteLine(module.Name.Fi);
            var ids = temp.getSubModules(module.Rule);
            Stack<string> stack = new Stack<string>(ids);

            while(stack.Count > 0)
            {
                var GroupID = stack.Pop(); 
                json = await SisuApi.GetModuleJson(GroupID);
                module = Module.FromJsonList(json);

                Console.WriteLine(module.Name.Fi);
                rulePrinter(module.Rule);
                var subs = temp.getSubModules(module.Rule).Distinct();
                foreach (var item in subs)
                {
                    stack.Push(item);
                }
                System.Threading.Thread.Sleep(1000);
            }



        }

        public static int rulePrinter(Rule rule,int i = 0)
        {
            var tab = new string('\t', i);
            i++;
            Console.WriteLine($"{tab} {rule.type} {rule.localId} - {rule.moduleGroupId}");

            if(rule.rules != null)
            {
                foreach (var r in rule.rules)
                {
                    rulePrinter(r,i);
                }
            }
            if(rule.rule != null)
            {
                rulePrinter(rule.rule,i);
            }
            return i;
        }
    }
}