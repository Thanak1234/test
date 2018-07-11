using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Xml;

namespace Workflow.Core.Report
{
    public class XmlReport
    {
        private ReportingService2005 rs = new ReportingService2005();
        private DataTable dtItems = new DataTable("Items");

        public XmlReport(string rsUri)
        {
            rs.Url = rsUri + "/reportservice2005.asmx";
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            dtItems.Columns.Add("Item", Type.GetType("System.String"));
            dtItems.Columns.Add("Indented", Type.GetType("System.String"));
            dtItems.Columns.Add("Path", Type.GetType("System.String"));
            dtItems.Columns.Add("Parent", Type.GetType("System.String"));
            dtItems.Columns.Add("Type", Type.GetType("System.Int16"));
        }

        #region LoadData
        public DataTable GetItems()
        {
            dtItems.Rows.Clear();
            //Adding parent
            DataRow dr = dtItems.NewRow();
            dr["Item"] = "/";
            dr["Indented"] = "/";
            dr["Path"] = "/";
            dr["Parent"] = "";
            dr["Type"] = ItemTypeEnum.Folder;
            dtItems.Rows.Add(dr);


            foreach (CatalogItem ci in rs.ListChildren("/", true))
            {
                //Adding folders or reports
                if (ci.Type == ItemTypeEnum.Folder || ci.Type == ItemTypeEnum.Report)
                {
                    dr = dtItems.NewRow();
                    dr["Item"] = ci.Name;
                    dr["Indented"] = ci.Name.PadLeft(ItemLevel(ci.Path) * 10);
                    dr["Path"] = ci.Path;
                    dr["Type"] = ci.Type;

                    int LastPosition = ci.Path.LastIndexOf("/");
                    if (LastPosition == 0)
                        dr["Parent"] = "/";
                    else
                        dr["Parent"] = ci.Path.Substring(0, LastPosition);

                    dtItems.Rows.Add(dr);
                }
            }
            return dtItems;
        }
        #endregion

        #region Download Reports
        public void GetReportByFolder(string SSRSFolder, string FileSystemFolder)
        {
            GetReportByFolder(SSRSFolder, FileSystemFolder, false);
        }

        public void GetReportByFolder(string SSRSFolder, string FileSystemFolder, bool IncludeSubFolders)
        {
            //Validanting file system folder
            if (!Directory.Exists(FileSystemFolder))
                throw (new Exception("Folder does not exists!"));

            //Validating if datatable is loaded and loading
            if (dtItems.Rows.Count == 0)
                GetItems();

            DataTable dtTmp = new DataTable();
            dtTmp = dtItems.Copy();
 
            //Validating if it is necessary to go through subfolders
            if (IncludeSubFolders)
                dtTmp.DefaultView.RowFilter = "Type IN (1, 2) AND Path LIKE '" + SSRSFolder + "*'";
            else
                dtTmp.DefaultView.RowFilter = "Type = 2 AND [Parent] = '" + SSRSFolder + "'";

            dtTmp.DefaultView.Sort = "Path";

            foreach (DataRowView dr in dtTmp.DefaultView)
            {
                String Folder;
                if (Convert.ToInt32(dr["Type"]) == Convert.ToInt32(ItemTypeEnum.Report))
                {
                    //Defining folder
                    Folder = GetDestinationFolder(FileSystemFolder, SSRSFolder, dr["Parent"].ToString(), dr["Parent"].ToString());
                    //Downloading file to folder
                    GetReportFile(dr["Path"].ToString(), Folder, dr["Item"].ToString());
                }
                else
                {
                    //Creating folder
                    Folder = GetDestinationFolder(FileSystemFolder, SSRSFolder, dr["Path"].ToString(), dr["Parent"].ToString());
                    Directory.CreateDirectory(Folder);
                }
            }
        }

        public void GetReportFile(string ReportPath, string FileSystemPath)
        {
            string FileName = ReportPath.Substring(ReportPath.LastIndexOf("/") + 1);
            GetReportFile(ReportPath, FileSystemPath, FileName);
        }

        public void GetReportFile(string ReportPath, string FileSystemPath, string FileName)
        {
            byte[] ReportDefinition = null;
            XmlDocument doc = new XmlDocument();

            if (!FileName.EndsWith(".rdl"))
                FileName += ".rdl";

            ReportDefinition = rs.GetReportDefinition(ReportPath);
            MemoryStream Stream = new MemoryStream(ReportDefinition);

            doc.Load(Stream);
            doc.Save(FileSystemPath + "\\" + FileName);
        }
        #endregion

        #region Xml Report
        public XmlDocument GetReportXml(string reportPath)
        {
            byte[] ReportDefinition = null;
            XmlDocument doc = new XmlDocument();

           
            ReportDefinition = rs.GetReportDefinition(reportPath);
            MemoryStream Stream = new MemoryStream(ReportDefinition);
            
            doc.Load(Stream);
            return doc;
        }

        #endregion
        #region Private Functions
        private int ItemLevel(string Path)
        {
            int Pos = 0, Qty = 0;

            if (Path != "/")
            {
                Pos = Path.IndexOf("/", Pos);
                while (Pos > -1)
                {
                    Qty++;
                    Pos = Path.IndexOf("/", Pos + 1);
                }
            }
            return Qty;
        }

        private string GetDestinationFolder(string FileSystemPath, string SSRSPath1, string SSRSPath2, string Parent)
        {
            int Level = ItemLevel(SSRSPath2);
 
            if (Level == 0)
                SSRSPath2 = "";
            else if (Level == 1 || SSRSPath1 == "/")
                SSRSPath2 = "\\" + SSRSPath2.Substring(SSRSPath1.Length);
            else
                SSRSPath2 = "\\" + SSRSPath2.Substring(SSRSPath1.Length + 1);

            string Result = FileSystemPath + SSRSPath2.Replace("/", "\\");
            return Result;
        }
        #endregion
    }
}