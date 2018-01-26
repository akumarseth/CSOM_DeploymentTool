using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSOM_DeploymentTool
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //upload folder and file everything
                //uploadhelper.uploadfoldersrecursively(@"c:\users\abhishek\desktop\angular 5\dist", "Documents");

                //Create List
                //ListOperation.DeleteListColumn();
                //ListOperation.DeleteList();
                //ListOperation.CreateList();
                //ListOperation.CreateListColumn();


                //ListOperation.CreateSiteColumn();
                //ListOperation.DeleteSiteColumn();

                //ContentTypeHelper.CreateContentType();
                //ContentTypeHelper.AddExistingSiteColumnToContentType();
                //ContentTypeHelper.RemoveSiteColumnFromContentType();

                //ListOperation.AssociateExistingContenTypeToList();

                //Create Library
                //ListOperation.CreateLibrary();


                Console.WriteLine("Completed");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                //throw ex;
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                Console.ReadKey();
            }
        }
    }
}
