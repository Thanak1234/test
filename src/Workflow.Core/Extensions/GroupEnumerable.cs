using Workflow.Core.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Workflow
{
    public static class GroupEnumerable
    {
        public static IList<dynamic> BuildTree(this IEnumerable<dynamic> source)
        {
            var groups = source.OrderBy(i => i.ParentId).GroupBy(i => i.ParentId);
            var roots = groups.FirstOrDefault().ToList();

            if (roots.Count > 0)
            {
                foreach (var parent in roots) {
                    AddChildren(parent, source.ToList());
                }
            }

            return roots;
        }

        public static string ToJson(this IEnumerable<dynamic> source)
        {
           return JsonConvert.SerializeObject(source, new JsonSerializerSettings
           {
               ContractResolver = new CamelCase()
           });
        }

        private static void AddChildren(dynamic parent, 
            IList<dynamic> source)
        {
            var children = source.Where(g => g.ParentId == parent.Id);
            if (children != null && children.Count() > 0) {
                parent.Children = children;

                foreach (var child in children)
                {
                    AddChildren(child, source);
                }
            }
        }
    }
}


/*
/* Param: dynamic node, IDictionary<int, List<dynamic>> source */
//if (source.ContainsKey(node.Id))
//{
//    node.Children = source[node.Id];
//    for (int i = 0; i < node.Children.Count; i++)
//        AddChildren(node.Children[i], source);
//}
//else
//{
//    node.Children = new List<dynamic>();
//} 