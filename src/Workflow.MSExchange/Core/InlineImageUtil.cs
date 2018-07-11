using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.MSExchange.Core {

    public class InlineImageUtil {

        private const string CidPattern = "cid:";

        private static HashSet<int> BuildCidIndex(string html) {
            var index = new HashSet<int>();
            var pos = html.IndexOf(CidPattern, 0);
            while (pos > 0) {
                var start = pos + CidPattern.Length;
                index.Add(start);
                pos = html.IndexOf(CidPattern, start);
            }
            return index;
        }

        private static void AdjustIndex(HashSet<int> index, int oldPos, int byHowMuch) {
            var oldIndex = new List<int>(index);
            index.Clear();
            foreach (var pos in oldIndex) {
                if (pos < oldPos)
                    index.Add(pos);
                else
                    index.Add(pos + byHowMuch);
            }
        }

        private static bool ReplaceCid(HashSet<int> index, ref string html, string cid, string path) {
            var posToRemove = -1;
            foreach (var pos in index) {
                if (pos + cid.Length < html.Length && html.Substring(pos, cid.Length) == cid) {
                    var sb = new StringBuilder();
                    sb.Append(html.Substring(0, pos - CidPattern.Length));
                    sb.Append(path);
                    sb.Append(html.Substring(pos + cid.Length));
                    html = sb.ToString();

                    posToRemove = pos;
                    break;
                }
            }

            if (posToRemove < 0)
                return false;

            index.Remove(posToRemove);
            AdjustIndex(index, posToRemove, path.Length - (CidPattern.Length + cid.Length));

            return true;
        }

        public static string GetHTMLWithInlineImage(Item mess) {
            string sHTMLCOntent = mess.Body.Text;
            FileAttachment[] attachments = null;
            if (mess.Attachments.Count != 0) {
                attachments = new FileAttachment[mess.Attachments.Count];
                for (int i = 0; i < mess.Attachments.Count; i++) {
                    try {
                        if (mess.Attachments[i].IsInline) {
                            string sType = mess.Attachments[i].ContentType.ToLower();
                            if (sType.Contains("image")) {
                                attachments[i] = (FileAttachment)mess.Attachments[i];
                                attachments[i].Load();
                                string sId = attachments[i].ContentId;
                                sType = sType.Replace("image/", "");
                                string oldString = "cid:" + sId;
                                string imagem =
                                    Convert.ToBase64String(attachments[i].Content);
                                sHTMLCOntent = sHTMLCOntent.Replace(oldString,
                                    "data:image/" + sType + ";base64," + imagem);
                                return sHTMLCOntent;
                            }
                        }
                    } catch (Exception) {
                        return sHTMLCOntent;
                    }
                }
            }
            return sHTMLCOntent;
        }
    }

}
