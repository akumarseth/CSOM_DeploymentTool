using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace CSOM_DeploymentTool
{
    class UploadHelper
    {

        #region UploadDocument_ALL

        internal static void UploadFoldersRecursively(string sourceFolder, string destinationLigraryTitle)
        {

            var clientContext = Helper.GetClientContext();
            //ExceptionHandlingScope scope = new ExceptionHandlingScope(context);

            Web web = clientContext.Web;
            var query = clientContext.LoadQuery(web.Lists.Where(p => p.Title == destinationLigraryTitle));
            clientContext.ExecuteQuery();
            List documentsLibrary = query.FirstOrDefault();
            var folder = documentsLibrary.RootFolder;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(sourceFolder);

            clientContext.Load(documentsLibrary.RootFolder);
            clientContext.ExecuteQuery();

            folder = documentsLibrary.RootFolder.Folders.Add(di.Name);
            clientContext.ExecuteQuery();

            UploadFolder(clientContext, di, folder);
        }

        public static void UploadFolder(ClientContext clientContext, System.IO.DirectoryInfo folderInfo, Folder folder)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            try
            {
                files = folderInfo.GetFiles("*.*");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    Console.WriteLine(fi.FullName);
                    clientContext.Load(folder);
                    clientContext.ExecuteQuery();
                    UploadDocument(clientContext, fi.FullName, folder.ServerRelativeUrl + "/" + fi.Name);
                }

                subDirs = folderInfo.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    Folder subFolder = folder.Folders.Add(dirInfo.Name);
                    clientContext.ExecuteQuery();
                    UploadFolder(clientContext, dirInfo, subFolder);
                }
            }
        }

        public static void UploadDocument(ClientContext clientContext, string sourceFilePath, string serverRelativeDestinationPath)
        {
            Web web = clientContext.Web;
            FileCreationInformation newFile = new FileCreationInformation();
            newFile.ContentStream = new MemoryStream(System.IO.File.ReadAllBytes(sourceFilePath));
            newFile.Url = serverRelativeDestinationPath;
            newFile.Overwrite = true;

            List docs = web.Lists.GetByTitle("Documents");
            Microsoft.SharePoint.Client.File uploadFile = docs.RootFolder.Files.Add(newFile);
            clientContext.Load(uploadFile);
            clientContext.ExecuteQuery();
            //using (var fs = new FileStream(sourceFilePath, FileMode.Open))
            //{
            //    var fi = new FileInfo(sourceFilePath);
            //    //Microsoft.SharePoint.Client.File.SaveBinaryDirect(clientContext, serverRelativeDestinationPath, fs, true);
            //}
        }

        #endregion UploadDocument_ALL
        
    }
}
